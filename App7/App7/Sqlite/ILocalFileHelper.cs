using System;
using System.Collections.Generic;
using System.Text;

namespace App7.Sqlite
{
    public interface ILocalFileHelper
    {
        SQLite.SQLiteConnection DbConnection();
    }
}
