using BirthDayApp.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BirthDayApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddItemPage : ContentPage
    {
        public event EventHandler CancelEvent;
        public event EventHandler DoneEvent;
        public AddItemPage()
        {
            InitializeComponent();
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            CancelEvent.Invoke(this, EventArgs.Empty);
        }
        private void Done_Clicked(object sender, EventArgs e)
        {
            if (FirstName.Text == null)
            {
                FirstName.TextColor = Color.DarkRed;
                FirstName.TextChanged += Text_Edit;
                return;
            }
            if (LastName.Text == null)
            {
                LastName.TextColor = Color.DarkRed;
                LastName.TextChanged += Text_Edit;
                return;
            }
            if (BirthDate.Date > DateTime.Now)
            {
                BirthDate.TextColor = Color.DarkRed;
                BirthDate.PropertyChanged += Date_Edit;
                return;
            }
            DoneEvent.Invoke(new Friend(FirstName.Text,LastName.Text,BirthDate.Date,Photo.Source.ToString()), EventArgs.Empty);
        }
        private void Text_Edit(object sender, EventArgs e)
        {
            Entry entry = sender as Entry;
            entry.TextColor = Color.Indigo;
            entry.TextChanged -= Text_Edit;            
        }
        private void Date_Edit(object sender, EventArgs e)
        {
            DatePicker datePicker = sender as DatePicker;
            datePicker.TextColor = Color.Indigo;
            datePicker.PropertyChanged -= Date_Edit;
        }

    }
}