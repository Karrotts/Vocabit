using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Vocabit
{
    public class Word
    {
        public string Term { get; set; }
        public Definition[] Definitions { get; set; }
    }

    public class Definition
    {
        public string Description { get; set; }
        public string Type { get; set; }
    }

    public static class Data
    {
        private static string connectionString = Environment.GetEnvironmentVariable("database");
        public static int TotalEntries;

        public static List<string> Query(string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    List<string> result = new List<string>();
                    while (reader.Read())
                    {
                        string line = "";
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            line += 1 == reader.FieldCount - i ? reader[i] : reader[i] + "[]";
                        }
                        result.Add(line);
                    }
                    reader.Close();
                    connection.Close();
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                return null;
            }
        }

        public static Word GetWord(string word)
        {
            string query = $"SELECT * FROM words AS w" +
                           $" INNER JOIN definitions AS d" +
                           $" ON w.id = d.word_id" +
                           $" WHERE term = '{word}'";

           return AnalyzeWordResults(Query(query));
        }

        public static Word GetWord(int id)
        {
            string query = $"SELECT * FROM words AS w" +
                           $" INNER JOIN definitions AS d" +
                           $" ON w.id = d.word_id" +
                           $" WHERE w.id = {id}";

            return AnalyzeWordResults(Query(query));
        }

        private static Word AnalyzeWordResults(List<string> results)
        {
            Word newWord = new Word();
            List<Definition> definitions = new List<Definition>();

            foreach (string result in results)
            {
                string[] splitResult = result.Split(new char[] { '[', ']' })
                                             .Where(x => !string.IsNullOrEmpty(x)).ToArray();

                //set term if not already
                if (String.IsNullOrEmpty(newWord.Term)) newWord.Term = splitResult[1];

                //add definitions
                Definition definition = new Definition();
                definition.Description += splitResult[4];
                definition.Type += splitResult.Length == 6 ? splitResult[5] : "";
                definitions.Add(definition);
                newWord.Definitions = definitions.ToArray();
            }

            return newWord;
        }
    }
}
