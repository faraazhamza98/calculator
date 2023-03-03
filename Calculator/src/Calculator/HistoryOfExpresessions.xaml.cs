namespace Calculator;

public partial class HistoryOfExpresessions : ContentPage
{
	public HistoryOfExpresessions()
	{
		InitializeComponent();
        BindingContext = App.historyData;
    }
}
