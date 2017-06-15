using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.KdTree
{
    /// <summary>
    /// Represents a k-dimensional tree.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class KdTree<TValue> where TValue : class
    {
        const string Err_Invalid_Coordinates = "Number of coordinates must be equal to the number of dimensions.";
        const string Err_Invalid_Dimensions = "The number of dimensions must be 2 or greater.";

        protected List<Coord<TValue>> points;
        protected Node<TValue> root;

        protected readonly int dimensions;

        public KdTree(int dimensions)
        {
            if (dimensions < 2)
                throw new ArgumentException(Err_Invalid_Dimensions);

            this.dimensions = dimensions;

            points = new List<Coord<TValue>>();
        }

        public virtual void Add(TValue item, double[] coordinates)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (coordinates.Length != dimensions)
                throw new ArgumentException(Err_Invalid_Coordinates);

            points.Add(new Coord<TValue>(item, coordinates));
        }

        public virtual void Build()
        {
            root = Build(points.ToArray(), 0);
        }

        public virtual TValue Nearest(double[] coordinates)
        {
            if (coordinates.Length != dimensions)
                throw new ArgumentException(Err_Invalid_Coordinates);

            var result = Query(root, coordinates, 0);

            return result;
        }

        protected virtual TValue Query(Node<TValue> node, double[] coordinates, int dimension)
        {
            int index = dimension % dimensions;

            if (node == null)
            {
                return null;
            }
            else if (node.Item != null)
            {
                // this is a leaf
                return node.Item;
            }

            if (coordinates[index] < node.Value)
            {
                return Query(node.Left, coordinates, dimension + 1);
            }
            else
            {
                return Query(node.Right, coordinates, dimension + 1);
            }
        }

        protected virtual Node<TValue> Build(Coord<TValue>[] points, int dimension)
        {
            int index = dimension % dimensions;

            if (points.Length == 0)
            {
                return null;
            }
            else if (points.Length == 1)
            {
                return new Node<TValue>() { Item = points.First().Value };
            }

            (double median, var leftPoints, var rightPoints) =
                SplitByMedian(points.OrderBy(a => a.Coordinates[index]).ToArray(), index);

            var node = new Node<TValue>()
            {
                Value = median
            };

            node.Left = Build(leftPoints, dimension + 1);
            node.Right = Build(rightPoints, dimension + 1);

            node.Dimension = index;

            return node;
        }

        protected virtual (double medianValue, Coord<TValue>[] left, Coord<TValue>[] right)
                SplitByMedian(Coord<TValue>[] array, int index)
        {
            if (array.Length % 2 == 0)
            {
                // count is even, need to get the middle two elements, add them together, then divide by 2
                double middleElement1 = array[(array.Length / 2) - 1].Coordinates[index];
                double middleElement2 = array[array.Length / 2].Coordinates[index];

                return (
                    (middleElement1 + middleElement2) / 2,
                    array.Take(array.Length / 2).ToArray(),
                    array.Skip(array.Length / 2).ToArray());
            }
            else
            {
                // count is odd, simply get the middle element.
                return (
                    array[array.Length / 2].Coordinates[index],
                    array.Take(array.Length / 2).ToArray(),
                    array.Skip(array.Length / 2).ToArray());
            }
        }
    }

    public class Node<TValue>
    {
        public Node<TValue> Left { get; set; }
        public Node<TValue> Right { get; set; }
        public double Value { get; set; }
        public TValue Item { get; set; }
        public int Dimension { get; set; }
    }

    public class Coord<TValue>
    {
        public Coord(TValue value, double[] coordinates)
        {
            Value = value;
            Coordinates = coordinates;
        }

        public double[] Coordinates { get; }
        public TValue Value { get; }
    }
}
