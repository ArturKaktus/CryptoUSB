﻿using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CryptoUSB.Services;
using CryptoUSB.ViewModels;
using CryptoUSB.Views;


namespace CryptoUSB;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MenuViewModel()
            };
            CastomModules();
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void CastomModules()
    {
        DeviceFinder.Instance.StartSearch();
    }
}
