using PulsarInstaller.Enums;

namespace PulsarInstaller.Classes;

public class Prerequisite
{
    // Type, target, description
    public string name {get; set;}
    public PrerequisiteType type {get; set;}
    public List<string> target {get; set;} = new List<string>();
    public string? description {get; set;}
    
    public Prerequisite(string name, PrerequisiteType type, List<string> target, string? description = null)
    {
        this.name = name;
        this.type = type;
        this.target = target;
        this.description = description;
    }
}