using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Osada.map
{
    public class Field
    {
        public int FieldId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Height { get; set; }
        public string Type { get; set; }
        public SolidColorBrush Color { get; set; }
        public Image Img { get; set; }
        public string Building { get; set; }
        public int Owner { get; set; }
        public Field(int nFieldId, int nX, int nY, string nHeight, string nType, SolidColorBrush nColor)
        {
            FieldId = nFieldId;
            X = nX;
            Y = nY;
            Height = nHeight;
            Type = nType;
            Color = nColor;
            Owner = 0;
            Building = "Pusto";
        }
        public void ImageGenerator()
        {
            if (Building != "Pusto")
            {
                string imgType = "pack://application:,,,/images/";
                imgType = imgType + Building + ".png";
                Image nImg = new Image();
                nImg.Source = (ImageSource)new BitmapImage(new Uri(imgType));
                Img = nImg;
            }
            else if (Type != "Pusto")
            {
                string imgType = "pack://application:,,,/images/";
                imgType = imgType + Type + ".png";
                Image nImg = new Image();
                nImg.Source = (ImageSource)new BitmapImage(new Uri(imgType));
                Img = nImg;
            }
        }

        
    }

}
