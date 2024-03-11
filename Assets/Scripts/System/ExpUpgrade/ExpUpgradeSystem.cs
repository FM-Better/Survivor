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
            var simpleDamage1 = Add(new ExpUpgradeItem()
                .WithKey("SimpleAbilityDamage_Lv1")
                .WithDescription("简单能力伤害提升1.5倍 Lv1")
                .OnUpgrade((item) =>
                {
                    Global.SimpleAbilityDamage.Value *= 1.5f;
                }));
            
            var simpleDamage2 = Add(new ExpUpgradeItem()
                .WithKey("SimpleAbilityDamage_Lv1")
                .WithDescription("简单能力伤害提升1.5倍 Lv2")
                .OnUpgrade((item) =>
                {
                    Global.SimpleAbilityDamage.Value *= 1.5f;
                }))
                .WithCondition((_) => simpleDamage1.UpgradeFinished);
            simpleDamage1.OnChanged.Register(() =>
            {
                simpleDamage2.OnChanged.Trigger();
            });
            
            var simpleDamage3 = Add(new ExpUpgradeItem()
                .WithKey("SimpleAbilityDamage_Lv1")
                .WithDescription("简单能力伤害提升1.5倍 Lv3")
                .OnUpgrade((item) =>
                {
                    Global.SimpleAbilityDamage.Value *= 1.5f;
                }))
                .WithCondition((_) => simpleDamage2.UpgradeFinished);
            simpleDamage2.OnChanged.Register(() =>
            {
                simpleDamage3.OnChanged.Trigger();
            });

            var simpleCD1 = Add(new ExpUpgradeItem()
                .WithKey("SimpleAbilityCD_Lv1")
                .WithDescription("简单能力间隔时间缩短0.5倍 Lv1")
                .OnUpgrade((item) =>
                {
                    Global.SimpleAbilityCD.Value *= 0.5f;
                }));
            
            var simpleCD2 = Add(new ExpUpgradeItem()
                    .WithKey("SimpleAbilityCD_Lv2")
                    .WithDescription("简单能力间隔时间缩短0.5倍 Lv2")
                    .OnUpgrade((item) =>
                    {
                        Global.SimpleAbilityCD.Value *= 0.5f;
                    }))
                .WithCondition((_) => simpleDamage1.UpgradeFinished);
            simpleCD1.OnChanged.Register(() =>
            {
                simpleCD2.OnChanged.Trigger();
            });
            
            var simpleCD3 = Add(new ExpUpgradeItem()
                    .WithKey("SimpleAbilityCD_Lv3")
                    .WithDescription("简单能力间隔时间缩短0.5倍 Lv3")
                    .OnUpgrade((item) =>
                    {
                        Global.SimpleAbilityCD.Value *= 0.5f;
                    }))
                .WithCondition((_) => simpleDamage2.UpgradeFinished);
            simpleCD2.OnChanged.Register(() =>
            {
                simpleCD3.OnChanged.Trigger();
            });
        }
    }
}
