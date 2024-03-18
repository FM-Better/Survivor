using System.Collections.Generic;
using System.Linq;
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
                                Global.SimpleSwordUnlocked.Value = true;
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
                                Global.SimpleKnifeUnlocked.Value = true;
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
            
            Add(new ExpUpgradeItem()
                    .WithKey("RotateSword")
                    .WithMaxLevel(10)
                    .WithDescription(lv =>
                    {
                        return lv switch
                        {
                            1 => $"守卫剑Lv{lv}：环绕主角身边的剑",
                            2 => $"守卫剑Lv{lv}：\n数量+1 攻击力+1",
                            3 => $"守卫剑Lv{lv}：\n攻击力+2 速度+25%",
                            4 => $"守卫剑Lv{lv}：\n速度+50%",
                            5 => $"守卫剑Lv{lv}：\n数量+1 攻击力+1",
                            6 => $"守卫剑Lv{lv}：\n攻击力+2 速度+25%",
                            7 => $"守卫剑Lv{lv}：\n数量+1 攻击力+1",
                            8 => $"守卫剑Lv{lv}：\n攻击力+2 速度+25%",
                            9 => $"守卫剑Lv{lv}：\n数量+1 攻击力+1",
                            10 => $"守卫剑Lv{lv}：\n攻击力+2 速度+25%",
                            _ => null,
                        };
                    })
                    .OnUpgrade((_, level) =>
                    {
                        switch (level)  
                        {
                            case 1:
                                Global.RotateSwordUnlocked.Value = true;
                                break;
                            case 2:
                                Global.RotateSwordCount.Value++;
                                Global.RotateSwordDamage.Value++;
                                break;
                            case 3:
                                Global.RotateSwordDamage.Value += 2;
                                Global.RotateSwordSpeed.Value *= 1.25f;
                                break;
                            case 4:
                                Global.RotateSwordSpeed.Value *= 1.5f;
                                break;
                            case 5:
                                Global.RotateSwordCount.Value++;
                                Global.RotateSwordDamage.Value++;
                                break;
                            case 6:
                                Global.RotateSwordDamage.Value += 2;
                                Global.RotateSwordSpeed.Value *= 1.25f;
                                break;
                            case 7:
                                Global.RotateSwordCount.Value++;
                                Global.RotateSwordDamage.Value++;
                                break;
                            case 8:
                                Global.RotateSwordDamage.Value += 2;
                                Global.RotateSwordSpeed.Value *= 1.25f;
                                break;
                            case 9:
                                Global.RotateSwordCount.Value++;
                                Global.RotateSwordDamage.Value++;
                                break;
                            case 10:
                                Global.RotateSwordDamage.Value += 2;
                                Global.RotateSwordSpeed.Value *= 1.25f;
                                break;
                        }
                    }));
            
            Add(new ExpUpgradeItem()
                    .WithKey("BasketBall")
                    .WithMaxLevel(10)
                    .WithDescription(lv =>
                    {
                        return lv switch
                        {
                            1 => $"篮球Lv{lv}：在屏幕内反弹的篮球",
                            2 => $"篮球Lv{lv}：\n攻击力+3",
                            3 => $"篮球Lv{lv}：\n数量+1",
                            4 => $"篮球Lv{lv}：\n攻击力+3",
                            5 => $"篮球Lv{lv}：\n数量+1",
                            6 => $"篮球Lv{lv}：\n攻击力+3",
                            7 => $"篮球Lv{lv}：\n速度+20%",
                            8 => $"篮球Lv{lv}：\n攻击力+3",
                            9 => $"篮球Lv{lv}：\n速度+20%",
                            10 => $"篮球Lv{lv}：\n数量+1",
                            _ => null,
                        };
                    })
                    .OnUpgrade((_, level) =>
                    {
                        switch (level)  
                        {
                            case 1:
                                Global.BasketBallUnlocked.Value = true;
                                break;
                            case 2:
                                Global.BasketBallDamage.Value += 3;
                                break;
                            case 3:
                                Global.BasketBallCount.Value++;
                                break;
                            case 4:
                                Global.BasketBallDamage.Value += 3;
                                break;
                            case 5:
                                Global.BasketBallCount.Value++;
                                break;
                            case 6:
                                Global.BasketBallDamage.Value += 3;
                                break;
                            case 7:
                                Global.BasktetBallSpeed.Value *= 1.2f;
                                break;
                            case 8:
                                Global.BasketBallDamage.Value += 3;
                                break;
                            case 9:
                                Global.BasktetBallSpeed.Value *= 1.2f;
                                break;
                            case 10:
                                Global.BasketBallCount.Value++;
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

            var abilities = items.Take(4);
            foreach (var ability in abilities)
            {
                if (ability != null)
                {
                    ability.Visible.Value = true;
                }
            }
            // var item = items.GetRandomItem();
            // if (item != null)
            // {
            //     item.Visible.Value = true;
            // }
        }
    }
}
