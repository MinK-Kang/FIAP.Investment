using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace FIAP.Repositories.Helpers
{
    public static class Configuration
    {
        public static void CreateSQLiteBase()
        {
            try
            {
                SQLiteConnection.CreateFile($@"{Environment.CurrentDirectory}\Investment.sqlite");
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
