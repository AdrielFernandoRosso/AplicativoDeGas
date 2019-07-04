using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace AppGas.Modelo
{
    public class ItemCarrinho
    {
        [PrimaryKey, AutoIncrement]
        public long ID { get; set; }
        public double PrecoItem { get; set; }
        public double PrecoTotal { get; set; }
        public int Quatidade { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }

        [ForeignKey(typeof(Cliente))]
        public long? ClienteID { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public Cliente Cliente { get; set; }


    }
}
