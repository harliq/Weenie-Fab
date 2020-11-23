using System;
using System.Collections.Generic;
using System.Text;

namespace WeenieFab
{
   public class DeathTreasure
    {
        public int ID { get; set; }
        public int DTdid { get; set; }
        public int Tier { get; set; }
        public int UnknownChances { get; set; }

        public int ItemChance { get; set; }
        public int ItemMinAmount { get; set; }
        public int ItemMaxAmount { get; set; }
        public int ItemTreasureTypeSelectionChances { get; set; }

        public int MagicItemChance { get; set; }
        public int MagicItemMinAmount { get; set; }
        public int MagicItemMaxAmount { get; set; }
        public int MagicItemTreasureTypeSelectionChances { get; set; }

        public int MundaneItemChance { get; set; }
        public int MundaneItemMinAmount { get; set; }
        public int MundaneItemMaxAmount { get; set; }
        public int MundaneItemTreasureTypeSelectionChances { get; set; }

        public float LootQualityMod { get; set; }

        public string LastModified { get; set; }
    }
}
