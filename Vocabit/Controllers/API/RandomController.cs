using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vocabit.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RandomController : Controller
    {
        [HttpGet("/api/random")]
        public Word Get()
        {
            Random random = new Random();
            Word word = new Word();
            while(string.IsNullOrEmpty(word.Term))
            {
                word = Data.GetWord(random.Next(1, Data.TotalEntries));
            }
            return word;
        }

        [HttpGet("/api/random/{amount}")]
        public Word[] GetAmount(int amount)
        {
            Word[] words = new Word[amount];
            Random random = new Random();
            
            for (int i = 0; i < amount; i++)
            {
                Word word = new Word();
                while (string.IsNullOrEmpty(word.Term) || CheckIfWordIsPresent(words, word))
                {
                    word = Data.GetWord(random.Next(1, Data.TotalEntries));
                }
                words[i] = word;
            }
            return words;
        }

        private bool CheckIfWordIsPresent(Word[] words, Word word)
        {
            foreach (Word w in words)
            {
                if (w != null && word.Term == w.Term) return true;
            }
            return false;
        }
    }
}
