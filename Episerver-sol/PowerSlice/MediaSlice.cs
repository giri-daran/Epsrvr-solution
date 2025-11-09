using EPiServer.ServiceLocation;
using EPiServer.Shell.ContentQuery;
using PowerSlice;

namespace EpiserverBH.PowerSlice
{
    [ServiceConfiguration(typeof(IContentQuery)), ServiceConfiguration(typeof(IContentSlice))]
    public class MediaSlice : ContentSliceBase<MediaData>
    {
        public override string Name => "Media";

        public override int SortOrder => 70;
    }
}
