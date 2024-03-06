using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace Survivor
{
    public class Global
    {
        /// <summary>
        /// 经验值
        /// </summary>
        public static BindableProperty<int> Exp = new BindableProperty<int>(0);
        
        /// <summary>
        /// 等级
        /// </summary>
        public static BindableProperty<int> Level = new BindableProperty<int>(1);

        /// <summary>
        /// 计时器
        /// </summary>
        public static BindableProperty<float> Timer = new BindableProperty<float>(0f);
        
        /// <summary>
        /// 简单能力的伤害值
        /// </summary>
        public static BindableProperty<float> SimpleAbilityDamage = new BindableProperty<float>(1f);
    }
}
