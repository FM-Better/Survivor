using QFramework;
using UnityEngine;
using UnityEngine.Rendering;

namespace Survivor
{
    public class Global : Architecture<Global>
    {
        #region Model
        public static BindableProperty<int> Hp = new BindableProperty<int>(5); // 血量
        public static BindableProperty<int> MaxHp = new BindableProperty<int>(5); // 最大血量
        public static BindableProperty<int> Exp = new BindableProperty<int>(0); // 经验值
        public static BindableProperty<int> Level = new BindableProperty<int>(1); // 等级
        public static BindableProperty<int> Gold = new BindableProperty<int>(0); // 金币数
        public static BindableProperty<float> Timer = new BindableProperty<float>(0f); // 计时器

        #region 能力相关
        public static BindableProperty<bool> SimpleSwordUnlocked = new BindableProperty<bool>(false); // 简单剑是否解锁
        public static BindableProperty<int> SimpleSwordCount = new BindableProperty<int>(AbilityConfig.InitSimpleSwordCount); // 简单剑的数量
        public static BindableProperty<float> SimpleSwordDamage = new BindableProperty<float>(AbilityConfig.InitSimpleSwordDamage); // 简单剑的伤害值
        public static BindableProperty<float> SimpleSwordCD = new BindableProperty<float>(AbilityConfig.InitSimpleSwordCD); // 简单剑的间隔时间
        public static BindableProperty<float> SimpleSwordRange = new BindableProperty<float>(AbilityConfig.InitSimpleSwordRange); // 简单剑的攻击范围
        
        public static BindableProperty<bool> SimpleKnifeUnlocked = new BindableProperty<bool>(false); // 飞刀是否解锁
        public static BindableProperty<int> SimpleKnifeCount = new BindableProperty<int>(AbilityConfig.InitSimpleKnifeCount); // 飞刀的数量
        public static BindableProperty<int> SimpleKnifeAttackCount = new BindableProperty<int>(AbilityConfig.InitSimpleKnifeAttackCount); // 飞刀可穿透的敌人数量
        public static BindableProperty<float> SimpleKnifeDamage = new BindableProperty<float>(AbilityConfig.InitSimpleKnifeDamage); // 飞刀的伤害值
        public static BindableProperty<float> SimpleKnifeCD = new BindableProperty<float>(AbilityConfig.InitSimpleKnifeCD); // 飞刀的间隔时间
        
        public static BindableProperty<bool> RotateSwordUnlocked = new BindableProperty<bool>(false); // 守卫剑是否解锁
        public static BindableProperty<int> RotateSwordCount = new BindableProperty<int>(AbilityConfig.InitRotateSwordCount); // 守卫剑的数量
        public static BindableProperty<float> RotateSwordDamage = new BindableProperty<float>(AbilityConfig.InitRotateSwordDamage); // 守卫剑的伤害值
        public static BindableProperty<float> RotateSwordSpeed = new BindableProperty<float>(AbilityConfig.InitRotateSwordSpeed); // 守卫剑的速度
        public static BindableProperty<float> RotateSwordRange = new BindableProperty<float>(AbilityConfig.InitRotateSwordRange); // 守卫剑的范围
        
        public static BindableProperty<bool> BasketBallUnlocked = new BindableProperty<bool>(false); // 篮球是否解锁
        public static BindableProperty<int> BasketBallCount = new BindableProperty<int>(AbilityConfig.InitBasketBallCount); // 篮球的数量
        public static BindableProperty<float> BasketBallDamage = new BindableProperty<float>(AbilityConfig.InitBasketBallDamage); // 篮球的伤害值
        public static BindableProperty<float> BasktetBallSpeed = new BindableProperty<float>(AbilityConfig.InitBasketBallSpeed); // 篮球的速度

        public static BindableProperty<bool> BombUnlocked = new BindableProperty<bool>(false); // 炸弹是否解锁
        public static BindableProperty<int> BombDropRate = new BindableProperty<int>(AbilityConfig.InitBombDropRate); // 炸弹的掉落概率
        public static BindableProperty<float> BombDamage = new BindableProperty<float>(AbilityConfig.InitBombDamage); // 炸弹的伤害值
        
        public static BindableProperty<int> CriticalRate = new BindableProperty<int>(AbilityConfig.InitCriticalRate); // 暴击率
        public static BindableProperty<float> DamageRate = new BindableProperty<float>(1f); // 额外伤害率
        public static BindableProperty<int> AdditionalFlyCount = new BindableProperty<int>(0); // 额外飞射物
        
        public static BindableProperty<float> SpeedRate = new BindableProperty<float>(1f); // 额外移速
        public static BindableProperty<float> PickUpAreaRange = new BindableProperty<float>(1f); // 拾取范围
        public static BindableProperty<int> AdditionalExpRate = new BindableProperty<int>(0); // 额外的经验掉落概率
        
        public static BindableProperty<bool> SuperSimpleSword = new BindableProperty<bool>(false); // 超级简单剑是否合成
        public static BindableProperty<bool> SuperKnife = new BindableProperty<bool>(false); // 超级飞刀是否合成
        public static BindableProperty<bool> SuperRotateSword = new BindableProperty<bool>(false); // 超级守卫剑是否合成
        public static BindableProperty<bool> SuperBasketBall = new BindableProperty<bool>(false); // 超级篮球是否合成
        public static BindableProperty<bool> SuperBomb = new BindableProperty<bool>(false); // 超级炸弹是否合成
        #endregion
        
        public static BindableProperty<bool> IsEnemySpawnOver = new BindableProperty<bool>(false); // 敌人是否生成完毕
        
        public static BindableProperty<int> ExpBallDropRate = new BindableProperty<int>(60); // 经验球掉落概率
        public static BindableProperty<int> GoldDropRate = new BindableProperty<int>(20); // 金币掉落概率
        public static BindableProperty<int> HpItemDropRate = new BindableProperty<int>(2); // 回血道具掉落概率
        public static BindableProperty<int> GetAllExpDropRate = new BindableProperty<int>(8); // 获取当前所有经验的道具的掉落概率
        #endregion
        
        protected override void Init()
        {
            GraphicsSettings.useScriptableRenderPipelineBatching = true; // 打开SRP Batcher
            this.RegisterSystem(new SaveSystem());
            this.RegisterSystem(new GoldUpgradeSystem());
            this.RegisterSystem(new ExpUpgradeSystem());
            this.RegisterSystem(new AchievementSystem());
        }
        
        [RuntimeInitializeOnLoadMethod]
        public static void InitGameData()
        {
            ResKit.Init();
            AudioKit.PlaySoundMode = AudioKit.PlaySoundModes.IgnoreSameSoundInGlobalFrames; // 声音优化
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

            SimpleKnifeUnlocked.Value = false;
            SimpleSwordCount.Value = AbilityConfig.InitSimpleSwordCount;
            SimpleSwordDamage.Value = AbilityConfig.InitSimpleSwordDamage;
            SimpleSwordCD.Value = AbilityConfig.InitSimpleSwordCD;
            SimpleSwordRange.Value = AbilityConfig.InitSimpleSwordRange;

            SimpleKnifeUnlocked.Value = false;
            SimpleKnifeCount.Value = AbilityConfig.InitSimpleKnifeCount;
            SimpleKnifeAttackCount.Value = AbilityConfig.InitSimpleKnifeAttackCount;
            SimpleKnifeDamage.Value = AbilityConfig.InitSimpleKnifeDamage;
            SimpleKnifeCD.Value = AbilityConfig.InitSimpleKnifeCD;

            RotateSwordUnlocked.Value = false;
            RotateSwordCount.Value = AbilityConfig.InitRotateSwordCount;
            RotateSwordDamage.Value = AbilityConfig.InitRotateSwordDamage;
            RotateSwordSpeed.Value = AbilityConfig.InitRotateSwordSpeed;
            RotateSwordRange.Value = AbilityConfig.InitRotateSwordRange;

            BasketBallUnlocked.Value = false;
            BasketBallCount.Value = AbilityConfig.InitBasketBallCount;
            BasketBallDamage.Value = AbilityConfig.InitBasketBallDamage;
            BasktetBallSpeed.Value = AbilityConfig.InitBasketBallSpeed;

            BombUnlocked.Value = false;
            BombDropRate.Value = AbilityConfig.InitBombDropRate;
            BombDamage.Value = AbilityConfig.InitBombDamage;

            CriticalRate.Value = AbilityConfig.InitCriticalRate;
            DamageRate.Value = 1;
            AdditionalFlyCount.Value = 0;
            SpeedRate.Value = 1f;
            PickUpAreaRange.Value = 1f;
            AdditionalExpRate.Value = 0;

            SuperSimpleSword.Value = false;
            SuperKnife.Value = false;
            SuperRotateSword.Value = false;
            SuperBasketBall.Value = false;
            SuperBomb.Value = false;
            
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
