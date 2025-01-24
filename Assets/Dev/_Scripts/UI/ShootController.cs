using UnityEngine;

namespace PocketZone
{
    public class ShootController : MonoBehaviour
    {
        [SerializeField] private GameObject _bulletPrefab;

        [SerializeField] private Transform _firePoint;

        public void Shoot()
        {
            Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
        }
    } 
}

