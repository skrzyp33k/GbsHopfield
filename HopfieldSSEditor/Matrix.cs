using System;

namespace HopfieldSSEditor
{
    public class Matrix
    {
        public double[,] rawData { get; private set; }
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public void setElement(int row, int column, double value)
        {
            rawData[row, column] = value;
        }

        public double getElement(int row, int column)
        {
            return rawData[row, column];
        }

        public void print()
        {
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Columns; j++)
                {
                    Console.Write(rawData[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public Matrix Transposition()
        {
            Matrix transposed = new Matrix(this.Columns, this.Rows);

            for (int i = 0; i < transposed.Rows; i++)
            {
                for (int j = 0; j < transposed.Columns; j++)
                {
                    transposed.setElement(i, j, this.getElement(j, i));
                }
            }

            return transposed;
        }

        public Matrix()
        {
            this.Rows = 0;
            this.Columns = 0;
            rawData = new double[this.Rows, this.Columns];
        }

        public Matrix(int rows, int columns)
        {
            this.Rows = rows;
            this.Columns = columns;
            rawData = new double[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    rawData[i, j] = 0;
                }
            }
        }

        public static Matrix operator *(Matrix firstMatrix, Matrix secondMatrix)
        {
            if (firstMatrix.Columns == secondMatrix.Rows)
            {
                Matrix result = new Matrix(firstMatrix.Rows, secondMatrix.Columns);

                for (int i = 0; i < firstMatrix.Rows; i++)
                {
                    for (int j = 0; j < secondMatrix.Columns; j++)
                    {
                        for (int k = 0; k < secondMatrix.Rows; k++)
                        {
                            double oldVal = result.getElement(i, j);
                            result.setElement(i, j, oldVal + firstMatrix.getElement(i, k) * secondMatrix.getElement(k, j));
                        }
                    }
                }

                return result;
            }
            else
            {
                throw new ArgumentException("First Matrix column count must be equal to second Matrix row count!");
            }
        }

        public static Matrix operator *(double number, Matrix matrix)
        {
            Matrix result = new Matrix(matrix.Rows, matrix.Columns);

            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    result.setElement(i, j, number * matrix.getElement(i, j));
                }
            }

            return result;
        }

        public static Matrix operator *(Matrix matrix, double number)
        {
            Matrix result = new Matrix(matrix.Rows, matrix.Columns);

            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    result.setElement(i, j, number * matrix.getElement(i, j));
                }
            }

            return result;
        }

        public static Matrix operator +(Matrix firstMatrix, Matrix secondMatrix)
        {
            if ((firstMatrix.Rows == secondMatrix.Rows) && (firstMatrix.Columns == secondMatrix.Columns))
            {
                Matrix result = new Matrix(firstMatrix.Rows, secondMatrix.Columns);

                for (int i = 0; i < secondMatrix.Rows; i++)
                {
                    for (int j = 0; j < firstMatrix.Columns; j++)
                    {
                        result.setElement(i, j, firstMatrix.getElement(i, j) + secondMatrix.getElement(i, j));
                    }
                }

                return result;
            }
            else
            {
                throw new ArgumentException("Matrix dimensions must be equal!");
            }
        }

        public static Matrix operator -(Matrix firstMatrix, Matrix secondMatrix)
        {
            if ((firstMatrix.Rows == secondMatrix.Rows) && (firstMatrix.Columns == secondMatrix.Columns))
            {
                Matrix result = new Matrix(firstMatrix.Rows, secondMatrix.Columns);

                for (int i = 0; i < secondMatrix.Rows; i++)
                {
                    for (int j = 0; j < firstMatrix.Columns; j++)
                    {
                        result.setElement(i, j, firstMatrix.getElement(i, j) - secondMatrix.getElement(i, j));
                    }
                }

                return result;
            }
            else
            {
                throw new ArgumentException("Matrix dimensions must be equal!");
            }
        }
    }
}
