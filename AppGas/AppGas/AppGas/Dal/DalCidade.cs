using AppGas.Infrastructure;
using AppGas.Modelo;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace AppGas.Dal
{
    public class DalCidade
    {
        private SQLiteConnection sqlConnection;

        public DalCidade()
        {
            this.sqlConnection = DependencyService.Get<IDatabaseConnection>().DbConnection();
            this.sqlConnection.CreateTable<Cidade>();
        }


        public void Add(Cidade cidade)
        {
            sqlConnection.Insert(cidade);
        }

        public List<Cidade> GetCidade(long IdEstado)
        {
            List<Cidade> cidade = new List<Cidade>();
            List<Cidade> cidadeEstado = new List<Cidade>();
            cidadeEstado.Clear();
            cidade.Clear();

            cidade = sqlConnection.GetAllWithChildren<Cidade>(it => it.EstadoID == IdEstado).OrderBy(i => i.Descricao).ToList();
            
            foreach (Cidade item in cidade)
            {
                if (item.EstadoID == IdEstado)
                {
                    cidadeEstado.Add(item);
                }
            }
            return cidadeEstado;
        }

        public List<Cidade> GetCidade()
        {
            return sqlConnection.GetAllWithChildren<Cidade>();
        }

        public void DeleteAll()
        {
            sqlConnection.DeleteAll<Cidade>();
        }

    }
}
