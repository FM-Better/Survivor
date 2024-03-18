using System;
using QFramework;

namespace Survivor
{
    public class ExpUpgradeItem
    {
        public bool IsWeapon { get; private set; }
        public string Key { get; private set; }
        public string Description => mDescriptionFunctory(CurrentLevel.Value + 1);
        public int MaxLevel { get; private set; }
        public BindableProperty<int> CurrentLevel = new BindableProperty<int>(0);
        public bool UpgradeFinished { get; set; } = false;
        public BindableProperty<bool> Visible = new BindableProperty<bool>(false);
        private Func<int, string> mDescriptionFunctory;
        
        private Action<ExpUpgradeItem, int> mOnUpgrade { get; set; }

        public ExpUpgradeItem(bool isWeapon = false) => IsWeapon = isWeapon;
        
        public void Upgrade()
        {
            CurrentLevel.Value++;
            mOnUpgrade?.Invoke(this, CurrentLevel.Value);
            if (CurrentLevel.Value > 10)
            {
                UpgradeFinished = true;    
            }
        }

        #region 初始化相关API
        // 采用链式API设计 用作初始化数据
        public ExpUpgradeItem WithKey(string key)
        {
            Key = key;
            return this;
        }

        public ExpUpgradeItem WithDescription(Func<int, string> descriptionFunctory)
        {
            mDescriptionFunctory = descriptionFunctory;
            return this;
        }

        public ExpUpgradeItem OnUpgrade(Action<ExpUpgradeItem, int> onUpgrade)
        {
            mOnUpgrade = onUpgrade;
            return this;
        }
        
        public ExpUpgradeItem WithMaxLevel(int maxLevel)
        {
            MaxLevel = maxLevel;
            return this;
        }
        #endregion
    }
}