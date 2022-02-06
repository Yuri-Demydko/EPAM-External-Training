using System;
using System.Collections.Generic;
using System.Linq;

namespace Task3_1_1
{
    public class CircleList<T>
    {

        private List<T> _listBase;
        private T _current;
        private int _curIndex = 0;

        public CircleList()
        {
            _listBase = new List<T>();
        }

        public CircleList(IEnumerable<T> baseList)
        {
            _listBase = new List<T>(baseList);
            _current = _listBase.FirstOrDefault();
        }

        public T Current => _current;
        public int Count => _listBase.Count;

        /// <summary>
        /// Add new element to circle
        /// </summary>
        /// <param name="element">New element</param>
        public void Add(T element)
        {
            _listBase.Add(element);
            _current ??= element;
        }

        /// <summary>
        /// Skip some elements and return current
        /// </summary>
        /// <param name="skip">Number of elements will be skipped. If u want got second element, you should skip 1, as example</param>
        /// <returns>Resulting element</returns>
        /// <exception cref="OverflowException">If skip value is greater than elements count</exception>
        public T GetNext(int skip)
        {
            if (skip > _listBase.Count)
                throw new OverflowException("Skip number mustn't be greater than size of the collection!");

            for (var i = 0; i <= skip; i++)
            {
                if (_curIndex == _listBase.Count - 1)
                {
                    _current = _listBase.First();
                    _curIndex = 0;
                }
                else
                {
                    _current = _listBase[_curIndex + 1];
                    _curIndex++;
                }
            }

            return _current;
        }

        /// <summary>
        /// Removes current element from a sequence
        /// </summary>
        /// <exception cref="Exception">If u try to remove last element</exception>
        public void RemoveCurrent()
        {
            if (_listBase.Count == 1)
                throw new Exception("Can't remove last element!");
            _listBase.Remove(Current);
            _curIndex--;
        }

        /// <summary>
        /// Get copy of circle's base list
        /// </summary>
        /// <returns>List with all elements of a sequence</returns>
        public List<T> GetAll() => new List<T>(_listBase);

    }
}