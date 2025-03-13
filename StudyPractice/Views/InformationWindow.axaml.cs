using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using StudyPractice.ViewModels;

namespace StudyPractice.Views;

public partial class InformationWindow : ReactiveUserControl<InformationWindowModel>
{
    public InformationWindow()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }
}