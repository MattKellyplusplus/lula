using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item
{
	public string itemName;
	public string itemID;
	public string itemDescription;
	public int itemMaxDurability;
	public int itemCurDurability;
	public int itemDamage;
	public int itemSpeed;
	public int itemEquipSlot;
	public int itemValue;
	public bool stackable;
	public int itemStackSize;
	public int itemMaxStack;
	public Texture2D itemIcon;
	public GameObject itemMesh;
	public Texture3D itemTexture;
	public ItemType itemType;
	/*---Character Equipment Reference---
	 * 0 - Head
	 * 1 - Neck
	 * 2 - Shoulder
	 * 3 - Back
	 * 4 - Hands
	 * 5 - Left Hand
	 * 6 - Right Hand
	 * 7 - Torso
	 * 8 - Legs
	 * 9 - Feet
	 */
	
	public enum ItemType
	{
		Weapon, 
		Equipment,
		Consumable,
		Quest,
		Trade,
		Trash
	}
	public Item (string name, string ID, string desc, int maxDura, int curDura, int damage, int speed, int slot, int value, string type, bool stack)
	{
		itemName = name;
		itemID = ID;
		itemDescription = desc;
		itemMaxDurability = maxDura;
		itemCurDurability = curDura;
		itemDamage = damage;
		itemSpeed = speed;
		itemEquipSlot = slot;
		itemValue = value;
		if (type == "Trade") {
			itemType = ItemType.Trade;
			itemMaxStack = 40;
		}
		if (type == "Consumable") {
			itemType = ItemType.Consumable;
			itemMaxStack = 40;
		}
		if (type == "Trash") {
			itemType = ItemType.Trash;
			itemMaxStack = 20;
		}
		if (type == "Weapon") {
			itemType = ItemType.Weapon;
		}
		if (type == "Quest") {
			itemType = ItemType.Quest;
		}
		if (type == "Equipment") {
			itemType = ItemType.Equipment;
		}
		stackable = stack;
		itemStackSize = 1;
		itemMesh = Resources.Load<GameObject> ("Models/" + itemName);
		itemIcon = Resources.Load<Texture2D> ("Icons/" + itemName);
	}

	public Item (string icon)
	{
		itemName = null;
		itemIcon = Resources.Load<Texture2D> ("Icons/" + icon);
	}

	public Item ()
	{
		itemName = null;
	}

	public string getName ()
	{
		return itemName;
	}

	public void useConsumable ()
	{
		this.itemStackSize--;
	}
}