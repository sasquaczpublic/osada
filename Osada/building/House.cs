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
    class House : Building
    {
        public House(ref Player player, ref Field field)
            : base(player, field)
        {
            Cost[0] = 50;
            Cost[1] = 20;
            Cost[2] = 30;
            Cost[3] = 0;

            CrewMax = 2;

            StockMax = new int[] {0,0,0,0};

            Type = "Dom";
            

            Productivity = 0;

            if (!Build(ref player, ref field))
                MessageBox.Show(Error);
            else
            {
                MessageBox.Show("Wybudowano!");
            }
            

        }
        public override bool Build(ref Player player, ref Field field)
        {
            if (!check(player, field))
                return false;
            substractResources(ref player);
            changeImageOnMap("Dom", ref field);
            field.Building = Type;
            return true;
            //dodawanie max osadnikow
        }
        public override void Work(ref Player player)
        {
            if ((Crew%2) == 0 && Crew > 1)
            {
                //powiązać z poziomem technologicznym
                if (player.SettlersAll < player.SettlersMax)
                {
                    player.SettlersAll++;
                    player.Settlers++;
                    player.SettlersMax += 4;
                    MessageBox.Show("Narodziny w: " + Type + " w: " + FieldId);
                }
            }
        }

        public override void Special()
        {
            throw new NotImplementedException();
        }

        public override void Destroy(ref Player player, ref Field field)
        {
            // odejmowanie max osadnikow, jesli jest wiecej niz miejsc to gina
            player.Settlers += Crew;
            field.Building = "Pusto";
            player.SettlersMax -= 4;
            field.Owner = 0;
            field.ImageGenerator();
        }
    }
}
