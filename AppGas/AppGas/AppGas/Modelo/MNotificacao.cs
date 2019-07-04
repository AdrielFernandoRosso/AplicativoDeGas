using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace AppGas.Modelo
{
    public class MNotificacao
    {
        [PrimaryKey, AutoIncrement]
        public long ID { get; set; }
        public int MediaHoras { get; set; }
        public int QuantidadeBocas { get; set; }
        public DateTime CauculoData { get; set; }


        [ForeignKey(typeof(Cliente))]
        public long? ClienteID { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public Cliente Cliente { get; set; }
    }
}
