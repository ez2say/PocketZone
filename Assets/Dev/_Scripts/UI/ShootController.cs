using UnityEngine;

namespace PocketZone
{
    public class ShootController : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject _bulletPrefab; // Создать класс AssetsProvider и через него получать префабы
        [SerializeField] private Transform _firePoint;
        [SerializeField] private float _bulletSpeed = 10f;

        private bool _isFacingRight = true;
        public void Interact()
        {
            Shoot();
        }

        private void Shoot()
        {
            Vector2 shootDirection = _isFacingRight ? Vector2.right : Vector2.left;

            GameObject bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);

            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();

            bulletRigidbody.velocity = shootDirection.normalized * _bulletSpeed;
        }

        public void UpdateShootDirection(bool isFacingRight)
        {
            _isFacingRight = isFacingRight;
        }
    }
}