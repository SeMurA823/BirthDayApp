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
        public bool IsFavorite { get; set; }
        public DateTime BirthDate { get; set; }
        [NonSerialized()] private DateTime BirthDay;
        public string PathImage { get; set; }
        [JsonConstructor]
        public Friend(string firstName, string lastName, DateTime birthDate, string pathImage, bool isFavorite = false)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            PathImage = pathImage;
            BirthDay = new DateTime(DateTime.Today.Year, birthDate.Month, birthDate.Day);
            BirthDay = (DateTime.Today > BirthDay) ? BirthDay.AddYears(1) : BirthDay;
            BirthDateStr = BirthDate.ToShortDateString();
            IsFavorite = isFavorite;
        }
        public DateTime GetBirthDay() => BirthDay;
    }
}
