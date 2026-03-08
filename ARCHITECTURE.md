# Multi-Page WPF Wizard Application Structure

## Folder Organization

```
PulsarInstaller/
├── Views/
│   ├── Pages/              # Wizard pages (reusable UserControls)
│   │   ├── WelcomePage.xaml
│   │   ├── WelcomePage.xaml.cs
│   │   ├── TaskSelectionPage.xaml
│   │   ├── TaskSelectionPage.xaml.cs
│   │   └── ...
│   └── Shared/             # Shared controls (buttons, headers, etc.)
│       ├── WizardHeader.xaml
│       └── ...
│
├── ViewModels/
│   ├── Base/               # Base classes for all ViewModels
│   │   ├── ViewModelBase.cs
│   │   └── WizardPageViewModelBase.cs
│   ├── Pages/              # ViewModels for each page
│   │   ├── WelcomePageViewModel.cs
│   │   ├── TaskSelectionPageViewModel.cs
│   │   └── ...
│   └── MainWindowViewModel.cs
│
├── Services/
│   ├── Navigation/         # Navigation service
│   │   ├── INavigationService.cs
│   │   └── NavigationService.cs
│   └── ...
│
├── Classes/
│   ├── Task.cs
│   ├── Prerequisite.cs
│   ├── ListIO.cs
│   └── ...
│
├── Enums/
│   ├── TaskType.cs
│   └── PrerequisiteType.cs
│
└── MainWindow.xaml         # Shell/container for wizard

```

## Architecture

### 1. **Base Classes** (ViewModels/Base/)
- `ViewModelBase`: Implements `INotifyPropertyChanged` for all ViewModels
- `WizardPageViewModelBase`: Extends ViewModelBase with wizard-specific properties and methods

### 2. **Views** (Views/Pages/)
- Each wizard page is a `UserControl` (not a full Window)
- Pages are **reusable** and can be shown in different contexts
- Data binding connects to their ViewModel via `DataContext`

### 3. **ViewModels** (ViewModels/Pages/)
- Each page has a corresponding ViewModel
- Inherits from `WizardPageViewModelBase`
- Handles page logic, data, and validation

### 4. **Navigation Service** (Services/Navigation/)
- Centralized page navigation management
- Maintains page history (Back button)
- Handles page lifecycle (OnActivated, OnDeactivated, ValidateAndProceed)

### 5. **MainWindow** (Shell)
- Acts as container for wizard pages
- Uses ContentPresenter to display current page
- Binds navigation buttons to NavigationService commands

## Key Differences from Android

| Feature | Android | WPF |
|---------|---------|-----|
| Activity/Fragment | Fragment (reusable UI) | UserControl (reusable, like Fragment) |
| Fragment Manager | Manages fragments | NavigationService manages pages |
| Data Sharing | ViewModel scoped | Shared ViewModel or service injection |
| Lifecycle | onCreate, onStart, onResume, onPause, onStop, onDestroy | OnActivated, OnDeactivated |
| UI Container | Activity | MainWindow (shell) |

## Workflow Example

1. **User opens app** → MainWindow loads → NavigationService shows WelcomePage
2. **User clicks "Next"** → WelcomePageViewModel.ValidateAndProceed() is called
3. **Validation passes** → NavigationService navigates to TaskSelectionPage
4. **Page activates** → TaskSelectionPageViewModel.OnActivated() loads task list
5. **User clicks "Back"** → NavigationService pops from history, shows previous page

## Are Views Reusable?

**Yes!** UserControls are fully reusable. You can:
- Use the same page in multiple wizards
- Reuse a page by embedding it in another view
- Change the ViewModel dynamically via binding
- Test pages independently

Example: Your `TaskSelectionPage` could be used in:
- Initial setup wizard
- Task modification dialog
- Import wizard
- All would share the same UserControl but different ViewModels/data

## MVVM Pattern (Model-View-ViewModel)

```
View (XAML) → binds to → ViewModel (C#) → accesses → Model (Data/Services)
     ↑                                              ↓
     └──────── Data Binding (INotifyPropertyChanged) ───┘
```

- **View**: UI markup (XAML)
- **ViewModel**: Logic, properties, commands
- **Model**: Data classes (Task, Prerequisite), Services (ListIO, NavigationService)

This keeps code organized and testable!

