using System.Text.Json;
using Uri = System.Uri;

namespace Calculator;
internal class QuestionModel
{
    public string questionText { get; set; }
    public int questionNumber { get; set; }
    public string optionOne { get; set; }
    public string optionTwo { get; set; }
    public string optionThree { get; set; }
    public string correctOptionValue { get; set; }
}
internal class RestService
{
    HttpClient _client;
    JsonSerializerOptions _serializerOptions;
    static string apiUrl = "http://3.6.127.17:8080/dques/";
    public QuestionModel question { get; private set; }

    public RestService()
    {
        _client = new HttpClient();
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
    }

    public async Task<QuestionModel> getCurrentQuestionData(int questionNumber)
    {
        question = new QuestionModel();

        Uri uri = new Uri(string.Format($"{apiUrl}{questionNumber}", string.Empty));
        try
        {
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                question = JsonSerializer.Deserialize<QuestionModel>(content, _serializerOptions);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return question;
    }
}

public partial class ExamPage : ContentPage
{

    public int currentQuestionNumber = 0;
    public string correctOptionValue;
    public ExamPage()
    {
        InitializeComponent();
        getQuestionNumberFromStorage();
        getQuestionData();
    }

    public async void getQuestionNumberFromStorage()
    {
        currentQuestionNumber = CustomStore.pageNum;
    }

    public void goToNextQuestion()
    {
        ++currentQuestionNumber;
        getQuestionData();
    }

    public async void getQuestionData()
    {
        RestService restService = new RestService();
        var questionData = await restService.getCurrentQuestionData(currentQuestionNumber);
        questionText.Text = questionData.questionText;
        optionOneBtn.Text = questionData.optionOne;
        optionTwoBtn.Text = questionData.optionTwo;
        optionThreeBtn.Text = questionData.optionThree;
        correctOptionValue = questionData.correctOptionValue;
    }

    private async void optionBtn_Clicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string buttonValue = button.Text;
        if (buttonValue != correctOptionValue)
        {
            await Navigation.PushAsync(new WrongPage());
            return;
        }
        await Navigation.PushAsync(new RightPage());
        return;
    }
}