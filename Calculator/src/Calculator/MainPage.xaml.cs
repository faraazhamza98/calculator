using System.Data;

namespace Calculator;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
        OnClear(this, null);

    }
    string currentEntry = "";
    int currentState = 1;
    string mathOperator;
    double firstNumber, secondNumber;
    string decimalFormat = "N0";
    string equation = "";
    string displayText = "";



    void OnSelectNumber(object sender, EventArgs e)
    {

        Button button = (Button)sender;
        string pressed = button.Text;
        this.equation += pressed;
        this.displayText += pressed;
        currentEntry += pressed;

        if ((this.resultText.Text == "0" && pressed == "0")
            || (currentEntry.Length <= 1 && pressed != "0")
            || currentState < 0)
        {
            this.resultText.Text = "";
            if (currentState < 0)
                currentState *= -1;
        }

        if (pressed == "." && decimalFormat != "N2")
        {
            decimalFormat = "N2";
        }

        this.resultText.Text += pressed;
    }

    void OnSelectOperator(object sender, EventArgs e)
    {
        LockNumberValue(resultText.Text);

        currentState = -2;
        Button button = (Button)sender;
        string pressed = button.Text;
        this.equation += pressed;
        this.displayText += pressed;
        mathOperator = pressed;
    }

    private void LockNumberValue(string text)
    {
        double number;
        if (double.TryParse(text, out number))
        {
            if (currentState == 1)
            {
                firstNumber = number;
            }
            else
            {
                secondNumber = number;
            }

            currentEntry = string.Empty;
        }
    }

    void OnClear(object sender, EventArgs e)
    {
        firstNumber = 0;
        secondNumber = 0;
        currentState = 1;
        decimalFormat = "N0";
        this.resultText.Text = "0";
        currentEntry = "";
        this.CurrentCalculation.Text = "";
        this.equation = "";
        this.displayText = "";
    }

    void OnCalculate(object sender, EventArgs e)
    {
        if (mathOperator != "Sqrt")
        {
            DataTable dt = new DataTable();
            System.Diagnostics.Debug.WriteLine(this.equation);
            var v = dt.Compute(this.equation, "");
            this.resultText.Text = v.ToString();
        }
        //Console.WriteLine(v);
        if (currentState == 2)
        {
            if (secondNumber == 0)
                LockNumberValue(resultText.Text);

            double result = Calculator.Calculate(firstNumber, secondNumber, mathOperator);

            this.CurrentCalculation.Text = this.displayText;

            //this.resultText.Text = result.ToTrimmedString(decimalFormat);
         
            firstNumber = result;
            secondNumber = 0;
            currentState = -1;
            currentEntry = string.Empty;
            App.historyData.addToHistory(this.displayText);
        }
    }

    void OnNegative(object sender, EventArgs e)
    {
        if (currentState == 1)
        {
            secondNumber = -1;
            mathOperator = "*";
            currentState = 2;
            OnCalculate(this, null);
        }
    }

    void OnPercentage(object sender, EventArgs e)
    {

        LockNumberValue(resultText.Text);
        this.equation += "*0.01";
        this.displayText += "%";
        decimalFormat = "N2";
        secondNumber = 0.01;
        mathOperator = "*";
        currentState = 2;

    }
}
