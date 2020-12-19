using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirthDayApp.Items
{
    public class Friend
    {
        public static IComparer<Friend> ComparerByDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDateStr { get; set; }
        public bool IsLocal { get; private set; }
        public DateTime BirthDate { get; set; }
        public string PathImage { get; set; }
        static Friend()
        {
            ComparerByDate = new FriendComparerByDate();
        }

        [JsonConstructor]
        public Friend(string firstName, string lastName, DateTime birthDate, string pathImage, bool isLocal = true)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            BirthDateStr = BirthDate.ToShortDateString();
            IsLocal = isLocal;
            PathImage = pathImage;
        }
        public DateTime GetBirthDay()
        {
            DateTime BirthDay = new DateTime(DateTime.Today.Year, BirthDate.Month, BirthDate.Day);
            return (DateTime.Today > BirthDay) ? BirthDay.AddYears(1) : BirthDay;
        }
        public int GetAge() => DateTime.Today.Year - BirthDate.Year;
        public bool TodayBirthDay() => DateTime.Today == GetBirthDay();
        public int BeforeBirthDay() => (GetBirthDay() - DateTime.Today).Days;
        private class FriendComparerByDate : IComparer<Friend>
        {
            public int Compare(Friend x, Friend y)
            {
                return x.BeforeBirthDay() - y.BeforeBirthDay();
            }
        }
    }
}
