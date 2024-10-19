using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_OOP_06_Paginator
{
    internal class Paginator
    {

        private string[] _items;
        //private string[][] _pages;
        private int _pageCount;
        private int _pageSize;


        public int ItemCount
        {
            get { return _items.Length; }
        }

        public int PageCount
        {
            get { return _pageCount; }
            private set { _pageCount = value; }
        }



        public Paginator(string[] items, int itemsPerPage)
        {
            _items = items;
            _pageSize = itemsPerPage;
            PageCount = _items.Length / _pageSize;
            if (_items.Length % _pageSize > 0) PageCount++;
            //_pages = new string[PageCount][];

            //for (int i = 0; i < _items.Length; i++)
            //{
                //_pages[(i - i % PageCount) / PageCount] = new string[];
                //_pages[(i - i % PageCount) / PageCount][i % _pageSize] = _items[i];
            //}
        }

        public int GetPageItemCount(int page)
        {
            if (page > PageCount) return 0;
            else if (page == PageCount-1) return _items.Length % PageCount;

            return _pageSize;
        }

        public string[] GetPage(int page)
        {
            if (page > PageCount) return new string[0];
            return _items
                .ToList()
                .GetRange(_pageSize*page, GetPageItemCount(page))
                .ToArray();
            //return _pages[page];
        }

        public int FindPage(string item)
        {
            int index = _items.ToList().IndexOf(item);
            if (index == -1) return index;
            return index / _pageSize;
        }



    }
}
