﻿using API.Contracts;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;

        public RoomController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var rooms = _roomRepository.GetAll();
            if (!rooms.Any())
            {
                return NotFound();
            }
            return Ok(rooms);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var room = _roomRepository.GetByGuid(guid);
            if (room is null)
            {
                return NotFound();
            }
            return Ok(room);
        }

        [HttpPost]
        public IActionResult Create(Room room)
        {
            var result = _roomRepository.Create(room);
            if (result is null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(Room room)
        {
            var isUpdated = _roomRepository.Update(room);
            if (!isUpdated)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _roomRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
