using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public float MaxHealth = 200f;
        public bool AutoPlay;
        public float Speed = 15.0f;
        public GameObject Projectile;
        public float ProjectileSpeed;
        public float FiringRate = 0.2f;
        public AudioClip ShootSound;

        private float _currentHealth;
        private const int BlockNumWidth = 16;
        private float _xMin;
        private float _xMax;
        private float _padding = 1f;

        // Use this for initialization
        void Start()
        {
            _currentHealth = MaxHealth;
            float distance = transform.position.z - Camera.main.transform.position.z;
            Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
            Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
            _xMin = leftMost.x + _padding;
            _xMax = rightMost.x - _padding;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position += Vector3.left * Speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.position += Vector3.right * Speed * Time.deltaTime;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                InvokeRepeating("Shoot", 0.000001f, FiringRate);
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                CancelInvoke("Shoot");
            }

            float newX = Mathf.Clamp(transform.position.x, _xMin, _xMax);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        }

        void OnTriggerEnter2D(Collider2D projectile)
        {
            Projectile thisProjectile = projectile.gameObject.GetComponent<Projectile>();

            Debug.Log("Player Hit!");
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

        private void Shoot()
        {
            AudioSource.PlayClipAtPoint(ShootSound, transform.position);
            GameObject projectile = Instantiate(Projectile, transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector3(0, ProjectileSpeed, 0);
        }

        private void Die()
        {
            SceneManager.LoadScene("Lose");
            Destroy(gameObject);
        }
    }
}
