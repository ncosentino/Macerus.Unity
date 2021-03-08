using System;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;

namespace Assets.Scripts.Scenes.MainMenu.Gui.Views.MainMenu
{
    public sealed class MainMenuViewModel : IMainMenuViewModel
    {
        public event EventHandler<EventArgs> NewGameRequested;
        
        public event EventHandler<EventArgs> OptionsRequested;
        
        public event EventHandler<EventArgs> ExitRequested;

        public event EventHandler<EventArgs> CloseRequested;

        public IIdentifier BackgroundImageResourceId { get; } =
            new StringIdentifier("Graphics/Gui/MainMenu/background");

        public void RequestClose() =>
            CloseRequested?.Invoke(this, EventArgs.Empty);

        public void RequestNewGame() =>
            NewGameRequested?.Invoke(this, EventArgs.Empty);

        public void RequestOptions() =>
            OptionsRequested?.Invoke(this, EventArgs.Empty);

        public void RequestExit() =>
            ExitRequested?.Invoke(this, EventArgs.Empty);
    }
}