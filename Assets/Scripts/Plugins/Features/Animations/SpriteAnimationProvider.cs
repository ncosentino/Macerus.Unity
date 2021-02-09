using System;
using System.Collections.Generic;

using Assets.Scripts.Plugins.Features.Animations.Api;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.UnityBehaviours
{
    public sealed class SpriteAnimationProvider : ISpriteAnimationProvider
    {
        private readonly Dictionary<IIdentifier, ISpriteAnimation> _animations;

        public SpriteAnimationProvider()
        {
            _animations = new Dictionary<IIdentifier, ISpriteAnimation>()
            {
                [new StringIdentifier("player_walk_back")] = new SpriteAnimation(
                    new[]
                    {
                        new SpriteAnimationFrame(
                            new StringIdentifier("graphics/actors/player"),
                            new StringIdentifier("player_1"),
                            false,
                            false,
                            0.2f,
                            Color.white),
                        new SpriteAnimationFrame(
                            new StringIdentifier("graphics/actors/player"),
                            new StringIdentifier("player_0"),
                            false,
                            false,
                            0.2f,
                            Color.white),
                        new SpriteAnimationFrame(
                            new StringIdentifier("graphics/actors/player"),
                            new StringIdentifier("player_1"),
                            false,
                            false,
                            0.2f,
                            Color.white),
                        new SpriteAnimationFrame(
                            new StringIdentifier("graphics/actors/player"),
                            new StringIdentifier("player_2"),
                            false,
                            false,
                            0.2f,
                            Color.white),
                    },
                    true),
                [new StringIdentifier("player_stand_back")] = new SpriteAnimation(
                    new[]
                    {
                        new SpriteAnimationFrame(
                            new StringIdentifier("graphics/actors/player"),
                            new StringIdentifier("player_1"),
                            false,
                            false,
                            60f,
                            Color.white),
                    },
                    true),
                [new StringIdentifier("player_walk_forward")] = new SpriteAnimation(
                    new[]
                    {
                        new SpriteAnimationFrame(
                            new StringIdentifier("graphics/actors/player"),
                            new StringIdentifier("player_4"),
                            false,
                            false,
                            0.2f,
                            Color.white),
                        new SpriteAnimationFrame(
                            new StringIdentifier("graphics/actors/player"),
                            new StringIdentifier("player_3"),
                            false,
                            false,
                            0.2f,
                            Color.white),
                        new SpriteAnimationFrame(
                            new StringIdentifier("graphics/actors/player"),
                            new StringIdentifier("player_4"),
                            false,
                            false,
                            0.2f,
                            Color.white),
                        new SpriteAnimationFrame(
                            new StringIdentifier("graphics/actors/player"),
                            new StringIdentifier("player_5"),
                            false,
                            false,
                            0.2f,
                            Color.white),
                    },
                    true),
                [new StringIdentifier("player_stand_forward")] = new SpriteAnimation(
                    new[]
                    {
                        new SpriteAnimationFrame(
                            new StringIdentifier("graphics/actors/player"),
                            new StringIdentifier("player_4"),
                            false,
                            false,
                            60f,
                            Color.white),
                    },
                    true),
                [new StringIdentifier("player_walk_left")] = new SpriteAnimation(
                    new[]
                    {
                        new SpriteAnimationFrame(
                            new StringIdentifier("graphics/actors/player"),
                            new StringIdentifier("player_7"),
                            false,
                            false,
                            0.2f,
                            Color.white),
                        new SpriteAnimationFrame(
                            new StringIdentifier("graphics/actors/player"),
                            new StringIdentifier("player_6"),
                            false,
                            false,
                            0.2f,
                            Color.white),
                        new SpriteAnimationFrame(
                            new StringIdentifier("graphics/actors/player"),
                            new StringIdentifier("player_7"),
                            false,
                            false,
                            0.2f,
                            Color.white),
                        new SpriteAnimationFrame(
                            new StringIdentifier("graphics/actors/player"),
                            new StringIdentifier("player_8"),
                            false,
                            false,
                            0.2f,
                            Color.white),
                    },
                    true),
                [new StringIdentifier("player_stand_left")] = new SpriteAnimation(
                    new[]
                    {
                        new SpriteAnimationFrame(
                            new StringIdentifier("graphics/actors/player"),
                            new StringIdentifier("player_7"),
                            false,
                            false,
                            60f,
                            Color.white),
                    },
                    true),
                [new StringIdentifier("player_walk_right")] = new SpriteAnimation(
                    new[]
                    {
                        new SpriteAnimationFrame(
                            new StringIdentifier("graphics/actors/player"),
                            new StringIdentifier("player_10"),
                            false,
                            false,
                            0.2f,
                            Color.white),
                        new SpriteAnimationFrame(
                            new StringIdentifier("graphics/actors/player"),
                            new StringIdentifier("player_9"),
                            false,
                            false,
                            0.2f,
                            Color.white),
                        new SpriteAnimationFrame(
                            new StringIdentifier("graphics/actors/player"),
                            new StringIdentifier("player_10"),
                            false,
                            false,
                            0.2f,
                            Color.white),
                        new SpriteAnimationFrame(
                            new StringIdentifier("graphics/actors/player"),
                            new StringIdentifier("player_11"),
                            false,
                            false,
                            0.2f,
                            Color.white),
                    },
                    true),
                [new StringIdentifier("player_stand_right")] = new SpriteAnimation(
                    new[]
                    {
                        new SpriteAnimationFrame(
                            new StringIdentifier("graphics/actors/player"),
                            new StringIdentifier("player_4"),
                            false,
                            false,
                            60f,
                            Color.white),
                    },
                    true),
            };
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
