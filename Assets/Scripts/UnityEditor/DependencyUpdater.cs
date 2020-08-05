#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Assets.Scripts.UnityEditor
{
    public sealed class DependencyUpdater
    {
        private const string MSBUILD_EXE_PATH = @"C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe";

        private static readonly IReadOnlyCollection<string> BUILD_TARGETS = new[]
        {
            //"\"" + new Uri(Path.Combine(Application.dataPath, @"..\..\..\..\libraries\projectxyz\projectxyz\projectxyz.csproj")).LocalPath + "\"",
            "\"" + new Uri(Path.Combine(Application.dataPath, @"..\..\..\..\libraries\projectxyz\projectxyz.sln")).LocalPath + "\"",
            "\"" + new Uri(Path.Combine(Application.dataPath, @"..\..\..\..\libraries\tiled.net\tiled.net.sln")).LocalPath + "\"",
            //"\"" + new Uri(Path.Combine(Application.dataPath, @"..\..\macerus-game\macerus\Macerus.csproj")).LocalPath + "\"",
            "\"" + new Uri(Path.Combine(Application.dataPath, @"..\..\macerus-game\macerus.sln")).LocalPath + "\"",
        };

        private readonly string _dataPath;
        private readonly string _librariesPath;
        private readonly string _localNugetRepoPath;

        public DependencyUpdater()
        {
            _dataPath = Application.dataPath;
            _librariesPath = Path.Combine(
                _dataPath,
                @"..\..\..\..\libraries");
            _localNugetRepoPath = Path.Combine(
                _dataPath,
                @"..\..\..\..\nuget-repo");
        }

        public Task UpdateDependenciesAsync()
        {
            return Task.Run(() =>
            {
                Debug.Log("Asynchronous dependency update started...");
                UpdateDependenciesInternal();
                Debug.Log("Asynchronous dependency update complete.");
            });
        }

        public void UpdateDependencies()
        {
            Debug.Log("Synchronous dependency update started...");
            UpdateDependenciesInternal();
            Debug.Log("Synchronous dependency update complete.");
        }

        private void UpdateDependenciesInternal()
        {
            Debug.Log($"Building prerequisites...");
            foreach (var buildTarget in BUILD_TARGETS)
            {
                BuildDependency(MSBUILD_EXE_PATH, buildTarget);
            }

            Debug.Log($"Copying dependencies...");

            var destinationPluginsDirectory = Path.Combine(
                _dataPath,
                @"Plugins");
            Debug.Log($"Destination Plugins Directory: '{destinationPluginsDirectory}'");

            if (Directory.Exists(destinationPluginsDirectory))
            {
                Debug.Log($"Deleting '{destinationPluginsDirectory}'...");
                Directory.Delete(destinationPluginsDirectory, true);

                Debug.Log($"Recreating '{destinationPluginsDirectory}'...");
                Directory.CreateDirectory(destinationPluginsDirectory);
                Debug.Log($"Recreated '{destinationPluginsDirectory}'.");
            }

            ProcessDependencies(destinationPluginsDirectory);
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

        private void ProcessDependencies(string destinationPluginsDirectory)
        {
            var dependencyEntries = new IDependencyEntry[]
            {
                new DependencyDirectoryEntry(
                    "Tiled.NET",
                    Path.Combine(
                        _librariesPath,
                        @"Tiled.Net\Tiled.Net.Tmx.Xml\bin\Debug"),
                    new[] { "*.dll" },
                    new string[0]),
                new NugetDependencyEntry(
                    "Project XYZ",
                    _localNugetRepoPath,
                    new[] { "ProjectXyz.*.*.*.nupkg" },
                    new string[0]),
                new NugetDependencyEntry(
                    "Macerus",
                    _localNugetRepoPath,
                    new[] { "Macerus.*.*.*.nupkg" },
                    new string[0]),
            };

            foreach (var dependencyEntry in dependencyEntries)
            {
                ProcessDependencyEntry(
                    destinationPluginsDirectory,
                    dependencyEntry);
            }
        }

        private void ProcessDependencyEntry(
            string destinationPluginsDirectory,
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
            NugetDependencyEntry dependencyEntry)
        {
            Debug.Log($"{dependencyEntry.Name} Nuget Package");

            var matchingNugetFilePaths = dependencyEntry
                .SearchPatterns
                .SelectMany(pattern => Directory.GetFiles(
                    dependencyEntry.NugetRepoPath,
                    pattern))
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
    }
}
#endif