using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GothicItemsLookup
{
    static class ColorsConst
    {
        public static Color resultSummary_NotFound = Color.FromArgb(200, 200, 200);

       // Source: ZEN
       public static Color resultWorlds_Chest       = Color.FromArgb(96, 145, 173);
       public static Color resultWorlds_Item        = Color.FromArgb(86, 222, 181);

       // Source: Scripts
       public static Color resultScripts_Npc        = Color.FromArgb(255, 219, 99);
       public static Color resultScripts_Trader     = Color.FromArgb(226, 122, 63);
       public static Color resultScripts_Mission    = Color.FromArgb(237, 80, 69);
       public static Color resultScripts_Other      = Color.FromArgb(250, 250, 220);

       public static int[] resultSelected = new int[] { -50, -50, -30 };
    }
}
