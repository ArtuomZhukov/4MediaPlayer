using System;
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
using System.Windows;

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

        public ICommand AtStart
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    var files = Environment.GetCommandLineArgs();
                    LoadMedia(files);
                    if (MediaCollection.Count > 0)
                    {
                        SelectedMedia = MediaCollection[0];
                    }
                });
            }
        }

        private string _SearchText { get; set; }
        public string SearchText
        {
            get => _SearchText;
            set
            {
                _SearchText = value;
                MediaView.Filter = (obj) =>
                {
                    if (obj is Media video)
                    {
                        return video.Name.ToLower().Contains(SearchText.ToLower());
                    }
                    return false;
                };
                MediaView.Refresh();
            }
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

        public void LoadMedia(string[] files, bool clear = false)
        {
            if (clear && files.Length > 0)
            {
                MediaCollection.Clear();
            }
            foreach (var file in files)
            {
                if (File.Exists(file))
                {
                    Media.MediaType type = Media.GetType(file);
                    if (type != Media.MediaType.Unknown)
                    {
                        MediaCollection.Add(new Media
                        {
                            Path = file,
                            Type = type,
                            Name = Path.GetFileName(file)
                        });
                    }
                }
            }
            SelectedMedia = MediaCollection.FirstOrDefault(s => s.Path == files.FirstOrDefault());
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
    }
}