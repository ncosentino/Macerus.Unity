#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.UnityEditor
{
    public sealed class UpdateDependencies
    {
        // NOTE: when this is all working, we won't want ANY example modules from ProjectXyz
        private static readonly IReadOnlyCollection<string> EXCLUDE_MODULES = new[]
        {
            "Examples.Modules.GameObjects",
            "Examples.Modules.Mapping",
        };

        [MenuItem("Macerus Tools/Update Dependencies")]
        public static void BuildGame()
        {
            Debug.Log($"Copying dependencies...");

            var destinationDependenciesDirectory = Path.Combine(
                Application.dataPath,
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

        private static void CopyMacerusDependencies(string destinationDependenciesDirectory)
        {
            var sourceDependencyDirectory = Path.Combine(
                Application.dataPath,
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

        private static void CopySharedLibraries(string destinationDependenciesDirectory)
        {
            var librariesDirectory = Path.Combine(
                Application.dataPath,
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

        private static void ProcessDependencyEntry(
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