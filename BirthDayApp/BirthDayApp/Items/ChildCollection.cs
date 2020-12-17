using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;

namespace BirthDayApp.Items
{
    public class ChildCollection<T>:ObservableCollection<T>
    {
        public Predicate<T> Filter { get; private set; }
        public ObservableCollection<T> Source { get; private set; }
        public ChildCollection(ObservableCollection<T> source, Predicate<T> filter)
        {
            Source = source;
            Filter = filter;
            source.CollectionChanged += ChangeSourceCollection;
            if (source.Count != 0)
            {
                foreach (T el in source)
                {
                    if (Filter(el)) base.Add(el);
                }
            }
        }
        public ChildCollection(ObservableCollection<T> source) : this(source, x=>true)
        {

        }
        private void ChangeSourceCollection(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (T el in args.NewItems)
                {
                    if (Filter(el)) Add(el);
                }
                return;
            }
            if (args.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (T el in args.OldItems)
                {
                    if (Filter(el)) Remove(el);
                }
                return;
            }
            if (args.Action == NotifyCollectionChangedAction.Reset)
            {
                Clear();
                return;
            }
            if (args.Action == NotifyCollectionChangedAction.Replace || args.Action == NotifyCollectionChangedAction.Move)
            {
                //Console.WriteLine("OOOOOOOOOOOOLLLLLLLLLLLDDDDDDDDDD ------- " + args.NewStartingIndex);
                T t = Source[args.OldStartingIndex];
                if (Filter(t))
                {
                    int index;
                    if ((index = IndexOf(t)) == -1) Add(t);
                    else base[index] = t;
                }
                else
                {
                    Remove(t);
                }
                return;
            }
        }
    }
}
