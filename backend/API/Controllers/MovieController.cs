using API.Database;
using API.DTOs;
using API.Models;
using API.Services.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class MovieController : BaseApiController
    {
        private readonly ILogger<MovieController> _logger;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileStorage _fileStorage;
        private string container = "movies";

        public MovieController(
            ILogger<MovieController> logger,
             AppDbContext context, 
             IMapper mapper,
            IFileStorage fileStorage )
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _fileStorage =fileStorage;
        }


        [HttpPost]
        public async Task<ActionResult<int>> Post([FromForm] CreateMovieDTO createMovieDTO)
        {
            var movie = _mapper.Map<Movie>(createMovieDTO);

            if (createMovieDTO.Poster != null)
            {
                movie.Poster = await _fileStorage.SaveFile(container, createMovieDTO.Poster);
            }

            AnnotateActorsOrder(movie);
            _context.Add(movie);
            await _context.SaveChangesAsync();
            return movie.Id;
        }

        [HttpGet("PostGet")]
        public async Task<ActionResult<MoviePostGetDTO>> PostGet()
        {
            var movieCinemas = await _context.MovieCinemas.OrderBy(x => x.Name).ToListAsync();
            var category = await _context.Categories.OrderBy(x => x.Name).ToListAsync();

            var movieCinemaDTO = _mapper.Map<List<MovieCinemaDTO>>(movieCinemas);
            var categoryDTO = _mapper.Map<List<CategoryDTO>>(category);

            return new MoviePostGetDTO() { Categories = categoryDTO, MovieCinemas = movieCinemaDTO };
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieDTO>> Get(int Id)
        {
            var movie = await _context.Movies
                .Include(x => x.MoviesCategories).ThenInclude(x => x.Category)
                .Include(x => x.MovieCinemasMovies).ThenInclude(x => x.MovieCinema)
                .Include(x => x.MoviesActors).ThenInclude(x => x.Actor)
                .FirstOrDefaultAsync(x => x.Id == Id);

            if (movie == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<MovieDTO>(movie);
            dto.Actors = dto.Actors.OrderBy(x => x.Order).ToList();
            return dto;
        }

                // for mapping movies according to the order which actor come to UI
        private void AnnotateActorsOrder(Movie movie)
        {
            if (movie.MoviesActors != null)  
            {
                for (int i = 0; i < movie.MoviesActors.Count; i++)
                {
                    movie.MoviesActors[i].Order = i;
                }
            }
        }

    }
}