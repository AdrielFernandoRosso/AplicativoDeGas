using AppGas.Infrastructure;
using AppGas.Modelo;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace AppGas.Dal
{
    public class DalNotificacao
    {
        private SQLiteConnection sqlConnection;

        public DalNotificacao()
        {
            this.sqlConnection = DependencyService.Get<IDatabaseConnection>().DbConnection();
            this.sqlConnection.CreateTable<MNotificacao>();
        }


        public void Add(MNotificacao notificacao)
        {
            sqlConnection.Insert(notificacao);
        }

        public void Atualizar(MNotificacao notificacao)
        {
            sqlConnection.Update(notificacao);
        }
        public void Delete(long Id)
        {
            sqlConnection.Delete<MNotificacao>(Id);
        }
        public void DeleteAll()
        {
            sqlConnection.DeleteAll<MNotificacao>();
        }

        public List<MNotificacao> GetAllID(Cliente cliente)
        {
            return sqlConnection.GetAllWithChildren<MNotificacao>(t=> t.ClienteID == cliente.ID);
        }
        
    }
}
