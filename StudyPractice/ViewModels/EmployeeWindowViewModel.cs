using System;
using System.Reactive;
using ReactiveUI;

namespace StudyPractice.ViewModels;

public class EmployeeWindowViewModel : ReactiveObject, IRoutableViewModel
{
    #region RoutingState
    
        public ReactiveCommand<Unit, IRoutableViewModel> GoHierarchy { get; }
    
        public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
        
        public IScreen HostScreen { get; }

    #endregion
    public EmployeeWindowViewModel(IScreen screen)
    {
        HostScreen = screen;
        
        GoHierarchy = ReactiveCommand.CreateFromObservable(() =>
            screen.Router.Navigate.Execute(new HierarchyWindowModel(screen)));
        
    }
}