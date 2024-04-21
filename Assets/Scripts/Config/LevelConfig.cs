using System;
using System.Collections.Generic;
using UnityEngine;

namespace Survivor
{
    [CreateAssetMenu]
    public class LevelConfig : ScriptableObject
    {
        public List<EnemyWaveGroup> EnemyWaveGroups = new List<EnemyWaveGroup>();
    }

    [Serializable]
    public class EnemyWaveGroup
    {
        public string Name;
        public bool Active = true;
        [TextArea] public string Description = String.Empty;

        public List<EnemyWave> Waves = new List<EnemyWave>();
    }

    [Serializable]
    public class EnemyWave
    {
        public string Name; // 名字
        public bool Active = true; // 是否激活
        public float DurationTime; // 持续时间
        public float SpawnCD; // 刷怪的CD
        public bool isWaited = false; // 是否等待完毕
        public float WaitTime; // 等待下一波的时间
        public GameObject EnemyPrefab;
        public float SpeedScale = 1f; // 血量系数
        public float HpScale = 1f; // 速度系数
    }
}
