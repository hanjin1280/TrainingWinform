using System;

namespace GenericApp
{
    public class SimpleGeneric<T>
    {
        private T[] values;
        private int index;
        
        public SimpleGeneric(int len) // Construtor
        {
            values = new T[len];
            index = 0;
        }

        public void Add(params T[] args)
        {
            foreach(T e in args)
            {
                values[index++] = e;
            }
        }
        public void Print()
        {
            foreach(T e in values)
            {
                Console.Write(e + " ");
            }
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            SimpleGeneric<Int32> gInteger = new SimpleGeneric<Int32>(10);
            SimpleGeneric<Double> gDouble = new SimpleGeneric<Double>(10);

            gInteger.Add(1, 2);
            gInteger.Add(1, 2, 3, 4, 5, 6, 7);
            gInteger.Add(10);
            gInteger.Print();

            gDouble.Add(10.0, 12.5, 37.4);
            gDouble.Print();
        }
    }
}
