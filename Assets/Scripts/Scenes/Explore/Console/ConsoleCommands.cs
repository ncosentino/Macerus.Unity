using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Scripts;
using Assets.Scripts.Behaviours;

using Autofac;

using IngameDebugConsole;

using Macerus.Api.Behaviors;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
using ProjectXyz.Shared.Behaviors.Filtering.Attributes;
using ProjectXyz.Shared.Framework;

using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Console
{
    public sealed class ConsoleCommandsBehaviour : MonoBehaviour
    {
        private ProjectXyz.Api.Logging.ILogger _logger;
        private IGameObjectManager _gameObjectManager;
        private ISkillRepository _skillRepository;
        private IFilterContextFactory _filterContextFactory;

        private void Start()
        {
            // NOTE: we're violating the sticher pattern here and *YES* using 
            // the most evil service locator pattern here BUT here me out...
            // This class will probably be mostly for test methods and this is 
            // the easiest shortcut for getting access to all kinds of stuff
            // without making separate classes for stitching etc...
            var container = GameDependencyBehaviour.Container;

            _logger = container.Resolve<ProjectXyz.Api.Logging.ILogger>();
            _gameObjectManager = container.Resolve<IGameObjectManager>();
            _skillRepository = container.Resolve<ISkillRepository>();
            _filterContextFactory = container.Resolve<IFilterContextFactory>();

            AddCommand(
                nameof(PlayerAddSkill),
                "Adds a skill with the specified ID to the player.");
        }

        private void AddCommand(string name, string description)
        {
            DebugLogConsole.AddCommandInstance(
                name,
                description,
                name,
                this);
        }

        private void PlayerAddSkill(string skillId)
        {
            var player = _gameObjectManager
                .GameObjects
                .Single(x => x.Has<IPlayerControlledBehavior>());

            var skill = _skillRepository
                .GetSkills(_filterContextFactory.CreateFilterContextForSingle(new IFilterAttribute[]
                {
                    new FilterAttribute(
                        new StringIdentifier("id"),
                        new IdentifierFilterAttributeValue(new StringIdentifier(skillId)),
                        true),
                }))
                .FirstOrDefault();
            if (skill == null)
            {
                _logger.Warn($"Could not find a skill with ID '{skillId}'.");
                return;
            }

            var hasSkillsbehavior = player.GetOnly<IHasSkillsBehavior>();
            hasSkillsbehavior.Add(new[] { skill });
        }
    }
}
