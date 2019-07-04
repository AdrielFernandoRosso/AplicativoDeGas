using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace AppGas.Modelo
{
    public class Pedido
    {
        [PrimaryKey, AutoIncrement]
        public long ID { get; set; }
        public string DescricaoProduto { get; set; }
        public double ValorTotal { get; set; }
        public double ValorUnitario { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataEntrega { get; set; }

        [ForeignKey(typeof(Cliente))]
        public long? ClienteID { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public Cliente Cliente { get; set; }
    }
}
