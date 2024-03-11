using System.Collections.Generic;
using QFramework;

namespace Survivor
{
    public class ExpUpgradeSystem : AbstractSystem
    {
        public List<ExpUpgradeItem> Items { get; } = new List<ExpUpgradeItem>();
        
        public static EasyEvent OnExpUpgradeSystemChanged = new EasyEvent();
        
        public ExpUpgradeItem Add(ExpUpgradeItem expUpgradeItem)
        {
            Items.Add(expUpgradeItem);
            return expUpgradeItem;
        }
        
        protected override void OnInit()
        {
            Add(new ExpUpgradeItem()
                .WithKey("SimpleAbilityDamage_Lv1")
                .WithDescription("简单能力伤害提升1.5倍 Lv1")
                .OnUpgrade((item) =>
                {
                    Global.SimpleAbilityDamage.Value *= 1.5f;
                }));
        }
    }
}
