using UnityEngine;

public interface IDamageable
{
    public void TakeDamage(Elemental elementalDamage);
    public void InitHp();
    public void KnockBackGauge(GameObject charactor, Elemental damage);
    public void RestoreKnockbackGauge();
}
