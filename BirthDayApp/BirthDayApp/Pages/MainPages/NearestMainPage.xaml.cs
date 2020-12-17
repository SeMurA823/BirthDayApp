using BirthDayApp.Items;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BirthDayApp.Pages.MainPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NearestMainPage : ContentPage
    {
        private ObservableCollection<Friend> friendList;
        public NearestMainPage()
        {
            InitializeComponent();
        }
        public void SetItemSource(ObservableCollection<Friend> friends)
        {
            soonList.ItemsSource = (friendList = friends);
        }
    }
}