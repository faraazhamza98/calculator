namespace Calculator;

public partial class WrongPage : ContentPage
{
    public WrongPage()
    {
        InitializeComponent();
    }
    public async void navigateToNextQuestion(object sender, EventArgs e)
    {
        var temp = CustomStore.pageNum + 1;
        CustomStore.pageNum = temp + 1;
        await Navigation.PushAsync(new ExamPage());
    }
    public async void tryAgain(object sender, EventArgs e)
    {
        // var temp = CustomStore.pageNum;
        // CustomStore.pageNum = temp;
        await Navigation.PushAsync(new ExamPage());
    }
}