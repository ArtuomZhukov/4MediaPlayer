using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Win32;
using DevExpress.Mvvm;
using _4MediaPlayer.Model;

namespace _4MediaPlayer.ViewModel
{
    public class MainViewModel : BaseVM
    {
        public ObservableCollection<Media> MediaCollection { get; set; }
        public ICollectionView MediaView { get; set; }
        public Media SelectedMedia { get; set; }

        public MainViewModel()
        {
            MediaCollection = new ObservableCollection<Media>();
            BindingOperations.EnableCollectionSynchronization(MediaCollection, new object());
            MediaView = CollectionViewSource.GetDefaultView(MediaCollection);

        }

        public ICommand AddItem
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    var ofd = new OpenFileDialog
                    {
                        Multiselect = true
                    };
                    if (ofd.ShowDialog() == true)
                    {
                        await Task.Factory.StartNew(() =>
                        {
                            LoadMedia(ofd.FileNames);
                        });
                    }
                });
            }
        }

        public ICommand LoadItem
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    var ofd = new OpenFileDialog
                    {
                        Multiselect = true
                    };
                    if (ofd.ShowDialog() == true)
                    {
                        await Task.Factory.StartNew(() =>
                        {
                            LoadMedia(ofd.FileNames, true);
                        });
                    }
                });
            }
        }

        public ICommand Clear
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    MediaCollection.Clear();
                });
            }
        }

        private void LoadMedia(string[] files, bool clear = false)
        {
            if (clear && files.Length > 0)
            {
                MediaCollection.Clear();
            }
            for (int i = 0; i < files.Length; i++)
            {
                var file = files[i];

                MediaCollection.Add(new Media
                {
                    Path = file,
                    Name = Path.GetFileNameWithoutExtension(file),
                });
            }
            SelectedMedia = MediaCollection.FirstOrDefault(s => s.Path == files.FirstOrDefault());
        }
    }
}
