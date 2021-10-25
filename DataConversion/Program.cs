using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace DataConversion
{
    class Program
    {
        static string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=Vocabit;Trusted_Connection=True;MultipleActiveResultSets=true";
        static void Main(string[] args)
        {
            string[] files = Directory.GetFiles(@"C:\Users\Wesle\OneDrive\Desktop\Dictionary", "*");
            foreach(string file in files)
            {
                using (StreamReader sr = File.OpenText(file))
                {
                    ConvertStringWord.WordList = new List<Word>();
                    string s = "";
                    int count = 0;
                    while ((s = sr.ReadLine()) != null)
                    {
                        if (s == "") continue;
                        s = s.Trim('"');
                        ConvertStringWord.Convert(s);
                        count++;
                    }
                    Console.WriteLine($"Converted {count} lines successfully!");
                    Console.WriteLine($"Total words: {ConvertStringWord.WordList.Count}");

                    foreach (Word word in ConvertStringWord.WordList)
                    {
                        Console.WriteLine($"Inserting {word.Term}into database...");
                        int id = InsertWord($"INSERT INTO words(term) OUTPUT INSERTED.id VALUES (@term)", word);
                        Console.WriteLine("Inserting definitions...");
                        InsertDefinitions(id, word);
                        Console.WriteLine($"{word.Term} was added successfully!");
                    }
                    Console.WriteLine("Complete!");
                }
            }
        }

        private static void InsertDefinitions(int id, Word word)
        {
            using (SqlConnection connection = new SqlConnection(
                       connectionString))
            {
                foreach (Definition definition in word.Definitions)
                {
                    string query = "INSERT INTO definitions(word_id, type, description) VALUES (@word_id, @type, @description)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@word_id", id);
                    command.Parameters.AddWithValue("@type", definition.Type);
                    command.Parameters.AddWithValue("@description", definition.Description);

                    command.Connection.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                    } 
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error:" + ex.Message);
                    }
                    command.Connection.Close();
                }
            }
        }

        private static int InsertWord(string query, Word word)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@term", word.Term);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        return (int)reader[0];
                    }
                    reader.Close();
                } 
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                connection.Close();
            }
            return 0;
        }
    }
}
