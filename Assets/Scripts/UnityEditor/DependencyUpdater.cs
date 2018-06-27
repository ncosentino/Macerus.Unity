#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Assets.Scripts.UnityEditor
{
    public sealed class DependencyUpdater
    {
        private const string MSBUILD_EXE_PATH = "C:\\Program Files (x86)\\MSBuild\\14.0\\Bin\\msbuild.exe";

        private static readonly IReadOnlyCollection<string> SOLUTION_FILE_PATHS_TO_BUILD = new[]
        {
            Path.Combine(Application.dataPath, @"..\..\..\..\libraries\projectxyz\projectxyz.sln"),
            Path.Combine(Application.dataPath, @"..\..\..\..\libraries\tiled.net\tiled.net.sln"),
            Path.Combine(Application.dataPath, @"..\..\macerus-game\Macerus.sln"),
        };

        // NOTE: when this is all working, we won't want ANY example modules from ProjectXyz
        private static readonly IReadOnlyCollection<string> EXCLUDE_MODULES = new[]
        {
            "Examples.Modules.GameObjects",
            "Examples.Modules.Mapping",
        };

        private readonly string _dataPath;

        public DependencyUpdater()
        {
            _dataPath = Application.dataPath;
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
            foreach (var solutionFilePath in SOLUTION_FILE_PATHS_TO_BUILD)
            {
                BuildDependency(MSBUILD_EXE_PATH, solutionFilePath);
            }

            Debug.Log($"Copying dependencies...");

            var destinationDependenciesDirectory = Path.Combine(
                _dataPath,
                @"Dependencies");
            Debug.Log($"Destination Dependencies Directory: '{destinationDependenciesDirectory}'");

            if (Directory.Exists(destinationDependenciesDirectory))
            {
                Debug.Log($"Deleting '{destinationDependenciesDirectory}'...");
                Directory.Delete(destinationDependenciesDirectory, true);

                Debug.Log($"Recreating '{destinationDependenciesDirectory}'...");
                Directory.CreateDirectory(destinationDependenciesDirectory);
                Debug.Log($"Recreated '{destinationDependenciesDirectory}'.");
            }

            CopySharedLibraries(destinationDependenciesDirectory);
            CopyMacerusDependencies(destinationDependenciesDirectory);
        }

        private void BuildDependency(
            string msbuildExePath,
            string solutionFilePath)
        {
            solutionFilePath = new Uri(solutionFilePath).LocalPath;

            Debug.Log($"Building '{solutionFilePath}' with '{msbuildExePath}'...");

            var psi = new ProcessStartInfo(
                msbuildExePath,
                $"\"{solutionFilePath}\"")
            {
                CreateNoWindow = true,
                UseShellExecute = false,
            };

            var process = Process.Start(psi);
            process.WaitForExit();
            if (process.ExitCode != 0)
            {
                throw new InvalidOperationException(
                    $"'{msbuildExePath}' exited with code {process.ExitCode} while building '{solutionFilePath}'.");
            }

            Debug.Log($"Built '{solutionFilePath}' with '{msbuildExePath}'.");
        }

        private void CopyMacerusDependencies(string destinationDependenciesDirectory)
        {
            var sourceDependencyDirectory = Path.Combine(
                _dataPath,
                @"..\..\macerus-game\");

            var dependencyEntries = new[]
            {
                new DependencyEntry(
                    "Macerus",
                    @"Macerus\bin\debug",
                    new[] { "*.dll" },
                    EXCLUDE_MODULES),
            };

            foreach (var dependencyEntry in dependencyEntries)
            {
                ProcessDependencyEntry(
                    sourceDependencyDirectory,
                    destinationDependenciesDirectory,
                    dependencyEntry);
            }
        }

        private void CopySharedLibraries(string destinationDependenciesDirectory)
        {
            var librariesDirectory = Path.Combine(
                _dataPath,
                @"..\..\..\..\libraries");
            Debug.Log($"Libraries Directory: '{librariesDirectory}'");

            var dependencyEntries = new[]
            {
                new DependencyEntry(
                    "Project XYZ",
                    @"projectXyz\ConsoleApplication1\bin\Debug",
                    new[] { "*.dll" },
                    EXCLUDE_MODULES),
                new DependencyEntry(
                    "Tiled.NET",
                    @"Tiled.Net\Tiled.Net.Tmx.Xml\bin\Debug",
                    new[] { "*.dll" },
                    new string[0]),
            };

            foreach (var dependencyEntry in dependencyEntries)
            {
                ProcessDependencyEntry(
                    librariesDirectory,
                    destinationDependenciesDirectory,
                    dependencyEntry);
            }
        }

        private void ProcessDependencyEntry(
            string projectsDirectory,
            string destinationDependenciesDirectory,
            DependencyEntry dependencyEntry)
        {
            var dependencyDirectory = Path.Combine(
                projectsDirectory,
                dependencyEntry.RelativePath);
            Debug.Log($"{dependencyEntry.Name} Directory: '{dependencyDirectory}'");

            foreach (var searchPattern in dependencyEntry.SearchPatterns)
            {
                foreach (var sourceDependencyFilePath in Directory
                    .GetFiles(dependencyDirectory, searchPattern)
                    .Where(fileName => !dependencyEntry.ExcludePatterns.Any(exclude => Regex.IsMatch(fileName, exclude))))
                {
                    var destinationFilePath = Path.Combine(
                        destinationDependenciesDirectory,
                        Path.GetFileName(sourceDependencyFilePath));

                    Debug.Log($"Copying '{sourceDependencyFilePath}' to '{destinationFilePath}'...");
                    File.Copy(
                        sourceDependencyFilePath,
                        destinationFilePath,
                        true);
                }
            }
        }

        private sealed class DependencyEntry
        {
            public DependencyEntry(
                string name,
                string relativePath,
                IEnumerable<string> searchPatterns,
                IEnumerable<string> excludePatterns)
            {
                Name = name;
                RelativePath = relativePath;
                SearchPatterns = searchPatterns.ToArray();
                ExcludePatterns = excludePatterns.ToArray();
            }

            public string Name { get; }

            public string RelativePath { get; }

            public IReadOnlyCollection<string> SearchPatterns { get; }

            public IReadOnlyCollection<string> ExcludePatterns { get; }
        }
    }
}
#endif