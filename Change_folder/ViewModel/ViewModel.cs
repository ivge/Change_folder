using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CompareFolder.ViewModel.Commands;
using System.IO;
using CompareFolder.Model;
using System.ComponentModel;

namespace CompareFolder.ViewModel
{
    public class ViewModel : ViewModelBase
    {

        public ViewModel()
        {
            this.Initialize();
        }

        private string firstFolder;
        public string FirstFolder
        {
            get { return firstFolder; }
            set
            {
                if (value != firstFolder)
                {
                    firstFolder = value;
                    base.RaisePropertyChangedEvent("FirstFolder");
                }
            }
        }

        private string secondFolder;
        public string SecondFolder
        {
            get { return secondFolder; }
            set
            {
                if (value != secondFolder)
                {
                    secondFolder = value;
                    base.RaisePropertyChangedEvent("SecondFolder");
                }
            }
        }

        private ObservableCollection<CompareFolder.Model.File> files;
        public ObservableCollection<CompareFolder.Model.File> Files
        {
            get { return files; }

            set
            {
                files = value;
                base.RaisePropertyChangedEvent("Files");
            }
        }

        private void Initialize()
        {
            this.SelectFolder = new SelectFolderCommand(this);
        }

        public ICommand SelectFolder { get; set; }

        public ObservableCollection<CompareFolder.Model.File> GetFilesList()
        {
            var result = new ObservableCollection<CompareFolder.Model.File>();
            //var firstFolderFiles = Directory.GetFiles(firstFolderPath);
            //var secondFolderFiles = Directory.GetFiles(secondFolderPath);
            foreach (var filePath in Directory.EnumerateFiles(FirstFolder, @"*.*", SearchOption.TopDirectoryOnly))
            {
                var file = new CompareFolder.Model.File();
                var fileInfo = new FileInfo(filePath);
                file.Name = Path.GetFileName(filePath);
                file.Size = fileInfo.Length;
                file.ModifyDate = fileInfo.LastWriteTimeUtc;
                file.Status = Model.File.Statuses.ExistsOnlyInFirstFolder;

                result.Add(file);
            }

            foreach (var filePath in Directory.EnumerateFiles(SecondFolder, @"*.*", SearchOption.TopDirectoryOnly))
            {
                var file = new CompareFolder.Model.File();
                var fileInfo = new FileInfo(filePath);
                file.Name = Path.GetFileName(filePath);
                file.Size = fileInfo.Length;
                file.ModifyDate = fileInfo.LastWriteTimeUtc;
                //var t = String.Compare(file.Name, f.Name, false);
                var _file = result.FirstOrDefault(f => f.Name.Equals(file.Name, StringComparison.CurrentCulture));

                if (_file == null)
                {
                    file.Status = Model.File.Statuses.ExistsOnlyInSecondFolder;
                    result.Add(file);
                }
                else if (_file.Size == file.Size)
                    _file.Status = Model.File.Statuses.ExistsInBothFoldersWithTheSameSizes;
                else
                    _file.Status = Model.File.Statuses.ExistsInBothFoldersWithDifferentSizes;
            }


            return result;

        }
    }


}
