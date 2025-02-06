namespace PocketZone
{
    public class HealthComponent
    {
        public delegate void OnDeathDelegate();
        public event OnDeathDelegate OnDeath;

        private HealthController _healthController;

        public HealthComponent(HealthController healthController)
        {
            _healthController = healthController;
            _healthController.OnDeath += HandleDeath;
        }

        private void HandleDeath()
        {
            OnDeath?.Invoke();
        }

        public void Cleanup()
        {
            _healthController.OnDeath -= HandleDeath;
        }
    }
}