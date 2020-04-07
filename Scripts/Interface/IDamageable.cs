namespace DSC.Actor
{
    public interface IDamageable<DamageData> where DamageData : struct
    {
        bool TakeDamage(DamageData hData);
    }
}