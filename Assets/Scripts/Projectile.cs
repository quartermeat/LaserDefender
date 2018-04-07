using UnityEngine;

namespace Assets.Scripts
{
    public class Projectile : MonoBehaviour
    {
        public float Damage = 100f;

        public float GetDamage()
        {
            return Damage;
        }

        public void Hit()
        {
            Destroy(gameObject);
        }

    }
}
