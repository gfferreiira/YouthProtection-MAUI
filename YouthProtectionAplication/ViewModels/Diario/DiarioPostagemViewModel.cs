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
        private long publicationId;
        private string publicationContent = string.Empty;
        private DateTime createdAt;
        private TipoPostagemEnum tpPostagem;
        private int valorTipoPostagemSelecionado;
        private bool isPublic;
        private bool isPrivate;


        #region Atributos Propriedades

        public bool IsPublic
        {
            get => isPublic;
            set
            {
                if (isPublic != value)
                {
                    isPublic = value;
                    OnPropertyChanged();
                    OnVisibilityChanged();
                }
            }
        }

        public bool IsPrivate
        {
            get => isPrivate;
            set
            {
                if (isPrivate != value)
                {
                    isPrivate = value;
                    OnPropertyChanged();
                    OnVisibilityChanged();
                }
            }
        }

        private void OnVisibilityChanged()
        {
            int visibilityValue = IsPublic ? 0 : 1;
            // Aqui você pode usar o valor conforme necessário
            // Ex: enviar para um serviço ou atualizar outra propriedade
        }



        public int ValorTipoPostagemSelecionado
        {
            get => valorTipoPostagemSelecionado;
            set
            {
                    valorTipoPostagemSelecionado = value;
                    OnPropertyChanged();
            }
        }



        

        public long PublicationId
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
        public DateTime CreatedAt
        {
            get => createdAt;
            set
            {
                createdAt = value;
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
            try
            {
                createdAt = DateTime.Now;

                if (isPrivate == false || isPublic == true)
                {
                    valorTipoPostagemSelecionado = 0;
                }
                else
                {
                    valorTipoPostagemSelecionado = 1;
                }


                if (isPrivate == false && isPublic == false)
                {
                    await Application.Current.MainPage
                        .DisplayAlert("Mensagem", "Selecione se a postagem é publica ou privada", "Ok");
                    return;
                }

                if (string.IsNullOrEmpty(publicationContent))
                {
                    await Application.Current.MainPage
                        .DisplayAlert("Mensagem", "Anotação não pode estar vazia", "Ok");
                    return;
                }

                Postagem model = new Postagem()
                {

                    PublicationContent = this.PublicationContent,
                    CreatedAt = CreatedAt,
                    PublicationsRole = (TipoPostagemEnum)this.valorTipoPostagemSelecionado,
                    publicationId = this.PublicationId
                    
                };
                if (model.publicationId == 0)
                    await UserDiarioService.PostPostagemAsync(model);

                await Application.Current.MainPage
                        .DisplayAlert("Mensagem", "Postagem feita com sucesso", "Ok");


                Application.Current.MainPage = new AppShell(Preferences.Get("UsuarioRole", 0));

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                     .DisplayAlert("Informação:", ex.Message + "\n" + ex.InnerException, "Ok");
            }

        }


        private void UpdateRemainingCharacters()
        {
            RemainingCharacters = MaxCharacters - (PublicationContent?.Length ?? 0);
        }

        #endregion








    }
}