using System;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;

namespace StudyPractice.ViewModels;

public class MainWindowViewModel : ReactiveObject, IScreen
{
    public InformationWindowModel UserControlStart { get; }

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
        UserControlStart = new InformationWindowModel(this);
        Router.Navigate.Execute(UserControlStart);
        
        ProcessTextCommand = ReactiveCommand.Create<string>(text =>
        {
            Console.WriteLine($"Текст изменён: {text}");
            UserControlStart.ProcessTextCommand.Execute(text).Subscribe();
        });
        
        this.WhenAnyValue(x => x.InputText)
            .InvokeCommand(ProcessTextCommand);
    }
    
}