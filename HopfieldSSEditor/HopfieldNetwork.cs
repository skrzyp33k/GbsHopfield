using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopfieldSSEditor
{
    public static class HopfieldNetwork
    {
        private static Matrix weightMatrix = new Matrix(MainWindow.N* MainWindow.M, MainWindow.N * MainWindow.M);

        static bool[,] newPixels;
        static bool lastPixel;

        private static int sgn(double d)
        {
            if (d > 0) return 1;
            else if (d < 0) return -1;
            else return 0;
        }

        private static double biSkok(double d)
        {
            int s = sgn(d);

            if (s == 0)
                return lastPixel ? 1 : -1;
            return s;
        }

        public static Matrix PixelsTo1DVector(bool[,] pixels)
        {
            Matrix m = new Matrix(MainWindow.N * MainWindow.M, 1);

            for (int j = 0; j < MainWindow.M; j++)
                for (int i = 0; i < MainWindow.N; i++)
                    m.setElement(i + j * MainWindow.M, 0, pixels[i, j] ? 1 : -1);

            m.print();

            return m;
        }

        public static void AddCurrentImageToWeightMatrix(bool[,] pixels)
        {
            Matrix tempMatrix = new Matrix(MainWindow.N * MainWindow.M, MainWindow.N * MainWindow.M);
            Matrix x = PixelsTo1DVector(pixels);
            Matrix xT = x.Transposition();
            

            for (int i = 0; i < MainWindow.N * MainWindow.M; i++)
            {
                for (int j = 0; j < MainWindow.N * MainWindow.M; j++)
                {
                    if (i != j)
                    {
                        tempMatrix.setElement(i, j, (x * xT).getElement(i,j));
                    }
                }
            }
            Console.WriteLine();
            weightMatrix += tempMatrix;
            weightMatrix.print();
        }

        public static void computePixel(int i, int j)
        {
            do
            {
                lastPixel = newPixels[i, j];

                Matrix neuronMatrix = new Matrix(1, MainWindow.N * MainWindow.M);
                for (int a = 0; a < MainWindow.N * MainWindow.M; a++)
                    neuronMatrix.setElement(0, a, weightMatrix.getElement(i, a));

                Matrix imageMatrix = PixelsTo1DVector(newPixels);

                newPixels[i,j] = biSkok((neuronMatrix*imageMatrix).getElement(0,0)) == 1 ? true : false;

            } while (lastPixel != newPixels[i, j]);
        }

        public static bool[,] RunHopfield(bool[,] pixels)
        {
            newPixels = pixels;

            for (int i = 0; i < MainWindow.N; i++)
                for (int j = 0; j < MainWindow.M; j++)
                    computePixel(i, j);

            return newPixels;
        }
    }
}
