namespace WeaponRunner
{
    public class Obstacle : DamageableBase
    {
        public override void Die()
        {
            base.Die();
            gameObject.SetActive(false);
        }
    }
}