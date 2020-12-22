using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BirthDayApp.Pages.NavigationPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsNavigationPage : NavigationPage
    {
        public SettingsNavigationPage(SettingsPage root) : base(root)
        {
            InitializeComponent();
        }
    }
}