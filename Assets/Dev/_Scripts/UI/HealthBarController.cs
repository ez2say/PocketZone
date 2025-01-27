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

        void LateUpdate()
        {
            if (_target != null)
            {
                transform.position = _target.position + _offset;
                transform.rotation = Camera.main.transform.rotation;
            }
        }
    }
}