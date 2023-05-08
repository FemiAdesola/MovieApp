using API.Database;
using API.DTOs;
using API.Helper;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class ActorController : BaseApiController
    {
        private readonly ILogger<ActorController> _logger;
        private readonly AppDbContext _context;
        protected readonly IMapper _mapper;

        public ActorController(ILogger<ActorController> logger, AppDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromForm] CreateActorDTO createActorDTO)
        {
            var actor = _mapper.Map<Category>(createActorDTO);
            _context.Add(actor);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        
        [HttpGet]
        public async Task<ActionResult<List<ActorDTO>>> GetAll([FromQuery] PaginationDTO paginationDTO)
        {
            // _logger.LogInformation("Getting all the genres");
            var queryable = _context.Actors.AsQueryable();
            
            await HttpContext.InsertParametersPaginationInHeader(queryable);
            var actor = await queryable
                .OrderBy(x =>x.Name)
                .Paginate(paginationDTO)
                .ToListAsync();

            return _mapper.Map<List<ActorDTO>>(actor);
        }

         [HttpGet( "{Id:int}")]
        public async Task<ActionResult<ActorDTO>>Get(int Id)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(x => x.Id == Id);
            if (actor == null)
            {
                return NotFound();
            }

            return _mapper.Map<ActorDTO>(actor);
        }

        [HttpPut("{Id:int}")]
       public async Task<ActionResult> Update(int Id, [FromBody] CreateActorDTO createActorDTO)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(x => x.Id == Id);

            if (actor == null)
            {
                return NotFound();
            }

            actor = _mapper.Map(createActorDTO, actor);
            await _context.SaveChangesAsync();
            return NoContent();
        }

       [HttpDelete("{Id:int}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var actorExists = await _context.Actors.AnyAsync(x => x.Id == Id);

            if (!actorExists)
            {
                return NotFound();
            }

            _context.Remove(new Actor() { Id = Id });
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}