using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using Windows.Storage;

namespace Kerncentrale
{
    class DatabaseConnect
    {
        public DatabaseConnect()
        {

        }

        /// <summary>
        /// init for the database
        /// </summary>
        public async static void CreateDB()
        {
            //file location for the DB
            await ApplicationData.Current.LocalFolder.CreateFileAsync("PowerPlantDatabase.db", CreationCollisionOption.OpenIfExists);
            string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "PowerPlantDatabase.db");
            using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
            {
                try
                {
                    con.Open();
                    //resets the powerplantdata table
                    //string DropPowerPlantInfo = "DROP TABLE [PowerPlantData];";
                    //creates the powerplantdata table with its columns
                    string PowerPlantInfo = "CREATE TABLE IF NOT EXISTS [PowerPlantData] (RodNumber NVARCHAR(25), FuelType NVARCHAR(25), Temparature NVARCHAR(25), Water NVARCHAR(25), Generated NVARCHAR(25));";
                    //creates the highscoredata table with its columns
                    string HighScoreInfo = "CREATE TABLE IF NOT EXISTS [HighScoreData] (GameNumber NVARCHAR(25), Name NVARCHAR(25), WaterUsed NVARCHAR(25), WattGenerated NVARCHAR(25), PointsScored NVARCHAR(25));";
                    //combined string that sets up the database
                    string initCMD = PowerPlantInfo + HighScoreInfo;
                    //combined database command
                    SqliteCommand CMDcreateTable = new SqliteCommand(initCMD, con);
                    //executes the database command
                    //CMDcreateTable.ExecuteReader();

                    CMDcreateTable.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        /// <summary>
        /// All data is saved
        /// </summary>
        /// <param name="FuelType"></param>
        /// <param name="Temparature"></param>
        /// <param name="Water"></param>
        /// <param name="Generated"></param>
        public static void UpdateCurrentGame(String FuelType, String Temparature, String Water, String Generated)
        {
            if (!FuelType.Equals("") && !Temparature.Equals("") && !Water.Equals("") && !Generated.Equals(""))
            {
                //the path to the database
                string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "PowerPlantDatabase.db");
                using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
                {
                    try
                    {
                        con.Open();
                        SqliteCommand CMD_Insert = new SqliteCommand();
                        CMD_Insert.Connection = con;
                        //command that prepares the data to be inserted to the database
                        CMD_Insert.CommandText = "INSERT INTO PowerPlantData (FuelType, Temparature, Water, Generated) VALUES (@FuelType, @Temparature, @Water, @Generated)";
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

        /// <summary>
        /// Database information can be set or returned
        /// </summary>
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

        /// <summary>
        /// Score information can be set or returned
        /// </summary>
        public class ScoreInfo
        {
            public String GameNumber { get; set; }
            public String WaterUsed { get; set; }
            public String WattGenerated { get; set; }
            public String Name { get; set; }
            public String PointsScored { get; set; }

            /// <summary>
            /// Sets the following data
            /// </summary>
            /// <param name="GameNumber1"></param>
            /// <param name="WaterUsed1"></param>
            /// <param name="WattGenerated1"></param>
            /// <param name="PointsScored1"></param>
            public ScoreInfo(String GameNumber1, String WaterUsed1, String WattGenerated1, String PointsScored1)
            {
                GameNumber = GameNumber1;
                WaterUsed = WaterUsed1;
                WattGenerated = WattGenerated1;
                PointsScored = PointsScored1;
            }

            /// <summary>
            /// Sets the following data
            /// </summary>
            /// <param name="Name1"></param>
            /// <param name="PointsScored1"></param>
            public ScoreInfo(String Name1, String PointsScored1)
            {
                Name = Name1;
                PointsScored = PointsScored1;
            }
        }
        /// <summary>
        /// Sets a new score into the database
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="PointsScored"></param>
        public static void SetScore(String Name, String PointsScored)
        {
            if (!Name.Equals("") && !PointsScored.Equals(""))
            {
                string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "PowerPlantDatabase.db");
                using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
                {
                    try
                    {
                        con.Open();
                        SqliteCommand CMD_Insert = new SqliteCommand();
                        CMD_Insert.Connection = con;
                        //command that prepares the data to be inserted to the database
                        CMD_Insert.CommandText = "INSERT INTO [HighScoreData] (Name, PointsScored) VALUES (@Name, @PointsScored)";
                        CMD_Insert.Parameters.AddWithValue("Name", Name);
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

        /// <summary>
        /// returns list of highscores 
        /// </summary>
        /// <returns></returns>
        public static List<ScoreInfo> GetScore()
        {
            List<ScoreInfo> ScoreList = new List<ScoreInfo>();
            string DBPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "PowerPlantDatabase.db");
            using (SqliteConnection con = new SqliteConnection($"Filename={DBPath}"))
            {
                con.Open();

                String selectCmd = "SELECT Name, PointsScored FROM HighScoreData ORDER BY PointsScored DESC";
                SqliteCommand cmd_getData = new SqliteCommand(selectCmd, con);
                SqliteDataReader reader = cmd_getData.ExecuteReader();
                while (reader.Read())
                {
                    ScoreList.Add(new ScoreInfo(reader.GetString(0), reader.GetString(1)));
                }
                con.Close();
            }
            return ScoreList;
        }

    }
}