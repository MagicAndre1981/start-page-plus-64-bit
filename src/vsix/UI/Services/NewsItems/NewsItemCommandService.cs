﻿namespace StartPagePlus.UI.Services
{
    using System;
    using System.Collections.Generic;

    using CommunityToolkit.Mvvm.Input;

    using StartPagePlus.UI.Interfaces.NewsItems;

    using StartPagePlus.UI.ViewModels;

    internal class NewsItemCommandService : ServiceBase, INewsItemCommandService
    {
        public NewsItemCommandService() : base()
        { }

        public List<CommandViewModel> GetCommands(Action refresh, Action openSettings)
            => new()
            {
                new CommandViewModel
                {
                    Name = "Refresh",
                    Command = new RelayCommand(refresh),
                },
                new CommandViewModel
                {
                    Name = "Settings",
                    Command = new RelayCommand(openSettings),
                }
            };
    }
}