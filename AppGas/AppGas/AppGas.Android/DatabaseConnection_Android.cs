using AppGas.Droid;
using AppGas.Infrastructure;
using SQLite;
using System;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection_Android))]

namespace AppGas.Droid
{
    public class DatabaseConnection_Android : IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {
            
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ProjGas.db");
            
            return new SQLiteConnection(path);
        }
    }
}