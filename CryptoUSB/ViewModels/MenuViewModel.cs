﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using CryptoUSB.Controllers;
using CryptoUSB.Views;
using ReactiveUI;
using System.Threading.Tasks;

namespace CryptoUSB.ViewModels;

public class MenuViewModel : ViewModelBase
{
    private string _data;

    public static string Settings => char.ConvertFromUtf32(0x2699);
    public static string Infos => char.ConvertFromUtf32(0x1F6C8);
    public string Data
    {
        get => _data;
        set => this.RaiseAndSetIfChanged(ref _data, value);
    }
    public static async Task OpenKKDFromPC()
    {
        Window owner = ((ClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime).MainWindow;
        if (owner == null)
            return;
        var dialog = new OpenFileDialog();
        string[]? result = null;
        dialog.Filters.Add(new FileDialogFilter() { Name = "Резервная копия Kakadu", Extensions = { "kkd" } });
        result = await dialog.ShowAsync(new Window());
        if (result != null && result.Length > 0)
        {
            try
            {
                bool isEntered = false;
                var passwordWindow = new Window
                {
                    Title = "Введите пароль",
                    Height = 200,
                    Width = 300
                };
                var enterPasswordControl = new EnterPassOpenFile(result[0]);
                passwordWindow.Content = enterPasswordControl;
                
                enterPasswordControl.AcceptButtonClicked += (sender, e) =>
                {
                    isEntered = true;
                    passwordWindow.Close();
                };
                passwordWindow.Closed += (sender, e) =>
                {
                    if (isEntered)
                    {
                        var dataContext = enterPasswordControl.DataContext as EnterPassOpenFileViewModel;
                        OpenFromPCController openFromPCController = new()
                        {
                            FilePath = result[0],
                            Password = dataContext.Password.ToCharArray()
                        };
                        openFromPCController.Open();
                    }
                    
                };
                await passwordWindow.ShowDialog(owner);
            }
            catch
            {

            }
            
        }
    }
}
