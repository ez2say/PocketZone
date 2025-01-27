using UnityEngine;

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

        private bool _isFacingRight = true;
        private float _nextFireTime;

        private void Start()
        {
            _currentAmmo = _maxAmmo;
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
            }
        }

        private bool CanShoot()
        {
            return Time.time >= _nextFireTime && 
                  (_currentAmmo > 0 || _infiniteAmmo);
        }

        private void Shoot()
        {
            Vector2 shootDirection = _isFacingRight ? Vector2.right : Vector2.left;
            GameObject bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = shootDirection.normalized * _bulletSpeed;
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
        }

        public void UpdateShootDirection(bool isFacingRight)
        {
            _isFacingRight = isFacingRight;
        }

        
    }
}