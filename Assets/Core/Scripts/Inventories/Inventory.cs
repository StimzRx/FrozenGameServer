using KableNet.Math;
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

            Initialize( );
        }

        private void Initialize()
        {
            Stacks = new ItemStack[ SlotCountX, SlotCountY ];
            for(int x = 0; x < SlotCountX; x++)
            {
                for(int y = 0; y < SlotCountY; y++)
                {
                    Stacks[ x, y ] = ItemStack.EMPTY;
                }
            }
        }

        /// <summary>
        /// Sets stack at location. Returning the item stack that was there, or empty if none.
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="stack"></param>
        /// <returns></returns>
        public ItemStack SetStack(Vec2i pos, ItemStack stack)
        {
            if(!SanityCheckPos(pos))
            {
                return ItemStack.EMPTY;
            }

            ItemStack oldStack = Stacks[ pos.x, pos.y ];
            Stacks[ pos.x, pos.y ] = stack;

            SlotUpdateEvent?.Invoke( pos, stack );

            return oldStack;
        }

        public ItemStack GetStack(Vec2i pos)
        {
            if(!SanityCheckPos(pos))
            {
                return ItemStack.EMPTY;
            }

            return Stacks[ pos.x, pos.y ];
        }

        /// <summary>
        /// Gets the first stack matching the given ItemStack and returns its pos
        /// </summary>
        /// <param name="stack"></param>
        /// <returns></returns>
        public Vec2i FindStack(ItemStack stack)
        {
            for( int x=0; x < SlotCountX; x++ )
            {
                for( int y=0; y < SlotCountY; y++)
                {
                    Vec2i foundPos = new Vec2i( x, y );
                    ItemStack found = GetStack( foundPos );
                    if(found == stack)
                    {
                        return foundPos;
                    }
                }
            }

            return null;
        }

        public ItemStack AddStack(ItemStack stack)
        {
            // Try to merge the stack with existing stacks of the same item
            for( int x = 0; x < SlotCountX; x++ )
            {
                for ( int y = 0; y < SlotCountY; y++ )
                {
                    Vec2i pos = new Vec2i( x, y );
                    if(!SanityCheckPos(pos))
                    {
                        continue;
                    }
                    ItemStack foundStack = GetStack( pos );
                    if( ItemStack.CheckMatchItems(stack, foundStack) )
                    {
                        if( foundStack.Count < foundStack.Item.MaxStackSize )
                        {
                            int freeSpace = foundStack.Item.MaxStackSize - foundStack.Count;
                            int filledSpace = Math.Clamp( stack.Count - freeSpace, 0, freeSpace );
                            foundStack.IncreaseCount( filledSpace );
                            stack.DecreaseCount( filledSpace );

                            SlotUpdateEvent?.Invoke( pos, foundStack );

                            if( stack.Count <= 0)
                            {
                                break;
                            }
                        }
                    }
                }
                if( stack.Count <= 0 )
                {
                    break;
                }
            }

            if( stack.Count > 0 )
            {
                return stack;
            }

            // Try to find a open slot to put the leftover stack
            for( int x = 0; x < SlotCountX; x++ )
            {
                for( int y = 0; y < SlotCountY; y++ )
                {

                    Vec2i pos = new Vec2i( x, y );

                    if(IsSlotFree(pos))
                    {
                        SetStack( pos, stack );
                        return ItemStack.EMPTY;
                    }
                }
            }

            return stack;
        }

        public bool IsSlotFree(Vec2i pos)
        {
            if( !SanityCheckPos(pos) )
            {
                return false;
            }

            ItemStack stack = GetStack( pos );
            if ( stack == ItemStack.EMPTY )
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if a position is valid within the inventory's Stacks array
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private bool SanityCheckPos(Vec2i pos)
        {
            if ( pos.x > 0 && pos.x < SlotCountX && pos.y > 0 && pos.y < SlotCountY )
            {
                ItemStack stack = GetStack( pos );
                if( stack is null )
                {
                    SetStack( pos, ItemStack.EMPTY );
                }

                return true;
            }
            return false;
        }

        protected ItemStack[ , ] Stacks { get; set; }

        public int SlotCountX { get; protected set; }
        public int SlotCountY { get; protected set; }


        public delegate void OnSlotUpdate( Vec2i pos, ItemStack newStack );
        public OnSlotUpdate SlotUpdateEvent;
    }
}
