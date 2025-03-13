using System;
using ReactiveUI;
using StudyPractice.Views;

namespace StudyPractice.ViewModels;

public class AppViewLocator : ReactiveUI.IViewLocator
{
    public IViewFor ResolveView<T>(T viewModel, string contract = null) => viewModel switch
    {
        HierarchyWindowModel context => new HierarchyWindow { DataContext = context },
        InformationWindowModel context => new InformationWindow { DataContext = context },
        _ => throw new ArgumentOutOfRangeException(nameof(viewModel), $"Неизвестный ViewModel: {viewModel?.GetType().FullName}")
    };
}