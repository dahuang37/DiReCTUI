using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using GMap.NET.WindowsPresentation;

namespace DiReCTUI.Map
{
    /// <summary>
    /// Interaction logic for CustomMarkerDemo.xaml
    /// </summary>
    public partial class RedMarker
    {
        Popup Popup;
        Label Label;
        GMapMarker Marker;
        //MainWindow MainWindow;
        //MapTest MapTest;
        //GMAP gmap;
        TouchDevice markerTouchDevice;

        //public RedMarker(GMAP gmap, GMapMarker marker, string title)
        //{
        //    this.InitializeComponent();
        //    this.gmap = gmap;
        //    //this.MapTest = window;
        //    this.Marker = marker;


        //    Popup = new Popup();
        //    Label = new Label();

        //    this.Loaded += new RoutedEventHandler(CustomMarkerDemo_Loaded);
        //    this.SizeChanged += new SizeChangedEventHandler(CustomMarkerDemo_SizeChanged);
        //    this.MouseEnter += new MouseEventHandler(MarkerControl_MouseEnter);
        //    this.MouseLeave += new MouseEventHandler(MarkerControl_MouseLeave);
        //    this.MouseMove += new MouseEventHandler(CustomMarkerDemo_MouseMove);
        //    this.MouseLeftButtonUp += new MouseButtonEventHandler(CustomMarkerDemo_MouseLeftButtonUp);
        //    this.MouseLeftButtonDown += new MouseButtonEventHandler(CustomMarkerDemo_MouseLeftButtonDown);
        //    this.TouchMove += new System.EventHandler<TouchEventArgs>(CustomMarkerDemo_TouchMove);
        //    this.TouchDown += new System.EventHandler<TouchEventArgs>(CustomMarkerDemo_TouchDown);

        //    Popup.Placement = PlacementMode.Mouse;
        //    {
        //        Label.Background = Brushes.Blue;
        //        Label.Foreground = Brushes.White;
        //        Label.BorderBrush = Brushes.WhiteSmoke;
        //        Label.BorderThickness = new Thickness(2);
        //        Label.Padding = new Thickness(5);
        //        Label.FontSize = 22;
        //        Label.Content = title;
        //    }
        //    Popup.Child = Label;
        //}

        //void CustomMarkerDemo_Loaded(object sender, RoutedEventArgs e)
        //{
        //    if (icon.Source.CanFreeze)
        //    {
        //        icon.Source.Freeze();
        //    }
        //}

        //void CustomMarkerDemo_SizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    Marker.Offset = new Point(-e.NewSize.Width / 2, -e.NewSize.Height);
        //}

        //void CustomMarkerDemo_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if (e.LeftButton == MouseButtonState.Pressed && IsMouseCaptured)
        //    {
        //        //Point p = e.GetPosition(MainWindow.MainMap);
        //        //Point p2 = e.GetPosition(MapTest.Map);
        //        //Marker.Position = MapTest.Map.FromLocalToLatLng((int)p2.X, (int)p2.Y);
        //        //Marker.Position = MainWindow.MainMap.FromLocalToLatLng((int)p.X, (int)p.Y);
        //        Point p = e.GetPosition(gmap);
        //        Marker.Position = gmap.Map.FromLocalToLatLng((int)p.X, (int)p.Y);
        //    }
        //}
        //void CustomMarkerDemo_TouchMove(object sender, TouchEventArgs e)
        //{
        //    TouchPoint tp = e.GetTouchPoint(gmap);
        //    Point p = tp.Position;
        //    Marker.Position = gmap.Map.FromLocalToLatLng((int)p.X, (int)p.Y);
        //}
        //void CustomMarkerDemo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    if (!IsMouseCaptured)
        //    {
        //        Mouse.Capture(this);
        //    }
        //}

        //void CustomMarkerDemo_TouchDown(object sender, TouchEventArgs e)
        //{
        //    e.TouchDevice.Capture(null);
        //    e.TouchDevice.Capture(this);
        //    if (markerTouchDevice == null)
        //    {
        //        markerTouchDevice = e.TouchDevice;
        //    }
        //    e.Handled = true;
        //}

        //void CustomMarkerDemo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    if (IsMouseCaptured)
        //    {
        //        Mouse.Capture(null);
        //    }
        //}
        //void CustomMarkerDemo_TouchUp(object sender, TouchEventArgs e)
        //{
        //    e.TouchDevice.Capture(null);
        //}

        //void MarkerControl_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    Marker.ZIndex -= 10000;
        //    Popup.IsOpen = false;
        //}

        //void MarkerControl_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    Marker.ZIndex += 10000;
        //    Popup.IsOpen = true;
        //}
    }
}
