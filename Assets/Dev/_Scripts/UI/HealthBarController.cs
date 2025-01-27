using UnityEngine;

namespace PocketZone
{
    public class HealthBarController : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _offset;

        

        public void SetTarget(Transform target, Vector3 offset)
        {
            _target = target;
            _offset = offset;
        }
    }
}