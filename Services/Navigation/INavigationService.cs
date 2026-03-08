using System.Windows.Controls;
using PulsarInstaller.ViewModels.Base;

namespace PulsarInstaller.Services.Navigation;

/// <summary>
/// Service interface for navigating between wizard pages
/// </summary>
public interface INavigationService
{
    /// <summary>
    /// Currently active page
    /// </summary>
    UserControl? CurrentPage { get; }

    /// <summary>
    /// Currently active page's ViewModel
    /// </summary>
    WizardPageViewModelBase? CurrentViewModel { get; }

    /// <summary>
    /// Navigate to a page by name
    /// </summary>
    void NavigateTo(string pageName);

    /// <summary>
    /// Go to next page
    /// </summary>
    void Next();

    /// <summary>
    /// Go to previous page
    /// </summary>
    void Back();

    /// <summary>
    /// Check if there's a previous page available
    /// </summary>
    bool CanGoBack();

    /// <summary>
    /// Check if there's a next page available
    /// </summary>
    bool CanGoNext();

    /// <summary>
    /// Register a page with the navigation service
    /// </summary>
    void RegisterPage(string name, UserControl view, WizardPageViewModelBase viewModel);
}

