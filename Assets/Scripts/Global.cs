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
        /// 简单能力的伤害值
        /// </summary>
        public static BindableProperty<float> SimpleAbilityDamage = new BindableProperty<float>(1f);
        
        /// <summary>
        /// 简单能力的间隔时间
        /// </summary>
        public static BindableProperty<float> SimpleAbilityCD = new BindableProperty<float>(1.5f);
        
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
        /// 获取当前所有经验
        /// </summary>
        public static BindableProperty<int> GetAllExpDropRate = new BindableProperty<int>(8);
        #endregion
        
        protected override void Init()
        {
            this.RegisterSystem(new GoldUpgradeSystem());
        }
        
        public static void InitGameData()
        {
            ResKit.Init();
            UIKit.Root.SetResolution(1920, 1080, 0f);
            
            ExpBallDropRate.Value = PlayerPrefs.GetInt("ExpBallDropRate", 60);
            GoldDropRate.Value = PlayerPrefs.GetInt("GoldDropRate", 20);
            Gold.Value = PlayerPrefs.GetInt("Gold", 0);
            MaxHp.Value = PlayerPrefs.GetInt("MaxHp", 5);
            
            Hp.Value = MaxHp.Value;
            Exp.Value = 0;
            Level.Value = 1;
            Timer.Value = 0f;
            SimpleAbilityDamage.Value = 1f;
            SimpleAbilityCD.Value = 1.5f;
            IsEnemySpawnOver.Value = false;
            
            EnemySpawner.enemyCount.Value = 0;
            Time.timeScale = 1f;
        }

        public static int CurrentLevelExp()
        {
            return Level.Value * 5;
        }

        public static void SpawnDrop(Vector3 spawnPosition)
        {
            var randomNum = Random.Range(1, 101);
            if (randomNum <= ExpBallDropRate.Value) // 掉落经验球
            {
                DropManager.Default.ExpBall.Instantiate()
                    .Position(spawnPosition)
                    .Show();
            }
            if (randomNum <= GoldDropRate.Value) // 掉落金币
            {
                DropManager.Default.Gold.Instantiate()
                    .Position(spawnPosition + Vector3.right)
                    .Show();
            }
            if (randomNum <= HpItemDropRate.Value) // 掉落回血道具
            {
                DropManager.Default.HpItem.Instantiate()
                    .Position(spawnPosition + Vector3.up)
                    .Show();
            }
            if (randomNum <= BombDropRate.Value) // 掉落炸弹
            {
                DropManager.Default.Bomb.Instantiate()
                    .Position(spawnPosition + Vector3.left)
                    .Show();
            }
            if (randomNum <= GetAllExpDropRate.Value) // 掉落获得当前所有经验
            {
                DropManager.Default.GetAllExp.Instantiate()
                    .Position(spawnPosition + Vector3.down)
                    .Show();
            }
        }
    }
}
