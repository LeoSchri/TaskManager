using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Windows;

namespace TaskManager
{
    public static class DatabaseConnector
    {
        static SqlConnection myConnection = new SqlConnection(@"server=(LocalDb)\MSSQLLocalDB;" +
                                       "Trusted_Connection=yes;" +
                                       "database=TaskManager; ");

        public static int Connect()
        {
            myConnection.Open();
            var state = myConnection.State;
            if(state != ConnectionState.Open)
            {
                MessageBox.Show("Database couldn't be connected", "Database state", MessageBoxButton.OK, MessageBoxImage.Error);
                return 1;
            }

            return 0;
        }

        public static int Disconnect()
        {
            myConnection.Close();
            var state = myConnection.State;
            if (state != ConnectionState.Closed)
            {
                MessageBox.Show("Database couldn't be closed", "Database state", MessageBoxButton.OK, MessageBoxImage.Information);
                return 1;
            }

            return 0;
        }

        public static Dictionary<int,List<string>> Select(string table, List<string> columns, int? ID = null)
        {
            try
            {
                SqlDataReader myReader = null;
                var myCommand = new SqlCommand($"select * from {table} {(ID!=null?$"where ID={ID}":"")}", myConnection);
                myReader = myCommand.ExecuteReader();

                var entries = new Dictionary<int,List<string>>();
                var counter = 0;
                while (myReader.Read())
                {
                    var values = new List<string>();
                    columns.ForEach(c=> values.Add(myReader[c].ToString()));
                    entries.Add(counter, values);
                    counter++;
                }

                myReader.Close();

                return entries;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public static void Insert(string table, List<string> columns, List<string> values)
        {
            var myCommand = new SqlCommand($"INSERT into {table} ({string.Join(",", columns)}) VALUES ({string.Join(",",values)});", myConnection);
             myCommand.ExecuteNonQuery();
        }

        public static void Update(string table, List<string> columns, List<string> values, int ID)
        {
            var setter = new List<string>();
            for(int i=0; i< columns.Count; i++)
            {
                setter.Add($"{columns[i]}={values[i]}");
            }

            var myCommand = new SqlCommand($"UPDATE {table} SET {string.Join(",", setter)} WHERE ID={ID};", myConnection);
            myCommand.ExecuteNonQuery();
        }


        public static void Delete(string table, int ID)
        {
            var myCommand = new SqlCommand($"DELETE FROM {table} where ID={ID}", myConnection);
            myCommand.ExecuteNonQuery();

        }

        public static void UpdateData()
        {
            var myCommand = new SqlCommand("EXEC dbo.updateDataM", myConnection);
            myCommand.ExecuteNonQuery();
        }
    }
}
