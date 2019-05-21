using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using App7.Sqlite;
using SQLite;

namespace App7.Droid
{
    public class LocalFilePath: ILocalFileHelper
    {
        public SQLiteConnection DbConnection()
        {

            var DbName = "Report.db3";
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), DbName);
            return new SQLiteConnection(path);
        }
    }
}