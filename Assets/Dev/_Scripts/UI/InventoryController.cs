using UnityEngine;
using UnityEngine.UI;

namespace PocketZone
{
    public class InventoryController : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject _inventoryPanel;
        [SerializeField] private InventorySlot[] slots;

        [SerializeField] private Button _removeOneButton;
        [SerializeField] private Button _removeAllButton;

        private InventorySlot _selectedSlot;

        private void Start()
        {
            _inventoryPanel.SetActive(true);

            HideButtons();

            _removeOneButton.onClick.AddListener(RemoveOneItem);
            _removeAllButton.onClick.AddListener(RemoveAllItems);

            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].Initialize(OnSlotClick, i); 
            }

            _inventoryPanel.SetActive(false);
        }

        public void Interact()
        {
            ToggleInventory();
        }

        public void ToggleInventory()
        {
            _inventoryPanel.SetActive(!_inventoryPanel.activeSelf);

            if (!_inventoryPanel.activeSelf)
            {
                HideButtons();
                _selectedSlot = null;
            }
        }

        public bool AddItem(Item item)
        {
            foreach (var slot in slots)
            {
                if (slot.Item != null && slot.Item.ID == item.ID && slot.Count < slot.Item.MaxStack)
                {
                    slot.Count++;
                    slot.UpdateUI();
                    return true;
                }
            }

            foreach (var slot in slots)
            {
                if (slot.Item == null)
                {
                    slot.Item = item;
                    slot.Count = 1;
                    slot.UpdateUI();
                    return true;
                }
            }

            return false;
        }

        private void OnSlotClick(InventorySlot slot)
        {
            if (_selectedSlot == slot)
            {
                HideButtons();

                _selectedSlot = null;
            }
            else
            {
                _selectedSlot = slot;
                
                ShowButtons(slot);

                Debug.Log("Выбран слот " + (slot.Index + 1));
            }
        }

        private void RemoveOneItem()
        {
            if (_selectedSlot != null && _selectedSlot.Item != null && _selectedSlot.Count > 0)
            {
                _selectedSlot.Count--;

                if (_selectedSlot.Count == 0)
                {
                    _selectedSlot.Item = null;
                }

                _selectedSlot.UpdateUI();
                HideButtons();
                _selectedSlot = null;
            }
        }

        // Удалить все предметы
        private void RemoveAllItems()
        {
            if (_selectedSlot != null && _selectedSlot.Item != null)
            {
                _selectedSlot.Item = null;

                _selectedSlot.Count = 0;

                _selectedSlot.UpdateUI();

                HideButtons();

                _selectedSlot = null;
            }
        }

        private void ShowButtons(InventorySlot slot)
        {
            _removeOneButton.gameObject.SetActive(true);

            if (slot.Count > 1)
            {
                _removeAllButton.gameObject.SetActive(true);
            }
            else
            {
                _removeAllButton.gameObject.SetActive(false);
            }
        }

        private void HideButtons()
        {
            _removeOneButton.gameObject.SetActive(false);

            _removeAllButton.gameObject.SetActive(false);
        }
    }

    [System.Serializable]
    public class InventorySlot
    {
        public Item Item;
        public int Count;
        public Image Icon;
        public Text CountText;

        public int Index; // Индекс слота

        private System.Action<InventorySlot> _onSlotClick;

        public void Initialize(System.Action<InventorySlot> onSlotClick, int index)
        {
            _onSlotClick = onSlotClick;
            Index = index;


            Button slotButton = Icon.GetComponentInParent<Button>();
            if (slotButton != null)
            {
                slotButton.onClick.AddListener(OnSlotClick);
            }
            else
            {
                Debug.LogWarning("No Button component found on slot " + (Index + 1));
            }
        }

        public void UpdateUI()
        {
            if (Item != null)
            {
                Icon.sprite = Item.Icon;

                Icon.enabled = true;

                if (Count > 1)
                {
                    CountText.text = Count.ToString();

                    CountText.enabled = true;
                }
                else
                {
                    CountText.enabled = false;
                }
            }
            else
            {
                Icon.enabled = false;

                CountText.enabled = false;
            }
        }

        private void OnSlotClick()
        {
            _onSlotClick?.Invoke(this);
        }
    }
}