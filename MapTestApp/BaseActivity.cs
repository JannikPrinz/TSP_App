//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using Java.Lang;
//using Org.Mapsforge.Core.Model;
//using Org.Mapsforge.Core.Util;
//using Org.Mapsforge.Map.Android.Input;
//using Org.Mapsforge.Map.Android.Util;
//using Org.Mapsforge.Map.Layer.Renderer;

//namespace MapTestApp
//{
//    [Activity(Label = "BaseActivity")]
//    public abstract class BaseActivity : MapViewerTemplate, ISharedPreferencesOnSharedPreferenceChangeListener
//    {
//        protected ISharedPreferences sharedPreferences;

//        //protected override int LayoutId => R.layout.mapviewer;

//        //protected override int MapViewId => R.id.mapView;

//        protected override MapPosition InitialPosition => getInitialPosition();

//        protected MapPosition getInitialPosition()
//        {
//            int tileSize = this.MapView.Model.DisplayModel.TileSize;
//            sbyte zoomLevel = LatLongUtils.ZoomForBounds(new Dimension(tileSize * 4, tileSize * 4), MapFile.BoundingBox(), tileSize);
//            return new MapPosition(MapFile.BoundingBox().CenterPoint, zoomLevel);
//        }

//        protected override void CreateLayers()
//        {
//            TileRendererLayer tileRendererLayer = AndroidUtil.CreateTileRendererLayer(this.TileCaches[0],
//                MapView.Model.MapViewPosition, MapFile, RenderTheme, false, true, false, HillsRenderConfig);
//            this.MapView.LayerManager.Layers.Add(tileRendererLayer);

//            // needed only for samples to hook into Settings.
//            setMaxTextWidthFactor();
//        }

//        protected override void CreateControls()
//        {
//            base.CreateControls();
//            setMapScaleBar();
//        }

//        protected override void CreateMapViews()
//        {
//            base.CreateMapViews();

//            MapView.MapZoomControls.SetZoomControlsOrientation(MapZoomControls.Orientation.VerticalInOut);
//            //MapView.MapZoomControls.SetZoomInResource(R.drawable.zoom_control_in);
//            //MapView.MapZoomControls.SetZoomOutResource(R.drawable.zoom_control_out);
//            //MapView.MapZoomControls.SetMarginHorizontal(Resources.GetDimensionPixelOffset(R.dimen.controls_margin));
//            //MapView.MapZoomControls.SetMarginVertical(Resources.GetDimensionPixelOffset(R.dimen.controls_margin));
//        }

//        protected override void CreateTileCaches()
//        {
//            bool persistent = sharedPreferences.GetBoolean(Constants.SETTING_TILECACHE_PERSISTENCE, true);

//            this.TileCaches.Add(AndroidUtil.CreateTileCache(this, PersistableId,
//                    this.MapView.Model.DisplayModel.TileSize, this.ScreenRatio,
//                    this.MapView.Model.FrameBufferModel.OverdrawFactor, persistent));
//        }

//        protected override string MapFileName => getMapFileName();
        
//        protected string getMapFileName()
//        {
//            string mapfile = (Samples.launchUrl == null) ? null : Samples.launchUrl.getQueryParameter("mapfile");
//            if (mapfile != null)
//            {
//                return mapfile;
//            }
//            return "germany.map";
//        }

//        @Override
//    protected File getMapFileDirectory()
//        {
//            String mapdir = (Samples.launchUrl == null) ? null : Samples.launchUrl.getQueryParameter("mapdir");
//            if (mapdir != null)
//            {
//                File file = new File(mapdir);
//                if (file.exists() && file.isDirectory())
//                {
//                    return file;
//                }
//                throw new RuntimeException(file + " does not exist or is not a directory (configured in launch URI " + Samples.launchUrl + " )");
//            }
//            return super.getMapFileDirectory();
//        }

//        protected override void OnCreate(Bundle savedInstanceState)
//        {
//            base.OnCreate(savedInstanceState);
//            Title = Class.SimpleName;
//        }

//        public void OnSharedPreferenceChanged(ISharedPreferences sharedPreferences, string key)
//        {
//            if (Constants.SETTING_SCALE.Equals(key))
//            {
//                this.mapView.getModel().displayModel.setUserScaleFactor(DisplayModel.getDefaultUserScaleFactor());
//                Log.d(SamplesApplication.TAG, "Tilesize now " + this.mapView.getModel().displayModel.getTileSize());
//                AndroidUtil.restartActivity(this);
//            }
//            if (SamplesApplication.SETTING_PREFERRED_LANGUAGE.equals(key))
//            {
//                String language = preferences.getString(SamplesApplication.SETTING_PREFERRED_LANGUAGE, null);
//                Log.d(SamplesApplication.TAG, "Preferred language now " + language);
//                AndroidUtil.restartActivity(this);
//            }
//            if (SamplesApplication.SETTING_TILECACHE_PERSISTENCE.equals(key))
//            {
//                if (!preferences.getBoolean(SamplesApplication.SETTING_TILECACHE_PERSISTENCE, false))
//                {
//                    Log.d(SamplesApplication.TAG, "Purging tile caches");
//                    for (TileCache tileCache : this.tileCaches)
//                    {
//                        tileCache.purge();
//                    }
//                }
//                AndroidUtil.restartActivity(this);
//            }
//            if (SamplesApplication.SETTING_TEXTWIDTH.equals(key))
//            {
//                AndroidUtil.restartActivity(this);
//            }
//            if (SETTING_SCALEBAR.equals(key))
//            {
//                setMapScaleBar();
//            }
//            if (SamplesApplication.SETTING_DEBUG_TIMING.equals(key))
//            {
//                MapWorkerPool.DEBUG_TIMING = preferences.getBoolean(SamplesApplication.SETTING_DEBUG_TIMING, false);
//            }
//            if (SamplesApplication.SETTING_RENDERING_THREADS.equals(key))
//            {
//                Parameters.NUMBER_OF_THREADS = preferences.getInt(SamplesApplication.SETTING_RENDERING_THREADS, 1);
//                AndroidUtil.restartActivity(this);
//            }
//            if (SamplesApplication.SETTING_WAYFILTERING_DISTANCE.equals(key) ||
//                    SamplesApplication.SETTING_WAYFILTERING.equals(key))
//            {
//                MapFile.wayFilterEnabled = preferences.getBoolean(SamplesApplication.SETTING_WAYFILTERING, true);
//                if (MapFile.wayFilterEnabled)
//                {
//                    MapFile.wayFilterDistance = preferences.getInt(SamplesApplication.SETTING_WAYFILTERING_DISTANCE, 20);
//                }
//            }
//        }

//        /**
//     * Sets the scale bar from preferences.
//     */
//        protected void setMapScaleBar()
//        {
//            String value = this.sharedPreferences.getString(SETTING_SCALEBAR, SETTING_SCALEBAR_BOTH);

//            if (SETTING_SCALEBAR_NONE.equals(value))
//            {
//                AndroidUtil.setMapScaleBar(this.mapView, null, null);
//            }
//            else
//            {
//                if (SETTING_SCALEBAR_BOTH.equals(value))
//                {
//                    AndroidUtil.setMapScaleBar(this.mapView, MetricUnitAdapter.INSTANCE, ImperialUnitAdapter.INSTANCE);
//                }
//                else if (SETTING_SCALEBAR_METRIC.equals(value))
//                {
//                    AndroidUtil.setMapScaleBar(this.mapView, MetricUnitAdapter.INSTANCE, null);
//                }
//                else if (SETTING_SCALEBAR_IMPERIAL.equals(value))
//                {
//                    AndroidUtil.setMapScaleBar(this.mapView, ImperialUnitAdapter.INSTANCE, null);
//                }
//                else if (SETTING_SCALEBAR_NAUTICAL.equals(value))
//                {
//                    AndroidUtil.setMapScaleBar(this.mapView, NauticalUnitAdapter.INSTANCE, null);
//                }
//            }
//        }

//        /**
//         * sets the value for breaking line text in labels.
//         */
//        protected void setMaxTextWidthFactor()
//        {
//            MapView.Model.DisplayModel.SetMaxTextWidthFactor(float.Parse(sharedPreferences.GetString(Constants.SETTING_TEXTWIDTH, "0.7")));
//        }
//    }
//}