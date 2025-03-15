using System;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;

namespace StudyPractice.ViewModels;

public class MainWindowViewModel : ReactiveObject, IScreen
{
    private string _inputText = null!;

    public string InputText
    {
        get => _inputText;
        set => this.RaiseAndSetIfChanged(ref _inputText, value);
    }

    private ReactiveCommand<string, Unit> ProcessTextCommand { get; }
    public RoutingState Router { get; } = new RoutingState();
    
    public MainWindowViewModel()
    {
        Router.Navigate.Execute(new HierarchyWindowModel(this));
        
        ProcessTextCommand = ReactiveCommand.Create<string>(text =>
        {
            Console.WriteLine($"Текст изменён: {text}");
        });
        
        this.WhenAnyValue(x => x.InputText)
            .Where(text => !string.IsNullOrWhiteSpace(text)) 
            .InvokeCommand(ProcessTextCommand);
    }
    
}