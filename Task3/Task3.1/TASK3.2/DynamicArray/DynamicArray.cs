using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DynamicArray
{
    public class DynamicArray<T>:IEnumerable, IEnumerable<T>
    {
        private const int DEFAULT_CAPACITY=8;
        private T[] _array;
        private int _capacity;
        private int counter=0;

        public DynamicArray()
        {
            _capacity = DEFAULT_CAPACITY;
            _array = new T[_capacity];
        }

        public DynamicArray(int capacity)
        {
            _capacity = capacity;
            _array = new T[_capacity];
        }

        public DynamicArray(IEnumerable<T> basis)
        {
            _capacity = basis.Count();
            _array = new T[_capacity];
            foreach (var el in basis)
            {
                this.Add(el);
            }
        }

        public int Length => counter;

        public int Capacity => _capacity;

        public void Add(T element)
        {
            if (counter < _array.Length)
                _array[counter] = element;
            else
            {
                //lookTo(Line 73 comment). But if u want to double...
                _capacity *= 2;
                reformArray();
            }
            _array[counter] = element;
            counter++;
        }

        private void reformArray()
        {
            var copy = new T[_capacity];
            for (var i = 0; i < _array.Length; i++)
            {
                copy[i] = _array[i];
            }

            _array = copy;
        }


        public void AddRange(IEnumerable<T> elements)
        {
            var len = elements.Count()+Length;
            
            if(_capacity < len)
            {
                //Double capacity here may potentially take more RAM than it should
                _capacity =  len +DEFAULT_CAPACITY;
                reformArray();
            }

            foreach (var el in elements)
            {
                Add(el);
            }
            
        }

        public bool Insert(T element, int index)
        {
            if (index < 0) throw new ArgumentOutOfRangeException($"Index must be non-nagative. Got: {index}!");
            if (index > _capacity)
            {
               _capacity = _capacity + index + DEFAULT_CAPACITY;
                reformArray();
                counter = index+1;
            }
            _array[index] = element;
            return true;
        }
        
        public bool Remove(T element)
        {
            for (var i = 0; i < counter; i++)
            {
                var el = _array[i];
                if(el.Equals(element))
                {
                    for (var j = i; j < _array.Length - Math.Max(i,1); j++)
                    {
                        _array[j] = _array[j + 1];
                    }
                    counter--;
                    return true;
                }
            }
            return false;
        }

        public T this [int index]
        {
            get => index<_capacity?_array[index]: throw new ArgumentOutOfRangeException("Index must be non-nagative and less than capacity. Got: {index}!");
            set => Insert(value, index);
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < counter; i++)
            {
                var el = _array[i];
                yield return el;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}