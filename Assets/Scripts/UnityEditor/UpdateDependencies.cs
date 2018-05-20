using System.Collections.Generic;
using UnityEditor;
using System.IO;
using System.Linq;
using UnityEngine;

public sealed class UpdateDependencies
{
    [MenuItem("Macerus Tools/Update Dependencies")]
    public static void BuildGame()
    {
        Debug.Log($"Copying dependencies...");

        var projectsDirectory = Path.Combine(
            Application.dataPath,
            @"..\..\");
        Debug.Log($"Projects Directory: '{projectsDirectory}'");

        var destinationDependenciesDirectory = Path.Combine(
            Application.dataPath,
            @"Dependencies");
        Debug.Log($"Destination Dependencies Directory: '{destinationDependenciesDirectory}'");

        var dependencyEntries = new[]
        {
            new DependencyEntry(
                "Project XYZ",
                @"projextXyz2\ConsoleApplication1\bin\Debug",
                "*.dll"),
            new DependencyEntry(
                "Tiled.NET",
                @"Tiled.Net\Tiled.Net\bin\Debug",
                "*.dll"),
        };

        foreach (var dependencyEntry in dependencyEntries)
        {
            ProcessDependencyEntry(
                projectsDirectory,
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