namespace Survivor
{
    public interface IEnemy
    {
        void Hurt(float damage, bool isCritical = false);
        void PopulateHp(float nowWaveHpScale);
        void PopulateSpeed(float nowWaveSpeedScale);
    }
}