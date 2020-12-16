using System;
using System.Collections.Generic;
using System.Text;

namespace BirthDayApp.Items
{
    public class TypeSortFriend
    {
        public static TypeSortFriend NORMAL { get; private set; }
        public static TypeSortFriend ABC_A_Z { get; private set; }
        public static TypeSortFriend ABC_Z_A { get; private set; }

        static TypeSortFriend()
        {
            NORMAL = new TypeSortFriend("По умолчанию", null);
            ABC_A_Z = new TypeSortFriend("По алфавиту (Возрастание)", null);
            ABC_Z_A = new TypeSortFriend("По алфавиту (Убывание)", null);
        }
        public IComparer<Friend> Comparer { get; private set; }
        public string Name { get; private set; }
        private TypeSortFriend(string name, IComparer<Friend> comparer)
        {
            Name = name;
            Comparer = comparer;
        }
    }
}
