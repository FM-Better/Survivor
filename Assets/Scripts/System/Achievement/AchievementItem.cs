using System;

namespace Survivor
{
    public class AchievementItem
    {
        public string Key { get; private set; }
        public string Name { get; private set; }
        public string IconName { get; private set; }
        public string Description { get; private set; }
        public bool Unlocked { get; set; }

        private Func<bool> mCondition;
        private Action<AchievementItem> mOnUnlocked;
        
        public bool ConditionCheck() => mCondition();

        #region 初始化相关API
        // 采用链式API设计 用作初始化数据
        public AchievementItem WithKey(string key)
        {
            Key = key;
            return this;
        }
        
        public AchievementItem WithName(string name)
        {
            Name = name;
            return this;
        }

        public AchievementItem WithIconName(string iconName)
        {
            IconName = iconName;
            return this;
        }
        
        public AchievementItem WithDescription(string description)
        {
            Description = description;
            return this;
        }
        
        public AchievementItem WithCondition(Func<bool> condition)
        {
            mCondition = condition;
            return this;
        }

        public AchievementItem OnUnlocked(Action<AchievementItem> onUnlocked)
        {
            mOnUnlocked = onUnlocked;
            return this;
        }
        
        public AchievementItem OnUpdate(Action<AchievementItem> onUnlocked)
        {
            mOnUnlocked = onUnlocked;
            return this;
        }

        public AchievementItem Load(SaveSystem saveSystem)
        {
            Unlocked = saveSystem.LoadBool($"achievement_first_{Key}", false);
            return this;
        }
        #endregion
        
        public void Unlock(SaveSystem saveSystem)
        {
            Unlocked = true;
            saveSystem.SaveBool($"achievement_first_{Key}", true);
            mOnUnlocked?.Invoke(this);
            AchievementSystem.OnAchievementUnlocked.Trigger(this);
        }
    }
}
