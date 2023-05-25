using API.Contracts;
using API.Models;
using API.Repositories;
using API.View_Models.Rooms;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper<Room, RoomVM> _mapper;

        public RoomController(IRoomRepository roomRepository,
            IMapper<Room, RoomVM>mapper,
            IBookingRepository bookingRepository,
            IEmployeeRepository employeeRepository)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
            _bookingRepository = bookingRepository;
            _employeeRepository = employeeRepository;
        }

        [HttpGet("RoomsByDateTime")]
        public IActionResult GetRoomsByDateTime(DateTime dateTime)
        {

            var room = _roomRepository.GetAll();
            var booking = _bookingRepository.GetAll();
            var emp = _employeeRepository.GetAll();
            var filteredRooms = booking.Where(booking => booking.StartDate <= dateTime && booking.EndDate >= dateTime).ToList();

            var result = filteredRooms.Select(room => new
            {

                BookedBy = room.Employee.FirstName + " " + room.Employee.LastName,
                Status = room.Status.ToString(),
                RoomName = room.Room.Name,
                Floor = room.Room.Floor,
                Capacity = room.Room.Capacity,
                StartDate = room.StartDate,
                EndDate = room.EndDate
            });

            return Ok(result);
        }
        
        [HttpGet("CurrentlyUsedRoomsByDate")]
        public IActionResult GetCurrentlyUsedRooms(DateTime dateTime)
        {
            var room = _roomRepository.GetByDate(dateTime);
            if (room is null)
            {
                return NotFound();
            }
            return Ok(room);
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var rooms = _roomRepository.GetAll();
            if (!rooms.Any())
            {
                return NotFound();
            }
            var data = rooms.Select(_mapper.Map).ToList();
            return Ok(data);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var room = _roomRepository.GetByGuid(guid);
            if (room is null)
            {
                return NotFound();
            }
            var data = _mapper.Map(room);
            return Ok(data);
        }

        [HttpPost]
        public IActionResult Create(RoomVM roomVM)
        {
            var roomConverted = _mapper.Map(roomVM);
            var result = _roomRepository.Create(roomConverted);
            if (result is null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(RoomVM roomVM)
        {
            var roomConverted = _mapper.Map(roomVM);
            var isUpdated = _roomRepository.Update(roomConverted);
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
