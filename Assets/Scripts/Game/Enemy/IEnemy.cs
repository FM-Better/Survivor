namespace Survivor
{
    public interface IEnemy
    {
        void Hurt(float damage);
        void PopulateHp(float nowWaveHpScale);
        void PopulateSpeed(float nowWaveSpeedScale);
    }
}