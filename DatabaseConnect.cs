using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Windows.Storage;
using System.Threading;

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
            await ApplicationData.Current.LocalFolder.CreateFileAsync("PowerPlantData.db", CreationCollisionOption.OpenIfExists);
            string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "PowerPlantData.db");
            using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
            {
                try { 
                con.Open();
                string initCMD = "CREATE TABLE IF NOT EXISTS " +
                    "PowerPlantData (" +
                        "RodNumber NVARCHAR(25) INTEGER PRIMARY KEY" +
                        "FuelType NVARCHAR(25)," +
                        "Temparature NVARCHAR(25)," +
                        "Water NVARCHAR(25)," +
                        "Generated NVARCHAR(25));";
                SqliteCommand CMDcreateTable = new SqliteCommand(initCMD, con);
                CMDcreateTable.ExecuteReader();
                con.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public static void InitRod(String FuelType, String Temparature, String Water, String Generated)
        {
            if (!FuelType.Equals("") && !Temparature.Equals("") && !Water.Equals("") && !Generated.Equals(""))
            {
                string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "PowerPlantData.db");
                using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
                {
                    try {
                        con.Open();
                        SqliteCommand CMD_Insert = new SqliteCommand();
                        CMD_Insert.Connection = con;
                        CMD_Insert.CommandText = "INSERT INTO [PowerPlantData] (FuelType, Temparature, Water, Generated) VALUES (@FuelType, @Temparature, @Water, @Generated)";
                        CMD_Insert.Parameters.AddWithValue("FuelType", FuelType);
                        CMD_Insert.Parameters.AddWithValue("Temparature", Temparature);
                        CMD_Insert.Parameters.AddWithValue("Water", Water);
                        CMD_Insert.Parameters.AddWithValue("Generated", Generated);
                        CMD_Insert.ExecuteReader();
                        con.Close();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }
        public class DBinfo
        {
            public String FuelType { get; set; }
            public String Temparature { get; set; }
            public String Water { get; set; }
            public String Generated { get; set; }
            public DBinfo(String FuelType1, String Temparature1, String Water1, String Generated1)
            {
                FuelType = FuelType1;
                Temparature = Temparature1;
                Water = Water1;
                Generated = Generated1;
            }
        }
        public static List<DBinfo> GetRecords()
        {
            List<DBinfo> powerPlantList = new List<DBinfo>();
            string DBPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "PowerPlantData.db");
            using (SqliteConnection con = new SqliteConnection($"Filename={DBPath}"))
            {
                con.Open();
                String selectCmd = "SELECT FuelType, Temparature, Water, Generated FROM PowerPlantData";
                SqliteCommand cmd_getData = new SqliteCommand(selectCmd,con);
                SqliteDataReader reader = cmd_getData.ExecuteReader();
                while (reader.Read())
                {
                    powerPlantList.Add(new DBinfo(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
                }
                con.Close();
            }
            return powerPlantList;
        }
    }
}