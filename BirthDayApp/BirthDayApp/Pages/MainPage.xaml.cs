using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BirthDayApp.Pages.MainPages;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace BirthDayApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : Xamarin.Forms.TabbedPage
    {
        public TodayMainPage TodayMainPage { get; set; }
        public NearestMainPage NearestMainPage { get; set; }
        public MainPage()
        {
            TodayMainPage = new TodayMainPage();
            NearestMainPage = new NearestMainPage();
            Children.Add(TodayMainPage);
            Children.Add(NearestMainPage);
            InitializeComponent();
        }
    }
}