using System;

using ProjectXyz.Api.Framework;

namespace Assets.Scripts.Scenes.MainMenu.Gui.Views.MainMenu
{
    public interface IMainMenuViewModel : IReadOnlyMainMenuViewModel
    {
        void RequestClose();

        void RequestNewGame();

        void RequestOptions();

        void RequestExit();
    }

    public interface IReadOnlyMainMenuViewModel
    {
        event EventHandler<EventArgs> NewGameRequested;

        event EventHandler<EventArgs> OptionsRequested;

        event EventHandler<EventArgs> ExitRequested;

        event EventHandler<EventArgs> CloseRequested;

        IIdentifier BackgroundImageResourceId { get; }
    }
}