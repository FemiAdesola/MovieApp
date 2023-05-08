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
    public class ActorController : BaseApiController
    {
        private readonly ILogger<ActorController> _logger;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileStorage _fileStorage;
        private readonly string containerName = "actors";

        public ActorController(
            ILogger<ActorController> logger, 
            AppDbContext context, 
            IMapper mapper,
            IFileStorage fileStorage)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _fileStorage =fileStorage;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] CreateActorDTO createActorDTO)
        {
            var actor = _mapper.Map<Actor>(createActorDTO);

            if(createActorDTO.Image != null)
            {
                actor.Image = await _fileStorage.SaveFile(containerName, createActorDTO.Image);
            }

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
       public async Task<ActionResult> Update(int Id, [FromForm] CreateActorDTO createActorDTO)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(x => x.Id == Id);

            if (actor == null)
            {
                return NotFound();
            }

            actor = _mapper.Map(createActorDTO, actor);

            if(createActorDTO.Image != null)
            {
                actor.Image = await _fileStorage.UpdateFile(
                    containerName, 
                    createActorDTO.Image, 
                    actor.Image);
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

       [HttpDelete("{Id:int}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var actorExists = await _context.Actors.FirstOrDefaultAsync(x => x.Id == Id);

            if (actorExists == null)
            {
                return NotFound();
            }

            _context.Remove(actorExists);
            await _context.SaveChangesAsync();
            await _fileStorage.DeleteFile(actorExists.Image, containerName);
            return NoContent();
        }
    }
}