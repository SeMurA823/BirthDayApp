using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirthDayApp.Items
{
    public class Friend
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDateStr { get; set; }
        public bool IsLocal { get; private set; }
        public DateTime BirthDate { get; set; }
        public string PathImage { get; set; }
        [JsonConstructor]
        public Friend(string firstName, string lastName, DateTime birthDate, string pathImage, bool isLocal = true)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            BirthDateStr = BirthDate.ToShortDateString();
            IsLocal = isLocal;
        }
        public DateTime GetBirthDay()
        {
            DateTime BirthDay = new DateTime(DateTime.Today.Year, BirthDate.Month, BirthDate.Day);
            return (DateTime.Today > BirthDay) ? BirthDay.AddYears(1) : BirthDay;
        }
        public int GetAge() => DateTime.Today.Year - BirthDate.Year;
        public bool TodayBirthDay() => DateTime.Today == GetBirthDay();
        public int BeforeBirthDay() => (GetBirthDay() - DateTime.Today).Days;
    }
}
