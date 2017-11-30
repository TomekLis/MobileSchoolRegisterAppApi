using System;

namespace Repository.Models.DTOs.DaySchedule
{
    public class DayScheduleDto
    {

        public int Id { get; set; }
        public Day Day { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

    }
}