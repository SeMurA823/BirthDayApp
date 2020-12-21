using BirthDayApp.Items;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BirthDayApp.Pages.MainPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NearestMainPage : ContentPage
    {
        private ObservableCollection<Friend> friendList;
        private ChildCollection<Friend> sortfriends;
        public NearestMainPage()
        {
            InitializeComponent();
        }

        private void collectChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            Sort();
        } 

        public void SetItemSource(ObservableCollection<Friend> friends)
        {

            nearestList.ItemsSource = sortfriends = new ChildCollection<Friend>(friendList = friends);
            friendList.CollectionChanged += collectChange;
            Sort();
        }
        public void Sort()
        {
            List<Friend> sort = friendList.OrderBy(x => x.BeforeBirthDay()).ToList<Friend>();
            int index = 0;
            sortfriends.Clear();
            foreach(var fr in sort)
            {
                sortfriends.Add(fr);
            }
        }
    }
}