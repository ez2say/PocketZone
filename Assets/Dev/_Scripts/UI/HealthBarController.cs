using UnityEngine;
using UnityEngine.UI;

namespace PocketZone
{
    public class HealthBarController : MonoBehaviour
    {
        private Slider _healthSlider;

        public void Initialize()
        {
            _healthSlider = GetComponent<Slider>();

            Debug.Log($"Слайдер{_healthSlider}");

            _healthSlider.value = 1;

            Debug.Log($"Значение{_healthSlider.value}");
        }

        public void DecreaseHealthBar(float value)
        {
            _healthSlider.value = value;
        }


    }
}