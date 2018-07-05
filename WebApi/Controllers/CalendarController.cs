using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/calendar")]
    public class CalendarController : Controller
    {
        List<List<DateTime>> weeks = new List<List<DateTime>>();
        DateTime now = DateTime.Now;
        MeetingRoomsContext db;
        long oneDay;


        public CalendarController(MeetingRoomsContext context)
        {
            db = context;
            DateTime a = new DateTime(),
                     b = new DateTime(1, 1, 2);
            oneDay = b.Subtract(a).Ticks;
        }

        [HttpGet]
        public IActionResult GetScheduler(DateTime currentDay)
        {
            currentDay = DateTime.Now;
            long startDay = (currentDay.Ticks - currentDay.TimeOfDay.Ticks) - this.oneDay * 14;

            for (int i = 0; i < 5; i++)
            {
                weeks.Add(new List<DateTime>());

                for (int j = 0; j < 7; j++)
                {
                    weeks[i].Add(new DateTime(startDay));
                    startDay += oneDay;
                }
            }

            return Ok(weeks);
        }

        [HttpGet("{day; month; year}")]
        public IActionResult GetDay(int day, int month, int year)
        {
            DateTime date = new DateTime(year, month, day);

            return Ok(date);
        }

        [HttpPost]
        public IActionResult Post([FromBody]DayOfBusy dayOfBusy)
        {
            if (db.DaysOfBusy.FirstOrDefault(x => x.Date == dayOfBusy.Date 
                                               && x.IdRoom == dayOfBusy.IdRoom) != null)
                return BadRequest();

            db.DaysOfBusy.Add(dayOfBusy);
            db.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody]DayOfBusy)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
