using Assets.Scripts.Behaviours.Generic;
using UnityEngine;

namespace Assets.Scripts.Scenes.MainMenu.Input
{
    public sealed class GuiInputStitcher : IGuiInputStitcher
    {
        private readonly IGuiInputController _guiInputController;

        public GuiInputStitcher(IGuiInputController guiInputController)
        {
            _guiInputController = guiInputController;
        }

        public void Attach(GameObject gameObject)
        {
            Debug.Log($"Adding '{_guiInputController}' via '{typeof(UpdateBehaviour)}' to '{gameObject}'...");
            var updateBehaviour = gameObject.AddComponent<UpdateBehaviour>();
            updateBehaviour.ToUpdate = _guiInputController;
            Debug.Log($"Added '{updateBehaviour}' to '{gameObject}'.");
        }
    }
}
