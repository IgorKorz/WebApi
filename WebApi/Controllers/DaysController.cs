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
    public class DaysController : Controller
    {
        DaysContext db;

        public DaysController(DaysContext context)
        {
            db = context;

            if (db.Days.Count() == 0)
            {
                db.Days.Add(new Day { DayOfWeek = 1, DayOfMonth = 1});
                db.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<Day> Get()
        {
            return db.Days.ToList();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Day day = db.Days.FirstOrDefault(x => x.Id == id);

            if (day == null) NotFound();

            return new ObjectResult(day);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Day day)
        {
            if (day == null) BadRequest();

            db.Days.Add(day);
            db.SaveChanges();

            return Ok(day);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody]Day day)
        {
            if (day == null) BadRequest();

            if (!db.Days.Any(x => x.Id == day.Id)) return NotFound();

            db.Update(day);
            db.SaveChanges();

            return Ok(day);
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Day day = db.Days.FirstOrDefault(x => x.Id == id);

            if (day == null) NotFound();

            db.Days.Remove(day);
            db.SaveChanges();

            return Ok(day);
        }
    }
}
