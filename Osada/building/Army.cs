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
    class Army : Building
    {
         public Army(ref Player player, ref Field field)
            : base(player, field)
        {
            Cost[0] = 0;
            Cost[1] = 0;
            Cost[2] = 0;
            Cost[3] = 0;

            CrewMax = 100;

            StockMax = new int[] {0,0,0,0};

            Type = "Army";
            
            Productivity = 0;

            if (!Build(ref player, ref field))
                MessageBox.Show(Error);
            else
            {
                MessageBox.Show("Obóz wojskowy rozstawiony!");
            }

        }
        public override bool Build(ref Player player, ref Field field)
        {
            if (!check(player, field))
                return false;
            if (player.Soldiers < 10)
            {
                Error = "Za mało żołnierzy na rozstawienie obozu";
                return false;
            }
            changeImageOnMap("Armia", ref field);
            field.Building = Type;
            return true;
        }

        public override void Work(ref Player player)
        {
            
        }

        public override void Special()
        {
            throw new NotImplementedException();
        }

        public override void Destroy(ref Player player, ref Field field)
        {
            player.Soldiers += Crew;
            field.Building = "Pusto";
            field.Owner = 0;
            field.ImageGenerator();
        }
    }
}
