using System.Collections.Generic;
using QFramework;

namespace Survivor
{
    public class GoldUpgradeSystem : AbstractSystem, ICanSave
    {
        public List<GoldUpgradeItem> Items { get; } = new List<GoldUpgradeItem>();
        
        public static EasyEvent OnGoldUpgradeSystemChanged = new EasyEvent();
        
        public GoldUpgradeItem Add(GoldUpgradeItem goldUpgradeItem)
        {
            Items.Add(goldUpgradeItem);
            return goldUpgradeItem;
        }
        
        protected override void OnInit()
        {
            #region 经验相关
            Add(new GoldUpgradeItem()
                    .WithKey("ExpBallDropRate_Lv1")
                    .WithDescription("提升经验掉落概率1% Lv1")
                    .WithCost(100)
                    .OnUpgrade((item) =>
                    {
                        Global.ExpBallDropRate.Value++;
                        Global.Gold.Value -= item.Cost;
                    }))
                .WithNext(Add(new GoldUpgradeItem()
                    .WithKey("ExpBallDropRate_Lv2")
                    .WithDescription("提升经验掉落概率1% Lv2")
                    .WithCost(500)
                    .OnUpgrade((item) =>
                    {
                        Global.ExpBallDropRate.Value++;
                        Global.Gold.Value -= item.Cost;
                    })))
                .WithNext(Add(new GoldUpgradeItem()
                    .WithKey("ExpBallDropRate_Lv3")
                    .WithDescription("提升经验掉落概率2% Lv3")
                    .WithCost(3000)
                    .OnUpgrade((item) =>
                    {
                        Global.ExpBallDropRate.Value++;
                        Global.Gold.Value -= item.Cost;
                    })));
            #endregion

            #region 金币相关
            Add(new GoldUpgradeItem()
                .WithKey("GoldDropRate_Lv1")
                .WithDescription("提升金币掉落概率1% Lv1")
                .WithCost(100)
                .OnUpgrade((item) =>
                {
                    Global.GoldDropRate.Value++;
                    Global.Gold.Value -= item.Cost;
                }))
                .WithNext(Add(new GoldUpgradeItem()
                    .WithKey("GoldDropRate_Lv2")
                    .WithDescription("提升金币掉落概率2%")
                    .WithCost(1000)
                    .OnUpgrade((item) =>
                    {
                        Global.GoldDropRate.Value += 2;
                        Global.Gold.Value -= item.Cost;
                    })))
                .WithNext(Add(new GoldUpgradeItem()
                    .WithKey("GoldDropRate_Lv3")
                    .WithDescription("提升金币掉落概率3% Lv3")
                    .WithCost(2000)
                    .OnUpgrade((item) =>
                    {
                        Global.GoldDropRate.Value += 3;
                        Global.Gold.Value -= item.Cost;
                    })))
                .WithNext(Add(new GoldUpgradeItem()
                    .WithKey("GoldDropRate_Lv4")
                    .WithDescription("提升金币掉落概率4% Lv4")
                    .WithCost(5000)
                    .OnUpgrade((item) =>
                    {
                        Global.GoldDropRate.Value += 4;
                        Global.Gold.Value -= item.Cost;
                    })));
            #endregion

            #region 最大生命值相关
            Add(new GoldUpgradeItem()
                .WithKey("MaxHp_Lv1")
                .WithDescription("玩家最大生命值+1 Lv1")
                .WithCost(1000)
                .OnUpgrade((item) =>
                {
                    Global.MaxHp.Value++;
                    Global.Gold.Value -= item.Cost;
                }))
                .WithNext(Add(new GoldUpgradeItem()
                    .WithKey("MaxHp_Lv2")
                    .WithDescription("玩家最大生命值+1 Lv2")
                    .WithCost(2000)
                    .OnUpgrade((item) =>
                    {
                        Global.MaxHp.Value ++;
                        Global.Gold.Value -= item.Cost;
                    })))
                .WithNext(Add(new GoldUpgradeItem()
                    .WithKey("MaxHp_Lv3")
                    .WithDescription("玩家最大生命值+1 Lv3")
                    .WithCost(4000)
                    .OnUpgrade((item) =>
                    {
                        Global.MaxHp.Value++;
                        Global.Gold.Value -= item.Cost;
                    })))
                .WithNext(Add(new GoldUpgradeItem()
                    .WithKey("MaxHp_Lv4")
                    .WithDescription("玩家最大生命值+1 Lv4")
                    .WithCost(5000)
                    .OnUpgrade((item) =>
                    {
                        Global.MaxHp.Value++;
                        Global.Gold.Value -= item.Cost;
                    })))
                .WithNext(Add(new GoldUpgradeItem()
                    .WithKey("MaxHp_Lv5")
                    .WithDescription("玩家最大生命值+1 Lv5")
                    .WithCost(6000)
                    .OnUpgrade((item) =>
                    {
                        Global.MaxHp.Value++;
                        Global.Gold.Value -= item.Cost;
                    })))
                .WithNext(Add(new GoldUpgradeItem()
                    .WithKey("MaxHp_Lv6")
                    .WithDescription("玩家最大生命值+1 Lv6")
                    .WithCost(7000)
                    .OnUpgrade((item) =>
                    {
                        Global.MaxHp.Value++;
                        Global.Gold.Value -= item.Cost;
                    })))
                .WithNext(Add(new GoldUpgradeItem()
                    .WithKey("MaxHp_Lv7")
                    .WithDescription("玩家最大生命值+1 Lv7")
                    .WithCost(8000)
                    .OnUpgrade((item) =>
                    {
                        Global.MaxHp.Value++;
                        Global.Gold.Value -= item.Cost;
                    })))
                .WithNext(Add(new GoldUpgradeItem()
                    .WithKey("MaxHp_Lv8")
                    .WithDescription("玩家最大生命值+1 Lv8")
                    .WithCost(9000)
                    .OnUpgrade((item) =>
                    {
                        Global.MaxHp.Value++;
                        Global.Hp.Value++;
                        Global.Gold.Value -= item.Cost;
                    })));
            #endregion
            
            Load(); // 加载存档数据

            OnGoldUpgradeSystemChanged.Register(() =>
            {
                Save();
            });
        }

        public void Save()
        {
            var saveSystem = this.GetSystem<SaveSystem>();
            foreach (var item in Items)
            {
                saveSystem.SaveBool(item.Key, item.UpgradeFinished);
            }
        }

        public void Load()
        {
            var saveSystem = this.GetSystem<SaveSystem>();
            foreach (var item in Items)
            {
                item.UpgradeFinished = saveSystem.LoadBool(item.Key,false);
            }
        }
    }
}
