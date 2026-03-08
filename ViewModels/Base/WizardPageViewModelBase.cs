using System.Windows.Input;

namespace PulsarInstaller.ViewModels.Base;

/// <summary>
/// Base class for wizard page ViewModels
/// Provides common wizard navigation and validation logic
/// </summary>
public abstract class WizardPageViewModelBase : ViewModelBase
{
    private string _title = string.Empty;
    private string _description = string.Empty;
    private bool _canProceed = true;
    private bool _canGoBack = true;

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    public bool CanProceed
    {
        get => _canProceed;
        set => SetProperty(ref _canProceed, value);
    }

    public bool CanGoBack
    {
        get => _canGoBack;
        set => SetProperty(ref _canGoBack, value);
    }

    /// <summary>
    /// Called when page is activated (shown)
    /// Override to load data or perform initialization
    /// </summary>
    public virtual void OnActivated()
    {
    }

    /// <summary>
    /// Called when proceeding to next page
    /// Override to validate data before moving forward
    /// </summary>
    public virtual bool ValidateAndProceed()
    {
        return true;
    }

    /// <summary>
    /// Called when going back to previous page
    /// Override to save state or perform cleanup
    /// </summary>
    public virtual void OnDeactivated()
    {
    }
}

