using AppGas.Dal;
using AppGas.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppGas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cadastrar : ContentPage
    {
        DalEstado dalEstado = new DalEstado();
        DalCidade dalCidade = new DalCidade();
        DalCadastroCliente dalCadastroCliente = new DalCadastroCliente();



        public Cadastrar()
        {
            InitializeComponent();

            foreach (Estado estadoNoBanco in dalEstado.GetEstado())
            {
                PikerEstado.Items.Add(estadoNoBanco.UK);
            }
        }

        //===================================================================================================
        //SELECIONA ESTADO
        long IdEstado = 0;
        private void SelecionaItemEstado_Cliked(object sender, EventArgs e)
        {
            PikerCidade.Items.Clear();

            var UKEstado = PikerEstado.Items[PikerEstado.SelectedIndex];

            foreach (Estado estadoNaList in dalEstado.GetEstado())
            {
                if (estadoNaList.UK == UKEstado)
                {
                    IdEstado = estadoNaList.ID;
                    //CARREGA CIDADE
                    foreach (Cidade cidadeNoBanco in dalCidade.GetCidade(IdEstado))
                    {
                        PikerCidade.Items.Add(cidadeNoBanco.Descricao);
                    }
                }
            }
        }

        //SELECIONA CIDADE
        long CidadeId = 0;
        private void SelecionaItemCidade_Cliked(object sender, EventArgs e)
        {
            try
            {
                var DescCidade = PikerCidade.Items[PikerCidade.SelectedIndex];

                foreach (Cidade cidadeNaList in dalCidade.GetCidade())
                {
                    if (DescCidade == cidadeNaList.Descricao)
                    {
                        var IdCidade = cidadeNaList.ID;
                        CidadeId = IdCidade;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        //===================================================================================================
        //BOTAO CADASTRAR CLIENTE
        private void Cadastrar_Clicked(object sender, EventArgs e)
        {
            Cliente newCliente = new Cliente();

            newCliente.Usuario = EntNome.Text;
            newCliente.CPF = EntCPF.Text;
            newCliente.Telefone = EntTelefone.Text;
            newCliente.Complemento = EntComplemento.Text;
            newCliente.Bairro = EntBairro.Text;
            newCliente.NumeroResidencia = EntNumResidencia.Text;
            newCliente.CidadeID = CidadeId;

            //SENHAS SAO IGUAIS
            if (EntSenha.Text == EntConfirmarSenha.Text)
            {
                newCliente.Senha = EntSenha.Text;

                //NAO HA CAMPOS EM BRANCO
                if (EntNome.Text != "" && EntTelefone.Text != "" && EntCPF.Text != "" && CidadeId != 0 &&
                    newCliente.Bairro != "")
                {
                    dalCadastroCliente.Add(newCliente);
                    DisplayAlert("Sucesso", "Cadastro Realizado", "OK");
                    Navigation.PopToRootAsync();
                }
                else
                {
                    DisplayAlert("Nao foi possivel cadastrar", "Campos em branco", "OK");
                }
            }
            else
            {
                DisplayAlert("Falha", "Senhas Diferentes", "OK");
            }
        }
    }
}