using AppGas.Infrastructure;
using AppGas.Modelo;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace AppGas.Dal
{
    public class DalPedido
    {
        private SQLiteConnection sqlConnection;
        public DalPedido()
        {
            this.sqlConnection = DependencyService.Get<IDatabaseConnection>().DbConnection();
            this.sqlConnection.CreateTable<Pedido>();
        }

        public void Add(Pedido pedido)
        {
            sqlConnection.Insert(pedido);
        }

        public void Delete(long Id)
        {
            sqlConnection.Delete<Pedido>(Id);
        }

        public List<Pedido> GetPedidoCliente(Cliente cliente)
        {
            return sqlConnection.GetAllWithChildren<Pedido>(t=> t.ClienteID == cliente.ID);
        }

    }
}
