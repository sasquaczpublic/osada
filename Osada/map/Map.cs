using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Osada.map
{
    class Map
    {
        
        public void MapGenerator(ObservableCollection<Field> Board)
        {
            int id = 0;
            Random generator = new Random(); // 454 id mapy
            string[] height = { "Woda", "Nizina", "Równina", "Wyżyna", "Góry" };
            string[] type = { "Pusto", "Las", "Kopalnia", "Budynek" };
            SolidColorBrush[] colorOnMap =
            {
                new SolidColorBrush(Color.FromArgb(255, 42, 179, 255)),
                new SolidColorBrush(Color.FromArgb(255, 130, 255, 130)),
                new SolidColorBrush(Color.FromArgb(255,50,255,50)),
                new SolidColorBrush(Color.FromArgb(255,250,242,99)),
                new SolidColorBrush(Color.FromArgb(255,239,149,57)),
                new SolidColorBrush(Color.FromArgb(255,0,109,22)),
                new SolidColorBrush(Color.FromArgb(255,192,98,2)),
            };
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++, id++)
                {
                    int heightRandom = generator.Next(0, 5);
                    int typeRandom;
                    int color;
                    if (heightRandom == 0)
                    {
                        typeRandom = 0;
                    }
                    else if (heightRandom == 3 || heightRandom == 4)
                    {
                        do
                        {
                            typeRandom = generator.Next(0, 3);
                        } while (typeRandom == 1);
                    }
                    else
                    {
                        do
                        {
                            typeRandom = generator.Next(0, 3);
                        } while (typeRandom == 2);
                    }
                    

                    Random rando = new Random();
                    Field fieldToAdd = new Field(id, j, i, height[heightRandom], type[typeRandom], colorOnMap[heightRandom]);
                    fieldToAdd.ImageGenerator();
                    Board.Add(fieldToAdd);
                }
            }
        }
    }
}
