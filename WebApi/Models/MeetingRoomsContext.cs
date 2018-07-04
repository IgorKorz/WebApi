using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Models
{
    public class MeetingRoomsContext : DbContext
    {
        public MeetingRoomsContext(DbContextOptions<MeetingRoomsContext> options)
            : base(options)
        { }

        public DbSet<MeetingRoom> MeetingRooms { get; set; }
    }
}
