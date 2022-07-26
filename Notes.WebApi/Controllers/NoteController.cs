using AutoMapper;
using System;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.Notes.Queries.GetNoteList;
using Notes.Application.Notes.Queries.GetNoteDetail;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Application.Notes.Commands.DeleteCommand;
using Notes.WebApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Notes.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class NoteController : BaseController
    {
        //маппер, чтобы преобразовать входные данные в команду
        private readonly IMapper _mapper;
        public NoteController(IMapper mapper) => _mapper = mapper;

        /// <summary>
        /// метод получения всех заметок. С помощью медиатора формируется запрос
        /// и результаты возвращаются клиенту
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<NoteListVm>> GetAll()
        {
            var query = new GetNoteListQuery
            {
                UserId = UserId,
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Метод получения одной заметки. Аналогично GetAll
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<NoteDetailsVm>> Get(Guid id)
        {
            var query = new GetNoteDetailsQuery
            {
                UserId = UserId,
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }


        /// <summary>
        /// параметры метода извлекаем из тела http-запроса
        /// </summary>
        /// <param name="createNoteDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateNoteDto createNoteDto)
        {
            var command = _mapper.Map<CreateNoteCommand>(createNoteDto);
            command.UserId = UserId;
            var noteId = await Mediator.Send(command); 
            return Ok(noteId);
        }


        /// <summary>
        /// В качестве рузультата NoContent - назад ничего не отправляем
        /// </summary>
        /// <param name="updateNoteDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateNoteDto updateNoteDto)
        {
            var command = _mapper.Map<UpdateNoteCommand>(updateNoteDto);
            command.UserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteNoteCommand
            {
                Id = id,
                UserId = UserId
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
