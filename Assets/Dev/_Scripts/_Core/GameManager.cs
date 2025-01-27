using UnityEngine;
using UnityEngine.UI;

namespace PocketZone
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("Inventory Settings")]
        [SerializeField] private GameObject _inventoryPanel;
        [SerializeField] private InventorySlot[] _slots;
        [SerializeField] private Button _removeOneButton;
        [SerializeField] private Button _removeAllButton;
        [SerializeField] private Button _toggleInventoryButton;

        [Header("Player Settings")]
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private JoystickController _joystick;

        private InventoryController _inventoryController;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            _inventoryController = new InventoryController(_inventoryPanel, _slots, _removeOneButton, _removeAllButton);

            _removeAllButton.onClick.AddListener(_inventoryController.OnButtonClick);

            _removeOneButton.onClick.AddListener(_inventoryController.OnButtonClick);

            _toggleInventoryButton.onClick.AddListener(ToggleInventory);

            IMoveHandler moveHandler = new MoveHandler();

            _playerController.SetMoveHandler(moveHandler);

            _playerController.SetJoystick(_joystick);
        }

        private void ToggleInventory()
        {
            _inventoryController.Interact();
        }

        public IInventory GetInventory()
        {
            return _inventoryController;
        }
    }
}