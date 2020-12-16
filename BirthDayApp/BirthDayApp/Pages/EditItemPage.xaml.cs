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
    public partial class EditItemPage : ContentPage
    {
        public Friend Friend { get; set; }
        public event EventHandler CancelEvent;
        public event EventHandler DoneEvent;
        public event EventHandler RemoveEvent;
        public EditItemPage(Friend friend)
        {
            Friend = friend;
            InitializeComponent();
            stackInfo.BindingContext = Friend;
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            CancelEvent.Invoke(this, EventArgs.Empty);
        }
        private void Done_Clicked(object sender, EventArgs e)
        {
            if (firstName.Text == null)
            {
                firstName.TextColor = Color.DarkRed;
                firstName.TextChanged += Text_Edit;
                return;
            }
            if (lastName.Text == null)
            {
                lastName.TextColor = Color.DarkRed;
                lastName.TextChanged += Text_Edit;
                return;
            }
            if (bdate.Date > DateTime.Now)
            {
                bdate.TextColor = Color.DarkRed;
                bdate.PropertyChanged += Date_Edit;
                return;
            }
            Friend.FirstName = firstName.Text;
            Friend.LastName = lastName.Text;
            Friend.BirthDate = bdate.Date;
            Friend.BirthDateStr = bdate.Date.ToShortDateString();
            DoneEvent.Invoke(this, EventArgs.Empty);
        }
        private void Remove_Clicked(object sender, EventArgs e)
        {
            RemoveEvent.Invoke(this, e);
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