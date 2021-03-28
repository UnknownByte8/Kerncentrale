using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Windows.Storage;

namespace Kerncentrale
{
    class DatabaseConnect
    {
        public DatabaseConnect()
        {
            CreateDB();
        }

        public async static void CreateDB()
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync("data.db", CreationCollisionOption.OpenIfExists);
            string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "data.db");
            using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
            { 
                con.Open();
                string initCMD = "CREATE TABLE IF NOT EXISTS " +
                    "PowerPlantData (RodNumber NVARCHAR(100) PRIMARY KEY," +
                        "FuelType NVARCHAR(25)" +
                        "Temparature NVARCHAR(25)" +
                        "Water NVARCHAR(25)" +
                        "Generated NVARCHAR(25)";
                SqliteCommand CMDcreateTable = new SqliteCommand(initCMD, con);
                CMDcreateTable.ExecuteReader();
                con.Close();
            }
        }

        public static void InitRod(String FuelType)
        {
            if (!FuelType.Equals(""))
            {
                string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "data.db");

                using(SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
                {
                    con.Open();
                    SqliteCommand CMD_Insert = new SqliteCommand();
                    CMD_Insert.Connection = con;
                    CMD_Insert.CommandText = "INSERT INTO PowerPlantData VALUES (@FuelType)";
                    CMD_Insert.Parameters.AddWithValue("@FuelType",FuelType);
                    CMD_Insert.ExecuteReader();
                    con.Close();
                }
            }
        }
        public class DBinfo
        {
            public String RodNumber { get; set; }
            public String FuelType { get; set; }
            public String Temparature { get; set; }
            public String Water { get; set; }
            public String Generated { get; set; }
            public DBinfo(String RodNumber1, String FuelType1, String Temparature1, String Water1, String Generated1)
            {
                RodNumber = RodNumber1;
                FuelType = FuelType1;
                Temparature = Temparature1;
                Water = Water1;
                Generated = Generated1;

            }
        }
        public static List<DBinfo> GetRecords()
        {
            List<DBinfo> powerPlantList = new List<DBinfo>();
            string DBPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "data.db");

            using (SqliteConnection con = new SqliteConnection($"Filename={DBPath}"))
            {
                con.Open();

                String selectCmd = "SELECT RodNumber, FuelType, Temparature, Water, Generated FROM PowerPlantData";
                SqliteCommand cmd_getData = new SqliteCommand(selectCmd,con);

                SqliteDataReader reader = cmd_getData.ExecuteReader();

                while (reader.Read())
                {
                    powerPlantList.Add(new DBinfo(reader.GetString(0), reader.GetString(1), reader.GetString(2),
                        reader.GetString(3), reader.GetString(4)));
                }
                con.Close();
            }
            return powerPlantList;
        }
    }
}
