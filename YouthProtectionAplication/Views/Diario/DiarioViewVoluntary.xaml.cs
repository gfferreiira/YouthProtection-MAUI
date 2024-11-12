using YouthProtectionAplication.ViewModels.Voluntario;

namespace YouthProtectionAplication.Views.Diario;

public partial class DiarioViewVoluntary : ContentPage
{
	DiarioListagemViewModelVoluntary diarioViewModel;
	public DiarioViewVoluntary()
	{
		InitializeComponent();

		diarioViewModel = new DiarioListagemViewModelVoluntary();
		BindingContext = diarioViewModel;
	}
}