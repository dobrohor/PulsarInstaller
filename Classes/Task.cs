using PulsarInstaller.Enums;

namespace PulsarInstaller.Classes;

public class Task
{
    // Name, description, type, prerequisites, is selected, source, destination 
    public string name {get; set;}
    public TaskType type {get; set;}
    public List<Prerequisite>? prerequisites {get; set;}
    public bool isSelected {get; set;}
    public string source {get; set;}
    public List<string> destination {get; set;}
    public string? description {get; set;}
    
    public Task(string name, TaskType type, string source, List<string> destination, string? description = null,
        List<Prerequisite>? prerequisites = null)
    {
        this.name = name;
        this.type = type;
        this.prerequisites = prerequisites;
        this.source = source;
        this.destination = destination;
        this.description = description;
    }
}