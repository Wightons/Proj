using FinalWPFproj.Infrastructure;
using FinalWPFproj.Model;
using FinalWPFproj.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace FinalWPFproj.ViewModel
{
    public class MainViewModel : BaseNotify
    {
        private ObservableCollection<Picture> _pictures;
        private Picture _selectedPicture;
        private string _searchString;
        private UserControl currentView;
        private UserControl updateView;
        public UserControl CurrentView
        {
            get { return currentView; }
            set 
            {
                currentView = value;
                Notify(nameof(CurrentView));
            }
        }

        public UserControl UpdateView
        {
            get { return updateView; }
            set
            {
                updateView = value;
                Notify(nameof(UpdateView));
            }
        }

        private ICollectionView picturesView;
        public ICollectionView PicturesView 
        {
            get => picturesView;
            set
            {
                picturesView = value;
                Notify();
            }
        } 
        public ICommand ShowPictureListCommand { set; get; }
        public string SearchString
        {
            get => _searchString;
            set
            {
                _searchString = value;
                Notify(nameof(SearchString));
                PicturesView?.Refresh();
            }
        }
        public CreateViewModel CreateViewModel { set; get; }

        private RelayCommand _create;

        public ICommand CreateCommand
        {
            get => _create ?? (_create = new RelayCommand(pic =>
              {
                  CurrentView = new CreateView();
              }));
        }

        public Picture SelectedPicture
        {
            get => _selectedPicture;
            set
            {
                _selectedPicture = value;
                Notify(nameof(SelectedPicture));
            }
        }
        public ICommand DeleteCommand { set; get; }
        public ICommand UpdateCommand { set; get; }
        private PictureRepository pictureRepository;
        public MainViewModel()
        {
            pictureRepository = new PictureRepository();
            CreateViewModel = new CreateViewModel();
            CurrentView = new PicturesList();
            _pictures = new ObservableCollection<Picture>(pictureRepository.GetAll());
            PicturesView = CollectionViewSource.GetDefaultView(_pictures);
            PicturesView.Filter = Filter;

            ShowPictureListCommand = new RelayCommand(x =>
            {
                pictureRepository.Refrech();
                _pictures = new ObservableCollection<Picture>(pictureRepository.GetAll());
                PicturesView = CollectionViewSource.GetDefaultView(_pictures);
                CurrentView = new PicturesList();
            });

            Notify(nameof(PicturesView));

            DeleteCommand = new RelayCommand(x =>
            {
                if (SelectedPicture!=null)
                {
                    pictureRepository.Remove(SelectedPicture);
                    _pictures.Remove(SelectedPicture);
                    PicturesView = CollectionViewSource.GetDefaultView(_pictures);
                    pictureRepository.WriteToFile();
                }

            });
            UpdateCommand = new RelayCommand(x =>
            {   
                pictureRepository.Remove(SelectedPicture);
                _pictures.Remove(SelectedPicture);
                PicturesView = CollectionViewSource.GetDefaultView(_pictures);
                pictureRepository.WriteToFile();
                CurrentView = new CreateView();
            });
        }

        private bool Filter(object obj)
        {
            var pic = obj as Picture;
            var isContainsName = true;
            if (!string.IsNullOrEmpty(_searchString) && !string.IsNullOrWhiteSpace(_searchString))
            {
                isContainsName = pic?.Name.ToUpper()?.Contains(_searchString.ToUpper()) ?? false;
            }
            return isContainsName;
        }
    
    }
}
