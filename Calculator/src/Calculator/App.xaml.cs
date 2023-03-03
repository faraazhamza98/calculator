namespace Calculator;

public partial class App : Application
{
    public static MyViewModel historyData;
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
        historyData = new MyViewModel();
        BindingContext = historyData;
    }
}
