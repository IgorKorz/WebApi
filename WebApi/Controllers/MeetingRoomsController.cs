using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/days")]
    public class MeetingRoomsController : Controller
    {
        MeetingRoomsContext db;

        public MeetingRoomsController(MeetingRoomsContext context)
        {
            db = context;

            if (db.MeetingRooms.Count() == 0)
            {
                db.MeetingRooms.Add(new MeetingRoom { IsBusy = false });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<MeetingRoom> Get()
        {
            return db.MeetingRooms.ToList();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            MeetingRoom day = db.MeetingRooms.FirstOrDefault(x => x.Id == id);

            if (day == null) NotFound();

            return new ObjectResult(day);
        }

        [HttpPost]
        public IActionResult Post([FromBody]MeetingRoom day)
        {
            if (day == null) BadRequest();

            db.MeetingRooms.Add(day);
            db.SaveChanges();

            return Ok(day);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody]MeetingRoom day)
        {
            if (day == null) BadRequest();

            if (!db.MeetingRooms.Any(x => x.Id == day.Id)) return NotFound();

            db.Update(day);
            db.SaveChanges();

            return Ok(day);
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            MeetingRoom day = db.MeetingRooms.FirstOrDefault(x => x.Id == id);

            if (day == null) NotFound();

            db.MeetingRooms.Remove(day);
            db.SaveChanges();

            return Ok(day);
        }
    }
}
