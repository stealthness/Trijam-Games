namespace _Scripts.Core
{
    public interface IDamageable
    {
        /// <summary>
        /// The method to apply damage to the object.
        /// </summary>
        /// <param name="damage"></param>
        void TakeDamage(int damage);
        
        /// <summary>
        /// Checks if the object can take damage.
        /// </summary>
        /// <returns>True if the object can be damage, false otherwise</returns>
        bool IsDamageable();
    }
}