using SQLite;

namespace AppGas.Modelo
{
    public class BotijaoPagPrincipal
    {
        [PrimaryKey, AutoIncrement]
        public long ID { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        public int Quantidade { get; set; }
        public int HorasCarga { get; set; }
        public string Imagem { get; set; }
    }
}
