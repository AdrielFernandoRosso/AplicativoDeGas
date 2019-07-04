using AppGas.Dal;
using AppGas.Modelo;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppGas.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConfiguracaoUsuario : ContentPage
	{
        Cliente clienteLogado = new Cliente();
        LoginTemporario Login = new LoginTemporario();
        DalEstado dalEstado = new DalEstado();
        DalCidade dalCidade = new DalCidade();
        DalLogin dalLogin = new DalLogin();
        DalCadastroCliente dalCadastroCliente = new DalCadastroCliente();
        

        public ConfiguracaoUsuario (Cliente cliente)
		{
			InitializeComponent ();
            clienteLogado = cliente;
            CarregaEstados();
            AtualizaENTR();
        }
        
        //================================================================================================
        //CARREGA ESTADOS
        public void CarregaEstados()
        {
            foreach (Estado item in dalEstado.GetEstado())
            {
                PikerEstado.Items.Add(item.UK);
            }
        }


        //ESTADO SELECIONADO
        long IdEstado = 0;
        private void SelecionaItemEstado_Cliked(object sender, EventArgs e)
        {
            PikerCidade.Items.Clear();

            var UKEstado = PikerEstado.Items[PikerEstado.SelectedIndex];

            foreach (Estado item in dalEstado.GetEstado())
            {
                if (item.UK == UKEstado)
                {
                    IdEstado = item.ID;
                    //CARREGA CIDADE
                    foreach (Cidade itemC in dalCidade.GetCidade(IdEstado))
                    {
                        PikerCidade.Items.Add(itemC.Descricao);
                    }
                }
            }
        }


        //CIDADE SELECIONADA
        long CidadeId = 0;
        private void SelecionaItemCidade_Cliked(object sender, EventArgs e)
        {
            try
            {
                var DescCidade = PikerCidade.Items[PikerCidade.SelectedIndex];

                foreach (Cidade item in dalCidade.GetCidade())
                {
                    if (DescCidade == item.Descricao)
                    {
                        var IdCidade = item.ID;
                        CidadeId = IdCidade;
                    }
                }
            }
            catch (Exception) { }
        }

        //================================================================================================
        //INSERIR DO BANCO DADOS NO ENTRYS
        public void AtualizaENTR()
        {
            try
            {
                EntNome.Text = clienteLogado.Usuario;
                EntCPF.Text = clienteLogado.CPF;
                EntTelefone.Text = clienteLogado.Telefone;
                EntComplemento.Text = clienteLogado.Complemento;
                EntBairro.Text = clienteLogado.Bairro;
                EntNumResidencia.Text = clienteLogado.NumeroResidencia;

            }
            catch (Exception)
            { 
            }
        }


        //================================================================================================
        //ALTERAR DADOS DO USUARIO
        public async void BtEditar_Clicked(object sender, EventArgs e)
        {
            clienteLogado.Usuario = EntNome.Text;
            clienteLogado.CPF = EntCPF.Text;
            clienteLogado.Telefone = EntTelefone.Text;
            clienteLogado.Complemento = EntComplemento.Text;
            clienteLogado.Bairro = EntBairro.Text;
            clienteLogado.NumeroResidencia = EntNumResidencia.Text;
            clienteLogado.CidadeID = CidadeId;
            Login.Usuario = EntNome.Text;
            Login.Senha = EntSenha.Text;

            //VERIFICA SENHAS SAO IGUAIS
            if (EntSenha.Text == EntConfirmarSenha.Text)
            {
                clienteLogado.Senha = EntSenha.Text;

                //VERIFICA SE NAO HA CAMPOS EM BRANCO
                if (EntNome.Text != "" && EntTelefone.Text != "" && EntCPF.Text != "" && CidadeId != 0 &&
                    clienteLogado.Bairro != "")
                {
                   var Status = await DisplayAlert("Alterar", "Deseja alterar os dados?", "Confirmar", "Cancelar");

                    //FAZ O UPDATE
                    if (Status)
                    {
                        dalLogin.Delete();
                        dalLogin.Add(Login);
                        dalCadastroCliente.Alterar(clienteLogado);
                        await DisplayAlert("Sucesso", "Cadastro Alterado", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Nao foi possivel alterar", "Campos em branco", "OK");
                }
            }
            else
            {
                await DisplayAlert("Falha", "Senhas Diferentes", "OK");
            }
        }
    }
}