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
            ResetData();

            Global.Level.Register(_ =>
            {
                RandomAbility();
            });
        }

        public void ResetData()
        {
            Items.Clear();
            
            var simpleDamage1 = Add(new ExpUpgradeItem()
                    .WithKey("SimpleAbilityDamage")
                    .WithDescription(lv => $"简单能力伤害提升1.5倍 Lv{lv}")
                    .OnUpgrade((item) =>
                    {
                        Global.SimpleAbilityDamage.Value *= 1.5f;
                    }))
                .WithMaxLevel(10);

            var simpleCD1 = Add(new ExpUpgradeItem()
                    .WithKey("SimpleAbilityCD")
                    .WithDescription(lv => $"简单能力间隔时间缩短0.5倍 Lv{lv}")
                    .OnUpgrade((item) =>
                    {
                        Global.SimpleAbilityCD.Value *= 0.5f;
                    }))
                .WithMaxLevel(10);
        }
        
        public void RandomAbility()
        {
            List<ExpUpgradeItem> items = new List<ExpUpgradeItem>();
            foreach (var expUpgradeItem in Items)
            {
                if (!expUpgradeItem.UpgradeFinished)
                {
                    items.Add(expUpgradeItem);
                }

                expUpgradeItem.Visible.Value = false;
            }

            var item = items.GetRandomItem();
            if (item != null)
            {
                item.Visible.Value = true;
            }
        }
    }
}
