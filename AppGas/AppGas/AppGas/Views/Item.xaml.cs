using AppGas.Dal;
using AppGas.Modelo;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppGas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Item : ContentPage
    {
        BotijaoPagPrincipal botijao = new BotijaoPagPrincipal();
        DalItemCarrinho dalItemCarrinho = new DalItemCarrinho();
        Cliente clienteLogado = new Cliente();

        public Item(BotijaoPagPrincipal item, Cliente cliente)
        {
            InitializeComponent();
            botijao = item;
            CarregaInformacoes(item);
            clienteLogado = cliente;
        }
        
        //===============================================================================================
        //CARREGA INFORMACOES DA PAGINA ANTERIOR
        public void CarregaInformacoes(BotijaoPagPrincipal item)
        {
            imgBotijao.Source = item.Imagem;
            lblDescricao.Text = item.Descricao;
            lblPreco.Text = Convert.ToString(item.Preco);
            lblQuantidade.Text = Convert.ToString(item.Quantidade);
        }


        //===============================================================================================
        //BOTOES DE CONTROLE (QUANTIDADE DE ITENS + e -)
        public void BtMais_Cliked(object sender, EventArgs e)
        {
            if (Convert.ToInt16(entQuantidade.Text) < 10)
            {
                entQuantidade.Text = Convert.ToString(Convert.ToInt16(entQuantidade.Text) + 1);
            }
        }

        public void BtMenos_Cliked(object sender, EventArgs e)
        {
            if (Convert.ToInt16(entQuantidade.Text) > 1)
            {
                entQuantidade.Text = Convert.ToString(Convert.ToInt16(entQuantidade.Text) - 1);
            }
        }


        //===============================================================================================
        //ADICIONAR ITEM NO CARRINHO
        public async void BtCarrinho_Cliked(object sender, EventArgs e)
        {
            if (clienteLogado.ID > 0)
            {

                var Status = await DisplayAlert("Deseja Inserir no Carrinho?", botijao.Descricao, "Confirmar", "Cancelar");

                if (Status.Equals(true))
                {
                    ItemCarrinho itemCarrinho = new ItemCarrinho();
                    itemCarrinho.Descricao = botijao.Descricao;
                    itemCarrinho.PrecoTotal = botijao.Preco * Convert.ToDouble(entQuantidade.Text);
                    itemCarrinho.PrecoItem = botijao.Preco;
                    itemCarrinho.Imagem = botijao.Imagem;
                    itemCarrinho.Quatidade = Convert.ToInt16(entQuantidade.Text);
                    itemCarrinho.ClienteID = clienteLogado.ID;
                    dalItemCarrinho.Add(itemCarrinho);
                }

            }
            else
            {
                await DisplayAlert("Para realizar esta operacao:", "Necessario fazer login ", "OK");
            }
        }
    }
}