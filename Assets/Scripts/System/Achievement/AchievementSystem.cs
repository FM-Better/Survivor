using System.Collections.Generic;
using System.Linq;
using QFramework;
using UnityEngine;

namespace Survivor
{
    public class AchievementSystem : AbstractSystem
    {
        public List<AchievementItem> Items = new List<AchievementItem>();
        
        public static EasyEvent<AchievementItem> OnAchievementUnlocked = new EasyEvent<AchievementItem>();

        protected override void OnInit()
        {
            var savesystem = this.GetSystem<SaveSystem>();

            Add(new AchievementItem()
                .WithKey("3_minutes")
                .WithName("坚持三分钟")
                .WithDescription("坚持3分钟\n奖励1000金币")
                .WithIconName("achievement_time_icon")
                .WithCondition(() => Global.Timer.Value >= 60 * 3)
                .OnUnlocked(_ =>
                {
                    Global.Gold.Value += 1000;
                })
                .Load(savesystem));
            
            Add(new AchievementItem()
                .WithKey("5_minutes")
                .WithName("坚持五分钟")
                .WithDescription("坚持5分钟\n奖励1000金币")
                .WithIconName("achievement_time_icon")
                .WithCondition(() => Global.Timer.Value >= 60 * 5)
                .OnUnlocked(_ =>
                {
                    Global.Gold.Value += 1000;
                })
                .Load(savesystem));
            
            Add(new AchievementItem()
                .WithKey("10_minutes")
                .WithName("坚持十分钟")
                .WithDescription("坚持10分钟\n奖励1000金币")
                .WithIconName("achievement_time_icon")
                .WithCondition(() => Global.Timer.Value >= 60 * 10)
                .OnUnlocked(_ =>
                {
                    Global.Gold.Value += 1000;
                })
                .Load(savesystem));
            
            Add(new AchievementItem()
                .WithKey("15_minutes")
                .WithName("坚持十五分钟")
                .WithDescription("坚持15分钟\n奖励1000金币")
                .WithIconName("achievement_time_icon")
                .WithCondition(() => Global.Timer.Value >= 60 * 15)
                .OnUnlocked(_ =>
                {
                    Global.Gold.Value += 1000;
                })
                .Load(savesystem));
            
            Add(new AchievementItem()
                .WithKey("lv30")
                .WithName("30级")
                .WithDescription("第一次升到30级\n奖励1000金币")
                .WithIconName("achievement_level_icon")
                .WithCondition(() => Global.Level.Value >= 30)
                .OnUnlocked(_ =>
                {
                    Global.Gold.Value += 1000;
                })
                .Load(savesystem));
            
            Add(new AchievementItem()
                .WithKey("lv50")
                .WithName("50级")
                .WithDescription("第一次升到50级\n奖励1000金币")
                .WithIconName("achievement_level_icon")
                .WithCondition(() => Global.Level.Value >= 30)
                .OnUnlocked(_ =>
                {
                    Global.Gold.Value += 1000;
                })
                .Load(savesystem));
            
            Add(new AchievementItem()
                .WithKey("first_time_paired_ball")
                .WithName("合成后的篮球")
                .WithDescription("第一次解锁合成后的篮球\n奖励1000金币")
                .WithIconName("paired_ball_icon")
                .WithCondition(() => Global.SuperBasketBall.Value)
                .OnUnlocked(_ =>
                {
                    Global.Gold.Value += 1000;
                })
                .Load(savesystem));
            
            Add(new AchievementItem()
                .WithKey("first_time_paired_bomb")
                .WithName("合成后的炸弹")
                .WithDescription("第一次解锁合成后的炸弹\n奖励1000金币")
                .WithIconName("paired_bomb_icon")
                .WithCondition(() => Global.SuperBomb.Value)
                .OnUnlocked(_ =>
                {
                    Global.Gold.Value += 1000;
                })
                .Load(savesystem));
            
            Add(new AchievementItem()
                .WithKey("first_time_paired_sword")
                .WithName("合成后的剑")
                .WithDescription("第一次解锁合成后的剑\n奖励1000金币")
                .WithIconName("paired_simple_sword_icon")
                .WithCondition(() => Global.SuperSimpleSword.Value)
                .OnUnlocked(_ =>
                {
                    Global.Gold.Value += 1000;
                })
                .Load(savesystem));
            
            Add(new AchievementItem()
                .WithKey("first_time_paired_knife")
                .WithName("合成后的飞刀")
                .WithDescription("第一次解锁合成后的飞刀\n奖励1000金币")
                .WithIconName("paired_simple_knife_icon")
                .WithCondition(() => Global.SuperKnife.Value)
                .OnUnlocked(_ =>
                {
                    Global.Gold.Value += 1000;
                })
                .Load(savesystem));
            
            Add(new AchievementItem()
                .WithKey("first_time_paired_circle")
                .WithName("合成后的守卫剑")
                .WithDescription("第一次解锁合成后的守卫剑\n奖励1000金币")
                .WithIconName("paired_simple_knife_icon")
                .WithCondition(() => Global.SuperRotateSword.Value)
                .OnUnlocked(_ =>
                {
                    Global.Gold.Value += 1000;
                })
                .Load(savesystem));
            
            Add(new AchievementItem()
                .WithKey("achievement_all")
                .WithName("全部能力升级")
                .WithDescription("全部能力升级完成\n奖励1000金币")
                .WithIconName("achievement_all_icon")
                .WithCondition(() => ExpUpgradeSystem.AllUnlockedFinish)
                .OnUnlocked(_ =>
                {
                    Global.Gold.Value += 1000;
                })
                .Load(savesystem));
            
            ActionKit.OnUpdate.Register(() =>
            {
                if (Time.frameCount % 10 == 0)
                {
                    foreach (var achievementItem in Items.Where(item => !item.Unlocked && item.ConditionCheck()))
                    {
                        achievementItem.Unlock(savesystem);
                    }
                }
            });
        }

        public AchievementItem Add(AchievementItem item)
        {
            Items.Add(item);
            return item;
        }
    }
}
