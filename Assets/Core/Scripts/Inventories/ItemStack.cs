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
    }
}
