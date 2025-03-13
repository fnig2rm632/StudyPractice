using System;
using System.Reactive;
using StudyPractice.ViewModels;
using ReactiveUI;

namespace StudyPractice.ViewModels;

public class InformationWindowModel : ReactiveObject, IRoutableViewModel
{
    public IScreen HostScreen { get; }

    public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
    
    public ReactiveCommand<Unit, IRoutableViewModel> GoBackToHierarchy { get; }
    public InformationWindowModel(IScreen screen) {
        
        HostScreen = screen;
        GoBackToHierarchy = ReactiveCommand.CreateFromObservable(() =>
            screen.Router.Navigate.Execute(new HierarchyWindowModel(screen)));
    }
}