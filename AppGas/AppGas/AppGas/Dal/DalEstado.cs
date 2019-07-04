using AppGas.Infrastructure;
using AppGas.Modelo;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace AppGas.Dal
{
    public class DalEstado
    {

        private SQLiteConnection sqlConnection;

        public DalEstado()
        {
            this.sqlConnection = DependencyService.Get<IDatabaseConnection>().DbConnection();
            this.sqlConnection.CreateTable<Estado>();
        }

        public void Add(Estado estado)
        {
                sqlConnection.Insert(estado);
        }


        public List<Estado> GetEstado()
        {
            return sqlConnection.GetAllWithChildren<Estado>();
        }

        public void DeleteAll()
        {
            sqlConnection.DeleteAll<Estado>();
        }

    }
}
