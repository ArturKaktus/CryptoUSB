using Avalonia.Controls;
using Avalonia.Interactivity;
using CryptoUSB.ViewModels;
using System;

namespace CryptoUSB.Views
{
    public partial class EnterPassSaveFile : UserControl
    {
        readonly EnterPassSaveFileViewModel viewModel;
        public event EventHandler AcceptButtonClicked;
        public EnterPassSaveFile()
        {
            InitializeComponent();
            
        }
        public EnterPassSaveFile(string path) : this() 
        {
            viewModel = new EnterPassSaveFileViewModel(path);
            this.DataContext = viewModel;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            AcceptButtonClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
