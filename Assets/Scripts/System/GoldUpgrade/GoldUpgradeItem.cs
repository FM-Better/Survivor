using System;

namespace Survivor
{
   public class GoldUpgradeItem
    {
        public string Key { get; private set; }
        public string Description { get; private set; }
        public int Cost { get; private set; }
        private Action mOnUpgrade { get; set; }

        public void Upgrade() => mOnUpgrade?.Invoke();
        
        // 采用链式API设计 用作配置数据
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

        public GoldUpgradeItem OnUpgrade(Action onUpgrade)
        {
            mOnUpgrade = onUpgrade;
            return this;
        }
    }
}
