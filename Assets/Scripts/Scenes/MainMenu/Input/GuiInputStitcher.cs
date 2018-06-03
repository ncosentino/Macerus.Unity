using Assets.Scripts.Behaviours.Generic;
using UnityEngine;

namespace Assets.Scripts.Scenes.MainMenu.Input
{
    using ILogger = ProjectXyz.Api.Logging.ILogger;

    public sealed class GuiInputStitcher : IGuiInputStitcher
    {
        private readonly IGuiInputController _guiInputController;
        private readonly ILogger _logger;

        public GuiInputStitcher(
            IGuiInputController guiInputController,
            ILogger logger)
        {
            _guiInputController = guiInputController;
            _logger = logger;
        }

        public void Attach(GameObject gameObject)
        {
            _logger.Debug($"Adding '{_guiInputController}' via '{typeof(UpdateBehaviour)}' to '{gameObject}'...");
            var updateBehaviour = gameObject.AddComponent<UpdateBehaviour>();
            updateBehaviour.ToUpdate = _guiInputController;
            _logger.Debug($"Added '{updateBehaviour}' to '{gameObject}'.");
        }
    }
}
