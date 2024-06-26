﻿using System.Collections.Specialized;
using System.Web;

using Microsoft.Windows.AppNotifications;

using UpWorker.Contracts.Services;
using Windows.System;

namespace UpWorker.Notifications;

public class AppNotificationService : IAppNotificationService
{
    private readonly INavigationService _navigationService;

    public AppNotificationService(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    ~AppNotificationService()
    {
        Unregister();
    }

    public void Initialize()
    {
        AppNotificationManager.Default.NotificationInvoked += OnNotificationInvoked;

        AppNotificationManager.Default.Register();
    }

    public async void OnNotificationInvoked(AppNotificationManager sender, AppNotificationActivatedEventArgs args)
    {
        // TODO: Handle notification invocations when your app is already running.

        //// // Navigate to a specific page based on the notification arguments.
        //// if (ParseArguments(args.Argument)["action"] == "Settings")
        //// {
        ////    App.MainWindow.DispatcherQueue.TryEnqueue(() =>
        ////    {
        ////        _navigationService.NavigateTo(typeof(SettingsViewModel).FullName!);
        ////    });
        //// }

        var arguments = ParseArguments(args.Argument);
        var urlString = arguments["url"];
        if (!string.IsNullOrEmpty(urlString) && Uri.TryCreate(urlString, UriKind.Absolute, out Uri uri))
        {
            //// For now, we are not using the webview because of a bug: https://github.com/microsoft/microsoft-ui-xaml/issues/9566
            /// Leaving the code here until it is patched. 

            //App.MainWindow.DispatcherQueue.TryEnqueue(() =>
            //{
            //    _navigationService.NavigateToWebView(typeof(WebViewViewModel).FullName, uri);
            //    App.MainWindow.BringToFront();
            //});

            bool success = await Launcher.LaunchUriAsync(uri);
        }
    }

    public bool Show(string payload)
    {
        var appNotification = new AppNotification(payload);

        AppNotificationManager.Default.Show(appNotification);

        return appNotification.Id != 0;
    }

    public NameValueCollection ParseArguments(string arguments)
    {
        return HttpUtility.ParseQueryString(arguments);
    }

    public void Unregister()
    {
        AppNotificationManager.Default.Unregister();
    }
}
