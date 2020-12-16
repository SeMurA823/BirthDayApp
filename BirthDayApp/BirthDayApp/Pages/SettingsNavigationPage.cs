using BirthDayApp.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BirthDayApp.Pages
{
    public class SettingsNavigationPage : NavigationPage
    {
        public SettingsNavigationPage(Page root): base(root)
        {
            Title = "Настройки";
            IconImageSource = "@drawable/SettingsIcon.png";
            BarTextColor = Color.White;
        }
    }
}
