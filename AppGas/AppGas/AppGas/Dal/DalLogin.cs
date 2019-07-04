using AppGas.Infrastructure;
using AppGas.Modelo;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace AppGas.Dal
{
    public class DalLogin
    {
        private SQLiteConnection sqlConnection;

        public DalLogin()
        {
            this.sqlConnection = DependencyService.Get<IDatabaseConnection>().DbConnection();
            this.sqlConnection.CreateTable<LoginTemporario>();
        }

        public void Add(LoginTemporario log)
        {
            sqlConnection.Insert(log);
        }

        public void Delete()
        {
            sqlConnection.DeleteAll<LoginTemporario>();
        }

        public List<LoginTemporario> GetLogin()
        {
            return sqlConnection.GetAllWithChildren<LoginTemporario>();
        }

    }
}
