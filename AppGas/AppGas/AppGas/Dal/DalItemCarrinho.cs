using AppGas.Infrastructure;
using AppGas.Modelo;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace AppGas.Dal
{
    public class DalItemCarrinho
    {
        private SQLiteConnection sqlConnection;

        string mensagem = "Nao foi possivel colocar no carrinho tente novamente";

        public DalItemCarrinho()
        {
            this.sqlConnection = DependencyService.Get<IDatabaseConnection>().DbConnection();
            this.sqlConnection.CreateTable<ItemCarrinho>();
        }

        public string Add(ItemCarrinho itemCarrinho)
        {
            try
            {
                sqlConnection.Insert(itemCarrinho);
                return mensagem = "Adicionado ao carrinho com sucesso";
            }
            catch (System.Exception)
            {
                return mensagem;
            }
        }

        public void Deletar(long item)
        {
            sqlConnection.Delete<ItemCarrinho>(item);

        }


        public void Alterar(ItemCarrinho itemCarrinho)
        {
            sqlConnection.Update(itemCarrinho);
        }


        public IEnumerable<ItemCarrinho> GetAllWithChildren()
        {
            return sqlConnection.GetAllWithChildren<ItemCarrinho>().OrderBy(i => i.Descricao).ToList();
        }
        

        public List<ItemCarrinho> GetItensPorUsuario(Cliente clienteID)
        {
            try
            {
                return sqlConnection.GetAllWithChildren<ItemCarrinho>(it => it.ClienteID == clienteID.ID).OrderBy(i => i.Descricao).ToList();
            }
            catch (System.Exception)
            {
                
                List<ItemCarrinho> Null = new List<ItemCarrinho>();
                return Null;
            }
            
        }

        public void DeleteAll()
        {
            sqlConnection.DeleteAll<ItemCarrinho>();
        }


        //public double getTotal(ArrayList<ItemCarrinho> list)
        //{

        //    double total = 0.0;
        //    for (int i = 0; i < list.size(); i++)
        //    {
        //        total = total + Double.parseDouble(list.get(i).getAmount());
        //    }
        //    return total;
        //}


    }
}
