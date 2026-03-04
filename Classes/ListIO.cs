using System.IO;
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
                new List<string>{"C:\\install"}, "This is another example task.", new List<Prerequisite>{examplePrerequisite1});
        taskList = new List<Task>();
        taskList.Add(exampleTask1);
        taskList.Add(exampleTask2);
        
        // Configure JsonSerializer options to convert enums to strings and format nicely
        var options = new System.Text.Json.JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
            Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
        };
        
        string json = System.Text.Json.JsonSerializer.Serialize(taskList, options);
        File.WriteAllText(path, json);
    }
}