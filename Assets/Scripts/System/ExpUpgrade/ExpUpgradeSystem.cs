using System.Collections.Generic;
using System.Linq;
using QAssetBundle;
using QFramework;
using UnityEngine.Serialization;

namespace Survivor
{
    public class ExpUpgradeSystem : AbstractSystem
    {
        public List<ExpUpgradeItem> Items { get; } = new List<ExpUpgradeItem>();
        public static bool AllUnlockedFinish = false;

        // 记录配套的能力组合
        public Dictionary<string, string> PairedKeys = new Dictionary<string, string>()
        {
            {AbilityConfig.SimpleSwordKey, AbilityConfig.CriticalKey},
            {AbilityConfig.BombKey, AbilityConfig.DamageKey},
            {AbilityConfig.SimpleKnifeKey, AbilityConfig.FlyKey},
            {AbilityConfig.BasketballKey, AbilityConfig.SpeedKey},
            {AbilityConfig.RotateSwordKey, AbilityConfig.ExpKey},
            
            {AbilityConfig.CriticalKey, AbilityConfig.SimpleSwordKey},
            {AbilityConfig.DamageKey, AbilityConfig.BombKey},
            {AbilityConfig.FlyKey, AbilityConfig.SimpleKnifeKey},
            {AbilityConfig.SpeedKey, AbilityConfig.BasketballKey},
            {AbilityConfig.ExpKey, AbilityConfig.RotateSwordKey},
        };
        
        // 记录每个key对应的超级武器是否合成
        public Dictionary<string, BindableProperty<bool>> keyToSuperIsAlive = new Dictionary<string, BindableProperty<bool>>()
        {
            {AbilityConfig.SimpleSwordKey, Global.SuperSimpleSword},
            {AbilityConfig.BombKey, Global.SuperBomb},
            {AbilityConfig.SimpleKnifeKey, Global.SuperKnife},
            {AbilityConfig.BasketballKey, Global.SuperBasketBall},
            {AbilityConfig.RotateSwordKey, Global.SuperRotateSword},
        };
        
        // 记录每个key对应的item
        public Dictionary<string, ExpUpgradeItem> keyToItems = new Dictionary<string, ExpUpgradeItem>();
        
        private void Add(ExpUpgradeItem expUpgradeItem)
        {
            Items.Add(expUpgradeItem);
            keyToItems.Add(expUpgradeItem.Key, expUpgradeItem);
        }
        
        protected override void OnInit()
        {
            ResetData();

            Global.Level.Register(_ =>
            {
                RandomAbility();
            });
        }

        public static void CheckAllUnlockedFinish()
        {
            AllUnlockedFinish = Global.Interface.GetSystem<ExpUpgradeSystem>().Items
                .All(item => item.UpgradeFinished);
        }
        
        public void ResetData()
        {
            Items.Clear();
            keyToItems.Clear();
            
            Add(new ExpUpgradeItem(true,true)
                    .WithKey(AbilityConfig.SimpleSwordKey)
                    .WithIconName(AbilityConfig.SimpleSwordIconName)
                    .WithPairedName("合成后的剑")
                    .WithPairedIconName(Icon.PAIRED_SIMPLE_SWORD_ICON)
                    .WithPairedDescription("攻击力翻倍 攻击范围翻倍")
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
            
            Add(new ExpUpgradeItem(true,true)
                    .WithKey(AbilityConfig.SimpleKnifeKey)
                    .WithIconName(AbilityConfig.SimpleKnifeIconName)
                    .WithPairedName("合成后的飞刀")
                    .WithPairedIconName(Icon.PAIRED_SIMPLE_KNIFE_ICON)
                    .WithPairedDescription("攻击力翻倍")
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
            
            Add(new ExpUpgradeItem(true,true)
                    .WithKey(AbilityConfig.RotateSwordKey)
                    .WithIconName(AbilityConfig.RotateSwordIconName)
                    .WithPairedName("合成后的守卫剑")
                    .WithPairedIconName(Icon.PAIRED_ROTATE_SWORD_ICON)
                    .WithPairedDescription("攻击力翻倍 旋转速度翻倍")
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
            
            Add(new ExpUpgradeItem(true,true)
                    .WithKey(AbilityConfig.BasketballKey)
                    .WithIconName(AbilityConfig.BasketballIconName)
                    .WithPairedName("合成后的篮球")
                    .WithPairedIconName(Icon.PAIRED_BALL_ICON)
                    .WithPairedDescription("攻击力翻倍 体积翻倍")
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
            
            Add(new ExpUpgradeItem(false,true)
                    .WithKey(AbilityConfig.BombKey)
                    .WithIconName(AbilityConfig.BombIconName)
                    .WithPairedName("合成后的炸弹")
                    .WithPairedIconName(Icon.PAIRED_BOMB_ICON)
                    .WithPairedDescription("每隔15秒爆炸一次")
                    .WithMaxLevel(10)
                    .WithDescription(lv =>
                    {
                        return lv switch
                        {
                            1 => $"炸弹Lv{lv}：\n攻击所有敌人(敌人掉落)",
                            2 => $"炸弹Lv{lv}：\n掉落概率+5% 攻击力+5",
                            3 => $"炸弹Lv{lv}：\n掉落概率+5% 攻击力+5",
                            4 => $"炸弹Lv{lv}：\n掉落概率+5% 攻击力+5",
                            5 => $"炸弹Lv{lv}：\n掉落概率+5% 攻击力+5",
                            6 => $"炸弹Lv{lv}：\n掉落概率+5% 攻击力+5",
                            7 => $"炸弹Lv{lv}：\n掉落概率+5% 攻击力+5",
                            8 => $"炸弹Lv{lv}：\n掉落概率+5% 攻击力+5",
                            9 => $"炸弹Lv{lv}：\n掉落概率+5% 攻击力+5",
                            10 => $"炸弹Lv{lv}：\n掉落概率+10% 攻击力+5",
                            _ => null,
                        };
                    })
                    .OnUpgrade((_, level) =>
                    {
                        switch (level)  
                        {
                            case 1:
                                Global.BombUnlocked.Value = true;
                                break;
                            case 2:
                                Global.BombDropRate.Value += 5;
                                Global.BombDamage.Value += 5;
                                break;
                            case 3:
                                Global.BombDropRate.Value += 5;
                                Global.BombDamage.Value += 5;
                                break;
                            case 4:
                                Global.BombDropRate.Value += 5;
                                Global.BombDamage.Value += 5;
                                break;
                            case 5:
                                Global.BombDropRate.Value += 5;
                                Global.BombDamage.Value += 5;
                                break;
                            case 6:
                                Global.BombDropRate.Value += 5;
                                Global.BombDamage.Value += 5;
                                break;
                            case 7:
                                Global.BombDropRate.Value += 5;
                                Global.BombDamage.Value += 5;
                                break;
                            case 8:
                                Global.BombDropRate.Value += 5;
                                Global.BombDamage.Value += 5;
                                break;
                            case 9:
                                Global.BombDropRate.Value += 5;
                                Global.BombDamage.Value += 5;
                                break;
                            case 10:
                                Global.BombDropRate.Value += 10;
                                Global.BombDamage.Value += 5;
                                break;
                        }
                    }));
            
            Add(new ExpUpgradeItem(false)
                    .WithKey(AbilityConfig.CriticalKey)
                    .WithIconName(AbilityConfig.CriticalIconName)
                    .WithMaxLevel(5)
                    .WithDescription(lv =>
                    {
                        return lv switch
                        {
                            1 => $"暴击Lv{lv}：\n每次伤害15%概率暴击",
                            2 => $"暴击Lv{lv}：\n每次伤害28%概率暴击",
                            3 => $"暴击Lv{lv}：\n每次伤害43%概率暴击",
                            4 => $"暴击Lv{lv}：\n每次伤害50%概率暴击",
                            5 => $"暴击Lv{lv}：\n每次伤害80%概率暴击",
                            _ => null,
                        };
                    })
                    .OnUpgrade((_, level) =>
                    {
                        switch (level)  
                        {
                            case 1:
                                Global.CriticalRate.Value = 15;
                                break;
                            case 2:
                                Global.CriticalRate.Value = 28;
                                break;
                            case 3:
                                Global.CriticalRate.Value = 43;
                                break;
                            case 4:
                                Global.CriticalRate.Value = 50;
                                break;
                            case 5:
                                Global.CriticalRate.Value = 80;
                                break;
                        }
                    }));
            
            Add(new ExpUpgradeItem(false)
                .WithKey(AbilityConfig.DamageKey)
                .WithIconName(AbilityConfig.DamageIconName)
                .WithMaxLevel(5)
                .WithDescription(lv =>
                {
                    return lv switch
                    {
                        1 => $"伤害率Lv{lv}：\n增加20%额外伤害",
                        2 => $"伤害率Lv{lv}：\n增加40%额外伤害",
                        3 => $"伤害率Lv{lv}：\n增加60%额外伤害",
                        4 => $"伤害率Lv{lv}：\n增加80%额外伤害",
                        5 => $"伤害率Lv{lv}：\n增加100%额外伤害",
                        _ => null,
                    };
                })
                .OnUpgrade((_, level) =>
                {
                    switch (level)  
                    {
                        case 1:
                            Global.DamageRate.Value = 1.2f;
                            break;
                        case 2:
                            Global.DamageRate.Value = 1.4f;
                            break;
                        case 3:
                            Global.DamageRate.Value = 1.6f;
                            break;
                        case 4:
                            Global.DamageRate.Value = 1.8f;
                            break;
                        case 5:
                            Global.DamageRate.Value = 2f;
                            break;
                    }
                }));
            
            Add(new ExpUpgradeItem(false)
                .WithKey(AbilityConfig.FlyKey)
                .WithIconName(AbilityConfig.FlyIconName)
                .WithMaxLevel(3)
                .WithDescription(lv =>
                {
                    return lv switch
                    {
                        1 => $"飞射物Lv{lv}：\n额外飞射物数量+1",
                        2 => $"飞射物Lv{lv}：\n额外飞射物数量+1",
                        3 => $"飞射物Lv{lv}：\n额外飞射物数量+1",
                        _ => null,
                    };
                })
                .OnUpgrade((_, level) =>
                {
                    switch (level)  
                    {
                        case 1:
                            Global.AdditionalFlyCount.Value++;
                            break;
                        case 2:
                            Global.AdditionalFlyCount.Value++;
                            break;
                        case 3:
                            Global.AdditionalFlyCount.Value++;
                            break;
                    }
                }));
            
            Add(new ExpUpgradeItem(false)
                .WithKey(AbilityConfig.SpeedKey)
                .WithIconName(AbilityConfig.SpeedIconName)
                .WithMaxLevel(5)
                .WithDescription(lv =>
                {
                    return lv switch
                    {
                        1 => $"移动速度Lv{lv}：\n增加25%移动速度",
                        2 => $"移动速度Lv{lv}：\n增加50%移动速度",
                        3 => $"移动速度Lv{lv}：\n增加75%移动速度",
                        4 => $"移动速度Lv{lv}：\n增加100%移动速度",
                        5 => $"移动速度Lv{lv}：\n增加150%移动速度",
                        _ => null,
                    };
                })
                .OnUpgrade((_, level) =>
                {
                    switch (level)  
                    {
                        case 1:
                            Global.SpeedRate.Value = 1.25f;
                            break;
                        case 2:
                            Global.SpeedRate.Value = 1.5f;
                            break;
                        case 3:
                            Global.SpeedRate.Value = 1.75f;
                            break;
                        case 4:
                            Global.SpeedRate.Value = 2f;
                            break;
                        case 5:
                            Global.SpeedRate.Value = 2.5f;
                            break;
                    }
                }));
            
            Add(new ExpUpgradeItem(false)
                .WithKey(AbilityConfig.PickUpAreaKey)
                .WithIconName(AbilityConfig.PickUpAreaIconName)
                .WithMaxLevel(3)
                .WithDescription(lv =>
                {
                    return lv switch
                    {
                        1 => $"拾取范围Lv{lv}：\n额外增加100%范围",
                        2 => $"拾取范围Lv{lv}：\n额外增加200%范围",
                        3 => $"拾取范围Lv{lv}：\n额外增加300%范围",
                        _ => null,
                    };
                })
                .OnUpgrade((_, level) =>
                {
                    switch (level)  
                    {
                        case 1:
                            Global.PickUpAreaRange.Value = 2f;
                            break;
                        case 2:
                            Global.PickUpAreaRange.Value = 3f;
                            break;
                        case 3:
                            Global.PickUpAreaRange.Value = 4f;
                            break;
                    }
                }));
            
            Add(new ExpUpgradeItem(false)
                .WithKey(AbilityConfig.ExpKey)
                .WithIconName(AbilityConfig.ExpIconName)
                .WithMaxLevel(5)
                .WithDescription(lv =>
                {
                    return lv switch
                    {
                        1 => $"经验值Lv{lv}：\n额外增加5%掉落概率",
                        2 => $"经验值Lv{lv}：\n额外增加8%掉落概率",
                        3 => $"经验值Lv{lv}：\n额外增加12%掉落概率",
                        4 => $"经验值Lv{lv}：\n额外增加17%掉落概率",
                        5 => $"经验值Lv{lv}：\n额外增加25%掉落概率",
                        _ => null,
                    };
                })
                .OnUpgrade((_, level) =>
                {
                    switch (level)  
                    {
                        case 1:
                            Global.AdditionalExpRate.Value = 5;
                            break;
                        case 2:
                            Global.AdditionalExpRate.Value = 8;
                            break;
                        case 3:
                            Global.AdditionalExpRate.Value = 12;
                            break;
                        case 4:
                            Global.AdditionalExpRate.Value = 17;
                            break;
                        case 5:
                            Global.AdditionalExpRate.Value = 25;
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

            if (items.Count >= 4)
            {
                items.GetAndRemoveRandomItem().Visible.Value = true;
                items.GetAndRemoveRandomItem().Visible.Value = true;
                items.GetAndRemoveRandomItem().Visible.Value = true;
                items.GetAndRemoveRandomItem().Visible.Value = true;
            }
            else
            {
                foreach (var item in items)
                {
                    if (item != null)
                    {
                        item.Visible.Value = true;
                    }
                }
            }
        }
    }
}
