namespace Org.Mapsforge.Core.Mapelements
{
    public partial class SymbolContainer : global::Org.Mapsforge.Core.Mapelements.MapElementContainer
    {
        public override int CompareTo(Java.Lang.Object o)
        {
            return CompareTo((MapElementContainer)o);
        }
    }

    public partial class WayTextContainer : global::Org.Mapsforge.Core.Mapelements.MapElementContainer
    {
        public override int CompareTo(Java.Lang.Object o)
        {
            return CompareTo((MapElementContainer)o);
        }
    }
}

namespace Org.Mapsforge.Core.Model
{
    public partial class LatLong : global::Java.Lang.Object, global::Java.Lang.IComparable
    {
        int global::Java.Lang.IComparable.CompareTo(Java.Lang.Object o)
        {
            return CompareTo((LatLong)o);
        }
    }

    public partial class Point : global::Java.Lang.Object, global::Java.IO.ISerializable, global::Java.Lang.IComparable
    {
        int global::Java.Lang.IComparable.CompareTo(Java.Lang.Object o)
        {
            return CompareTo((Point)o);
        }
    }

    public partial class Tag : global::Java.Lang.Object, global::Java.IO.ISerializable, global::Java.Lang.IComparable
    {
        int global::Java.Lang.IComparable.CompareTo(Java.Lang.Object o)
        {
            return CompareTo((Tag)o);
        }
    }
}
