using System.Configuration;
using System.Data;
using System.Windows;

namespace PulsarInstaller;
using Classes;

/// <summary>
/// Interaction logic for App.xaml
/// Core class. Gets imported list -> executes tasks.
/// All tasks must be run as separate threads, waiting for prerequisites to be met before executing.
/// TODO: Add "smart" system to shuffle tasks if one depends on other
/// </summary>
public partial class App : Application
{
    public App()
    {
        ListIO.CreateList("C:\\Users\\Work\\Desktop\\list.json");

        ListIO.GetTaskList(ListIO.OpenFileSelectionDialog());
    }
}