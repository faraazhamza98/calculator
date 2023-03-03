namespace Calculator;

public partial class RightPage : ContentPage
{
    public RightPage()
    {
        InitializeComponent();
    }

    public async void navigateToNextQuestion(object sender, EventArgs e)
    {
        var temp = CustomStore.pageNum + 1;
        CustomStore.pageNum = temp + 1;
        await Navigation.PushAsync(new ExamPage());
    }

}