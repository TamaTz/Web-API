﻿using API.Contracts;
using API.Models;
using API.View_Models.Bookings;
using API.View_Models.Other;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : BaseController<Booking, BookingVM>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper<Booking, BookingVM> _mapper;
        public BookingController(IBookingRepository bookingRepository, 
            IMapper<Booking, BookingVM> mapper) : base(bookingRepository, mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }


        // Kelompok 4
        [Authorize(Roles = "Manager")]
        [HttpGet("BookingDetail")]
        public IActionResult GetAllBookingDetail()
        {
            try
            {
                var bookingDetails = _bookingRepository.GetAllBookingDetail();

                return Ok(new ResponseVM<IEnumerable<BookingDetailVM>>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Success",
                    Data = bookingDetails
                });

            }
            catch
            {
                return NotFound(new ResponseVM<BookingDetailVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Booking Detail Not Found",
                    Data = null
                });
            }
        }

        [HttpGet("BookingDetailByGuid")]
        public IActionResult GetDetailByGuid(Guid guid)
        {
            try
            {
                var booking = _bookingRepository.GetBookingDetailByGuid(guid);
                if (booking is null)
                {
                    return NotFound(new ResponseVM<BookingDetailVM>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Booking Detail Not Found",
                        Data = null
                    });
                }
                return Ok(new ResponseVM<BookingDetailVM>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Success",
                    Data = booking
                });
            }
            catch
            {
                return NotFound(new ResponseVM<BookingDetailVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Booking Detail Not Found"
                });
            }
        }

        /*
                [HttpGet]
                public IActionResult GetAll()
                {
                    var bookings = _bookingRepository.GetAll();
                    if (!bookings.Any())
                    {
                        return NotFound(new ResponseVM<BookingVM>
                        {
                            Code = StatusCodes.Status400BadRequest,
                            Status = HttpStatusCode.BadRequest.ToString(),
                            Message = "Data not found"
                        });
                    }
                    var data = bookings.Select(_mapper.Map).ToList();
                    return Ok(new ResponseVM<List<BookingVM>>
                    {
                        Code = StatusCodes.Status200OK,
                        Status = HttpStatusCode.OK.ToString(),
                        Message = "Data Di Tampilkan",
                        Data = data
                    });
                }

                [HttpGet("{guid}")]
                public IActionResult GetByGuid(Guid id)
                {
                    var booking = _bookingRepository.GetByGuid(id);
                    if (booking is null)
                    {
                        return NotFound(new ResponseVM<BookingVM>
                        {
                            Code = StatusCodes.Status404NotFound,
                            Status = HttpStatusCode.NotFound.ToString(),
                            Message = "GetByGuid NotFound"
                        });
                    }
                    var data = _mapper.Map(booking);
                    return Ok(new ResponseVM<BookingVM>
                    {
                        Code = StatusCodes.Status200OK,
                        Status = HttpStatusCode.OK.ToString(),
                        Message = "GeByGuid Success",
                        Data = data
                    });
                }

                [HttpPost]
                public IActionResult Create(BookingVM bookingVM)
                {
                    var bookingConverted = _mapper.Map(bookingVM);
                    var result = _bookingRepository.Create(bookingConverted);
                    if (result is null)
                    {
                        return BadRequest(new ResponseVM<BookingVM>
                        {
                            Code = StatusCodes.Status400BadRequest,
                            Status = HttpStatusCode.BadRequest.ToString(),
                            Message = "Failed Create Booking"
                        });
                    }
                    return Ok(new ResponseVM<BookingVM>
                    {
                        Code = StatusCodes.Status200OK,
                        Status = HttpStatusCode.OK.ToString(),
                        Message = "Success Create Booking"
                    });
                }

                [HttpPut]
                public IActionResult Update(BookingVM bookingVM)
                {
                    var bookingConverted = _mapper.Map(bookingVM);
                    var IsUpdate = _bookingRepository.Update(bookingConverted);
                    if (!IsUpdate)
                    {
                        return BadRequest(new ResponseVM<BookingVM>
                        {
                            Code = StatusCodes.Status400BadRequest,
                            Status = HttpStatusCode.OK.ToString(),
                            Message = "Failed Update Booking"
                        });
                    }
                    return Ok(new ResponseVM<BookingVM>
                    {
                        Code = StatusCodes.Status200OK,
                        Status = HttpStatusCode.OK.ToString(),
                        Message = "Success Update Booking"
                    });
                }

                [HttpDelete("{guid}")]
                public IActionResult Delete(Guid guid)
                {
                    var isDeleted = _bookingRepository.Delete(guid);
                    if (isDeleted)
                    {
                        return BadRequest(new ResponseVM<Guid>
                        {
                            Code = StatusCodes.Status400BadRequest,
                            Status = HttpStatusCode.OK.ToString(),
                            Message = "Failed Delete Booking"
                        });
                    }
                    return Ok(new ResponseVM<Guid>
                    {
                        Code = StatusCodes.Status200OK,
                        Status = HttpStatusCode.OK.ToString(),
                        Message = "Success Delete Guid"
                    });
                }*/

        [HttpGet("BookingDuration")]
        public IActionResult GetDuration()
        {
            var bookingLengths = _bookingRepository.GetBookingDuration();
            if (!bookingLengths.Any())
            {
                return NotFound(new ResponseVM<BookingDurationVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Booking Duration not found",
                    Data = null
                });
            }

            return Ok(new ResponseVM<IEnumerable<BookingDurationVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success",
                Data = bookingLengths
            });
        }
    }
}
