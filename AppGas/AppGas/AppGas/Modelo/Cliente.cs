using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace AppGas.Modelo
{
    public class Cliente
    {
        [PrimaryKey, AutoIncrement]
        public long ID { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Endereco  { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string NumeroResidencia { get; set; }

        [ForeignKey(typeof(Cidade))]
        public long? CidadeID { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public Cidade Cidade { get; set; }
    }
}
