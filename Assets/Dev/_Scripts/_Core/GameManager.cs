using UnityEngine;

namespace PocketZone
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private UIManager _uIManager;
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] private EnemyManager _enemyManager;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            _uIManager.Initialize();

            _playerManager.Initialize();

            _enemyManager.Initialize();
        }

        public IInventory GetInventory()
        {
            return _uIManager.GetInventory();
        }
    }
}