﻿#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.UnityEditor
{
    public sealed class UpdateDependencies
    {
        [MenuItem("Macerus Tools/Update Dependencies")]
        public static void BuildGame()
        {
            Debug.Log($"Copying dependencies...");

            var destinationDependenciesDirectory = Path.Combine(
                Application.dataPath,
                @"Dependencies");
            Debug.Log($"Destination Dependencies Directory: '{destinationDependenciesDirectory}'");

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
                    "*.dll"),
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
                    "*.dll"),
                new DependencyEntry(
                    "Tiled.NET",
                    @"Tiled.Net\Tiled.Net.Tmx.Xml\bin\Debug",
                    "*.dll"),
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
                foreach (var sourceDependencyFilePath in Directory.GetFiles(dependencyDirectory, searchPattern))
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
                params string[] searchPatterns)
            {
                Name = name;
                RelativePath = relativePath;
                SearchPatterns = searchPatterns.ToArray();
            }

            public string Name { get; }

            public string RelativePath { get; }

            public IReadOnlyCollection<string> SearchPatterns { get; }
        }
    }
}
#endif