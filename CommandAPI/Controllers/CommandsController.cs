
namespace CommandAPI.Controllers
{
    using System.Collections.Generic;
    using CommandAPI.Data;
    using CommandAPI.Models;
    using Microsoft.AspNetCore.Mvc;
    using CommandAPI.Dtos;
    using System.Linq;
    using System;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using System.IO;
    using System.Threading.Tasks;

    [Route("api/commands")]
    [ApiController]
    public class Commands : Controller
    {
        private readonly ICommandRepo _repo ;

        public IActionResult Index()
        {
            
            return View();
        }

        public Commands(ICommandRepo repo)
        {
            _repo = repo;
        }


        [HttpGet("{id}",Name="GetCommandById")]
        public IActionResult GetCommandById(int id)
        {
            Command command = _repo.GetCommandById(id);
            if(command == null)
            {
                return NotFound();
            }
            CommandReadDto dto = CreateReadDto(command);
            return Ok(dto);
        }       

        [HttpGet]
        //[Authorize]
        public IActionResult GetAllCommands()
        {
            IEnumerable<Command> commandResult = _repo.GetAllCommands();
            IEnumerable<CommandReadDto> readDtos = commandResult.Select(command => CreateReadDto(command));

            return Ok(readDtos);
        }

        private static CommandReadDto CreateReadDto(Command command)
        {
            return new CommandReadDto
            {
                HowTo = command.HowTo,
                Id = command.Id,
                Line = command.Line
            };
        }

        private static Command CreateCommandFromDto(CommandCreateDto commandCreateDto)
        {
            return new Command
            {
                HowTo = commandCreateDto.HowTo,
                Line = commandCreateDto.Line,
                Platform = commandCreateDto.Platform
            };
        }


        [HttpPost]
        [Consumes("application/json")]
        public IActionResult CreateCommand(CommandCreateDto commandCreateDto)
        {
            Command command = CreateCommandFromDto(commandCreateDto);
            _repo.CreateCommand(command);
            _repo.SaveChanges();
            CommandReadDto dto = CreateReadDto(command);
            return CreatedAtRoute(nameof(GetCommandById),new{Id= dto.Id},dto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCommand(int id,CommandUpdateDto cmdUpdateDto)
        {
            Command existingCommand = _repo.GetCommandById(id);
            if (existingCommand == null)
            {
                return NotFound();
            }

            MapCommadUpdateDtoToCommand(cmdUpdateDto, existingCommand);
            _repo.UpdateCommand(existingCommand);
            _repo.SaveChanges();
            return NoContent();
        }

        [DisableRequestSizeLimit] 
        [HttpPost("upload")]
        public async Task<IActionResult> PostFile(ICollection<IFormFile> files)
        {
            string path = @"C:\Users\z003wrzm\Desktop\PostFolder";
            foreach(var file in files)
            {
                string guidVal = Guid.NewGuid().ToString();
                string filePath = Path.Combine(path,guidVal);
                using(Stream sr = new FileStream(filePath,FileMode.Create))
                {
                    await file.CopyToAsync(sr);
                }
                
            }
            return Ok();
        }

        [HttpPost("uploadb")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> PostFile()
        {
            string path = @"C:\Users\z003wrzm\Desktop\PostFolder";
            string guidVal = Guid.NewGuid().ToString();
            string filePath = Path.Combine(path,guidVal);
            using (Stream sr = new FileStream(filePath, FileMode.Create))
            {
                await Request.Body.CopyToAsync(sr);
            }

            return Ok();
        }



        private static void MapCommadUpdateDtoToCommand(CommandUpdateDto cmdUpdateDto, Command existingCommand)
        {
            existingCommand.HowTo = cmdUpdateDto.HowTo;
            existingCommand.Line = cmdUpdateDto.Line;
            existingCommand.Platform = cmdUpdateDto.Platform;
        }

        private static void MapCommandToCommadUpdateDto(CommandUpdateDto cmdUpdateDto, Command existingCommand)
        {
            cmdUpdateDto.HowTo = existingCommand.HowTo;
            cmdUpdateDto.Line = existingCommand.Line;
            cmdUpdateDto.Platform = existingCommand.Platform;
        }

        [HttpPatch("{id}")]
        public IActionResult PartialCommandUpdate(int id,JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            Command existingCommand = _repo.GetCommandById(id);
            if(existingCommand == null)
            {
                return NotFound();
            }

            var commandUpdateDto = new CommandUpdateDto();

            MapCommandToCommadUpdateDto(commandUpdateDto,existingCommand);
               
            patchDoc.ApplyTo(commandUpdateDto,ModelState);
            
            if(!TryValidateModel(commandUpdateDto))
            {
                return ValidationProblem(ModelState);
            }

            MapCommadUpdateDtoToCommand(commandUpdateDto, existingCommand); 

            _repo.SaveChanges();   

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCommand(int id)
        {
            Command command = _repo.GetCommandById(id);
            if(command == null)
            {
                return NotFound();
            }

            _repo.DeleteCommand(command);

            _repo.SaveChanges();

            return NoContent();

        }
        
    }
}