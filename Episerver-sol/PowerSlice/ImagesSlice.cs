using EPiServer.ServiceLocation;
using EPiServer.Shell.ContentQuery;
using PowerSlice;

namespace EpiserverBH.PowerSlice
{
    [ServiceConfiguration(typeof(IContentQuery)), ServiceConfiguration(typeof(IContentSlice))]
    public class ImagesSlice : ContentSliceBase<ImageData>
    {
        public override string Name => "Images";

        public override int SortOrder => 71;
    }
}
