using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PokeInfo.Models;

namespace PokeInfo.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult ReturnMain()
        {
            return RedirectToAction("Index");
        }
        [HttpGet("pokemon")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("pokemon/{pokeId}")]
        public IActionResult QueryPoke(int pokeid)
        {
            var PokeInfo = new Dictionary<string, object>();
            WebRequest.GetPokemonDataAsync(pokeid, ApiReponse =>
            {
                PokeInfo = ApiReponse;
            }
            ).Wait();
            ViewBag.Info = PokeInfo;
            return View("Info");
        }
    }    
}
