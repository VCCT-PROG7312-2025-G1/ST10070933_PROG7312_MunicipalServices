using Microsoft.AspNetCore.Mvc;

namespace ST10070933_PROG7312_MunicipalServices.DataStructures
{
    public class BSTNode<T>
    {
        public int Key { get; set; }          // NumericId or Priority
        public T Value { get; set; }          // ServiceRequest or any object
        public BSTNode<T> Left { get; set; }
        public BSTNode<T> Right { get; set; }
        public int Height { get; set; }

        public BSTNode(int key, T value)
        {
            Key = key;
            Value = value;
            Height = 1;
        }
    }
}
