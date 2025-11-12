using Microsoft.AspNetCore.Mvc;

namespace ST10070933_PROG7312_MunicipalServices.DataStructures
{
    // This datastructure is used for efficiently organizing and retrieving service requests.
    public class AVLTree<T>
    {
        private BSTNode<T> _root;

        // Get height of node
        private int Height(BSTNode<T> node) => node?.Height ?? 0;

        // Get balance factor
        private int GetBalance(BSTNode<T> node) => node == null ? 0 : Height(node.Left) - Height(node.Right);

        // Right rotation
        private BSTNode<T> RotateRight(BSTNode<T> y)
        {
            var x = y.Left;
            var T2 = x.Right;

            x.Right = y;
            y.Left = T2;

            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;
            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;

            return x;
        }

        // Left rotation
        private BSTNode<T> RotateLeft(BSTNode<T> x)
        {
            var y = x.Right;
            var T2 = y.Left;

            y.Left = x;
            x.Right = T2;

            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;
            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;

            return y;
        }

        // Insert node
        public void Insert(int key, T value)
        {
            _root = Insert(_root, key, value);
        }

        private BSTNode<T> Insert(BSTNode<T> node, int key, T value)
        {
            if (node == null)
                return new BSTNode<T>(key, value);

            if (key < node.Key)
                node.Left = Insert(node.Left, key, value);
            else if (key > node.Key)
                node.Right = Insert(node.Right, key, value);
            else
                return node; 

            node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));
            int balance = GetBalance(node);

            // Rotations for balancing
            if (balance > 1 && key < node.Left.Key)
                return RotateRight(node);

            if (balance < -1 && key > node.Right.Key)
                return RotateLeft(node);

            if (balance > 1 && key > node.Left.Key)
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }

            if (balance < -1 && key < node.Right.Key)
            {
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }

            return node;
        }

        // Search by key
        public T Search(int key)
        {
            var node = SearchNode(_root, key);
            return node != null ? node.Value : default;
        }

        private BSTNode<T> SearchNode(BSTNode<T> node, int key)
        {
            if (node == null || node.Key == key)
                return node;

            if (key < node.Key)
                return SearchNode(node.Left, key);

            return SearchNode(node.Right, key);
        }

        // In-order 
        public List<T> InOrderTraversal()
        {
            var list = new List<T>();
            InOrder(_root, list);
            return list;
        }

        private void InOrder(BSTNode<T> node, List<T> list)
        {
            if (node == null)
                return;

            InOrder(node.Left, list);
            list.Add(node.Value);
            InOrder(node.Right, list);
        }
    }
}
