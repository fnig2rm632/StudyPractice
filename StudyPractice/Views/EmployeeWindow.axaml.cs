using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using StudyPractice.ViewModels;

namespace StudyPractice.Views;

public partial class EmployeeWindow : ReactiveUserControl<EmployeeWindowViewModel>
{
    public EmployeeWindow()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }
}