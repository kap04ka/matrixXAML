using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace matrixXAML.Classes
{
    public class Matrix<T>
    {
        private int dimension;
        private T[,] array;

        public Matrix(int dimension)
        {
            this.dimension = dimension;
            array = new T[dimension, dimension];
        }

        public T this[int index1, int index2]
        {
            get => array[index1, index2];
            set => array[index1, index2] = value;
        }

        public static Matrix<T> operator +(Matrix<T> A, Matrix<T> B)
        {
            if (A.dimension != B.dimension) throw new ArgumentException();
            Matrix<T> resultMatix = new Matrix<T>(A.dimension);
            for (int i = 0; i < A.dimension; i++)
            {
                for (int j = 0; j < B.dimension; j++)
                {
                    resultMatix.array[i, j] = (dynamic)A.array[i, j] + (dynamic)B.array[i, j];
                }
            }
            return resultMatix;
        }

        public void Filling(int i, int j, Func<int, int, T> filling)
        {
            array[i, j] = (dynamic)filling(i, j);
        }

        public static Matrix<T> operator *(Matrix<T> A, Matrix<T> B)
        {
            if (A.dimension != B.dimension) throw new ArgumentException();
            Matrix<T> resultMatix = new Matrix<T>(A.dimension);

            for (int i = 0; i < A.dimension; i++)
            {
                for (int j = 0; j < B.dimension; j++)
                {
                    dynamic value = 0;
                    for (int k = 0; k < A.dimension; k++)
                    {
                        value += (dynamic)A.array[i, k] * (dynamic)B.array[k, j];
                    }

                    resultMatix.array[i, j] = value;
                }
            }
            return resultMatix;
        }

    }
}