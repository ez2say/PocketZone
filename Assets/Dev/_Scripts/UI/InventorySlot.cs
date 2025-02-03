using UnityEngine.UI;
using System;
using UnityEngine;

namespace PocketZone
{
    [System.Serializable]
    public class InventorySlot
    {
        public Item Item;
        public int Count;
        public Image Icon;
        public Text CountText;
        public int Index;
        private ISlotClickHandler _clickHandler;

        public void Initialize(ISlotClickHandler clickHandler, int index)
        {
            _clickHandler = clickHandler;
            Index = index;

            if (Icon == null)
            {
                Debug.LogError("Icon is not assigned for inventory slot at index " + index);
                return;
            }

            Button slotButton = Icon.GetComponent<Button>();
            if (slotButton != null)
            {
                slotButton.onClick.AddListener(OnSlotClick);
            }
            else
            {
                Debug.LogError("Button component not found for inventory slot at index " + index + ". Ensure that the parent GameObject of the Icon has a Button component.");
            }
        }

        public void UpdateUI()
        {
            if (Item != null)
            {
                if (Icon != null)
                {
                    Icon.sprite = Item.Icon;
                    Icon.enabled = true;
                }
                else
                {
                    Debug.LogError("Icon is not assigned for inventory slot at index " + Index);
                }

                if (CountText != null)
                {
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
                    Debug.LogError("CountText is not assigned for inventory slot at index " + Index);
                }
            }
            else
            {
                if (Icon != null)
                {
                    Icon.enabled = false;
                }
                else
                {
                    Debug.LogError("Icon is not assigned for inventory slot at index " + Index);
                }

                if (CountText != null)
                {
                    CountText.enabled = false;
                }
                else
                {
                    Debug.LogError("CountText is not assigned for inventory slot at index " + Index);
                }
            }
        }

        private void OnSlotClick()
        {
            _clickHandler?.OnSlotClick(this);
        }
    }
}