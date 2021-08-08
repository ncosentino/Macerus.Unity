using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using UnityEditor;

using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Assets.Scripts.UnityEditor
{
    public sealed class DependencyUpdater
    {
        private const string MSBUILD_CONFIGURATION = "Debug";
        private const string MSBUILD_VERBOSITY = "quiet";
        private static readonly string MSBUILD_PARAMS = $"-t:Build -p:Configuration={MSBUILD_CONFIGURATION} -verbosity:{MSBUILD_VERBOSITY} -clp:ErrorsOnly";
        private const string MSBUILD_EXE_PATH = @"C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe";

        private static readonly IReadOnlyCollection<string> BUILD_TARGETS = new[]
        {
            "\"" + new Uri(Path.Combine(Application.dataPath, @"..\..\..\..\libraries\projectxyz\projectxyz\projectxyz.csproj")).LocalPath + "\" " + MSBUILD_PARAMS,
            "\"" + new Uri(Path.Combine(Application.dataPath, @"..\..\macerus-game\macerus\macerus.csproj")).LocalPath + "\" " + MSBUILD_PARAMS,
        };

        private readonly string _dataPath;
        private readonly string _localNugetRepoPath;

        public DependencyUpdater()
        {
            _dataPath = Application.dataPath;
            _localNugetRepoPath = Path.Combine(
                _dataPath,
                @"..\..\..\..\nuget-repo");
        }

        public Task UpdateDependenciesAsync(bool buildDependencies)
        {
            return Task.Run(() =>
            {
                Debug.Log("Asynchronous dependency update started...");
                UpdateDependenciesInternal(buildDependencies);
                Debug.Log("Asynchronous dependency update complete.");
            });
        }

        public void UpdateDependencies(bool buildDependencies)
        {
            Debug.Log("Synchronous dependency update started...");
            UpdateDependenciesInternal(buildDependencies);
            Debug.Log("Synchronous dependency update complete.");
        }

        private void UpdateDependenciesInternal(bool buildDependencies)
        {
            if (buildDependencies)
            {
                Debug.Log($"Building prerequisites...");
                foreach (var buildTarget in BUILD_TARGETS)
                {
                    BuildDependency(MSBUILD_EXE_PATH, buildTarget);
                }
            }

            Debug.Log($"Copying dependencies...");

            var destinationPluginsDirectory = Path.Combine(
                _dataPath,
                @"MacerusPlugins");
            Debug.Log($"Destination Plugins Directory: '{destinationPluginsDirectory}'");

            var resourcePairings = new[]
            {
                new ResourcePair(
                    "content/mapping/maps/",
                    Path.Combine(_dataPath, @"Resources/Mapping/Maps")),
                new ResourcePair(
                    "content/resources/graphics/",
                    Path.Combine(_dataPath, @"Resources/Graphics")),
            };
            Debug.Log($"Resource Pairings:");
            foreach (var pair in resourcePairings)
            {
                Debug.Log(
                    $"\tSource Prefix: '{pair.SourceResourcePathPrefix}'\r\n" +
                    $"\tDestination: '{pair.DestinationResourcePath}'");
            }

            UsingEnsuredPluginsDirectory(
                destinationPluginsDirectory,
                () =>
            UsingEnsuredDirectory(
                Path.Combine(_dataPath, "Resources"),
                null,
                () =>
            {
                ProcessDependencies(
                    destinationPluginsDirectory,
                    resourcePairings);
                UpdateTestAssemblyReferences(destinationPluginsDirectory);
            }));
        }

        private void UpdateTestAssemblyReferences(string destinationPluginsDirectory)
        {
            var asmDefPath = "Assets /Tests/EditorModeTests/EditorModeTests.asmdef";
            var asmDefContents = File.ReadAllText(asmDefPath);
            var dependencyRegex = new Regex("(\"precompiledReferences\": \\[)(.+)(\\],)", RegexOptions.Singleline);
            var dlls = Directory.GetFiles(destinationPluginsDirectory, "*.dll");
            var replacement =
                "\"nunit.framework.dll\",\r\n" +
                "\"Autofac.dll\",\r\n";
            replacement += string.Join(",\r\n", dlls.Select(x => $"\"{new FileInfo(x).Name}\""));
            var replacedAsmDefContents = dependencyRegex.Replace(asmDefContents, $"$1{replacement}$3");
            File.WriteAllText(asmDefPath, replacedAsmDefContents);
        }

        private void BuildDependency(
            string msbuildExePath,
            string buildTarget)
        {
            Debug.Log($"Executing build '{buildTarget}' with '{msbuildExePath}'...");

            var psi = new ProcessStartInfo(
                msbuildExePath,
                buildTarget)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
            };

            var process = Process.Start(psi);
            process.WaitForExit();
            if (process.ExitCode != 0)
            {
                throw new InvalidOperationException(
                    $"'{msbuildExePath}' exited with code {process.ExitCode} while building '{buildTarget}'.");
            }

            Debug.Log($"Finished build '{buildTarget}' with '{msbuildExePath}'.");
        }

        private void UsingEnsuredPluginsDirectory(
            string destinationPluginsDirectory,
            Action callback)
        {
            var backupDestinationPluginsDirectory = Path.Combine(
                _dataPath,
                @"MacerusPluginsBackup");
            UsingEnsuredDirectory(
                destinationPluginsDirectory,
                backupDestinationPluginsDirectory,
                callback);
        }

        private void UsingEnsuredDirectory(
            string destinationDirectory,
            string backupDirectory,
            Action callback)
        {
            if (string.IsNullOrWhiteSpace(backupDirectory))
            {
                Debug.Log($"No backup directory specified for '{destinationDirectory}'.");
            }
            else if (Directory.Exists(backupDirectory))
            {
                Debug.Log($"Deleting '{backupDirectory}'...");
                Directory.Delete(backupDirectory, true);
                Debug.Log($"Deleted '{backupDirectory}'.");
            }

            if (Directory.Exists(destinationDirectory))
            {
                if (string.IsNullOrWhiteSpace(backupDirectory))
                {
                    Debug.LogWarning(
                        $"No backup directory specified for '{destinationDirectory}' " +
                        $"but the directory already exists. Contents may be " +
                        $"interleaved between old and new.");
                }
                else
                {
                    Debug.Log($"Backing up '{destinationDirectory}' to {backupDirectory}...");
                    Directory.Move(destinationDirectory, backupDirectory);
                    Debug.Log($"Backed up '{destinationDirectory}' to {backupDirectory}.");
                }
            }

            if (!Directory.Exists(destinationDirectory))
            {
                Debug.Log($"Creating '{destinationDirectory}'...");
                Directory.CreateDirectory(destinationDirectory);
                Debug.Log($"Created '{destinationDirectory}'.");
            }

            try
            {
                callback.Invoke();
            }
            catch
            {
                if (!string.IsNullOrWhiteSpace(backupDirectory) &&
                    Directory.Exists(backupDirectory))
                {
                    Debug.Log($"Attempting restoration of backup '{backupDirectory}' due to failure...");

                    if (Directory.Exists(destinationDirectory))
                    {
                        Debug.Log($"Deleting '{destinationDirectory}'...");
                        Directory.Delete(destinationDirectory, true);
                        Debug.Log($"Deleted '{destinationDirectory}'.");
                    }

                    Directory.Move(backupDirectory, destinationDirectory);
                }

                throw;
            }
            finally
            {
                if (!string.IsNullOrWhiteSpace(backupDirectory) && Directory.Exists(backupDirectory))
                {
                    Debug.Log($"Deleting '{backupDirectory}'...");
                    Directory.Delete(backupDirectory, true);
                    Debug.Log($"Deleted '{backupDirectory}'.");
                }
            }
        }

        private void ProcessDependencies(
            string destinationPluginsDirectory,
            IReadOnlyCollection<ResourcePair> resourcePairings)
        {
            var dependencyEntries = new IDependencyEntry[]
            {
                new NugetDependencyEntry(
                    "NexusLabs.Framework",
                    @"C:\dev\nexus\libraries\NexusLabs.Framework\NexusLabs.Framework\bin\Debug",
                    new[] { "NexusLabs.Framework.*.*.*.nupkg" },
                    new string[0]),
                new NugetDependencyEntry(
                    "NexusLabs.Contracts",
                    @"C:\dev\nexus\libraries\NexusLabs.Framework\NexusLabs.Contracts\bin\Debug",
                    new[] { "NexusLabs.Contracts.*.*.*.nupkg" },
                    new string[0]),
                new NugetDependencyEntry(
                    "NexusLabs.Collections.Generic",
                    @"C:\dev\nexus\libraries\NexusLabs.Framework\NexusLabs.Collections.Generic\bin\Debug",
                    new[] { "NexusLabs.Collections.Generic.*.*.*.nupkg" },
                    new string[0]),
                new NugetDependencyEntry(
                    "Project XYZ",
                    _localNugetRepoPath,
                    new[] { "ProjectXyz.*.*.*.*.nupkg" },
                    new string[0]),
                new NugetDependencyEntry(
                    "Macerus",
                    _localNugetRepoPath,
                    new[] { "Macerus.*.*.*.*.nupkg" },
                    new string[0]),
            };

            foreach (var dependencyEntry in dependencyEntries)
            {
                ProcessDependencyEntry(
                    destinationPluginsDirectory,
                    resourcePairings,
                    dependencyEntry);
            }
        }

        private void ProcessDependencyEntry(
            string destinationPluginsDirectory,
            IReadOnlyCollection<ResourcePair> resourcePairings,
            IDependencyEntry dependencyEntry)
        {
            if (dependencyEntry is DependencyDirectoryEntry)
            {
                ProcessDirectoryDependencyEntry(
                    destinationPluginsDirectory,
                    (DependencyDirectoryEntry)dependencyEntry);
            }
            else if (dependencyEntry is NugetDependencyEntry)
            {
                ProcessNugetDependencyEntry(
                    destinationPluginsDirectory,
                    resourcePairings,
                    (NugetDependencyEntry)dependencyEntry);
            }
            else
            {
                throw new NotSupportedException(
                    $"Dependency type '{dependencyEntry.GetType()}' is not supported.");
            }
        }

        private void ProcessNugetDependencyEntry(
            string destinationPluginsDirectory,
            IReadOnlyCollection<ResourcePair> resourcePairings,
            NugetDependencyEntry dependencyEntry)
        {
            Debug.Log($"{dependencyEntry.Name} Nuget Package");

            var matchingNugetFilePaths = dependencyEntry
                .SearchPatterns
                .SelectMany(pattern => Directory.GetFiles(
                    dependencyEntry.NugetRepoPath,
                    pattern))
                .OrderByDescending(x => new FileInfo(x).LastWriteTimeUtc)
                .ToArray();

            if (!matchingNugetFilePaths.Any())
            {
                throw new NotSupportedException(
                    $"No matching nuget packages for '{dependencyEntry.Name}' " +
                    $"located in nuget repository '{dependencyEntry.NugetRepoPath}'.");
            }

            Debug.Log($"Found {matchingNugetFilePaths.Length} matching files...");
            foreach (var matchingFile in matchingNugetFilePaths)
            {
                Debug.Log($"\t{matchingFile}");
            }

            var nugetFilePath = matchingNugetFilePaths.First();
            Debug.Log($"Using '{nugetFilePath}' for '{dependencyEntry.Name}'.");

            using (var zipFile = ZipFile.OpenRead(nugetFilePath))
            {
                foreach (var entry in zipFile.Entries.Where(x => x.FullName.Contains("lib/net46/")))
                {
                    if (dependencyEntry
                        .ExcludePatterns
                        ?.Any(x => x.Equals(entry.Name, StringComparison.OrdinalIgnoreCase)) == true)
                    {
                        Debug.Log($"Skipping '{entry.Name}' because it matches an exclude pattern.");
                        continue;
                    }

                    var destinationFileName = Path.Combine(
                        destinationPluginsDirectory,
                        entry.Name);
                    Debug.Log($"Extracting '{entry.Name}' to '{destinationFileName}'...");
                    entry.ExtractToFile(destinationFileName, true);
                }

                // resources!
                foreach (var entry in zipFile
                    .Entries
                    .Select(x => new
                    {
                        ZipEntry = x,
                        ResourcePair = resourcePairings.FirstOrDefault(resPair => x.FullName.IndexOf(
                            resPair.SourceResourcePathPrefix,
                            StringComparison.OrdinalIgnoreCase) != -1)
                    })
                    .Where(x => x.ResourcePair != null))
                {
                    if (dependencyEntry
                        .ExcludePatterns
                        ?.Any(x => x.Equals(entry.ZipEntry.Name, StringComparison.OrdinalIgnoreCase)) == true)
                    {
                        Debug.Log($"Skipping '{entry.ZipEntry.Name}' because it matches an exclude pattern.");
                        continue;
                    }

                    var filePathPart = entry.ZipEntry.FullName.Substring(
                        entry.ZipEntry.FullName.IndexOf(entry.ResourcePair.SourceResourcePathPrefix, StringComparison.OrdinalIgnoreCase) + entry.ResourcePair.SourceResourcePathPrefix.Length);
                    var destinationFileName = Path.Combine(
                        entry.ResourcePair.DestinationResourcePath,
                        filePathPart);
                    Debug.Log($"Extracting '{entry.ZipEntry.Name}' to '{destinationFileName}'...");

                    if (!Directory.Exists(Path.GetDirectoryName(destinationFileName)))
                    {
                        Debug.Log($"Creating '{Path.GetDirectoryName(destinationFileName)}'...");
                        Directory.CreateDirectory(Path.GetDirectoryName(destinationFileName));
                    }

                    entry.ZipEntry.ExtractToFile(destinationFileName, true);
                }
            }
        }

        private void ProcessDirectoryDependencyEntry(
            string destinationPluginsDirectory,
            DependencyDirectoryEntry dependencyEntry)
        {
            if (!Directory.Exists(dependencyEntry.FullPath))
            {
                throw new NotSupportedException(
                    $"Dependency '{dependencyEntry.Name}' could not be found " +
                    $"at '{dependencyEntry.FullPath}'.");
            }

            var dependencyDirectory = dependencyEntry.FullPath;
            Debug.Log($"{dependencyEntry.Name} Directory: '{dependencyDirectory}'");

            foreach (var searchPattern in dependencyEntry.SearchPatterns)
            {
                foreach (var sourceDependencyFilePath in Directory
                    .GetFiles(dependencyDirectory, searchPattern)
                    .Where(fileName => !dependencyEntry.ExcludePatterns.Any(exclude => Regex.IsMatch(fileName, exclude))))
                {
                    var destinationFilePath = Path.Combine(
                        destinationPluginsDirectory,
                        Path.GetFileName(sourceDependencyFilePath));

                    Debug.Log($"Copying '{sourceDependencyFilePath}' to '{destinationFilePath}'...");
                    File.Copy(
                        sourceDependencyFilePath,
                        destinationFilePath,
                        true);
                }
            }
        }

        private interface IDependencyEntry
        {
        }

        private sealed class DependencyDirectoryEntry : IDependencyEntry
        {
            public DependencyDirectoryEntry(
                string name,
                string fullPath,
                IEnumerable<string> searchPatterns,
                IEnumerable<string> excludePatterns)
            {
                Name = name;
                FullPath = fullPath;
                SearchPatterns = searchPatterns.ToArray();
                ExcludePatterns = excludePatterns.ToArray();
            }

            public string Name { get; }

            public string FullPath { get; }

            public IReadOnlyCollection<string> SearchPatterns { get; }

            public IReadOnlyCollection<string> ExcludePatterns { get; }
        }

        private sealed class NugetDependencyEntry : IDependencyEntry
        {
            public NugetDependencyEntry(
                string name,
                string nugetRepoPath,
                IEnumerable<string> searchPatterns,
                IEnumerable<string> excludePatterns)
            {
                Name = name;
                NugetRepoPath = nugetRepoPath;
                SearchPatterns = searchPatterns.ToArray();
                ExcludePatterns = excludePatterns.ToArray();
            }

            public string Name { get; }

            public string NugetRepoPath { get; }

            public IReadOnlyCollection<string> SearchPatterns { get; }

            public IReadOnlyCollection<string> ExcludePatterns { get; }
        }

        private sealed class ResourcePair
        {
            public ResourcePair(
                string sourceResourcePathPrefix,
                string destinationResourcePath)
            {
                SourceResourcePathPrefix = sourceResourcePathPrefix;
                DestinationResourcePath = destinationResourcePath;
            }

            public string SourceResourcePathPrefix { get; }

            public string DestinationResourcePath { get; }
        }
    }
}