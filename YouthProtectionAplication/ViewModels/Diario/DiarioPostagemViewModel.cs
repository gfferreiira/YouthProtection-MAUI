using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Services.Maps;
using YouthProtectionAplication.Services.Diario;

namespace YouthProtectionAplication.ViewModels.Diario
{
    public class DiarioPostagemViewModel : BaseViewModel
    {
        private DiarioService UserDiarioService;

        public DiarioPostagemViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            UserDiarioService = new DiarioService(token);

            _ = ObterTipoPostagem();
        }

        private int idUser;
        private int idPostagem;
        private string titulo = string.Empty;
        private string texto = string.Empty;
        private DateTime dataPostagem;
        private ObservableCollection<TipoPostagemEnum> listaTipoPostagem;

        #region Atributos Propriedades
        public int IdUser 
        { 
            get => idUser;
            set => idUser = value;
        }
        public int IdPostagem 
        {
            get => idPostagem; 
            set => idPostagem = value; 
        }
        public string Titulo 
        { 
            get => titulo; 
            set => titulo = value;
        }
        public string Texto
        { 
            get => texto; 
            set => texto = value; 
        }
        public DateTime DataPostagem
        {
            get => dataPostagem; 
            set => dataPostagem = value;
        }

         public ObservableCollection<TipoPostagemEnum> listaTipoPostagem
        {
            get { return listaTipoPostagem; }
            set
             {
                 if (value != null)
                 {
                     listaTipoPostagem = value;
                     OnPropertyChanged();
                }
            }
        }


        #endregion

        #region Métodos 
    
        public async Task ObterTipoPostagem()
        {
            try
            {
                ListaTipoPostagem = new ObservableCollection<TipoPostagemEnum>();
                ListaTipoPostagem.Add(new TipoPostagemEnum() {Id = 0, Descricao = "Público" });
                ListaTipoPostagem.Add(new TipoPostagemEnum() {Id = 1, Descricao = "Privado" });
                OnPropertyChanged(nameof(ListaTipoPostagem));
            }
             catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
         }
                

        
        #endregion
        


        

       

        
}
