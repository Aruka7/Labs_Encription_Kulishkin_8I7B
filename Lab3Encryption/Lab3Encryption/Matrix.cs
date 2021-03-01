using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3Encryption
{
    public class Matrix
    {
        public int CountRows { get; private set; }
        public int CountColumns { get; private set; }
        private double[,] _field { get; set; }

        public Matrix(int i, int j)
        {
            CountRows = i;
            CountColumns = j;
            _field = new double[i,j];
        }
        public double this[int i, int j]
        {

            get
            {
                if (i >= CountRows || j >= CountColumns)
                {
                    throw new Exception("Выход за пределы матрицы");
                }
                return _field[i, j];
            }
            set
            {
                if (i >= CountRows || j >= CountColumns)
                {
                    throw new Exception("Выход за пределы матрицы");
                }

                _field[i, j] = value;
            }
        }

        public void SetField(double[,] array)
        {
            _field = array;
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if(m1.CountColumns != m2.CountRows) throw new Exception("Невозможно перемножить данные матрицы");
            Matrix m = new Matrix(m1.CountRows, m2.CountColumns);
            for (int i = 0; i < m1.CountRows; i++)
            {
                for (int j = 0; j < m2.CountColumns; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < m1.CountColumns; k++)
                    {
                        sum += m1[i, k] * m2[k, j];
                    }

                    m[i, j] = sum;
                }
            }

            return m;
        }

        public Matrix Inverse()
        {
            if(CountColumns != CountRows) throw new Exception("Для данной матрицы невозможно определить обратную");
            Matrix E = new Matrix(CountColumns, CountColumns);
            Matrix A = new Matrix(CountColumns, CountColumns);
            for (int i = 0; i < CountColumns; i++)
            {
                for (int j = 0; j < CountColumns; j++)
                {
                    A[i, j] = _field[i, j];
                }
            }
            for (int i = 0; i < CountColumns; i++)
            for (int j = 0; j < CountColumns; j++)
            {
                E[i,j] = 0.0;

                if (i == j)
                    E[i,j] = 1.0;
            }
            double temp;
            for (int k = 0; k < CountColumns; k++)
            {
                temp = A[k,k];

                for (int j = 0; j < CountColumns; j++)
                {
                    A[k, j] /= temp;
                    E[k,j] /= temp;
                }

                for (int i = k + 1; i < CountColumns; i++)
                {
                    temp = A[i,k];

                    for (int j = 0; j < CountColumns; j++)
                    {
                        A[i,j] -= A[k,j] * temp;
                        E[i,j] -= E[k,j] * temp;
                    }
                }
            }
            for (int k = CountColumns - 1; k > 0; k--)
            {
                for (int i = k - 1; i >= 0; i--)
                {
                    temp = A[i,k];

                    for (int j = 0; j < CountColumns; j++)
                    {
                        A[i,j] -= A[k,j] * temp;
                        E[i,j] -= E[k,j] * temp;
                    }
                }
            }
            for (int i = 0; i < CountColumns; i++)
            for (int j = 0; j < CountColumns; j++)
                A[i,j] = E[i,j];
            return A;
        }
    }
}
