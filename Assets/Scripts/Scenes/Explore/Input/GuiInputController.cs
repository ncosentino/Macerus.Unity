using UnityEngine.SceneManagement;

namespace Assets.Scripts.Scenes.Explore.Input
{
    public sealed class GuiInputController : IGuiInputController
    {
        #region Fields
        private readonly IKeyboardControls _keyboardControls;
        #endregion

        #region Constructors
        public GuiInputController(IKeyboardControls keyboardControls)
        {
            _keyboardControls = keyboardControls;
        }
        #endregion

        #region Methods
        public void Update(float deltaTime)
        {
            if (UnityEngine.Input.GetKeyUp(_keyboardControls.GuiClose))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
        #endregion
    }
}
