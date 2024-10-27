using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YouthProtectionAplication.Models;
using YouthProtectionAplication.Models.Enums;
using YouthProtectionAplication.Services.Diario;
using YouthProtectionAplication.Views.Diario;

namespace YouthProtectionAplication.ViewModels.Diario
{
    public class DiarioEdicaoViewModel : BaseViewModel
    {
        
        private DiarioService UserDiarioService;

       public ICommand SalvarUpdateCommand { get; }

        public DiarioEdicaoViewModel(Postagem postagem)
        { 
           Postagem p = postagem;
            string token = Preferences.Get("UsuarioToken", string.Empty);
            UserDiarioService = new DiarioService(token);
            remainingCharacters = MaxCharacters;
           
            PublicationContent = p.PublicationContent;
            PublicationId = p.publicationId;



            SalvarUpdateCommand = new Command (async () => { await UpdatePostagem(p); });



        }




        private const int MaxCharacters = 260;
        private int remainingCharacters;
        private long publicationId;
        private string publicationContent = string.Empty;
        private DateTime modificationDate;
        private bool isPublic;
        private bool isPrivate;
        private TipoPostagemEnum tpPostagem;
        private int valorTipoPostagemSelecionado;

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
                if (publicationContent != value)
                {
                    publicationContent = value;
                    OnPropertyChanged();
                    UpdateRemainingCharacters();
                }
            }
        }
        public DateTime ModificationDate
        {
            get => modificationDate;
            set
            {
                modificationDate = value;
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

        private void UpdateRemainingCharacters()
        {
            RemainingCharacters = MaxCharacters - (PublicationContent?.Length ?? 0);
        }





        #endregion

        #region Métodos

        public async Task UpdatePostagem(Postagem postagem)
        {
            try
            {

                modificationDate = DateTime.Now;

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
                    Application.Current.MainPage = new AppShell();
                    return;
                }

                if (string.IsNullOrEmpty(publicationContent))
                {
                    await Application.Current.MainPage
                        .DisplayAlert("Mensagem", "Anotação não pode estar vazia", "Ok");
                    Application.Current.MainPage = new AppShell();
                    return; 
                   
                }

                Postagem pAtualizada = new Postagem()
                {
                    PublicationContent = this.PublicationContent,
                    publicationId = postagem.publicationId,
                    ModificationDate = modificationDate,
                    PublicationsRole = (TipoPostagemEnum)this.valorTipoPostagemSelecionado,
                };
                if (pAtualizada.publicationId != 0)
                    await UserDiarioService.PutPostagemAsync(pAtualizada);

                await Application.Current.MainPage
                       .DisplayAlert("Mensagem", "Postagem Atualizada com sucesso", "Ok");

                Application.Current.MainPage = new AppShell();


            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                      .DisplayAlert("Informação:", ex.Message + "\n" + ex.InnerException, "Ok");
            }

        }





        #endregion
    }
}

