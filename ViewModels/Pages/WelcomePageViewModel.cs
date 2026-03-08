using PulsarInstaller.ViewModels.Base;

namespace PulsarInstaller.ViewModels.Pages;

/// <summary>
/// ViewModel for the Welcome page - first page of the wizard
/// </summary>
public class WelcomePageViewModel : WizardPageViewModelBase
{
    public WelcomePageViewModel()
    {
        Title = "Welcome to Pulsar Installer";
        Description = "This wizard will help you install and configure Pulsar.";
        CanGoBack = false; // Can't go back from first page
    }

    public override void OnActivated()
    {
        // Load any welcome-specific data here
    }
}

