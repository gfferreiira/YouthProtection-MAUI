using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YouthProtectionAplication.Models;
using YouthProtectionAplication.Models.Enums;
using YouthProtectionAplication.Services.Diario;
using YouthProtectionAplication.Views.Diario;

namespace YouthProtectionAplication.ViewModels.Diario
{
    [QueryProperty("PostagemSelecionadaId", "pId")]
    public class DiarioPostagemViewModel : BaseViewModel
    {
        private DiarioService UserDiarioService;

        

        public ICommand SalvarCommand { get; }

        public DiarioPostagemViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            UserDiarioService = new DiarioService(token);
            remainingCharacters = MaxCharacters;

            SalvarCommand = new Command(async () => { await SalvarPostagem(); });

           
        }

        private const int MaxCharacters = 260;
        private int remainingCharacters;
        private int publicationId;
        private string publicationContent = string.Empty;
        private DateTime dataPostagem;
        private TipoPostagemEnum tpPostagem;
        private int valorTipoPostagemSelecionado;

        public int ValorTipoPostagemSelecionado
        {
            get => valorTipoPostagemSelecionado;
            set
            {
                if (valorTipoPostagemSelecionado != value)
                {
                    valorTipoPostagemSelecionado = value;
                    OnPropertyChanged();


                }
            }
        }



        #region Atributos Propriedades

        public int PublicationId
        {
            get => publicationId;
            set
            {
                publicationId = value;
                OnPropertyChanged();
            }
        }
        
        public string PublicationContent
        {
            get => publicationContent;
            set
            {
                if(publicationContent != value)
                {
                    publicationContent = value;
                    OnPropertyChanged();
                    UpdateRemainingCharacters();
                }
            }
        }
        public DateTime DataPostagem
        {
            get => dataPostagem;
            set
            {
                dataPostagem = value;
                OnPropertyChanged();
            }
        }
        public TipoPostagemEnum TpPostagem
        {
            get => tpPostagem;
            set
            {
                if (tpPostagem != null)
                {
                    {
                        tpPostagem = value;
                        OnPropertyChanged();
                    }

                }
                else
                {
                    tpPostagem = TipoPostagemEnum.Privado;
                    OnPropertyChanged();
                }
            }
        }

       

        public int RemainingCharacters
        {
            get => remainingCharacters;
            private set
            {
                if (remainingCharacters != value)
                {
                    remainingCharacters = value;
                    OnPropertyChanged();
                }
            }
        }

       



        #endregion

        #region Métodos 


        public async Task SalvarPostagem()
        {
            DataPostagem = DateTime.Now;

            Postagem model = new Postagem()
            {


                PublicationContent = this.PublicationContent,
                DataPostagem = DataPostagem,
                TipoPost = (TipoPostagemEnum)this.ValorTipoPostagemSelecionado,
                publicationId = this.PublicationId
            };
            if (model.publicationId == 0)
                await UserDiarioService.PostPostagemAsync(model);
            else
                await UserDiarioService.PutPostagemAsync(model);

            await Application.Current.MainPage
                    .DisplayAlert("Mensagem", "Postagem feita com sucesso", "Ok");

            Application.Current.MainPage = new DiarioViewUser();
           
        }


        private void UpdateRemainingCharacters()
        {
            RemainingCharacters = MaxCharacters - (PublicationContent?.Length ?? 0);
        }


        #endregion








    }
}