using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirthDayApp.Items
{
    public class Friend
    {
        
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime BirthDate { get; private set; }
        [NonSerialized()] private DateTime BirthDay;
        public string PathImage { get; private set; }
        [JsonConstructor]
        public Friend(string firstName, string lastName, DateTime birthDate, string pathImage)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            PathImage = pathImage;
            BirthDay = new DateTime(DateTime.Today.Year, birthDate.Month, birthDate.Day);
            BirthDay = (DateTime.Today > BirthDay) ? BirthDay.AddYears(1) : BirthDay;
        }
        public DateTime GetBirthDay() => BirthDay;
    }
}
