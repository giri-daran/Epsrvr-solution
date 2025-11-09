using EPiServer.ServiceLocation;
using EPiServer.Shell.ContentQuery;
using EpiserverBH.Models.Blocks;
using PowerSlice;

namespace EpiserverBH.PowerSlice
{
    [ServiceConfiguration(typeof(IContentQuery)), ServiceConfiguration(typeof(IContentSlice))]
    public class BlocksSlice : ContentSliceBase<SiteBlockData>
    {
        public override string Name
        {
            get { return "Blocks"; }
        }
    }
}
