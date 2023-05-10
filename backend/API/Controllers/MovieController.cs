using API.Database;
using API.DTOs;
using API.Helper;
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

        [HttpGet]
        public async Task<ActionResult<HomePageDTO>> Get()
        {
            var top = 6;
            var today = DateTime.Today;

            var upcomingReleases = await _context.Movies
                .Where(x => x.ReleaseDate > today)
                .OrderBy(x => x.ReleaseDate)
                .Take(top)
                .ToListAsync();

            var inCinemas = await _context.Movies
                .Where(x => x.InCinemas)
                .OrderBy(x => x.ReleaseDate)
                .Take(top)
                .ToListAsync();

            var homePageDTO = new HomePageDTO();
            homePageDTO.UpcomingReleases = _mapper.Map<List<MovieDTO>>(upcomingReleases);
            homePageDTO.InCinemas = _mapper.Map<List<MovieDTO>>(inCinemas);
            return homePageDTO;
        }

        [HttpGet("putget/{id:int}")]
        public async Task<ActionResult<MoviePutGetDTO>> PutGet(int id)
        {
            var movieActionResult = await Get(id);
            if (movieActionResult.Result is NotFoundResult) { return NotFound(); }

            var movie = movieActionResult.Value;

            var categoriesSelectedIds = movie?.Categories?.Select(x => x.Id).ToList();
            var nonSelectedCategories= await _context.Categories.Where(x => !categoriesSelectedIds!.Contains(x.Id))
                .ToListAsync();

            var movieCinemasIds = movie?.MovieCinemas?.Select(x => x.Id).ToList();
            var nonSelectedMoviecinemas = await _context.MovieCinemas.Where(x =>
            !categoriesSelectedIds!.Contains(x.Id)).ToListAsync();

            var nonSelectedCategoriesDTOs = _mapper.Map<List<CategoryDTO>>(nonSelectedCategories);
            var nonSelectedMovieCinemasDTO = _mapper.Map<List<MovieCinemaDTO>>(nonSelectedMoviecinemas);

            var response = new MoviePutGetDTO();
            response.Movie = movie;
            response.SelectedCategories= movie?.Categories;
            response.NonSelectedCategories = nonSelectedCategoriesDTOs;
            response.SelectedMovieCinemas = movie?.MovieCinemas;
            response.NonSelectedMovieCinemas = nonSelectedMovieCinemasDTO;
            response.Actors = movie?.Actors;
            return response;
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] CreateMovieDTO createMovieDTO)
        {
            var movie = await _context.Movies.Include(x => x.MoviesActors)
                .Include(x => x.MoviesCategories)
                .Include(x => x.MovieCinemasMovies)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            movie = _mapper.Map(createMovieDTO, movie);

            if (createMovieDTO.Poster != null)
            {
                movie.Poster = await _fileStorage.UpdateFile(container, createMovieDTO.Poster,
                    movie.Poster!);
            }

            AnnotateActorsOrder(movie);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            _context.Remove(movie);
            await _context.SaveChangesAsync();
            await _fileStorage.DeleteFile(movie.Poster!, container);
            return NoContent();
        }
        
        [HttpGet("filter")]
        public async Task<ActionResult<List<MovieDTO>>> Filter([FromQuery] FilterMoviesDTO filterMoviesDTO)
        {
            var moviesQueryable = _context.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(filterMoviesDTO.Title))
            {
                moviesQueryable = moviesQueryable.Where(x => x.Title!.Contains(filterMoviesDTO.Title));
            }

            if (filterMoviesDTO.InCinemas)
            {
                moviesQueryable = moviesQueryable.Where(x => x.InCinemas);
            }

            if (filterMoviesDTO.UpcomingReleases)
            {
                var today = DateTime.Today;

                moviesQueryable = moviesQueryable
                    .Where(x => x.ReleaseDate > today);
            }

            if (filterMoviesDTO.CategoryId != 0)
            {
                moviesQueryable = moviesQueryable
                    .Where(x => x.MoviesCategories!.Select(y => y.CategoryId)
                    .Contains(filterMoviesDTO.CategoryId));
            }

            await HttpContext.InsertParametersPaginationInHeader(moviesQueryable);
            var movies = await moviesQueryable
                .OrderBy(x => x.Title)
                .Paginate(filterMoviesDTO.PaginationDTO)
                .ToListAsync();

            return _mapper.Map<List<MovieDTO>>(movies);
        }



        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieDTO>> Get(int id)
        {
            var movie = await _context.Movies
                .Include(x => x.MoviesCategories).ThenInclude(x => x.Category)
                .Include(x => x.MovieCinemasMovies).ThenInclude(x => x.MovieCinema)
                .Include(x => x.MoviesActors).ThenInclude(x => x.Actor)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<MovieDTO>(movie);
            dto.Actors = dto.Actors!.OrderBy(x => x.Order).ToList();
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