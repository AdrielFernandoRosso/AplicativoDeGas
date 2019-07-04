using AppGas.Dal;
using AppGas.Modelo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppGas.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Pedidos : ContentPage
	{
        DalPedido dalPedido = new DalPedido();
        Pedido pedidos = new Pedido();
        Cliente clienteLogado = new Cliente();

        public Pedidos (Cliente cliente)
		{
			InitializeComponent ();
            ListaPedidos.ItemsSource = dalPedido.GetPedidoCliente(cliente);
            clienteLogado = cliente;

        }

        //================================================================================
        //PEGA ITEM SELECIONADO
        public void ItemSelecionado(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var selecionado = e.SelectedItem as Pedido;
                pedidos = selecionado;
                DeletarItemSelecionado(pedidos);
            }
        }
        
        //================================================================================
        //APAGA ITEM SELECIONADO
        public async void DeletarItemSelecionado(Pedido pedido)
        {
            var Status = await DisplayAlert("Cancelamento","Deseja cancelar o pedido","Sim","Nao");

            if (Status)
            {
                dalPedido.Delete(pedido.ID);
                await DisplayAlert("Cancelamento", "Cancelado com sucesso", "OK");
                ListaPedidos.ItemsSource = dalPedido.GetPedidoCliente(clienteLogado);
            }
        }
    }
}