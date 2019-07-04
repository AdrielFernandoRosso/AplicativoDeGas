using SQLite;

namespace AppGas.Infrastructure
{
    public interface IDatabaseConnection
    {
        SQLiteConnection DbConnection();
    }
}
