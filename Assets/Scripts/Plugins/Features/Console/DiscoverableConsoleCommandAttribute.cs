using System;

namespace Assets.Scripts.Plugins.Features.Console
{
    public sealed class DiscoverableConsoleCommandAttribute : Attribute
    {
        public DiscoverableConsoleCommandAttribute(string description)
        {
            Description = description;
        }

        public string Description { get; }
    }
}
