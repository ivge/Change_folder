using System;
using System.IO;
using System.Windows.Input;
using System.Windows.Forms;

namespace CompareFolder.ViewModel.Commands
{
    public class SelectFolderCommand : ICommand
    {
        private readonly ViewModel viewModel;

        public SelectFolderCommand(ViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return (string.IsNullOrWhiteSpace(viewModel.FirstFolder) || string.IsNullOrWhiteSpace(viewModel.SecondFolder));
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            if (string.IsNullOrWhiteSpace(viewModel.FirstFolder)) viewModel.FirstFolder = SelectFolder();
            if (string.IsNullOrWhiteSpace(viewModel.SecondFolder)) viewModel.SecondFolder = SelectFolder();
            var t = viewModel.GetFilesList();
        }
        private string SelectFolder()
        {
            var result = string.Empty;
            using (var selectedFolder = new FolderBrowserDialog())
            {
                DialogResult dialogResult = selectedFolder.ShowDialog();
                if (dialogResult == DialogResult.OK && !string.IsNullOrWhiteSpace(selectedFolder.SelectedPath))
                {
                    result = selectedFolder.SelectedPath;
                }
            }
            return result;
        }
    }
}
