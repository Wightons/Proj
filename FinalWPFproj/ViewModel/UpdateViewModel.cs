using FinalWPFproj.Infrastructure;
using FinalWPFproj.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FinalWPFproj.ViewModel
{
    public class UpdateViewModel : BaseNotify
    {
        public string Path { get; set; }

        public string Name { get; set; }
        public string AuthorName { get; set; }

        public int Price { get; set; }
        public ICommand UpdateCommand { set; get; }

        private PictureRepository pictureRepository;
        public UpdateViewModel()
        {
            UpdateCommand = new RelayCommand(a =>
            {
                pictureRepository = new PictureRepository();
                pictureRepository.Add(new Picture(Path, Name, AuthorName, Price));
                pictureRepository.WriteToFile();

            });
        }
    }
}
