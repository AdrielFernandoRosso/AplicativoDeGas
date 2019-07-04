using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;


namespace AppGas.Modelo
{
    public class Estado
    {
        [PrimaryKey, AutoIncrement]
        public long ID { get; set; }
        public string UK { get; set; }
        public string Descricao { get; set; }

        [OneToMany]
        public List<Cidade> Cidade { get; set; }
    }
}
