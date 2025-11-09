using EPiServer.ServiceLocation;
using EPiServer.Shell.ContentQuery;
using PowerSlice;

namespace EpiserverBH.PowerSlice
{
    [ServiceConfiguration(typeof(IContentQuery)), ServiceConfiguration(typeof(IContentSlice))]
    public class EverythingSlice : ContentSliceBase<IContent>
    {
        public override string Name
        {
            get { return "Everything"; }
        }
    }
}
