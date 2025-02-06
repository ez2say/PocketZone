using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PocketZone
{
    public class ShootController : MonoBehaviour, IInteractable
    {   
        public int GetCurrentAmmo() => _currentAmmo;
        public int GetMaxAmmo() => _maxAmmo;

        [Header("Shooting Settings")]
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private float _bulletSpeed = 10f;
        [SerializeField] private float _fireRate = 0.5f;

        [Header("Ammo Settings")]
        [SerializeField] private int _maxAmmo = 30;
        [SerializeField] private int _currentAmmo;
        [SerializeField] private bool _infiniteAmmo = false;
        [SerializeField] private TextMeshProUGUI _ammoText;

        [Header("Object Pool")]
        [SerializeField] private ObjectPool _objectPool;
        [SerializeField] private int _bulletPoolSize = 10;

        private bool _isFacingRight = true;
        private float _nextFireTime;

        private void Start()
        {
            _currentAmmo = _maxAmmo;
            UpdateAmmoText();

            InitializeBulletPool();
        }

        private void InitializeBulletPool()
        {
            _objectPool.AddPool(_bulletPrefab, _bulletPoolSize);
        }

        public void Interact()
        {
            TryShoot();
        }

        private void TryShoot()
        {
            if (CanShoot())
            {
                Shoot();
                ConsumeAmmo();
                _nextFireTime = Time.time + _fireRate;
                UpdateAmmoText();
            }
        }

        private bool CanShoot()
        {
            return Time.time >= _nextFireTime && 
                   (_currentAmmo > 0 || _infiniteAmmo);
        }

        private void Shoot()
        {
            GameObject bullet = _objectPool.GetObject(_bulletPrefab);
            if (bullet != null)
            {
                bullet.transform.position = _firePoint.position;
                bullet.transform.rotation = _firePoint.rotation;

                Vector2 shootDirection = _isFacingRight ? Vector2.right : Vector2.left;

                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = shootDirection.normalized * _bulletSpeed;

                var bulletScript = bullet.GetComponent<Bullet>();
                bulletScript.Initialize(_objectPool);
            }
        }

        private void ConsumeAmmo()
        {
            if (!_infiniteAmmo)
            {
                _currentAmmo--;
                _currentAmmo = Mathf.Max(_currentAmmo, 0);
            }
        }

        public void AddAmmo(int amount)
        {
            _currentAmmo = Mathf.Clamp(_currentAmmo + amount, 0, _maxAmmo);
            UpdateAmmoText();
        }

        public void UpdateShootDirection(bool isFacingRight)
        {
            _isFacingRight = isFacingRight;
        }

        private void UpdateAmmoText()
        {
            if (_ammoText != null)
            {
                if (_infiniteAmmo)
                {
                    _ammoText.text = "∞";
                }
                else
                {
                    _ammoText.text = $"{_currentAmmo}/{_maxAmmo}";
                }
            }
        }
    }
}