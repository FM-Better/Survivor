using System;
using QFramework;

namespace Survivor
{
   public class GoldUpgradeItem
    {
        public string Key { get; private set; }
        public string Description { get; private set; }
        public int Cost { get; private set; }
        public bool UpgradeFinished { get; set; } = false;
        public EasyEvent OnChanged = new EasyEvent();

        private GoldUpgradeItem mNext = null;
        
        private Action<GoldUpgradeItem> mOnUpgrade { get; set; }
        private Func<GoldUpgradeItem, bool> mCondition { get; set; }

        public void Upgrade()
        {
            mOnUpgrade?.Invoke(this);
            UpgradeFinished = true;
            TriggerOnChanged();
            GoldUpgradeSystem.OnGoldUpgradeSystemChanged.Trigger();
        }

        public void TriggerOnChanged()
        {
            OnChanged.Trigger();
            mNext?.TriggerOnChanged();
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
        
        public GoldUpgradeItem WithNext(GoldUpgradeItem next)
        {
            mNext = next;
            mNext.WithCondition(_ => UpgradeFinished);
            return mNext;
        }
        
        private void WithCondition(Func<GoldUpgradeItem, bool> condition)
        {
            mCondition = condition;
        }
        #endregion
    }
}
