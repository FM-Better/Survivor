using UnityEngine;

namespace Survivor
{
    public class DamageSystem
    {
        public static void CalculateDamage(float baseDamage, IEnemy enemy, float criticalDamageTime = 5f)
        {
            baseDamage *= Global.DamageRate.Value;
            if (Random.Range(0, 100) < Global.CriticalRate.Value)
            {
                enemy.Hurt(baseDamage * Random.Range(2, criticalDamageTime), true);
            }
            else
            {
                enemy.Hurt(baseDamage + Random.Range(-1, 2));
            }
        }
    }
}