using System;

namespace WebApi.Models
{
    public class MeetingRoom
    {
        public int Id { get; set; }
        public bool IsBusy { get; set; }
        public DateTime DateBusy { get; set; }
        public DateTime DateFree { get; set; }
    }
}