using System.Windows.Controls;
using PulsarInstaller.ViewModels.Base;

namespace PulsarInstaller.Services.Navigation;

/// <summary>
/// Implementation of INavigationService for wizard-style navigation
/// Manages page transitions and maintains page history
/// </summary>
public class NavigationService : INavigationService
{
    private readonly Dictionary<string, (UserControl View, WizardPageViewModelBase ViewModel)> _registeredPages = new();
    private readonly Stack<string> _navigationHistory = new();
    private string _currentPageName = string.Empty;

    public UserControl? CurrentPage => GetCurrentPageTuple().View;
    public WizardPageViewModelBase? CurrentViewModel => GetCurrentPageTuple().ViewModel;

    public void RegisterPage(string name, UserControl view, WizardPageViewModelBase viewModel)
    {
        if (!_registeredPages.ContainsKey(name))
        {
            _registeredPages[name] = (view, viewModel);
        }
    }

    public void NavigateTo(string pageName)
    {
        if (!_registeredPages.ContainsKey(pageName))
        {
            throw new ArgumentException($"Page '{pageName}' is not registered.", nameof(pageName));
        }

        if (!string.IsNullOrEmpty(_currentPageName))
        {
            CurrentViewModel?.OnDeactivated();
            _navigationHistory.Push(_currentPageName);
        }

        _currentPageName = pageName;
        CurrentViewModel?.OnActivated();
    }

    public void Next()
    {
        if (!CurrentViewModel?.ValidateAndProceed() ?? false)
        {
            return; // Validation failed, don't proceed
        }
        // Override in subclass or use events to determine next page
    }

    public void Back()
    {
        if (_navigationHistory.Count == 0)
            return;

        CurrentViewModel?.OnDeactivated();
        _currentPageName = _navigationHistory.Pop();
        CurrentViewModel?.OnActivated();
    }

    public bool CanGoBack() => _navigationHistory.Count > 0;

    public bool CanGoNext() => CurrentViewModel?.CanProceed ?? false;

    private (UserControl View, WizardPageViewModelBase ViewModel) GetCurrentPageTuple()
    {
        return _registeredPages.ContainsKey(_currentPageName)
            ? _registeredPages[_currentPageName]
            : (null!, null!);
    }
}

