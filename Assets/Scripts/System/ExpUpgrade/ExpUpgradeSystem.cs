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
            
            Add(new ExpUpgradeItem()
                    .WithKey("SimpleSword")
                    .WithMaxLevel(10)
                    .WithDescription(lv =>
                    {
                        return lv switch
                        {
                            1 => $"剑Lv{lv}：攻击身边的敌人",
                            2 => $"剑Lv{lv}：\n攻击力+3 数量+2",
                            3 => $"剑Lv{lv}：\n攻击力+2 间隔-0.25s",
                            4 => $"剑Lv{lv}：\n攻击力+2 间隔-0.25s",
                            5 => $"剑Lv{lv}：\n攻击力+3 数量+2",
                            6 => $"剑Lv{lv}：\n范围+1 间隔-0.25s",
                            7 => $"剑Lv{lv}：\n攻击力+3 数量+2",
                            8 => $"剑Lv{lv}：\n攻击力+2 范围+1",
                            9 => $"剑Lv{lv}：\n攻击力+3 间隔-0.25s",
                            10 => $"剑Lv{lv}：\n攻击力+3 数量+2",
                            _ => null,
                        };
                    })
                    .OnUpgrade((_, level) =>
                    {
                        switch (level)  
                        {
                            case 1:
                                break;
                            case 2:
                                Global.SimpleSwordDamage.Value += 3;
                                Global.SimpleSwordCount.Value += 2;
                                break;
                            case 3:
                                Global.SimpleSwordDamage.Value += 2;
                                Global.SimpleSwordCD.Value -= 0.25f;
                                break;
                            case 4:
                                Global.SimpleSwordDamage.Value += 2;
                                Global.SimpleSwordCD.Value -= 0.25f;
                                break;
                            case 5:
                                Global.SimpleSwordDamage.Value += 3;
                                Global.SimpleSwordCount.Value += 2;
                                break;
                            case 6:
                                Global.SimpleSwordRange.Value++;
                                Global.SimpleSwordCD.Value -= 0.25f;
                                break;
                            case 7:
                                Global.SimpleSwordDamage.Value += 3;
                                Global.SimpleSwordCount.Value += 2;
                                break;
                            case 8:
                                Global.SimpleSwordDamage.Value += 2;
                                Global.SimpleSwordRange.Value++;
                                break;
                            case 9:
                                Global.SimpleSwordDamage.Value += 3;
                                Global.SimpleSwordCD.Value -= 0.25f;
                                break;
                            case 10:
                                Global.SimpleSwordDamage.Value += 3;
                                Global.SimpleSwordCount.Value += 2;
                                break;
                        }
                    }));
            
            Add(new ExpUpgradeItem()
                    .WithKey("SimpleKnife")
                    .WithMaxLevel(10)
                    .WithDescription(lv =>
                    {
                        return lv switch
                        {
                            1 => $"飞刀Lv{lv}：向最近的敌人发射一把飞刀",
                            2 => $"飞刀Lv{lv}：\n攻击力+3 数量+2",
                            3 => $"飞刀Lv{lv}：\n间隔-0.1s 攻击力+3 数量+1",
                            4 => $"飞刀Lv{lv}：\n间隔-0.1s 穿透+1 数量+1",
                            5 => $"飞刀Lv{lv}：\n攻击力+3 数量+1",
                            6 => $"飞刀Lv{lv}：\n间隔-0.1s 数量+1",
                            7 => $"飞刀Lv{lv}：\n间隔-0.1s 穿透+1 数量+1",
                            8 => $"飞刀Lv{lv}：\n攻击力+3 数量+1",
                            9 => $"飞刀Lv{lv}：\n间隔-0.1s 数量+1",
                            10 => $"飞刀Lv{lv}：\n攻击力+3 数量+1",
                            _ => null,
                        };
                    })
                    .OnUpgrade((_, level) =>
                    {
                        switch (level)  
                        {
                            case 1:
                                break;
                            case 2:
                                Global.SimpleKnifeDamage.Value += 3;
                                Global.SimpleKnifeCount.Value += 2;
                                break;
                            case 3:
                                Global.SimpleKnifeCD.Value -= 0.1f;
                                Global.SimpleKnifeDamage.Value += 3;
                                Global.SimpleKnifeCount.Value++;
                                break;
                            case 4:
                                Global.SimpleKnifeCD.Value -= 0.1f;
                                Global.SimpleKnifeAttackCount.Value++;
                                Global.SimpleKnifeCount.Value++;
                                break;
                            case 5:
                                Global.SimpleKnifeDamage.Value += 3;
                                Global.SimpleKnifeCount.Value++;
                                break;
                            case 6:
                                Global.SimpleKnifeCD.Value -= 0.1f;
                                Global.SimpleKnifeCount.Value++;
                                break;
                            case 7:
                                Global.SimpleKnifeCD.Value -= 0.1f;
                                Global.SimpleKnifeAttackCount.Value++;
                                Global.SimpleKnifeCount.Value++;
                                break;
                            case 8:
                                Global.SimpleKnifeDamage.Value += 3;
                                Global.SimpleKnifeCount.Value++;
                                break;
                            case 9:
                                Global.SimpleKnifeCD.Value -= 0.1f;
                                Global.SimpleKnifeCount.Value++;
                                break;
                            case 10:
                                Global.SimpleKnifeDamage.Value += 3;
                                Global.SimpleKnifeCount.Value++;
                                break;
                        }
                    }));
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
