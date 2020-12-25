using BirthDayApp.themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BirthDayApp.Pages.SettingPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditColorSchemePage : ContentPage
    {
        public event EventHandler<ThemeEventArgs> EditColorSchemeEvent;
        public EditColorSchemePage()
        {
            InitializeComponent();
        }

        private void yellowBtn_Clicked(object sender, EventArgs e)
        {
            EditColorSchemeEvent.Invoke(sender, new ThemeEventArgs(EThemes.BLACK_YELLOW));
        }

        private void indigoBtn_Clicked(object sender, EventArgs e)
        {
            EditColorSchemeEvent.Invoke(sender, new ThemeEventArgs(EThemes.INDIGO));
        }

        private void blueBtn_Clicked(object sender, EventArgs e)
        {
            EditColorSchemeEvent.Invoke(sender, new ThemeEventArgs(EThemes.BLUE));
        }

        public class ThemeEventArgs
        {
            public EThemes Theme { get; private set; }
            public ThemeEventArgs(EThemes theme)
            {
                Theme = theme;
            }
        }
    }
}