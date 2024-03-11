using System;
using QFramework;

namespace Survivor
{
    public class ExpUpgradeItem
    {
        public string Key { get; private set; }
        public string Description { get; private set; }
        public bool UpgradeFinished { get; set; } = false;
        public EasyEvent OnChanged = new EasyEvent();
        
        private Action<ExpUpgradeItem> mOnUpgrade { get; set; }
        private Func<ExpUpgradeItem, bool> mCondition { get; set; }

        public void Upgrade()
        {
            mOnUpgrade?.Invoke(this);
            UpgradeFinished = true;
            OnChanged.Trigger();
            ExpUpgradeSystem.OnExpUpgradeSystemChanged.Trigger();
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
        public ExpUpgradeItem WithKey(string key)
        {
            Key = key;
            return this;
        }

        public ExpUpgradeItem WithDescription(string description)
        {
            Description = description;
            return this;
        }

        public ExpUpgradeItem OnUpgrade(Action<ExpUpgradeItem> onUpgrade)
        {
            mOnUpgrade = onUpgrade;
            return this;
        }
        
        public ExpUpgradeItem WithCondition(Func<ExpUpgradeItem, bool> condition)
        {
            mCondition = condition;
            return this;
        }
        #endregion
    }
}