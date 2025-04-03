using System;
using System.Collections.Generic;

namespace Question1
{
    public class Group<T>
    {
        public string Name { get; set; }
        private List<T> listT;

        public Group(string name)
        {
            Name = name;
            listT = new List<T>();
        }

        public void Add(T item)
        {
            listT.Add(item);
        }

        public void Remove(T item)
        {
            listT.Remove(item);
        }

        public void Show(Presentation<T> presentation)
        {
            Console.WriteLine($"Group {Name} has {listT.Count} students. List of students:");
            foreach (var item in listT)
            {
                presentation(item);
            }
        }
    }
}
