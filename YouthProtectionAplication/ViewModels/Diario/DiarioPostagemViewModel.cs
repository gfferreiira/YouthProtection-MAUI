using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouthProtectionAplication.Models;
using YouthProtectionAplication.Models.Enums;
using YouthProtectionAplication.Services.Diario;
using YouthProtectionAplication.Views.Diario;

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
        private string texto = string.Empty;
        private DateTime dataPostagem;

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



        private ObservableCollection<TipoPostagem> listaTiposPostagem;
        public ObservableCollection<TipoPostagem> ListaTiposPostagem
        {
            get { return listaTiposPostagem; }
            set
            {
                if (value != null)
                {
                    listaTiposPostagem = value;
                    OnPropertyChanged();
                }
            }
        }

        private TipoPostagem tipoPostagemSelecionado;

        public TipoPostagem TipoPostagemSelecionado
        {
            get { return tipoPostagemSelecionado; }
            set
            {
                if (value != null)
                {
                    tipoPostagemSelecionado = value;
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
                ListaTiposPostagem = new ObservableCollection<TipoPostagem>();
                listaTiposPostagem.Add(new TipoPostagem() { Id = 0, Descricao = "Público" });
                listaTiposPostagem.Add(new TipoPostagem() { Id = 1, Descricao = "Privado" });
                OnPropertyChanged(nameof(ListaTiposPostagem));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async Task SalvarPostagem()
        {
            DataPostagem = DateTime.Now;
            Postagem model = new Postagem()
            {
                Texto = this.Texto,
                DataPostagem = DataPostagem.ToString("dd/MM/yyyy HH:mm"),
                TipoPost = (TipoPostagemEnum)tipoPostagemSelecionado.Id,
                idUser = this.IdUser,
                idPostagem = this.IdPostagem
            };
            if (model.idPostagem == 0)
                await UserDiarioService.PostPostagemAsync(model);
            else
                await UserDiarioService.PutPostagemAsync(model);

            await Application.Current.MainPage
                    .DisplayAlert("Mensagem", "Postagem feita com sucesso", "Ok");

            Application.Current.MainPage = new DiarioViewUser();
        }



        #endregion








    }
}