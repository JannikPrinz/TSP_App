using Java.Lang;

namespace Org.Mapsforge.Map.Android.Graphics
{
    public sealed partial class AndroidGraphicFactory : global::Java.Lang.Object, global::Org.Mapsforge.Core.Graphics.IGraphicFactory
    {
        global::Org.Mapsforge.Core.Graphics.IHillshadingBitmap global::Org.Mapsforge.Core.Graphics.IGraphicFactory.CreateMonoBitmap(int width, int height, byte[] buffer, int padding, global::Org.Mapsforge.Core.Model.BoundingBox area)
        {
            return CreateMonoBitmap(width, height, buffer, padding, area);
        }
    }

    public partial class AndroidPointTextContainer : global::Org.Mapsforge.Core.Mapelements.PointTextContainer
    {
        public override int CompareTo(Object o)
        {
            return CompareTo((Core.Mapelements.MapElementContainer)o);
        }
    }
}
