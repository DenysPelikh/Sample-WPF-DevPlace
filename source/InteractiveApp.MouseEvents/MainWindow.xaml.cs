using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace InteractiveApp.MouseEvents
{
    public partial class MainWindow : Window
    {
        private readonly Random _random;
        private bool _isAttached;
        private FrameworkElement _attachedElement;
        private Point _clickPoint;
        private ColorWindow _colorWindow;

        public MainWindow()
        {
            InitializeComponent();

            _random = new Random();

            CircleContainCanvas.MouseMove += CircleContainCanvasOnMouseMove;

            Loaded += (sender, args) =>
            {
                CreateEllipse();
                CreateEllipse();
                CreateEllipse();
            };
        }

        private void CircleContainCanvasOnMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            if (_isAttached && _attachedElement != null)
            {
                Canvas.SetLeft(_attachedElement, mouseEventArgs.GetPosition(CircleContainCanvas).X - _clickPoint.X);
                Canvas.SetTop(_attachedElement, mouseEventArgs.GetPosition(CircleContainCanvas).Y - _clickPoint.Y);
            }
        }

        private void CreateEllipse()
        {
            var ellipse = new Ellipse
            {
                Height = 100,
                Width = 100,
                Fill = new SolidColorBrush(Colors.PaleGreen)
            };

            ellipse.MouseLeftButtonDown += EllipseOnMouseLeftButtonDown;
            ellipse.MouseLeftButtonUp += EllipseOnMouseLeftButtonUp;

            Canvas.SetLeft(ellipse, _random.Next(0, (int)((CircleContainCanvas.ActualWidth + 50) - ellipse.Width)));
            Canvas.SetLeft(ellipse, _random.Next(0, (int)((CircleContainCanvas.ActualWidth + 50) - ellipse.Height)));

            ellipse.MouseLeave += Ellipse_MouseLeave;
            ellipse.MouseEnter += Ellipse_MouseEnter;

            CircleContainCanvas.Children.Add(ellipse);
        }

        private void Ellipse_MouseEnter(object sender, MouseEventArgs e)
        {
            var ellipse = sender as Ellipse;
            if (ellipse != null) ellipse.Fill = Brushes.Orange;
        }

        private void Ellipse_MouseLeave(object sender, MouseEventArgs e)
        {
            _isAttached = false;
            var ellipse = sender as Ellipse;
            if (ellipse != null) ellipse.Fill = Brushes.PaleGreen;
        }

        private void EllipseOnMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            _isAttached = false;
        }

        private void EllipseOnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            _clickPoint = mouseButtonEventArgs.GetPosition(sender as FrameworkElement);
            _attachedElement = sender as FrameworkElement;

            _isAttached = true;
        }

        private void NewWindow_Button_Click(object sender, RoutedEventArgs e)
        {
            _colorWindow = new ColorWindow
            {
                Visibility = Visibility.Visible
            };          
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            _colorWindow?.Close();
        }
    }
}
