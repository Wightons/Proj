using FinalWPFproj.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalWPFproj.Model
{
    public class Picture : BaseNotify
    {
        private string _imgPath;
        private string _name;
        private string _authorName;
        private int _price;


        public Picture(string path,string name, string authorname, int price)
        {
            _imgPath = path;
            _name = name;
            _authorName = authorname;
            _price = price;
        }

        public Picture()
        {
            _imgPath = String.Empty;
            _name = String.Empty;
            _authorName = String.Empty;
            _price = -1;
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                Notify(nameof(Name));
            }
        }
        public string AuthorName
        {
            get => _authorName;
            set
            {
                _authorName = value;
                Notify(nameof(AuthorName));
            }
        }
        public int Price
        {
            get => _price;
            set
            {
                _price = value;
                Notify(nameof(Price));
            }
        }

        public string ImagePath
        {
            get => _imgPath;
            set
            {
                _imgPath = value;
                Notify(nameof(ImagePath));
            }
        }

    }
}
