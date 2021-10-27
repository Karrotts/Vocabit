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
            { "(a.)", "adjective"},
            { "(adv.)", "adverb" },
            { "(v.)", "verb" },
            { "(v. t.)", "transitive verb" },
            { "(n. pl.)", "plural noun"},
            { "(pl. )", "plural"},
            { "(prep.)", "preposition" }
        };

        public static void Convert(string s)
        {
            string[] splitData = s.Split(' ');

            // find the complete word
            string word = "";
            int startIndex = 0;
            foreach (char c in s)
            {
                if (c == '(') break;
                startIndex++;
                word += c;
            }

            word = word.Trim();

            // determine if the word is already in the list, since it is in alphabetical order
            // all that needs to be compared is the last item in the list
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


            // find the type of word from the items in '()'
            // compare to dictionary of terms
            try
            {
                string key = s.Substring(startIndex, s.IndexOf(')') - startIndex + 1);
                Definition definition = new Definition();
                if (types.ContainsKey(key))
                {
                    definition.Type = types[key];
                }
                else
                {
                    definition.Type = "";
                }

                // combine the rest of the string to find the definition
                string defCombined = "";
                bool endFound = false;
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] == ')')
                    {
                        endFound = true;
                        continue;
                    }
                    if (endFound)
                    {
                        defCombined += s[i];
                    }

                }

                definition.Description = defCombined.Trim();
                wordObj.Definitions.Add(definition);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return;
            }
        }
    }
}
