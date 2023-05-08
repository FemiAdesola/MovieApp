using API.Database;
using API.DTOs;
using API.Helper;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class  MovieCinemaController : BaseApiController
    {
        private readonly ILogger<MovieCinemaController> _logger;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public MovieCinemaController(ILogger<MovieCinemaController> logger, AppDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<MovieCinemaDTO>>> Get([FromQuery] PaginationDTO paginationDTO)
        {
            var queryable = _context.MovieCinemas.AsQueryable();
            await HttpContext.InsertParametersPaginationInHeader(queryable);
            var entities = await queryable
                .OrderBy(x => x.Name)
                .Paginate(paginationDTO)
                .ToListAsync();
            return _mapper.Map<List<MovieCinemaDTO>>(entities);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieCinemaDTO>> Get(int id)
        {
            var movieCinema = await _context.MovieCinemas.FirstOrDefaultAsync(x => x.Id == id);

            if (movieCinema == null)
            {
                return NotFound();
            }

            return _mapper.Map<MovieCinemaDTO>(movieCinema);
        }

        [HttpPost]
        public async Task<ActionResult> Post(CreateMovieCinemaDTO createMovieCinemaDTO)
        {
            var movieCinema = _mapper.Map<MovieCinema>(createMovieCinemaDTO);
            _context.Add(movieCinema);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, CreateMovieCinemaDTO createMovieCinemaDTO)
        {
            var movieCinema = await _context.MovieCinemas.FirstOrDefaultAsync(x => x.Id == id);

            if (movieCinema == null)
            {
                return NotFound();
            }

            movieCinema = _mapper.Map(createMovieCinemaDTO, movieCinema);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var movieCinema = await _context.MovieCinemas.FirstOrDefaultAsync(x => x.Id == id);

            if (movieCinema == null)
            {
                return NotFound();
            }

            _context.Remove(movieCinema);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}