using AppGas.Dal;
using AppGas.Modelo;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppGas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Principal : MasterDetailPage
    {

        DalPrincipalBotijao dalPrincipalBotijao = new DalPrincipalBotijao();
        DalCarrgaTudoParaAplicacao dalUpload = new DalCarrgaTudoParaAplicacao();
        DalCadastroCliente dalCadastroCliente = new DalCadastroCliente();
        DalLogin dalLogin = new DalLogin();
        Cliente clienteLogado = new Cliente();

        public Principal()
        {
            InitializeComponent();
            LoginUsuarioAsync();
            dalUpload.UploadBotijao();
            dalUpload.UploadEstado();
            dalUpload.UploadCidade();
            ListaPagPrincipal.ItemsSource = dalPrincipalBotijao.GetAllBotijao();
        }
        
        //========================================================================================
        //cARREGA DADOS DO USUARIO JA LOGADO
        public async void LoginUsuarioAsync()
        {
            
            LoginTemporario loginTemporario = new LoginTemporario();

            lblUsuario.Text = "";

            //Jogando no modelo loginTemporario
            foreach (LoginTemporario loginTemporarioBanco in dalLogin.GetLogin())
            {
                loginTemporario.Usuario = loginTemporarioBanco.Usuario;
                loginTemporario.Senha = loginTemporarioBanco.Senha;
            }

            //Buscando no banco qual usuario e senha sao compativeis com o loginTemporario

            DalNotificacao dalNotificacao = new DalNotificacao();

            string DataTrocadeGas = string.Empty;

            foreach (Cliente clienteL in dalCadastroCliente.GetClietes())
            {
                if (clienteL.Usuario == loginTemporario.Usuario && clienteL.Senha == loginTemporario.Senha)
                {
                    clienteLogado = clienteL;
                    lblUsuario.Text = "Bem Vindo: " + clienteLogado.Usuario + "\nResidencia: " + clienteLogado.Cidade.Descricao +
                        "\nBairo: " + clienteLogado.Bairro + "\nNumero Residencia: " + clienteLogado.NumeroResidencia;
                    Logado(true);

                    //CARREGAR NOTIFICACAO DA PROXIMA TROCA DE GAS
                    foreach (MNotificacao notificacao in dalNotificacao.GetAllID(clienteLogado))
                    {
                        DataTrocadeGas = notificacao.CauculoData.ToString("dd/MM/yyyy"); break;
                    }
                    break;
                }
                else
                {
                    clienteLogado.ID = 0; 
                }
            }
            if (DataTrocadeGas != "")
            {
                await DisplayAlert("Notificacao", "Data para a proxima troca de gas sera: " + DataTrocadeGas, "OK");
            }
        }

        //===================================
        //BOTOES VISIVEIS 
        // true == logado
        // false == deslogado
        public void Logado(bool Status)
        {
            btCarrinho.IsVisible = Status;
            btEntrar.IsVisible = !Status;
            btCadastrar.IsVisible = !Status;
            btNotificacao.IsVisible = Status;
            btSair.IsVisible = Status;
            StkUsuario.IsVisible = Status;
            btConfiguracao.IsVisible = Status;
            btPedido.IsVisible = Status;
        }
        
        //===============================================================================================================
        //SELECIONANDO ITEM DA LIST DE BOTIJOES
        void SelecionaItem(object sender, SelectedItemChangedEventArgs e)
        {
            if(e.SelectedItem != null)
            {
                var selecionado = e.SelectedItem as BotijaoPagPrincipal;
                Detail.Navigation.PushAsync(new Item(selecionado, clienteLogado));
                IsPresented = false;
            }
        }
        
        //===============================================================================================================
        //BOTOES DE NAVEGACAO
        public void BotaoEntrar_Clicked(object sender, EventArgs e)
        {
            Detail.Navigation.PushAsync(new Entrar());
        }
        
        public void BotaoCadastrar_Cliked(object sender, EventArgs e)
        {
            Detail.Navigation.PushAsync(new Cadastrar());
        }

        public void BotaoCarrinho_Clicked(object sender, EventArgs e)
        {
            Detail.Navigation.PushAsync(new Carrinho(clienteLogado));
        }
        
        public void BotaoNotificacao_Clicked(object sender, EventArgs e)
        {
            Detail.Navigation.PushAsync(new Notificacao(clienteLogado));
        }

        public void BotaoPedido_Clicked(object sender, EventArgs e)
        {
            Detail.Navigation.PushAsync(new Pedidos(clienteLogado));
        }

        public void BotaoConfiguracao_Clicked(object sender, EventArgs e)
        {
            Detail.Navigation.PushAsync(new ConfiguracaoUsuario(clienteLogado));
        }

        public void BtAtualizar_Clicked(object sender, EventArgs e)
        {
            LoginUsuarioAsync();
        }

        public void BotaoSair_Clicked(object sender, EventArgs e)
        {
            dalLogin.Delete();
            Logado(false);
            Navigation.PopToRootAsync();
            LoginUsuarioAsync();
        }
    }
}