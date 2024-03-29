namespace WeaponRunner
{
    public interface IDamageable
    {
        int Health { get; }
        void TakeDamage(int damage);
        void Die();
    }
}