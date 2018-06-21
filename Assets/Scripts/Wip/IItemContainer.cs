using System.Collections.Generic;
using ProjectXyz.Api.GameObjects;

namespace Assets.Scripts.Wip
{
    // FIXME: this type of thing should exist in the back-end code
    public interface IItemContainer : IEnumerable<IGameObject>
    {
    }
}