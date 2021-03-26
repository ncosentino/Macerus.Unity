﻿using Assets.Scripts.Scenes.MainMenu.Gui.Views.MainMenu;
using Assets.Scripts.Scenes.MainMenu.Gui.Views.MainMenu.Noesis;
using Assets.Scripts.Scenes.MainMenu.Input;

using Autofac;

namespace Assets.Scripts.Scenes.MainMenu.Autofac
{
    public sealed class MainMenuModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<GuiInputController>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<GuiInputStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MainMenuLoadHook>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MainMenuSetup>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MainMenuView>()
                .AsImplementedInterfaces()
                .SingleInstance()
                .UsingConstructor(typeof(IMainMenuNoesisViewModel));
            builder
                .RegisterType<MainMenuNoesisViewModel>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MainMenuViewModel>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MainMenuController>()
                .AutoActivate()
                .AsSelf();
        }
    }
}