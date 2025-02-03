using UnityEngine;
using UnityEngine.UI;


namespace PocketZone
{
    public class EnemyManager : MonoBehaviour 
    {   
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private PlayerController _playerController;

        public void Initialize()
        {
            _enemySpawner.Initialize(_playerController.transform);
        } 
    }
    
}