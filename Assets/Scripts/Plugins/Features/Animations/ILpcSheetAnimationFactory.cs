using System.Collections.Generic;

using Macerus.Plugins.Features.Animations.Api;

using ProjectXyz.Api.Framework;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.UnityBehaviours
{
    public interface ILpcSheetAnimationFactory
    {
        IEnumerable<KeyValuePair<IIdentifier, ISpriteAnimation>> CreateForSheet(IIdentifier spriteSheetResourceId);
    }
}