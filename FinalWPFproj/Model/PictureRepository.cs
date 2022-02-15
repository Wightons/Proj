using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalWPFproj.Model
{
    public class PictureRepository
    {
        private  List<Picture> pictures;

        public PictureRepository()
        {
            Refrech();
        }
        public void Refrech()
        {
            pictures = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Picture>>(File.ReadAllText("picures.json"));
        }
        public IEnumerable<Picture> GetAll() => pictures;
        public Picture Get(string name) => pictures.FirstOrDefault(x => x.Name == name);
        public void Add(Picture picture)
        {
            pictures.Add(picture);
            WriteToFile();
        }
        public void Remove(Picture picture) 
        {
            pictures.Remove(pictures.FirstOrDefault(x=>x.Name==picture.Name));
            WriteToFile();
        }

        public void Edit(Picture picture)
        {
            var pictureNew = pictures.FirstOrDefault(x => x.Name == picture.Name);

            pictureNew.Name = picture.Name;
            pictureNew.AuthorName = picture.AuthorName;
            pictureNew.Price = picture.Price;
            picture.ImagePath = picture.ImagePath;
        }

        public void WriteToFile()
        {
             
            File.WriteAllText("picures.json", Newtonsoft.Json.JsonConvert.SerializeObject(pictures));
        }


    }
}
