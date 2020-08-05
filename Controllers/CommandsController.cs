using System.Collections.Generic;
using AutoMapper;
using dotnetWebApi.Data;
using dotnetWebApi.DTOs;
using dotnetWebApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace dotnetWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommandsController: ControllerBase
    {
        private readonly ICommanderRepo _repository;
        public IMapper _mapper { get; }

        public CommandsController(ICommanderRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAll()
        {
            var commandItems = _repository.GetAllCommands();

            if (commandItems != null)
                return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));

            return NotFound();
        }

        [HttpGet("{id:int}")]
        public ActionResult<CommandReadDto> Find(int id)
        {
            var commandItem = _repository.GetCommandById(id);

            if (commandItem != null)
                return Ok(_mapper.Map<CommandReadDto>(commandItem));

            return NotFound();
        }
    
        [HttpPost]
        public ActionResult<CommandReadDto> Create(CommandCreateDto dto)
        {
            var command = _mapper.Map<Command>(dto);

            _repository.CreateCommand(command);
            _repository.SaveChanges();

            var readDto = _mapper.Map<CommandReadDto>(command);

            // For CreatedAtRoute to work, the Find method needs to have Name="Find" in the attribute 
            // return CreatedAtRoute(nameof(Find), new { id = readDto.Id }, readDto);

            return CreatedAtAction("Find", new { id = readDto.Id }, readDto);
        }
    
        [HttpPut("{id:int}")]
        public ActionResult Update(int id, CommandCreateDto dto)
        {
            var command = _repository.GetCommandById(id);

            if (command == null)
                return NotFound();

            _mapper.Map(dto, command);
            
            // Don't actually need this because of how EF works
            // But it's here as a good practice because other ORMs would need it 
            //_repository.UpdateCommand(command);

            _repository.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public ActionResult PartialUpdate(int id, JsonPatchDocument<CommandUpdateDto> dto)
        {
            var command = _repository.GetCommandById(id);

            if (command == null)
                return NotFound();

            var commandToPatch = _mapper.Map<CommandUpdateDto>(command);

            dto.ApplyTo(commandToPatch, ModelState);

            if (!TryValidateModel(commandToPatch))
                return ValidationProblem(ModelState);

            _mapper.Map(commandToPatch, command);

            _repository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var command = _repository.GetCommandById(id);

            if (command == null)
                return NotFound();

            _repository.DeleteCommand(command);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}