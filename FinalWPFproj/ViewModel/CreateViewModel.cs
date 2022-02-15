using FinalWPFproj.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using FinalWPFproj.View;
using System.ComponentModel;
using System.Collections.ObjectModel;
using FinalWPFproj.Model;
using System.IO;

namespace FinalWPFproj.ViewModel
{
    public class CreateViewModel : BaseNotify
    { 
        public string Path { get; set; }

        public string Name { get; set; }
        public string AuthorName { get; set; }

        public int Price { get; set; }
        public ICommand AddCommand { set; get; }

        private PictureRepository pictureRepository;
        public CreateViewModel()
        {
            AddCommand = new RelayCommand(a =>
            {
                pictureRepository = new PictureRepository();
                pictureRepository.Add(new Picture(Path, Name, AuthorName, Price));
                pictureRepository.WriteToFile();

            });
        }
        
    }




}
