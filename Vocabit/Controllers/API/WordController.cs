using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vocabit.Controllers.API
{
    [ApiController]
    [Route("[controller]")]
    public class WordController : Controller
    {
        [HttpGet("/api/word/{item}")]
        public Word GetWord(string item)
        {
            return Data.GetWord(item);
        }

        [HttpGet("/api/id/{id}")]
        public Word GetWordById(int id)
        {
            return Data.GetWord(id);
        }
    }
}
