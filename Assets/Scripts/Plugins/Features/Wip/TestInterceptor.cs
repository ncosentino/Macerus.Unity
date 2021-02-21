using System.Linq;

using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;

using Macerus.Api.Behaviors;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
using ProjectXyz.Shared.Behaviors.Filtering.Attributes;
using ProjectXyz.Shared.Framework;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Interceptors
{
    public sealed class TestInterceptor : IDiscoverableGameObjectBehaviorInterceptor
    {
        private readonly IFilterContextFactory _filterContextFactory;
        private readonly ISkillRepository _skillRepository;

        public TestInterceptor(
            IFilterContextFactory filterContextFactory,
            ISkillRepository skillRepository)
        {
            _filterContextFactory = filterContextFactory;
            _skillRepository = skillRepository;
        }

        public void Intercept(
            IGameObject gameObject, 
            GameObject unityGameObject)
        {
            var playerControlled = gameObject
                .Get<IPlayerControlledBehavior>()
                .Any();
            if (!playerControlled)
            {
                return;
            }

            var glow = _skillRepository
                .GetSkills(_filterContextFactory.CreateFilterContextForSingle(new IFilterAttribute[]
                {
                    new FilterAttribute(
                        new StringIdentifier("id"),
                        new IdentifierFilterAttributeValue(new StringIdentifier("green-glow")),
                        true),
                }))
                .Single();

            var hasSkillsbehavior = gameObject.GetOnly<IHasSkillsBehavior>();
            hasSkillsbehavior.Add(new[] { glow });
        }
    }
}