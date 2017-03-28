using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace InteractiveApp.MouseEvents
{
    public partial class ColorWindow : Window
    {
        private readonly Random _random;

        private Brush _rectangleBrush;

        public ColorWindow()
        {
            InitializeComponent();

            _random = new Random();
        }

        private void RectanglesStackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            CreateRectangles(2);
        }

        private void CreateRectangles(int number)
        {
            var rectangleHeigh = (int)RectanglesStackPanel.ActualWidth / number;

            var rectangleColor = Color.FromRgb((byte)_random.Next(0, 255), (byte)_random.Next(0, 255), (byte)_random.Next(0, 255));

            for (var i = 0; i < number; i++)
            {
                var coef = 1 - i * (1F / number);

                var rectangle = new Rectangle
                {
                    Height = rectangleHeigh,
                    Fill = new SolidColorBrush(Color.Multiply(rectangleColor, coef))
                };

                rectangle.MouseEnter += Rectangle_MouseEnter;
                rectangle.MouseLeave += Rectangle_MouseLeave;

                RectanglesStackPanel.Children.Add(rectangle);
            }
        }

        private void Rectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            var rectangle = sender as Rectangle;

            if (rectangle != null)
            {
                _rectangleBrush = rectangle.Fill.CloneCurrentValue();
                rectangle.Fill = Brushes.Orange;
            }
        }

        private void Rectangle_MouseLeave(object sender, MouseEventArgs e)
        {
            var rectangle = sender as Rectangle;
            if (rectangle != null) rectangle.Fill = _rectangleBrush;
        }
    }
}
