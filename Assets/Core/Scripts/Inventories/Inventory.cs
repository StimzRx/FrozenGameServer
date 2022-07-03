using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Scripts.Inventories
{
    public class Inventory
    {
        public Inventory(int slotsX, int slotsY)
        {
            SlotCountX = slotsX;
            SlotCountY = slotsY;


        }



        public int SlotCountX { get; protected set; }
        public int SlotCountY { get; protected set; }

    }
}
