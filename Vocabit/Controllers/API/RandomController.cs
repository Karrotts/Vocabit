﻿using Microsoft.AspNetCore.Mvc;
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
            return Data.GetWord(random.Next(1, Data.TotalEntries));
        }

        [HttpGet("/api/random/{amount}")]
        public string GetAmount(int amount)
        {
            return amount.ToString();
        }
    }
}
