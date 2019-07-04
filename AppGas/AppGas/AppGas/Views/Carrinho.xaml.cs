using AppGas.Dal;
using AppGas.Modelo;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppGas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Carrinho : ContentPage
    {
        DalItemCarrinho dalItemCarrinho = new DalItemCarrinho();
        ItemCarrinho ItemCarrinho = new ItemCarrinho();
        Cliente clienteLogado = new Cliente();

        public Carrinho(Cliente cliente)
        {
            InitializeComponent();
            clienteLogado = cliente;
            ListaCarrinho.ItemsSource = dalItemCarrinho.GetItensPorUsuario(clienteLogado);
            lblValorPedido.Text = Convert.ToString(pegarValorTotalPreco());

        }


        //=======================================================================
        //METODO PARA CALCULAR O TOTAL DE TODOS OS ITEM QUE TEM NO CARRINHO
        public double pegarValorTotalPreco()
        {
            double PrecoCarrinhoTotal = 0;
            foreach (ItemCarrinho itensCarrinho in dalItemCarrinho.GetItensPorUsuario(clienteLogado))
            {
                PrecoCarrinhoTotal += itensCarrinho.PrecoTotal;
            }

            if (PrecoCarrinhoTotal <= 0)
            {
                btFinalizarPedido.IsVisible = false;
            }
            else
            {
                btFinalizarPedido.IsVisible = true;
            }
            return PrecoCarrinhoTotal;
        }

        //=======================================================================
        //SELECIONA UM ITEM NO LIST VIEW DO CARRINHO E JOGA PARA UMA NOVA LIST
        //PARA USAR OS DADOS EM OUTROS METODOS.
        //ABRE MENU
        public void ItemSelecionado(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var itemSelecionado = e.SelectedItem as ItemCarrinho;
                ItemCarrinho = itemSelecionado;
                lblDescricaoProduto.Text = itemSelecionado.Descricao;
                entQuantidade.Text = Convert.ToString(itemSelecionado.Quatidade);
                StatusMenu(true);
            }
        }

        //FECHAR MENU
        public void BtFecharJanela_Cliked(object sender, EventArgs e)
        {
            StatusMenu(false);
        }

        //=======================================================================
        //STATUS MENU
        public void StatusMenu(bool Status)
        {
            GridMenu.IsVisible = Status;
            StakDescr.IsVisible = Status;
        }
        
        //=======================================================================
        //MENU EXCLUIR/ALTERAR ITEM NO CARRINHO

        //EXCLUIR
        public async void BtExcluir_Cliked(object sender, EventArgs e)
        {
            bool Selecionado = await DisplayAlert("Deseja excluir?",ItemCarrinho.Descricao,"Sim","Nao");

            if (Selecionado)
            {
                dalItemCarrinho.Deletar(ItemCarrinho.ID);
                lblValorPedido.Text = Convert.ToString(pegarValorTotalPreco());
                ListaCarrinho.ItemsSource = dalItemCarrinho.GetItensPorUsuario(clienteLogado);
                StatusMenu(false);
                await DisplayAlert("Excluido com sucesso", ItemCarrinho.Descricao, "OK");
            }
        }

        //ALTERAR +1 ITEM
        public void BtMais_Cliked(object sender, EventArgs e)
        {
            if (ItemCarrinho.Quatidade < 10)
            {
                ItemCarrinho.Quatidade = ItemCarrinho.Quatidade + 1;
                ItemCarrinho.PrecoTotal = ItemCarrinho.Quatidade * ItemCarrinho.PrecoItem;
                dalItemCarrinho.Alterar(ItemCarrinho);
                entQuantidade.Text = Convert.ToString(ItemCarrinho.Quatidade);
                ListaCarrinho.ItemsSource = dalItemCarrinho.GetItensPorUsuario(clienteLogado);
                lblValorPedido.Text = Convert.ToString(pegarValorTotalPreco());
            }
        }

        //ALTERAR -1 ITEM
        public void BtMenos_Cliked(object sender, EventArgs e)
        {
            if (ItemCarrinho.Quatidade > 1)
            {
                ItemCarrinho.Quatidade = ItemCarrinho.Quatidade - 1;
                ItemCarrinho.PrecoTotal = ItemCarrinho.Quatidade * ItemCarrinho.PrecoItem;
                dalItemCarrinho.Alterar(ItemCarrinho);
                entQuantidade.Text = Convert.ToString(ItemCarrinho.Quatidade);
                ListaCarrinho.ItemsSource = dalItemCarrinho.GetItensPorUsuario(clienteLogado);
                lblValorPedido.Text = Convert.ToString(pegarValorTotalPreco());
            }
        }

        //=======================================================================
        //BOTAO FAZER PEDIDO 
        //PEDIDO RECEBE ITEM CARRINHO 
        //CRIA PEDIDO E EXCLUI ITEM DO CARRINHO
        public async void BtFZPedido_Cliked(object sender, EventArgs e)
        {
            var Status = await DisplayAlert("Pedido", "Deseja fazer o pedido?", "Confirmar", "Cancelar");

            if (Status)
            {

                DalPedido dalPedido = new DalPedido();
                Pedido pedidosInserirBanco = new Pedido();
                string DataEntrega = string.Empty;

                foreach (ItemCarrinho itemCarrinho in dalItemCarrinho.GetItensPorUsuario(clienteLogado))
                {
                    pedidosInserirBanco.ValorTotal = itemCarrinho.PrecoTotal;
                    pedidosInserirBanco.Quantidade = itemCarrinho.Quatidade;
                    pedidosInserirBanco.DescricaoProduto = itemCarrinho.Descricao;
                    pedidosInserirBanco.ValorUnitario = itemCarrinho.PrecoItem;
                    pedidosInserirBanco.ClienteID = clienteLogado.ID;
                    pedidosInserirBanco.DataEntrega = DateTime.Now.AddDays(2);
                    dalPedido.Add(pedidosInserirBanco);
                    dalItemCarrinho.Deletar(itemCarrinho.ID);
                }
                
                await DisplayAlert("Pedido", "Realizado com sucesso", "OK");
                lblValorPedido.Text = "0";
                ListaCarrinho.ItemsSource = dalItemCarrinho.GetItensPorUsuario(clienteLogado);
            }
        }
    }
}