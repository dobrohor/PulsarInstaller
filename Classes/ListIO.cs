using System.IO;
using System.Windows;
using Microsoft.Win32;
using PulsarInstaller.Enums;

namespace PulsarInstaller.Classes;

// For creation/reading list.json
public class ListIO 
{
    public static List<Task>? taskList {get; set;}
    
    public static void CreateList(string path)
    {
        // Create list.json with example task
        Task exampleTask1 = 
            new Task("Example Task1", TaskType.Copy, "C:\\source", 
                new List<string>{"C:\\destination"}, "This is an example task.");
        Prerequisite examplePrerequisite1 = 
            new Prerequisite("Example Prerequisite1", Enums.PrerequisiteType.TaskCompleted, 
                new List<string>{"Example Task1"}, "Proceed only if Example Task1 is completed.");
        Task exampleTask2 = 
            new Task("Example Task2", TaskType.Install, "C:\\installer.exe", 
                new List<string>{"C:\\install"}, "This is another example task.",
                new List<Prerequisite>{examplePrerequisite1});
        taskList = new List<Task>();
        taskList.Add(exampleTask1);
        taskList.Add(exampleTask2);
        
        // Create fine options for serialization to JSON
        var options = new System.Text.Json.JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
            Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
        };
        
        // Serialize list to JSON and write to file
        string json = System.Text.Json.JsonSerializer.Serialize(taskList, options);
        File.WriteAllText(path, json);
    }

    public static List<Task>? GetTaskList(string? path)
    {
        // This is another check after initial selection in case file was not reachable or was deleted after selection.
        // If file is not found, user will be prompted to select another file until a valid one is selected, or they cancel the dialog.
        while (true)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = OpenFileSelectionDialog();
                
                // User cancelled the dialog
                if (string.IsNullOrEmpty(path))
                {
                    return null;
                }
            }
            
            // Check if file exists
            FileInfo fileInfo = new FileInfo(path);
            if (!fileInfo.Exists)
            {
                MessageBox.Show($"File not found: {path}\n\nPlease select a valid file.", "File Not Found", 
                    System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                path = null; // Reset to prompt user again
                continue;
            }
            
            // File exists, deserialize with same option and return
            string json = File.ReadAllText(path);
            var options = new System.Text.Json.JsonSerializerOptions
            {
                PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
                Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
            };
            
            taskList = System.Text.Json.JsonSerializer.Deserialize<List<Task>>(json, options);
            return taskList;
        }
    }

    public static string? OpenFileSelectionDialog()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Title = "Select a JSON task list file",
            Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
            DefaultExt = ".json",
            CheckFileExists = true
        };

        if (openFileDialog.ShowDialog() == true)
        {
            return openFileDialog.FileName;
        }

        return null;
    }
}