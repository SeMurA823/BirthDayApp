using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace BirthDayApp.Items
{
    public class DateCounter
    {
        public event EventHandler TimeChangeEvent;
        public DateTime Date { get; set; }
        public string TimeStr { get; private set; }
        public TimeSpan Time { get; private set; }
        public DateCounter(DateTime date)
        {
            Date = date;
            UpdateTimer();
            Device.StartTimer(TimeSpan.FromSeconds(1.0), UpdateTimer);
        }
        private bool UpdateTimer()
        {
            Time = Date - DateTime.Now;
            TimeChangeEvent?.Invoke(this, EventArgs.Empty);
            return DateTime.Now < Date;
        }
    }
}
