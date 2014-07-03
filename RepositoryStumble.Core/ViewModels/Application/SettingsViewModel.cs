﻿using System;
using ReactiveUI;
using RepositoryStumble.Core.Services;
using System.Reactive.Linq;
using Xamarin.Utilities.Core.ViewModels;

namespace RepositoryStumble.Core.ViewModels.Application
{
    public class SettingsViewModel : BaseViewModel
    {
        private bool _syncWithGitHub;
        public bool SyncWithGitHub
        {
            get { return _syncWithGitHub; }
            set { this.RaiseAndSetIfChanged(ref _syncWithGitHub, value); }
        }

        public IReactiveCommand LogoutCommand { get; private set; }

        public SettingsViewModel(IApplicationService applicationService)
        {
            SyncWithGitHub = applicationService.Account.SyncWithGitHub;
            LogoutCommand = new ReactiveCommand();

            this.WhenAnyValue(x => x.SyncWithGitHub).Skip(1).Subscribe(x =>
            {
                applicationService.Account.SyncWithGitHub = x;
                applicationService.Account.Save();
            });
        }
    }
}
