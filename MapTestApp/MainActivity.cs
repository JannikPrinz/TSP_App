using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

using Org.Mapsforge.Map.Android.Graphics;
using Org.Mapsforge.Map.Android.Util;
using Org.Mapsforge.Map.Android.View;
using Org.Mapsforge.Map.Datastore;
using Org.Mapsforge.Map.Layer.Cache;
using Org.Mapsforge.Map.Layer.Renderer;
using Org.Mapsforge.Map.Reader;
using Org.Mapsforge.Map.Rendertheme;

using Java.IO;
using System.IO;
using System.Reflection;

namespace MapTestApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            AndroidGraphicFactory.CreateInstance(Application);

            MapView mapView = FindViewById<MapView>(Resource.Id.mapView);

            mapView.Clickable = true;
            mapView.MapScaleBar.Visible = true;
            mapView.SetBuiltInZoomControls(true);

            ITileCache tileCache = AndroidUtil.CreateTileCache(this, "mapcache", mapView.Model.DisplayModel.TileSize, 1f, mapView.Model.FrameBufferModel.OverdrawFactor);

            //var assembly = System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(MainActivity)).Assembly;
            //foreach (var res in assembly.GetManifestResourceNames())
            //{
            //    System.Diagnostics.Debug.WriteLine("found resource: " + res);
            //}
            try
            {
                //System.Console.WriteLine("1");
                var destinationPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "berlin.map");

                System.Console.WriteLine("2");
                using (System.IO.Stream source = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("MapTestApp.Resources.berlin.map"))
                {
                    System.Console.WriteLine("3");
                    using (var destination = System.IO.File.Create(destinationPath))
                    {
                        System.Console.WriteLine("4");
                        source.CopyTo(destination);
                    }
                }
                //System.Console.WriteLine("5");

                //var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MainActivity)).Assembly;
                //Stream stream = assembly.GetManifestResourceStream("WorkingWithFiles.PCLTextResource.txt");
                //string text = "";
                //using (var reader = new System.IO.StreamReader(stream))
                //{
                //    reader.
                //    text = reader.ReadToEnd();
                //}

                Java.IO.File mapFile = new Java.IO.File(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "berlin.map");
                //File mapFile = new File("", "berlin.map");

                System.Console.WriteLine("******************* File Found!");

                MapDataStore mapDataStore = new MapFile(mapFile);
                TileRendererLayer tileRendererLayer = new TileRendererLayer(tileCache, mapDataStore, mapView.Model.MapViewPosition, AndroidGraphicFactory.Instance);
                tileRendererLayer.SetXmlRenderTheme(InternalRenderTheme.Default);

                mapView.LayerManager.Layers.Add(tileRendererLayer);
            } catch (Exception e)
            {
                System.Console.WriteLine("###################" + e.StackTrace);
            }

    mapView.SetCenter(new Org.Mapsforge.Core.Model.LatLong(52.517037, 13.38886));
            mapView.SetZoomLevel(12);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }
	}
}

