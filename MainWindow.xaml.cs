using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using matrixXAML.Classes;

namespace matrixXAML
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TextBox[,] MatrixATextBoxes, MatrixBTextBoxes;
        private int lastSize;

        public MainWindow()
        {
            InitializeComponent();
        }

        private TextBox[,] UpdateGrid(UniformGrid uniformGrid, int size)
        {
            lastSize = size;
            TextBox[,] result = new TextBox[size, size];
            //Задание опций гридов и отчистка гридов
            uniformGrid.Rows = size;
            uniformGrid.Columns = size;
            uniformGrid.Children.Clear();

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result[i, j] = new TextBox();
                    result[i, j].Text = "0";
                    uniformGrid.Children.Add(result[i, j]);
                }
            }
            return result;
        }

        private void btGenerateMatrix_Click(object sender, RoutedEventArgs e)
        {
            MatrixATextBoxes = UpdateGrid(MatrixA, Convert.ToInt32(dimensionMatrix.Text));
            MatrixBTextBoxes = UpdateGrid(MatrixB, Convert.ToInt32(dimensionMatrix.Text));
        }

        private void RandomFiling(TextBox[,] textBoxes)
        {
            Random rand = new Random();
            foreach (TextBox item in textBoxes)
            {
                item.Text = rand.Next(0, 10).ToString();
            }
        }
        private void btFillingRandomMatrix_Click(object sender, RoutedEventArgs e)
        {
            RandomFiling(MatrixATextBoxes);
            RandomFiling(MatrixBTextBoxes);
        }
       
        private void btCalculateMatrix_Click(object sender, RoutedEventArgs e)
        {
            CalculateMatrix();
        }

        private Matrix<int> GetResult(Matrix<int> A, Matrix<int> B)
        {
            return operations.SelectedIndex switch
            {
                0 => A + B,
                1 => A * B,
                _ => throw new NotImplementedException(),
            };
        }

        private void CalculateMatrix()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            int dimension = Convert.ToInt32(dimensionMatrix.Text);

            Matrix<int> A = new Matrix<int>(dimension);
            Matrix<int> B = new Matrix<int>(dimension);

            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    A[i, j] = Convert.ToInt32(MatrixATextBoxes[i, j].Text);
                    B[i, j] = Convert.ToInt32(MatrixBTextBoxes[i, j].Text);
                }
            }
            Matrix<int> resultMatrix = GetResult(A, B);

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            double time = ts.TotalMilliseconds;

            TextBox[,] resultFields = UpdateGrid(ResultMatrix, lastSize);

            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    resultFields[i, j].Text = Convert.ToString(resultMatrix[i, j]);
                }
            }
            MessageBox.Show($"Time = {time.ToString()}");
        }


    }
}
