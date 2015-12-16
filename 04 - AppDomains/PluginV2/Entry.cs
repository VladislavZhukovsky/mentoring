using CommonLibrary;
using System;

namespace PluginV2
{
    public class Entry: BaseEntry
    {
        public Entry()
        {
            DomainName = AppDomain.CurrentDomain.FriendlyName;
            PluginVersion = "v2";
        }
    }
}
