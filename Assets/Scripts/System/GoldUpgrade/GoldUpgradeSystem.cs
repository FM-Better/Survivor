using System.Collections.Generic;
using QFramework;

namespace Survivor
{
    public class GoldUpgradeSystem : AbstractSystem
    {
        public List<GoldUpgradeItem> Items { get; } = new List<GoldUpgradeItem>();
        
        protected override void OnInit()
        {
            Items.Add(new GoldUpgradeItem()
                .WithKey("ExpBallDropRate")
                .WithDescription("提升经验掉落概率5%")
                .WithCost(5)
                .OnUpgrade(() =>
                {
                    Global.ExpBallDropRate.Value += 5;
                    Global.Gold.Value -= 5;
                }));
            Items.Add(new GoldUpgradeItem()
                .WithKey("GoldDropRate")
                .WithDescription("提升金币掉落概率1%")
                .WithCost(5)
                .OnUpgrade(() =>
                {
                    Global.GoldDropRate.Value++;
                    Global.Gold.Value -= 5;
                }));
            Items.Add(new GoldUpgradeItem()
                .WithKey("MaxHp")
                .WithDescription("玩家最大生命值+1")
                .WithCost(20)
                .OnUpgrade(() =>
                {
                    Global.MaxHp.Value++;
                    Global.Hp.Value++;
                    Global.Gold.Value -= 20;
                }));
        }
    }
}
