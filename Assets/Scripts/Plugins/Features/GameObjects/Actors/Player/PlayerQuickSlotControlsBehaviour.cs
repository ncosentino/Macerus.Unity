using System;
using System.Linq;

using Assets.Scripts.Input.Api;
using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;
using Assets.Scripts.Plugins.Features.IngameDebugConsole.Api;
using Assets.Scripts.Unity.Input;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Skills.Api;

using NexusLabs.Contracts;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Plugins.Features.TurnBased.Api;
using ProjectXyz.Shared.Framework;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{

    public sealed class PlayerQuickSlotControlsBehaviour :
        MonoBehaviour,
        IPlayerQuickSlotControlsBehaviour
    {
        public IDebugConsoleManager DebugConsoleManager { get; set; }

        public IKeyboardControls KeyboardControls { get; set; }

        public IKeyboardInput KeyboardInput { get; set; }

        public ProjectXyz.Api.Logging.ILogger Logger { get; set; }

        public ISkillUsage SkillUsage { get; set; }

        public ISkillHandlerFacade SkillHandlerFacade { get; set; }

        // FIXME: delete this, just for testing
        public IMapGameObjectManager MapGameObjectManager { get; set; }

        // FIXME: delete this, just for testing
        public ITurnBasedManager TurnBasedManager { get; set; }

        private void Start()
        {
            UnityContracts.RequiresNotNull(this, Logger, nameof(Logger));
            UnityContracts.RequiresNotNull(this, KeyboardInput, nameof(KeyboardInput));
            UnityContracts.RequiresNotNull(this, KeyboardControls, nameof(KeyboardControls));
            UnityContracts.RequiresNotNull(this, DebugConsoleManager, nameof(DebugConsoleManager));
            UnityContracts.RequiresNotNull(this, SkillHandlerFacade, nameof(SkillHandlerFacade));
        }

        private void Update()
        {
            HandleQuickSlotControls();
        }

        private IGameObject GetPlayer()
        {
            var player = gameObject
                .GetComponent<IReadOnlyHasGameObject>()
                .GameObject;
            return player;
        }

        private void HandleQuickSlotControls()
        {
            if (DebugConsoleManager.GetConsoleWindowVisible())
            {
                return;
            }

            if (KeyboardInput.GetKeyUp(KeyboardControls.QuickSlot1))
            {
                var player = GetPlayer();
                var skills = player
                    .GetOnly<IHasSkillsBehavior>()
                    .Skills;
                // FIXME: this should actually pull this information from an assigned slot
                var firstUsableSkill = skills.FirstOrDefault(x => x.Has<IUseOutOfCombatSkillBehavior>());
                if (firstUsableSkill == null)
                {
                    return;
                }

                if (!SkillUsage.CanUseSkill(
                    player,
                    firstUsableSkill))
                {
                    return;
                }

                // FIXME: move this logic into the backend into some class?
                // FIXME: check the targeting?
                SkillUsage.UseRequiredResources(
                    player,
                    firstUsableSkill);

                SkillHandlerFacade.Handle(
                    player,
                    firstUsableSkill);

                TurnBasedManager.SetApplicableObjects(new[] { player });
            }
            else if (KeyboardInput.GetKeyUp(KeyboardControls.QuickSlot2))
            {
                var player = GetPlayer();
                var skills = player
                    .GetOnly<IHasSkillsBehavior>()
                    .Skills;

                // FIXME: this should actually pull this information from an assigned slot
                var firstUsableSkill = skills.FirstOrDefault(x => x.Has<ICombinationSkillBehavior>());
                if (firstUsableSkill == null)
                {
                    return;
                }

                if (!SkillUsage.CanUseSkill(
                    player,
                    firstUsableSkill))
                {
                    return;
                }

                // FIXME: move this logic into the backend into some class?
                // FIXME: check the targeting?
                SkillUsage.UseRequiredResources(
                    player,
                    firstUsableSkill);

                SkillHandlerFacade.Handle(
                    player,
                    firstUsableSkill);

                TurnBasedManager.SetApplicableObjects(new[] { player });
            }
            else if (KeyboardInput.GetKeyUp(KeyboardControls.QuickSlot3))
            {

            }
        }
    }


}
