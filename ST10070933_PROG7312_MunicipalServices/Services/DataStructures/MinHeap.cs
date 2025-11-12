using Microsoft.AspNetCore.Mvc;

namespace ST10070933_PROG7312_MunicipalServices.Services.DataStructures
{
    // This datastructure is used for managing urgent/high-priority service requests .
    public class MinHeap<T> where T : IComparable<T>
    {
        private readonly List<T> _elements = new List<T>();

        public int Count => _elements.Count;

        public void Add(T item)
        {
            _elements.Add(item);
            HeapifyUp(_elements.Count - 1);
        }

        public T Peek()
        {
            if (_elements.Count == 0)
                throw new InvalidOperationException("Heap is empty.");
            return _elements[0];
        }

        public T Pop()
        {
            if (_elements.Count == 0)
                throw new InvalidOperationException("Heap is empty.");

            T result = _elements[0];
            _elements[0] = _elements[^1];
            _elements.RemoveAt(_elements.Count - 1);
            HeapifyDown(0);
            return result;
        }

        private void HeapifyUp(int index)
        {
            while (index > 0)
            {
                int parent = (index - 1) / 2;
                if (_elements[index].CompareTo(_elements[parent]) >= 0)
                    break;

                Swap(index, parent);
                index = parent;
            }
        }

        private void HeapifyDown(int index)
        {
            while (true)
            {
                int left = index * 2 + 1;
                int right = index * 2 + 2;
                int smallest = index;

                if (left < _elements.Count && _elements[left].CompareTo(_elements[smallest]) < 0)
                    smallest = left;

                if (right < _elements.Count && _elements[right].CompareTo(_elements[smallest]) < 0)
                    smallest = right;

                if (smallest == index) break;

                Swap(index, smallest);
                index = smallest;
            }
        }

        private void Swap(int i, int j)
        {
            (_elements[i], _elements[j]) = (_elements[j], _elements[i]);
        }
    }
}
