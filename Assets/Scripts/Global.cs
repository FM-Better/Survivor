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
    }
}