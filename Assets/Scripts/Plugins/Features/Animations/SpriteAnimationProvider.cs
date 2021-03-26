using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Macerus.Plugins.Features.Animations.Api;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.UnityBehaviours
{
    public sealed class SpriteAnimationProvider : ISpriteAnimationProvider
    {
        private readonly Dictionary<IIdentifier, ISpriteAnimation> _animations;

        public SpriteAnimationProvider(ILpcSheetAnimationFactory lpcSheetAnimationFactory)
        {
            _animations = new Dictionary<IIdentifier, ISpriteAnimation>();
            var lcpPathPrefix = "graphics/actors/LpcUniversal/";
            var lpcUniversalPath = $"assets/resources/{lcpPathPrefix}";
            
            foreach (var file in Directory.GetFiles(lpcUniversalPath))
            {
                var resourcePortionStart = file.LastIndexOf(
                    lcpPathPrefix,
                    StringComparison.OrdinalIgnoreCase);
                var resourcePortion = file.Substring(
                    resourcePortionStart,
                    file.Length - resourcePortionStart - Path.GetExtension(file).Length);
                _animations.AddRange(lpcSheetAnimationFactory.CreateForSheet(new StringIdentifier(resourcePortion)));
            }
        }

        public ISpriteAnimation GetAnimationById(IIdentifier animationId)
        {
            if (!TryGetAnimationById(
                animationId,
                out var spriteAnimation))
            {
                throw new InvalidOperationException(
                    $"Could not find animation with id '{animationId}'.");
            }

            return spriteAnimation;
        }

        public bool TryGetAnimationById(
            IIdentifier animationId,
            out ISpriteAnimation spriteAnimation)
        {
            if (!_animations.TryGetValue(
                animationId,
                out spriteAnimation))
            {
                return false;
            }

            return true;
        }
    }
}
