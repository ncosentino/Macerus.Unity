using System;

namespace Assets.Scripts.Gui.Noesis.ViewModels
{
    public sealed class NotifyForWrappedPropertyAttribute : Attribute
    {
        public NotifyForWrappedPropertyAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }

        public string PropertyName { get; }
    }
}
