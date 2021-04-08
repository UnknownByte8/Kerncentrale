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
            
        }

        public async static void CreateDB()
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync("PowerPlantDatabase.db", CreationCollisionOption.OpenIfExists);
            string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "PowerPlantDatabase.db");
            using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
            {
                try
                {
                    con.Open();
                    string DropPowerPlantInfo = "DROP TABLE PowerPlantData;";
                    string PowerPlantInfo = "CREATE TABLE IF NOT EXISTS PowerPlantData (RodNumber NVARCHAR(25), FuelType NVARCHAR(25), Temparature NVARCHAR(25), Water NVARCHAR(25), Generated NVARCHAR(25));";
                    string HighScoreInfo = "CREATE TABLE IF NOT EXISTS HighScoreData (GameNumber NVARCHAR(25), WaterUsed NVARCHAR(25), WattGenerated NVARCHAR(25), PointsScored NVARCHAR(25));";
                    string initCMD = DropPowerPlantInfo + PowerPlantInfo + HighScoreInfo;
                      
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

        public static void UpdateCurrentGame(String FuelType, String Temparature, String Water, String Generated)
        {
            if (!FuelType.Equals("") && !Temparature.Equals("") && !Water.Equals("") && !Generated.Equals(""))
            {
                string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "PowerPlantDatabase.db");
                using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
                {
                    try
                    {
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
            string DBPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "PowerPlantDatabase.db");
            using (SqliteConnection con = new SqliteConnection($"Filename={DBPath}"))
            {
                con.Open();
                String selectCmd = "SELECT FuelType, Temparature, Water, Generated FROM PowerPlantData";
                SqliteCommand cmd_getData = new SqliteCommand(selectCmd, con);
                SqliteDataReader reader = cmd_getData.ExecuteReader();
                while (reader.Read())
                {
                    powerPlantList.Add(new DBinfo(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
                }
                con.Close();
            }
            return powerPlantList;
        }
        public class ScoreInfo
        {
            public String GameNumber { get; set; }
            public String WaterUsed { get; set; }
            public String WattGenerated { get; set; }
            public String PointsScored { get; set; }
            public ScoreInfo(String GameNumber1, String WaterUsed1, String WattGenerated1, String PointsScored1)
            {
                GameNumber = GameNumber1;
                WaterUsed = WaterUsed1;
                WattGenerated = WattGenerated1;
                PointsScored = PointsScored1;
            }
        }
        public static List<ScoreInfo> GetHighscore()
        {
            List<ScoreInfo> ScoreList = new List<ScoreInfo>();
            string DBPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "PowerPlantDatabase.db");
            using (SqliteConnection con = new SqliteConnection($"Filename={DBPath}"))
            {
                con.Open();
                String selectCmd = "SELECT GameNumber, WaterUsed, WattGenerated, PointsScored FROM HighScoreData ORDER BY PointsScored ASC";
                SqliteCommand cmd_getData = new SqliteCommand(selectCmd, con);
                SqliteDataReader reader = cmd_getData.ExecuteReader();
                while (reader.Read())
                {
                    ScoreList.Add(new ScoreInfo(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
                }
                con.Close();
            }
            return ScoreList;
        }
        public static void UpdateHighScore(String GameNumber, String WaterUsed, String WattGenerated, String PointsScored)
        {
            if (!GameNumber.Equals("") && !WaterUsed.Equals("") && !WattGenerated.Equals("") && !PointsScored.Equals(""))
            {
                string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "PowerPlantDatabase.db");
                using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
                {
                    try
                    {
                        con.Open();
                        SqliteCommand CMD_Insert = new SqliteCommand();
                        CMD_Insert.Connection = con;
                        CMD_Insert.CommandText = "INSERT INTO [HighScoreData] (GameNumber, WaterUsed, WattGenerated, PointsScored) VALUES (@GameNumber, @WaterUsed, @WattGenerated, @PointsScored)";
                        CMD_Insert.Parameters.AddWithValue("GameNumber", GameNumber);
                        CMD_Insert.Parameters.AddWithValue("WaterUsed", WaterUsed);
                        CMD_Insert.Parameters.AddWithValue("WattGenerated", WattGenerated);
                        CMD_Insert.Parameters.AddWithValue("PointsScored", PointsScored);
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

    }
}