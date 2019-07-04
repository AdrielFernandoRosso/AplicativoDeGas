using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace AppGas.Modelo
{
    public class Cidade
    {
        [PrimaryKey, AutoIncrement]
        public long ID { get; set; }
        public string Descricao { get; set; }

        [ForeignKey(typeof(Estado))]
        public long? EstadoID { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public Estado Estado { get; set; }

        [OneToMany]
        public List<Cliente> Cliente { get; set; }
    }
}
