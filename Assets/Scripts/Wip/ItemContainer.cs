using System.Collections;
using System.Collections.Generic;
using ProjectXyz.Api.GameObjects;

namespace Assets.Scripts.Wip
{
    // FIXME: this type of thing should exist in the back-end code
    public sealed class ItemContainer : IItemContainer
    {
        private readonly List<IGameObject> _items;

        public ItemContainer()
        {
            _items = new List<IGameObject>();
            
            // FIXME: just for testing... let's add a couple things in here
            _items.Add((IGameObject)null);
            _items.Add((IGameObject)null);
            _items.Add((IGameObject)null);
        }

        public IEnumerator<IGameObject> GetEnumerator() => _items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}