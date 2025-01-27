using UnityEngine;
using UnityEngine.UI;

namespace PocketZone
{
    public class HealthBarController : MonoBehaviour
    {
        private Slider _healthSlider;

        private void Start()
        {
            _healthSlider = GetComponentInChildren<Slider>(true);

            _healthSlider.value = 1;

            Debug.Log($"{_healthSlider.value}");
        }

        public void DecreaseHealthBar(float value)
        {
            _healthSlider.value = value;
        }


    }
}