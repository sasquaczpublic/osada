using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Osada.map;
using Osada.player;

namespace Osada.building
{
    class Ground : Building
    {
        public Ground(ref Player player, ref Field field): base(player, field)
        {
            Error = "Za mało danych";
            MessageBox.Show(Error);
        }
        public Ground(ref Player player, ref Field field, string typeOfFood): base(player, field)
        {
            Cost[0] = 50;
            Cost[1] = 20;
            Cost[2] = 30;
            Cost[3] = 0;

            CrewMax = 2;
            //Crew = 1;
            StockMax = new int[] { 0, 0, 0, 0 };

            if (!Build(ref player, ref field, typeOfFood))
                MessageBox.Show(Error);
            else
            {
                MessageBox.Show("Wybudowano!");
            }
            

        }
        public override bool Build(ref Player player, ref Field field)
        {
            return false;
        }
        public bool Build(ref Player player, ref Field field, string typeOfFood)
        {
            if (!check(player, field))
                return false;
            if (typeOfFood == "Pole - zboże")
            {
                Productivity = 100;
                // zmienić na zalezne od technologii
            }
            else if (typeOfFood == "Pole - kukurydza")
            {
                Productivity = 120;
            }
            else
            {
                return false;
            }
            Type = typeOfFood;
            field.Building = Type;
            substractResources(ref player);
            field.ImageGenerator();
            return true;
        }
        public override void Work(ref Player player)
        {
            if (Crew > 0)
            {
                float production = Convert.ToSingle(Crew) / Convert.ToSingle(CrewMax);
                player.Resource[0] += Convert.ToInt32(Productivity*production);
                player.Resource[1] += Convert.ToInt32(Productivity * production);
                player.Resource[2] += Convert.ToInt32(Productivity * production);
                player.Resource[3] += Convert.ToInt32(Productivity * production);
                MessageBox.Show("Produkcja w budynku: " + Type + " w: " + FieldId + " Produkcja: " + Convert.ToString(Convert.ToInt32(Productivity * production)));

            }
        }

        public override void Special()
        {
            throw new NotImplementedException();
        }

        public override void Destroy(ref Player player, ref Field field)
        {

            player.Settlers += Crew;
            field.Building = "Pusto";
            field.Owner = 0;
            field.ImageGenerator();
        }
    }
}
