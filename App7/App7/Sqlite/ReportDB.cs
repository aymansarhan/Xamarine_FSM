using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App7.Model;
using App7.Services;
using SQLite;
using App7.Helpers;

namespace App7.Sqlite
{
    public class ReportDB
    {
        List<Report> Reports;
        private SQLiteConnection database;
        private static object collisionLoc = new object();
        APIServices api = new APIServices();

        public ReportDB(string dbPath)
        {
            database = new SQLiteConnection(dbPath);
            database.CreateTable<Report>();
            this.Reports = new List<Report>(database.Table<Report>());
        }

        public async void GetReports()
        {
            List<Report> replost = new List<Report>();
            replost= database.Table<Report>().ToList();
            await api.ReportList(replost, Settings.accessToken);
            DeleteAll();
        }

        public int SaveReports(Report reports)
        {
            lock (collisionLoc)
            {
                if (reports.Id != 0)
                {
                    database.Update(reports);
                    return reports.Id;
                }
                else
                {
                    database.Insert(reports);
                    return reports.Id;
                }
            }
        }
        
        public void DeleteAll()
        {
            lock (collisionLoc)
            {
                database.DropTable<Report>();
                database.CreateTable<Report>();
            }
            this.Reports = null;
            this.Reports = new List<Report>(database.Table<Report>());
        }
    }
}