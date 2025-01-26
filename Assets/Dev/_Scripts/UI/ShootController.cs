using UnityEngine;

namespace PocketZone
{
    public class ShootController : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject _bulletPrefab;

        [SerializeField] private Transform _firePoint;

        
        public void Interact()
        {
            Shoot();
        }
        
        private void Shoot()
        {
            Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
        }
    } 
}

