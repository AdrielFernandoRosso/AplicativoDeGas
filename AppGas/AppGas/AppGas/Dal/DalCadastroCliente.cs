using AppGas.Infrastructure;
using AppGas.Modelo;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace AppGas.Dal
{
    class DalCadastroCliente
    {
        private SQLiteConnection sqlConnection;

        public DalCadastroCliente()
        {
            this.sqlConnection = DependencyService.Get<IDatabaseConnection>().DbConnection();
            this.sqlConnection.CreateTable<Cliente>();
        }

        public void Add(Cliente cliente)
        {
            sqlConnection.Insert(cliente);
        }

        public void Alterar(Cliente cliente)
        {
            sqlConnection.Update(cliente);

        }

        public List<Cliente> GetClietes()
        {
            return sqlConnection.GetAllWithChildren<Cliente>().OrderBy(i => i.Usuario).ToList();
        }

        public void Delete (long id)
        {
            sqlConnection.Delete<Cliente>(id);
        }
        public void DeleteAll()
        {
            sqlConnection.DeleteAll<Cliente>();
        }

    }
}
