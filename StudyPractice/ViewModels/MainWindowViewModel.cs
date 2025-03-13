using System;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using StudyPractice.Views;

namespace StudyPractice.ViewModels;

public class MainWindowViewModel : ReactiveObject, IScreen
{
    private string _inputText;

    public string InputText
    {
        get => _inputText;
        set => this.RaiseAndSetIfChanged(ref _inputText, value);
    }
    
    public ReactiveCommand<string, Unit> ProcessTextCommand { get; }
    public RoutingState Router { get; } = new RoutingState();
    public ReactiveCommand<Unit, IRoutableViewModel> GoInformation { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> GoHierarchy { get; }
    
    public MainWindowViewModel()
    {
        Router.Navigate.Execute(new HierarchyWindowModel(this));
        
        GoInformation = ReactiveCommand.CreateFromObservable(
            () => Router.Navigate.Execute(new InformationWindowModel(this))
        );
        
        GoHierarchy = ReactiveCommand.CreateFromObservable(
            () => Router.Navigate.Execute(new HierarchyWindowModel(this))
        );
        
        ProcessTextCommand = ReactiveCommand.Create<string>(text =>
        {
            Console.WriteLine($"Текст изменён: {text}");
        });
        
        this.WhenAnyValue(x => x.InputText)
            .Where(text => !string.IsNullOrWhiteSpace(text)) 
            .InvokeCommand(ProcessTextCommand);
    }
}