using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Maps.Services;
using System.Device.Location; // Provides the GeoCoordinate class.
using Windows.Devices.Geolocation; //Provides the Geocoordinate class.
using System.Windows.Media;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.Data;
using System.Globalization;


namespace ITWEM1_Project.Resources.Maps
{
    public partial class Maps : PhoneApplicationPage
    {
        RouteQuery MyQuery = null;
        GeocodeQuery Mygeocodequery = null;
        List<GeoCoordinate> MyCoordinates = new List<GeoCoordinate>();
        double lat = 0.0;
        double lon = 0.0;

        public Maps()
        {
            InitializeComponent();


            String res = webServicesConnect("http://pierrelt.fr/WindowsPhone/getLocation.php?id=" + MainPage.id+"&status="+Waiting.Waiting.status);

         
           
        }
        String webServicesConnect(String url)
        {
            String result = "";

            Uri uri = new Uri(url);
            WebClient webClient = new WebClient();
            // Register the callback
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);
            webClient.DownloadStringAsync(uri);
            return result;

        }
        
        void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Result);

            dynamic jsonObj = JsonConvert.DeserializeObject(e.Result);
           // System.Diagnostics.Debug.WriteLine(jsonObj.toString());

            foreach (var child in jsonObj.Children())
            {
              

              string latTmp = child.lat.Value;
              string lonTmp = child.lon.Value;

              lat = Convert.ToDouble(latTmp, new CultureInfo("en-US"));
;
              lon = Convert.ToDouble(lonTmp, new CultureInfo("en-US"));
;
               //  lon = child.lon.Value;

            }

            GetCoordinates(lat, lon);

         
            
        }
       

        private async void GetCoordinates(double lat, double lon)
        {
            // Get the phone's current location.
            Geolocator MyGeolocator = new Geolocator();
            MyGeolocator.DesiredAccuracyInMeters = 5;
            Geoposition MyGeoPosition = null;
            try
            {
                MyGeoPosition = await MyGeolocator.GetGeopositionAsync(TimeSpan.FromMinutes(1), TimeSpan.FromSeconds(10));
                MyCoordinates.Add(new GeoCoordinate(MyGeoPosition.Coordinate.Latitude, MyGeoPosition.Coordinate.Longitude));
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Location is disabled in phone settings or capabilities are not checked.");
            }
            catch (Exception ex)
            {
                // Something else happened while acquiring the location.
                MessageBox.Show(ex.Message);
            }

            //RECEVIED COORDINATES
           
           
            double myLat =MyGeoPosition.Coordinate.Latitude;
            double myLon =MyGeoPosition.Coordinate.Longitude;

            double queryLat = (lat+myLat)/2;
            double queryLon = (lon+myLon)/2;

            MyMap.Center = new GeoCoordinate(myLat, myLon);

            System.Diagnostics.Debug.WriteLine(myLat+"=>"+myLon+"  "+queryLat+"=>"+queryLon);

            Mygeocodequery = new GeocodeQuery();
            Mygeocodequery.SearchTerm = queryLat.ToString() +", "+queryLon.ToString();
            //Mygeocodequery.GeoCoordinate = new GeoCoordinate(48.8599, 002.5187);

            Mygeocodequery.GeoCoordinate = new GeoCoordinate(myLat,myLon );
            Mygeocodequery.QueryCompleted += Mygeocodequery_QueryCompleted;
            Mygeocodequery.QueryAsync();
        }

        void Mygeocodequery_QueryCompleted(object sender, QueryCompletedEventArgs<IList<MapLocation>> e)
        {
            if (e.Error == null)
            {
                MyQuery = new RouteQuery();
                MyQuery.TravelMode = TravelMode.Walking;
                MyCoordinates.Add(e.Result[0].GeoCoordinate);
                MyQuery.Waypoints = MyCoordinates;
                MyQuery.QueryCompleted += MyQuery_QueryCompleted;
                MyQuery.QueryAsync();
                Mygeocodequery.Dispose();
            }
        }

        void MyQuery_QueryCompleted(object sender, QueryCompletedEventArgs<Route> e)
        {
            if (e.Error == null)
            {
                Route MyRoute = e.Result;
                MapRoute MyMapRoute = new MapRoute(MyRoute);
                MyMap.AddRoute(MyMapRoute);

                List<string> RouteList = new List<string>();
                foreach (RouteLeg leg in MyRoute.Legs)
                {
                    foreach (RouteManeuver maneuver in leg.Maneuvers)
                    {
                        RouteList.Add(maneuver.InstructionText);
                    }
                }

                RouteLLS.ItemsSource = RouteList;

                MyQuery.Dispose();
            }
        }
        /*private async void ShowMyLocationOnTheMap()
        {
            // Get my current location.
            Geolocator myGeolocator = new Geolocator();
            myGeolocator.DesiredAccuracy = Windows.Devices.Geolocation.PositionAccuracy.High;
            Geoposition myGeoposition = await myGeolocator.GetGeopositionAsync();
            Geocoordinate myGeocoordinate = myGeoposition.Coordinate;
            GeoCoordinate myGeoCoordinate =
                CoordinateConverter.ConvertGeocoordinate(myGeocoordinate);
            this.mapWithMyLocation.Center = myGeoCoordinate;
            this.mapWithMyLocation.ZoomLevel = 13;

            RouteQuery MyQuery = null;
            GeocodeQuery Mygeocodequery = null;

            Mygeocodequery = new GeocodeQuery();
            Mygeocodequery.SearchTerm = "Seattle, WA";
            Mygeocodequery.GeoCoordinate = new GeoCoordinate(MyGeoPosition.Coordinate.Latitude, MyGeoPosition.Coordinate.Longitude);

            // Create a small circle to mark the current location.
            Ellipse myCircle = new Ellipse();
            myCircle.Fill = new SolidColorBrush(Colors.Blue);
            myCircle.Height = 20;
            myCircle.Width = 20;
            myCircle.Opacity = 50;
            // Create a MapOverlay to contain the circle.
            MapOverlay myLocationOverlay = new MapOverlay();
            myLocationOverlay.Content = myCircle;
            myLocationOverlay.PositionOrigin = new Point(0.5, 0.5);
            myLocationOverlay.GeoCoordinate = myGeoCoordinate;
            // Create a MapLayer to contain the MapOverlay.
            MapLayer myLocationLayer = new MapLayer();
            myLocationLayer.Add(myLocationOverlay);
            mapWithMyLocation.Layers.Add(myLocationLayer);
        }*/
    }
}