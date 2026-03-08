using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PulsarInstaller.ViewModels.Base;

/// <summary>
/// Base class for all ViewModels, implements INotifyPropertyChanged for data binding
/// </summary>
public abstract class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected void SetProperty<T>(ref T backingField, T value, [CallerMemberName] string? propertyName = null)
    {
        if (!Equals(backingField, value))
        {
            backingField = value;
            OnPropertyChanged(propertyName);
        }
    }
}

