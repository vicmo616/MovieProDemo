using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MovieProDemo.Data;
using MovieProDemo.Models.Database;
using MovieProDemo.Models.Settings;
using MovieProDemo.Services.Interfaces;

namespace MovieProDemo.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IRemoteMovieService _tmdbMovieService;
        private readonly IDataMappingService _mappingService;

        public ActorsController(IRemoteMovieService tmdbMovieService, IDataMappingService mappingService)
        {
            _tmdbMovieService = tmdbMovieService;
            _mappingService = mappingService;
        }


        public async Task<IActionResult> Detail(int id)
        {
            var actor = await _tmdbMovieService.ActorDetailAsync(id);
            actor = _mappingService.MapActorDetail(actor);

            return View(actor); 
        }
    }
}
