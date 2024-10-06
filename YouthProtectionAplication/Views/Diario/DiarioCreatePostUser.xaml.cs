namespace YouthProtectionAplication.Views.Diario;

public partial class DiarioCreatePostUser : ContentPage
{
    private const int MaxLenght = 260;
    public DiarioCreatePostUser()
	{
		

		InitializeComponent();
	}

	private void OnTextChanged(object sender, TextChangedEventArgs e)
	{
		string postText = e.NewTextValue ?? string.Empty;


		int remainingCharacters = MaxLenght - postText.Length;


		CountPostLabel.Text = $"{remainingCharacters} caracteres restantes";

        if (remainingCharacters <= 10)
        {
            CountPostLabel.TextColor = Colors.Red; // Alerta quando está próximo do limite
        }
        else
        {
            CountPostLabel.TextColor = Colors.Black;
        }

    }
}