using AppGas.Infrastructure;
using AppGas.Modelo;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace AppGas.Dal
{
    public class DalPrincipalBotijao
    {
        private SQLiteConnection sqlConnection;

        public DalPrincipalBotijao()
        {
            this.sqlConnection = DependencyService.Get<IDatabaseConnection>().DbConnection();
            this.sqlConnection.CreateTable<BotijaoPagPrincipal>();
        }

        public void Add(BotijaoPagPrincipal botijaoPag)
        {
            sqlConnection.Insert(botijaoPag);
        }

        public void Update(BotijaoPagPrincipal botijaoPag)
        {
            sqlConnection.Update(botijaoPag);
        }
        
        public void Deletar(long id)
        {
            sqlConnection.Delete<BotijaoPagPrincipal>(id);
        }

        public List<BotijaoPagPrincipal> GetAllBotijao()
        {
            return sqlConnection.GetAllWithChildren<BotijaoPagPrincipal>().OrderBy(i => i.Descricao).ToList();
        }

        public void DeleteAll()
        {
            sqlConnection.DeleteAll<BotijaoPagPrincipal>();
        }


    }
}
