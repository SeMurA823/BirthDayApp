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
        public const string DEFAULT_PATHIMAGE = "@drawable/ProfileLogo.png";
        public Friend Friend { get; set; }
        public event EventHandler CancelEvent;
        public event EventHandler DoneEvent;
        private SocialManager.items.Friend webFriend;
        public AddItemPage()
        {
            InitializeComponent();
            Photo.Source = DEFAULT_PATHIMAGE;
        }
        public AddItemPage(SocialManager.items.Friend friend)
        {
            InitializeComponent();
            webFriend = friend;
            FirstName.Text = friend.FirstName;
            LastName.Text = friend.LastName;
            Photo.Source = friend.Photo200;
            try
            {
                BirthDate.Date = DateTime.Parse(friend.BDate);
            } catch (SystemException){

            }
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
            Friend = new Friend(
                FirstName.Text,
                LastName.Text,
                BirthDate.Date,
                (webFriend == null) ? DEFAULT_PATHIMAGE : webFriend.Photo200,
                (webFriend == null) ? DEFAULT_PATHIMAGE : webFriend.Photo50
                );
            DoneEvent.Invoke(this, EventArgs.Empty);
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