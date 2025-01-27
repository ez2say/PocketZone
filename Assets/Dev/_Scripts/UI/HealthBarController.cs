using UnityEngine;
using UnityEngine.UI;

namespace PocketZone
{
    public class HealthBarController : MonoBehaviour
    {
        private Slider _healthSlider;

        private void Start()
        {
            _healthSlider = GetComponent<Slider>();
        }

        public void DecreaseHealthBar(float value)
        {
            _healthSlider.value = value;
        }


    }
}