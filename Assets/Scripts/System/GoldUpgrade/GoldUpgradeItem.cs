using System;

namespace Survivor
{
   public class GoldUpgradeItem
    {
        public string Key { get; private set; }
        public string Description { get; private set; }
        public int Cost { get; private set; }
        public bool UpgradeFinished { get; set; } = false; 
        
        private Action<GoldUpgradeItem> mOnUpgrade { get; set; }
        private Func<GoldUpgradeItem, bool> mCondition { get; set; }

        public void Upgrade()
        {
            mOnUpgrade?.Invoke(this);
            UpgradeFinished = true;
            GoldUpgradeSystem.OnGoldUpgradeSystemChanged.Trigger();
        } 

        public bool ConditionCheck()
        {
            if (mCondition != null)
            {
                return !UpgradeFinished && mCondition.Invoke(this);
            }

            return !UpgradeFinished;
        }

        #region 初始化相关API
        // 采用链式API设计 用作初始化数据
        public GoldUpgradeItem WithKey(string key)
        {
            Key = key;
            return this;
        }

        public GoldUpgradeItem WithDescription(string description)
        {
            Description = description;
            return this;
        }

        public GoldUpgradeItem WithCost(int cost)
        {
            Cost = cost;
            return this;
        }

        public GoldUpgradeItem OnUpgrade(Action<GoldUpgradeItem> onUpgrade)
        {
            mOnUpgrade = onUpgrade;
            return this;
        }
        
        public GoldUpgradeItem WithCondition(Func<GoldUpgradeItem, bool> condition)
        {
            mCondition = condition;
            return this;
        }
        #endregion
    }
}
