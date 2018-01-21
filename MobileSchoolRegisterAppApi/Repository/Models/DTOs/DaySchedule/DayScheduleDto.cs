using System;

namespace Repository.Models.DTOs.DaySchedule
{
    public class DayScheduleDto : IComparable<DayScheduleDto>
    {

        public int Id { get; set; }
        public DayOfWeek Day { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public int CompareTo(DayScheduleDto other)
        {
            if (this.Day == other.Day)
            {
                return StartTime >= other.StartTime ? 1 : 0;
            }
            else
            {
                return IsAfter(other) ? 1 : 0;
            }
        }

        private bool IsAfter(DayScheduleDto other)
        {
            DayOfWeek[] days = 
            {
                DayOfWeek.Monday,
                DayOfWeek.Tuesday,
                DayOfWeek.Wednesday,
                DayOfWeek.Thursday,
                DayOfWeek.Friday,
                DayOfWeek.Saturday,
                DayOfWeek.Sunday
            };
            int currentIndex = Array.IndexOf(days, DateTime.Now.DayOfWeek);
            return (Array.IndexOf(days, Day) - currentIndex) >= (Array.IndexOf(days, other.Day) - currentIndex);
        }
    }
}