using Assets.Core.Scripts.Interfaces;
using Assets.Core.Scripts.Registries;
using Assets.Core.Scripts.Serialization;
using KableNet.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Scripts.Inventories
{
    public class ItemStack : Serializable<ItemStack>
    {
        public Item Item { get; set; }
        public int Count { get; set; } = 1;
        public float StackWeight { get; set; }
        public string StackName { get; set; }
        public string StackDescription { get; set; }

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

        public SerialData ToSerial( )
        {
            SerialData data = new SerialData( );
            data.Write( StackName );
            data.Write( StackDescription );
            data.Write( Count );
            data.Write( StackWeight );

            if(Item is not null)
            {
                data.Write( Item.ToSerial( ) );
            }
            else
            {
                Item tmpItem = Item.EMPTY;
                data.Write( tmpItem.ToSerial( ) );
            }

            return data;
        }

        public static ItemStack FromSerial(SerialData data)
        {
            string name = data.ReadString( );
            string desc = data.ReadString( );
            int count = data.ReadInt( );
            float weight = data.ReadFloat( );

            Item item = Item.FromSerial( data );

            return new ItemStack( )
            {
                StackName = name,
                StackDescription = desc,
                Count = count,
                StackWeight = weight,
                Item = item,
            };
        }

        public static ItemStack EMPTY
        {
            get
            {
                return new ItemStack( )
                {
                    Item = Item.EMPTY,
                    Count = 0,
                    StackName = "NULL",
                    StackDescription = "NULL",
                    StackWeight = 0,
                };
            }
        }
    }
}
