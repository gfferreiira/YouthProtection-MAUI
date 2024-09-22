using System.Text.RegularExpressions;
using YouthProtectionAplication.ViewModels.Usuarios;

namespace YouthProtectionAplication.Views.Usuarios;

public partial class CreateAccountPage : ContentPage
{
    UsuarioViewModel viewModel;
    public CreateAccountPage()
    {
        InitializeComponent();
        viewModel = new UsuarioViewModel();
        BindingContext = viewModel;
    }

    private async void OnButtonCadastrarPressed(object sender, EventArgs e)
    {
        // Animação de escala
        await btnCadastrar.ScaleTo(0.9, 50, Easing.Linear);
        await Task.Delay(150);
        await btnCadastrar.ScaleTo(1, 50, Easing.Linear);

    }

    private void OnDateEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        string newText = e.NewTextValue;
        if (newText.Length > 2 && newText[2] != '/')
        {
            newText = newText.Insert(2, "/");
        }

        if (newText.Length > 5 && newText[5] != '/')
        {
            newText = newText.Insert(5, "/");
        }

        dateEntry.CursorPosition = newText.Length;
        ((Entry)sender).Text = newText;
    }

    private void OnCellPhoneEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        string newText = e.NewTextValue;
        if (newText.Length > 2 && newText[0] != '(')
        {
            newText = newText.Insert(0, "(");
        }

        if (newText.Length > 5 && newText[3] != ')')
        {
            newText = newText.Insert(3, ") ");
        }

        cellPhoneEntry.CursorPosition = newText.Length;
        ((Entry)sender).Text = newText;
    }
   }