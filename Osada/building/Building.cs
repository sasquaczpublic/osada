using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Osada.map;
using Osada.player;

namespace Osada.building
{
    public abstract class Building
    {
        public int[] Cost { get; set; }
        public int Crew { get; set; }
        public int CrewMax { get; set; }
        public int Health { get; set; }
        public int Owner { get; set; }
        public int Productivity { get; set; }
        public int[] Stock { get; set; }
        public int[] StockMax { get; set; }
        //public float[] StockPercent { get; set; }
        public string Type { get; set; }
        public string FoodType { get; set; }
        public string Error { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int FieldId { get; set; }
        protected Building(Player player, Field field)
        {
            Owner = player.PlayerId;
            field.Owner = Owner;
            FieldId = field.FieldId;
            Crew = 0;
            Health = 100;
            Stock = Cost = new[] {0,0,0,0};
            FoodType = null;
            //StockPercent = new float[] {0f, 0f, 0f, 0f};
        }
        public abstract bool Build(ref Player player, ref Field field);
        private bool checkResource(Player player)
        {
            if (Cost[0] > player.Resource[0])
            {
                Error = "Za mało żywności";
                return false;
            }
            if (Cost[1] > player.Resource[1])
            {
                Error = "Za mało złota";
                return false;
            }
            if (Cost[2] > player.Resource[2])
            {
                Error = "Za mało kamieni";
                return false;
            }
            if (Cost[3] > player.Resource[3])
            {
                Error = "Za mało drewna";
                return false;
            }
            return true;
        }
        private bool checkField(Field field)
        {
            if (field.Building != "Pusto")
            {
                Error = "Coś już jest w tym miejscu";
                return false;
            }
            if(field.Height == "Woda")
            {
                Error = "Na wodzie nie można stawiać budynków";
                return false;
            }
            if (field.Height == "Góry")
            {
                Error = "Na górach nie można stawiać budynków";
                return false;
            }
            return true;
        }
        protected bool check(Player player, Field field)
        {
            if (!checkField(field))
                return false;
            if (!checkResource(player))
                return false;
            return true;
        }
        protected void substractResources(ref Player player)
        {
            player.Resource[0] -= Cost[0];
            player.Resource[1] -= Cost[1];
            player.Resource[2] -= Cost[2];
            player.Resource[3] -= Cost[3];
        }
        protected void changeImageOnMap(string type, ref Field field)
        {
            string imgType = "pack://application:,,,/images/";
            imgType = imgType + type + ".png";
            Image nImg = new Image();
            nImg.Source = (ImageSource)new BitmapImage(new Uri(imgType));
            field.Img = nImg;
        }
        public bool EmployCrew(ref Player player)
        {
            if (Crew >= CrewMax)
                return false;
            if (player.Settlers <= 0)
                return false;
            player.Settlers--;
            Crew++;
            return true;
        }
        public bool ReleaseCrew(ref Player player)
        {
            if (Crew < 1)
                return false;
            player.Settlers++;
            Crew--;
            return true;
        }
        protected void productivityRefresh()
        {
            
        }




        public abstract void Work(ref Player player);
        public abstract void Special();
        public abstract void Destroy(ref Player player, ref Field field);
    }
}
