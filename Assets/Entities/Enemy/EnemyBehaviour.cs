using System.Collections;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Entities.Enemy
{
    public class EnemyBehaviour : MonoBehaviour
    {
        public float MaxHealth = 200f;
        public GameObject Projectile;
        public float ProjectileSpeed;
        public float ShotsPerSecond = 0.5f;
        public AudioClip ShootSound;
        public AudioClip DieSound;
        public AudioClip FlyIn;


        private float _currentHealth;
        private ScoreKeeper _scoreKeeper;
        private int _scoreValue = 1;

        void Start()
        {
            AudioSource.PlayClipAtPoint(FlyIn, transform.position);
            _currentHealth = MaxHealth;
            _scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
        }

        void Update()
        {
            float probability = Time.deltaTime * ShotsPerSecond;
            if (Random.value < probability && Projectile != null)
            {
                AudioSource.PlayClipAtPoint(ShootSound, transform.position);
                GameObject projectile = Instantiate(Projectile, transform.position, Quaternion.identity);
                projectile.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -ProjectileSpeed, 0);
            }

        }

        void OnTriggerEnter2D(Collider2D projectile)
        {
            Debug.Log("Enemy Hit");

            Projectile thisProjectile = projectile.gameObject.GetComponent<Projectile>();

            Debug.Log("hit by Projectile");
            if (thisProjectile)
            {
                _currentHealth -= thisProjectile.Damage;
                if (_currentHealth <= MaxHealth / 2)
                {
                    gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                }
                if (_currentHealth <= 0)
                {
                    Die();
                    
                }
            }
        }

        private void Die()
        {
            AudioSource.PlayClipAtPoint(DieSound, transform.position);
            _scoreKeeper.UpdateScore(_scoreValue);
            Destroy(gameObject);
        }
    }
}
