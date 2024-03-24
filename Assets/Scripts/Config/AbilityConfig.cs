namespace Survivor
{
    public class AbilityConfig
    {
        #region 初始化数据相关
        public const int InitSimpleSwordCount = 3;
        public const float InitSimpleSwordDamage = 3f;
        public const float InitSimpleSwordCD = 1.5f;
        public const float InitSimpleSwordRange = 3f;
        
        public const int InitSimpleKnifeCount = 3;
        public const int InitSimpleKnifeAttackCount = 1;
        public const float InitSimpleKnifeDamage = 5f;
        public const float InitSimpleKnifeCD = 1f;
        
        public const int InitRotateSwordCount = 1;
        public const float InitRotateSwordDamage = 5f;
        public const float InitRotateSwordSpeed = 2f;
        public const float InitRotateSwordRange = 2f;

        public const int InitBasketBallCount = 1;
        public const float InitBasketBallDamage = 5f;
        public const float InitBasketBallSpeed = 10f;

        public const int InitBombDropRate = 5;
        public const float InitBombDamage = 10f;
        
        public const int InitCriticalRate = 5;
        #endregion

        #region Key
        public const string SpeedKey = "Speed";
        public const string SimpleSwordKey = "SimpleSword";
        public const string SimpleKnifeKey = "SimpleKnife";
        public const string RotateSwordKey = "RotateSword";
        public const string BasketballKey = "BasketBall";
        public const string BombKey = "Bomb";
        public const string CriticalKey = "Critical";
        public const string DamageKey = "Damage";
        public const string FlyKey = "Fly";
        public const string PickUpAreaKey = "PickUpArea";
        public const string ExpKey = "Exp";
        #endregion
        
        #region IconName
        public const string SuperBasketballIconName = "paired_ball_icon";
        public const string SuperBombIconName = "paired_bomb_icon";
        public const string SuperRotateSwordIconName = "paired_rotate_sword_icon";
        public const string SuperSimpleKnifeIconName = "paired_simple_knife_icon";
        public const string SuperSimpleSwordIconName = "paired_simple_sword_icon";
        
        public const string BasketballIconName = "ball_icon";
        public const string BombIconName = "bomb_icon";
        public const string PickUpAreaIconName = "collectable_icon";
        public const string CriticalIconName = "critical_icon";
        public const string DamageIconName = "damage_icon";
        public const string ExpIconName = "exp_icon";
        public const string FlyIconName = "fly_icon";
        public const string SpeedIconName = "movement_icon";
        public const string SimpleSwordIconName = "simple_sword_icon";
        public const string SimpleKnifeIconName = "simple_knife_icon";
        public const string RotateSwordIconName = "rotate_sword_icon";
        #endregion
    }
}