using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HopfieldSSEditor
{
    public partial class MainWindow : Window
    {
        private const int N = 9; //rows
        private const int M = 9; //columns
        private const int PixelSize = 60;

        private bool[,] pixels;

        private Button[,] button;

        private void colorButtons()
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    if (pixels[i, j])
                    {
                        button[i, j].Background = new SolidColorBrush(Colors.Black);
                    }
                    else
                    {
                        button[i, j].Background = new SolidColorBrush(Colors.White);
                    }
                }
            }
        }

        private void pixelButtonClicked(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;

            string[] str = b.Name.Split('_');

            int i = Int32.Parse(str[1]);
            int j = Int32.Parse(str[2]);

            pixels[i, j] = !pixels[i, j];

            colorButtons();
        }

        private void buildStructure()
        {
            MWindow.Title = "HopfieldSS Editor " + N.ToString() + "x" + M.ToString();
            MWindow.ResizeMode = ResizeMode.CanMinimize;
            MWindow.Height = N * PixelSize + 39 + 150;
            MWindow.Width = M * PixelSize + 16;
            MWindow.Padding = new Thickness(0);

            Grid PixelGrid = new Grid();

            PixelGrid.Height = N * PixelSize;
            PixelGrid.Width = M * PixelSize;
            PixelGrid.HorizontalAlignment = HorizontalAlignment.Center;
            PixelGrid.VerticalAlignment = VerticalAlignment.Center;
            PixelGrid.ShowGridLines = true;
            PixelGrid.Margin = new Thickness(0);

            RowDefinition[] row = new RowDefinition[N];
            ColumnDefinition[] column = new ColumnDefinition[M];

            for (int i = 0; i < N; i++)
            {
                row[i] = new RowDefinition();
                PixelGrid.RowDefinitions.Add(row[i]);
            }

            for (int i = 0; i < M; i++)
            {
                column[i] = new ColumnDefinition();
                PixelGrid.ColumnDefinitions.Add(column[i]);
            }

            button = new Button[N, M];

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    button[i, j] = new Button();

                    button[i, j].Width = PixelSize;
                    button[i, j].Height = PixelSize;
                    button[i, j].Margin = new Thickness(0);
                    button[i, j].Padding = new Thickness(0);
                    button[i, j].BorderThickness = new Thickness(0);
                    button[i, j].Background = new SolidColorBrush(Colors.White);
                    button[i, j].Click += this.pixelButtonClicked;
                    button[i, j].Name = "pixel_" + i.ToString() + "_" + j.ToString();

                    Grid.SetRow(button[i, j], i);
                    Grid.SetColumn(button[i, j], j);

                    PixelGrid.Children.Add(button[i, j]);
                }
            }

            Grid MainGrid = new Grid();

            RowDefinition pixelRow = new RowDefinition();
            RowDefinition buttonRow = new RowDefinition();

            pixelRow.Height = GridLength.Auto;

            buttonRow.MinHeight = 150;
            buttonRow.MaxHeight = 150;

            MainGrid.RowDefinitions.Add(pixelRow);
            MainGrid.RowDefinitions.Add(buttonRow);

            Grid.SetRow(PixelGrid, 0);

            Grid ButtonGrid = new Grid();

            ButtonGrid.HorizontalAlignment = HorizontalAlignment.Center;
            ButtonGrid.VerticalAlignment = VerticalAlignment.Center;

            TextBlock buttonText = new TextBlock();

            buttonText.Text = "Uruchom sieć";
            buttonText.FontWeight = FontWeights.ExtraBold;
            buttonText.FontSize = N + M;

            Button runNetworkButton = new Button();

            runNetworkButton.Name = "runNetworkButton";
            runNetworkButton.Content = buttonText;
            runNetworkButton.Padding = new Thickness(10);
            runNetworkButton.Height = 130;
            runNetworkButton.Width = (M * PixelSize) - 20;
            runNetworkButton.Click += runNetworkButtonClick;

            ButtonGrid.Children.Add(runNetworkButton);

            Grid.SetRow(ButtonGrid, 1);

            MainGrid.Children.Add(PixelGrid);
            MainGrid.Children.Add(ButtonGrid);

            MWindow.Content = MainGrid;
        }

        private void runNetworkButtonClick(object sender, RoutedEventArgs e)
        {

        }

        public MainWindow()
        {
            InitializeComponent();

            pixels = new bool[N, M];

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    pixels[i, j] = false;
                }
            }

            buildStructure();
        }
    }
}