using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace BirthDayApp.Pages.MainPages
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((DateTime)value).ToShortDateString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DateTime.Parse((string)value);
        }
    }

    public class BeforeDayConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime birthDate = (DateTime)value;
            DateTime birthDay = new DateTime(DateTime.Today.Year, birthDate.Month, birthDate.Day);
            if (birthDay < DateTime.Today) birthDay = birthDay.AddYears(1);
            int beforeDays = (birthDay - DateTime.Today).Days;
            string res = "Осталось " + beforeDays;
            if (beforeDays % 10 == 1) return res + " день";
            if (beforeDays >= 2 && beforeDays <= 4) return res + " дня";
            return res + " дней";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return int.Parse(((string)value).Split(' ')[1]);
        }
    }
    public class AgeConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime birthDate = (DateTime)value;
            int age = DateTime.Today.Year - birthDate.Year + 1;
            if (age >= 5 && age <= 20) return "Исполняется " + age + " лет";
            if (age % 10 == 1) return "Исполняется " + age + " год";
            if (age % 10 >= 2 && age % 10 <= 4) return "Исполняется " + age + " года";
            return "Исполняется " + age + " лет";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return int.Parse(((string)value).Split(' ')[1]);
        }
    }
}
