using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using StudyPractice.ViewModels;

namespace StudyPractice.Views;

public partial class HierarchyWindow : ReactiveUserControl<HierarchyWindowModel>
{
    public HierarchyWindow()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
        
    }
}