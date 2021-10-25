using System;
using System.Collections.Generic;
using System.Text;

namespace DataConversion
{
    public class Word
    {
        public string Term { get; set; }
        public List<Definition> Definitions = new List<Definition>();
    }

    public class Definition
    {
        public string Description { get; set; }
        public string Type { get; set; }
    }

    public static class ConvertStringWord
    {
        public static List<Word> WordList = new List<Word>();
        private static Dictionary<string, string> types = new Dictionary<string, string>()
        {
            { "()", "" },
            { "(n.)", "noun" },
            { "(adv.)", "adverb" },
            { "(v.)", "verb" },
            { "(v. t.)", "transitive verb" },
            { "(pl.)", "plural"},
            { "(prep.)", "preposition" }
        };

        public static void Convert(string s)
        {
            string[] splitData = s.Split(' ');

            // find the complete word
            string word = "";
            int endPoint = 0;
            foreach (string data in splitData)
            {
                if (data[0] == '(') break;
                word += data + " ";
                endPoint++;
            }

            // determine if the word is already in the list, since it is in alphabetical order
            // all the needs to be compared is the last item in the list
            Word wordObj;
            if (WordList.Count > 0 && word == WordList[WordList.Count - 1].Term)
            {
                wordObj = WordList[WordList.Count - 1];
                Console.WriteLine($"{word} already exists, modifying original word.");
            }
            else
            {
                wordObj = new Word();
                wordObj.Term = word;
                WordList.Add(wordObj);
                Console.WriteLine($"New word found: {word}");
            }

            // create new definition and fill in data
            Definition definition = new Definition();
            if (types.ContainsKey(splitData[endPoint]))
            {
                definition.Type = types[splitData[endPoint]];
            }
            else
            {
                definition.Type = "";
            }
            endPoint++;

            string defCombined = "";
            for (int i = endPoint; i < splitData.Length; i++)
            {
                defCombined += splitData[i] + " ";
            }

            definition.Description = defCombined;
            wordObj.Definitions.Add(definition);
            Console.WriteLine("Complete!");
        }
    }
}
