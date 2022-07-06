using Assets.Core.Scripts.Helpers;
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
        public Item Item { get; protected set; }
        public int Count { get; protected set; } = 1;
        public int StackWeight { get; protected set; }
        public string StackName { get; protected set; }
        public string StackDescription { get; protected set; }

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

            data.Write( Item.ToSerial( ) );

            return data;
        }

        public static ItemStack FromSerial(SerialData data)
        {
            string name = data.ReadString( );
            string desc = data.ReadString( );
            int count = data.ReadInt( );
            int weight = data.ReadInt( );

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
                    Item = null,
                    Count = 0,
                    StackName = "NULL",
                    StackDescription = null,
                    StackWeight = 0,
                };
            }
        }
    }
}
