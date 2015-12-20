using CommonLibrary;
using System;

namespace Task
{
    class Program
    {
        static void Main(string[] args)
        {
            var plugin1Path = @"D:\Vlad\Mentoring\project\04 - AppDomains\PluginV1\bin\Debug\PluginV1.dll";
            //var plugin2Path = @"D:\Vlad\Mentoring\project\04 - AppDomains\PluginV2\bin\Debug\PluginV2.dll";
            var entryClassName = "Entry";

            var manager = new PluginManager.PluginManager();
            var result = manager.LoadPlugin(plugin1Path, entryClassName);

            var entry = (BaseEntry)result.Entry;

            Console.WriteLine("Plugin path: {0}", plugin1Path);
            Console.WriteLine("Domain name: {0}", entry.DomainName);
            Console.WriteLine("Plugin version: {0}", entry.PluginVersion);

            Console.WriteLine("Unloading domain {0}", result.Domain.FriendlyName);
            manager.UnloadPlugin(result.Domain);
            Console.WriteLine("Domain unloaded");

            Console.ReadKey();
        }
    }
}
