using CommonLibrary;
using System;

namespace PluginV1
{
    public class Entry: BaseEntry
    {
        public Entry()
        {
            DomainName = AppDomain.CurrentDomain.FriendlyName;
            PluginVersion = "v1";
        }
    }
}
