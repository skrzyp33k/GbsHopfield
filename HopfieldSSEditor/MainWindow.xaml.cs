using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HopfieldSSEditor
{
    public partial class MainWindow : Window
    {
        public const int N = 9; //rows
        public const int M = 9; //columns
        private const int PixelSize = 30;

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
            MWindow.ResizeMode = ResizeMode.NoResize;
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

            ColumnDefinition col1 = new ColumnDefinition();
            ColumnDefinition col2 = new ColumnDefinition();

            ButtonGrid.ColumnDefinitions.Add(col1);
            ButtonGrid.ColumnDefinitions.Add(col2);

            TextBlock addPictureButtonText = new TextBlock();

            addPictureButtonText.Text = "Zapisz obraz";
            addPictureButtonText.FontWeight = FontWeights.Bold;
            addPictureButtonText.FontSize = 18;

            Button addPictureButton = new Button();

            addPictureButton.Name = "addPictureButton";
            addPictureButton.Content = addPictureButtonText;
            addPictureButton.Padding = new Thickness(10);
            addPictureButton.Height = 130;
            addPictureButton.Width = ((M * PixelSize) - 20) / 2;
            addPictureButton.Click += addPictureButtonClick;

            TextBlock restorePictureButtonText = new TextBlock();

            restorePictureButtonText.Text = "Odtwórz obraz";
            restorePictureButtonText.FontWeight = FontWeights.Bold;
            restorePictureButtonText.FontSize = 18;

            Button restorePictureButton = new Button();

            restorePictureButton.Name = "restorePictureButton";
            restorePictureButton.Content = restorePictureButtonText;
            restorePictureButton.Padding = new Thickness(10);
            restorePictureButton.Height = 130;
            restorePictureButton.Width = ((M * PixelSize) - 20) / 2;
            restorePictureButton.Click += restorePictureButtonClick;

            Grid.SetColumn(addPictureButton, 0);
            Grid.SetColumn(restorePictureButton, 1);

            ButtonGrid.Children.Add(addPictureButton);
            ButtonGrid.Children.Add(restorePictureButton);

            Grid.SetRow(ButtonGrid, 1);

            MainGrid.Children.Add(PixelGrid);
            MainGrid.Children.Add(ButtonGrid);

            MWindow.Content = MainGrid;
        }

        private void addPictureButtonClick(object sender, RoutedEventArgs e)
        {
            HopfieldNetwork.AddCurrentImageToWeightMatrix(pixels);
        }

        private void restorePictureButtonClick(object sender, RoutedEventArgs e)
        {
            pixels = HopfieldNetwork.RunHopfield(pixels);
            colorButtons();
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