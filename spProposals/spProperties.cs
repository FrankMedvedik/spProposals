using Microsoft.SharePoint.Client;

namespace spProposals
{
    public class SpProperties
    {
#if DEBUG
        public const string BlueBerryHomeUrl = "http://home.reckner.com/Blueberry/";
        public const string BlueBerryProposalsDetailUrl = "http://home.reckner.com/BlueBerry/Lists/Proposals/DispForm.aspx?ID=";
        public const string WorkUrl = "http://work.reckner.com/Jobs/";

#else
        public const string BlueBerryHomeUrl = "http://home.reckner.com" + "/Blueberry/";
        public const string BlueBerryProposalsDetailUrl = "http://home.reckner.com" + "/BlueBerry/Lists/Proposals/DispForm.aspx?ID=";
        public const string WorkUrl = "http://work.reckner.com/Jobs/";

#endif
    }
}

