using Assets.Core.Scripts.Registries;
using KableNet.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Scripts.Inventories
{
    public class ItemStack
    {
        public Item Item { get; protected set; }
        public int Count { get; protected set; } = 1;
        public string StackName { get; protected set; }
        public List<string> StackDescription { get; protected set; }

        internal void DecreaseCount(int amt)
        {
            Count -= amt;
        }
        internal void IncreaseCount(int amt)
        {
            Count += amt;
        }

        public static bool CheckMatchItems(ItemStack a, ItemStack b)
        {
            return ItemRegistry.GetIdentifierForItem( a.Item ) == ItemRegistry.GetIdentifierForItem( b.Item );
        }


        public static ItemStack EMPTY
        {
            get
            {
                return new ItemStack( )
                {
                    Item = null,
                    Count = 0,
                    StackName = "NULL",
                    StackDescription = new List<string>( ),
                };
            }
        }
    }
}
