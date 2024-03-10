using System.Collections.Generic;
using QFramework;
using UnityEditor;

namespace Survivor
{
    public class GoldUpgradeSystem : AbstractSystem
    {
        public List<GoldUpgradeItem> Items { get; } = new List<GoldUpgradeItem>();

        public GoldUpgradeItem Add(GoldUpgradeItem goldUpgradeItem)
        {
            Items.Add(goldUpgradeItem);
            return goldUpgradeItem;
        }
        
        protected override void OnInit()
        {
            #region 经验相关
            var expLevel1 = Add(new GoldUpgradeItem()
                .WithKey("ExpBallDropRate_Lv1")
                .WithDescription("提升经验掉落概率5% Lv1")
                .WithCost(5)
                .OnUpgrade((item) =>
                {
                    Global.ExpBallDropRate.Value += 5;
                    Global.Gold.Value -= item.Cost;
                }));

            var expLevel2 = Add(new GoldUpgradeItem()
                .WithKey("ExpBallDropRate_Lv2")
                .WithDescription("提升经验掉落概率5% Lv2")
                .WithCost(15)
                .OnUpgrade((item) =>
                {
                    Global.ExpBallDropRate.Value += 5;
                    Global.Gold.Value -= item.Cost;
                }))
                .WithCondition((_) => expLevel1.UpgradeFinished);
            
            var expLevel3 = Add(new GoldUpgradeItem()
                    .WithKey("ExpBallDropRate_Lv3")
                    .WithDescription("提升经验掉落概率5% Lv3")
                    .WithCost(25)
                    .OnUpgrade((item) =>
                    {
                        Global.ExpBallDropRate.Value += 5;
                        Global.Gold.Value -= item.Cost;
                    }))
                .WithCondition((_) => expLevel2.UpgradeFinished);
            #endregion

            #region 金币相关
            var goldLevel1 = Add(new GoldUpgradeItem()
                .WithKey("GoldDropRate_Lv1")
                .WithDescription("提升金币掉落概率1% Lv1")
                .WithCost(5)
                .OnUpgrade((item) =>
                {
                    Global.GoldDropRate.Value++;
                    Global.Gold.Value -= item.Cost;
                }));
            
            var goldLevel2 = Add(new GoldUpgradeItem()
                .WithKey("GoldDropRate_Lv2")
                .WithDescription("提升金币掉落概率2%")
                .WithCost(20)
                .OnUpgrade((item) =>
                {
                    Global.GoldDropRate.Value += 2;
                    Global.Gold.Value -= item.Cost;
                }))
                .WithCondition((_) => goldLevel1.UpgradeFinished);
            
            var goldLevel3 = Add(new GoldUpgradeItem()
                .WithKey("GoldDropRate_Lv3")
                .WithDescription("提升金币掉落概率3% Lv3")
                .WithCost(50)
                .OnUpgrade((item) =>
                {
                    Global.GoldDropRate.Value += 3;
                    Global.Gold.Value -= item.Cost;
                }))
                .WithCondition((_) => goldLevel2.UpgradeFinished);
            #endregion

            #region 最大生命值相关
            var maxHpLevel1 = Add(new GoldUpgradeItem()
                .WithKey("MaxHp_Lv1")
                .WithDescription("玩家最大生命值+1 Lv1")
                .WithCost(20)
                .OnUpgrade((item) =>
                {
                    Global.MaxHp.Value++;
                    Global.Hp.Value++;
                    Global.Gold.Value -= item.Cost;
                }));
            
            var maxHpLevel2 = Add(new GoldUpgradeItem()
                .WithKey("MaxHp_Lv2")
                .WithDescription("玩家最大生命值+2 Lv2")
                .WithCost(40)
                .OnUpgrade((item) =>
                {
                    Global.MaxHp.Value += 2;
                    Global.Hp.Value += 2;
                    Global.Gold.Value -= item.Cost;
                }))
                .WithCondition((_) => maxHpLevel1.UpgradeFinished);
            
            var maxHpLevel3 = Add(new GoldUpgradeItem()
                .WithKey("MaxHp_Lv3")
                .WithDescription("玩家最大生命值+4 Lv3")
                .WithCost(70)
                .OnUpgrade((item) =>
                {
                    Global.MaxHp.Value += 4;
                    Global.Hp.Value += 4;
                    Global.Gold.Value -= item.Cost;
                }))
                .WithCondition((_) => maxHpLevel2.UpgradeFinished);
            #endregion
        }
    }
}
