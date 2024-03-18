using QFramework;
using UnityEngine;

namespace Survivor
{
    public class Global : Architecture<Global>
    {
        #region Model
        /// <summary>
        /// 血量
        /// </summary>
        public static BindableProperty<int> Hp = new BindableProperty<int>(5);
        /// <summary>
        /// 最大血量
        /// </summary>
        public static BindableProperty<int> MaxHp = new BindableProperty<int>(5);
        /// <summary>
        /// 经验值
        /// </summary>
        public static BindableProperty<int> Exp = new BindableProperty<int>(0);
        /// <summary>
        /// 等级
        /// </summary>
        public static BindableProperty<int> Level = new BindableProperty<int>(1);
        /// <summary>
        /// 金币数
        /// </summary>
        public static BindableProperty<int> Gold = new BindableProperty<int>(0);
        /// <summary>
        /// 计时器
        /// </summary>
        public static BindableProperty<float> Timer = new BindableProperty<float>(0f);
        
        /// <summary>
        /// 简单剑的数量
        /// </summary>
        public static BindableProperty<int> SimpleSwordCount = new BindableProperty<int>(AbilityConfig.InitSimpleSwordCount);
        /// <summary>
        /// 简单剑的伤害值
        /// </summary>
        public static BindableProperty<float> SimpleSwordDamage = new BindableProperty<float>(AbilityConfig.InitSimpleSwordDamage);
        /// <summary>
        /// 简单剑的间隔时间
        /// </summary>
        public static BindableProperty<float> SimpleSwordCD = new BindableProperty<float>(AbilityConfig.InitSimpleSwordCD);
        /// <summary>
        /// 简单剑的攻击范围
        /// </summary>
        public static BindableProperty<float> SimpleSwordRange = new BindableProperty<float>(AbilityConfig.InitSimpleSwordRange);
        
        /// <summary>
        /// 飞刀的数量
        /// </summary>
        public static BindableProperty<int> SimpleKnifeCount = new BindableProperty<int>(AbilityConfig.InitSimpleKnifeCount);
        /// <summary>
        /// 飞刀可穿透的敌人数量
        /// </summary>
        public static BindableProperty<int> SimpleKnifeAttackCount = new BindableProperty<int>(AbilityConfig.InitSimpleKnifeAttackCount);
        /// <summary>
        /// 飞刀的伤害值
        /// </summary>
        public static BindableProperty<float> SimpleKnifeDamage = new BindableProperty<float>(AbilityConfig.InitSimpleKnifeDamage);
        /// <summary>
        /// 飞刀的间隔时间
        /// </summary>
        public static BindableProperty<float> SimpleKnifeCD = new BindableProperty<float>(AbilityConfig.InitSimpleKnifeCD);
        
        /// <summary>
        /// 守卫剑的数量
        /// </summary>
        public static BindableProperty<int> RotateSwordCount = new BindableProperty<int>(AbilityConfig.InitRotateSwordCount);
        /// <summary>
        /// 守卫剑的伤害值
        /// </summary>
        public static BindableProperty<float> RotateSwordDamage = new BindableProperty<float>(AbilityConfig.InitRotateSwordDamage);
        /// <summary>
        /// 守卫剑的速度
        /// </summary>
        public static BindableProperty<float> RotateSwordSpeed = new BindableProperty<float>(AbilityConfig.InitRotateSwordSpeed);
        /// <summary>
        /// 守卫剑的范围
        /// </summary>
        public static BindableProperty<float> RotateSwordRange = new BindableProperty<float>(AbilityConfig.InitRotateSwordRange);
        
        /// <summary>
        /// 敌人是否生成完毕
        /// </summary>
        public static BindableProperty<bool> IsEnemySpawnOver = new BindableProperty<bool>(false);
        
        /// <summary>
        /// 经验球掉落概率
        /// </summary>
        public static BindableProperty<int> ExpBallDropRate = new BindableProperty<int>(60);
        /// <summary>
        /// 金币掉落概率
        /// </summary>
        public static BindableProperty<int> GoldDropRate = new BindableProperty<int>(20);
        /// <summary>
        /// 回血道具掉落概率
        /// </summary>
        public static BindableProperty<int> HpItemDropRate = new BindableProperty<int>(2);
        /// <summary>
        /// 炸弹掉落概率
        /// </summary>
        public static BindableProperty<int> BombDropRate = new BindableProperty<int>(1);
        /// <summary>
        /// 获取当前所有经验的道具的掉落概率
        /// </summary>
        public static BindableProperty<int> GetAllExpDropRate = new BindableProperty<int>(8);
        #endregion
        
        protected override void Init()
        {
            this.RegisterSystem(new SaveSystem());
            this.RegisterSystem(new GoldUpgradeSystem());
            this.RegisterSystem(new ExpUpgradeSystem());
        }
        
        [RuntimeInitializeOnLoadMethod]
        public static void InitGameData()
        {
            ResKit.Init();
            UIKit.Root.SetResolution(1920, 1080, 0f);
            
            ExpBallDropRate.Value = PlayerPrefs.GetInt("ExpBallDropRate", 60);
            GoldDropRate.Value = PlayerPrefs.GetInt("GoldDropRate", 20);
            Gold.Value = PlayerPrefs.GetInt("Gold", 0);
            MaxHp.Value = PlayerPrefs.GetInt("MaxHp", 5);

            ResetData();
        }

        public static void ResetData()
        {
            Hp.Value = MaxHp.Value;
            Exp.Value = 0;
            Level.Value = 1;
            Timer.Value = 0f;
            
            SimpleSwordCount.Value = AbilityConfig.InitSimpleSwordCount;
            SimpleSwordDamage.Value = AbilityConfig.InitSimpleSwordDamage;
            SimpleSwordCD.Value = AbilityConfig.InitSimpleSwordCD;
            SimpleSwordRange.Value = AbilityConfig.InitSimpleSwordRange;

            SimpleKnifeCount.Value = AbilityConfig.InitSimpleKnifeCount;
            SimpleKnifeAttackCount.Value = AbilityConfig.InitSimpleKnifeAttackCount;
            SimpleKnifeDamage.Value = AbilityConfig.InitSimpleKnifeDamage;
            SimpleKnifeCD.Value = AbilityConfig.InitSimpleKnifeCD;

            RotateSwordCount.Value = AbilityConfig.InitRotateSwordCount;
            RotateSwordDamage.Value = AbilityConfig.InitRotateSwordDamage;
            RotateSwordSpeed.Value = AbilityConfig.InitRotateSwordSpeed;
            RotateSwordRange.Value = AbilityConfig.InitRotateSwordRange;
            
            IsEnemySpawnOver.Value = false;
            EnemySpawner.enemyCount.Value = 0;
            Interface.GetSystem<ExpUpgradeSystem>().ResetData();
            Time.timeScale = 1f;
        }
        
        public static int CurrentLevelExp()
        {
            return Level.Value * 5;
        }
    }
}
