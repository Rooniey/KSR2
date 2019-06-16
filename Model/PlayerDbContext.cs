using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    
    public class PlayerDbContext
    {
        private const int ID = 0;
        private const int Name = 1;
        private const int SprintSpeed = 52;
        public static DataTable OpenConnection()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string filepath = Directory.GetParent(workingDirectory).Parent.Parent.FullName + "\\raw_data\\all_fifa.db";
            SQLiteConnection myConnection = new SQLiteConnection($"Data Source={filepath};Version=3;");
            myConnection.Open();
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = myConnection;
            cmd.CommandText = "Select * from fifa";
            using (SQLiteDataReader sdr = cmd.ExecuteReader())
            {
                DataTable dt = new DataTable();
                dt.Load(sdr);
                sdr.Close();
                myConnection.Close();
                return dt;
            }
        }

        public static List<Player> ReadData()
        {
            DataTable dataSet = OpenConnection();
            List<Player> players = new List<Player>();
            var columns = dataSet.Columns;

            foreach (DataRow dataSetRow in dataSet.Rows)
            {
                Player player = new Player();
                foreach (var prop in typeof(Player).GetProperties())
                {
                    var val = dataSetRow.ItemArray[columns.IndexOf(prop.Name)];
                    prop.SetValue(player, Convert.ChangeType(val, prop.PropertyType));
                }
                players.Add(player);
            }
            return players;
        }       
    }
}

