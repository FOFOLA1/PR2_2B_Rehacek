using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_OOP_07_Loop
{
    internal class Loop<T>
    {
        private List<T> _items;
        private int _index;
        public T Current
        {
            get { return _items[_index]; }
        }



        public Loop(T[] numbs)
        {
            _items = numbs.ToList<T>();
            _index = 0;
        }

        public void Right(int value = 1)
        {
            _index += value;
            if (_index >= _items.Count) _index -= _items.Count;
        }

        public void Left(int value = 1)
        {
            _index -= value;
            if (_index < 0) _index += _items.Count;
        }

        public void Delete()
        {
            _items.RemoveAt(_index);
        }

        public void Insert(T item)
        {
            _items.Insert(_index + 1, item);
        }


    }
}
