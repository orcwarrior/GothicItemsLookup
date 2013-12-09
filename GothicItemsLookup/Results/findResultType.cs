using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GothicItemsLookup.Scanners
{
    public enum findResultType
    {
        CHEST, ITEM,/*<=worlds*/
        MISSION, NPC, TRADER, OTHER,MAX
    }/*<=scripts*/
    internal static class findResultType_Extension
    {
        internal static string getName(this findResultType e)
        {
            switch (e)
            {
                case findResultType.CHEST: return "Skrzynia";
                case findResultType.ITEM: return "Przedmiot";
                case findResultType.MISSION: return "Dialog";
                case findResultType.NPC: return "Npc";
                case findResultType.TRADER: return "Handlarz";
                case findResultType.OTHER: return "Inne";
            }
            throw new NotImplementedException("enum extension getName(), not fully implemented!");
        }
        internal static findResultType getAtIndex(this findResultType e,int idx)
        {
            switch (idx)
            {
                case 0:return findResultType.CHEST;
                case 1:return findResultType.ITEM;
                case 2:return findResultType.MISSION;
                case 3:return findResultType.NPC;
                case 4:return findResultType.TRADER;
                case 5: return findResultType.OTHER;
            }
            throw new NotImplementedException("enum extension getName(), not fully implemented!");
        }
    }
}