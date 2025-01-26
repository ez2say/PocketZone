using UnityEngine;

namespace PocketZone
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    public class Item : ScriptableObject
    {
        public string ID;
        public Sprite Icon;
        public int MaxStack = 10;
    }
}
