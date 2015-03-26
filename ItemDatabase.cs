using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {
	
	public List<Item> items = new List<Item>();
	//public Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	
	int itemID;
	string stringID;
	string endID;
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
	void Start () {
		addItem ("Carrot", "Tasty Carrot", 0, 0, 15, 0, 0, 10, "Consumable", true);
	}
	public void addItem(string name, string desc, int maxDura, int curDura, int damage, int speed, int slot, int value, string type, bool stack)
	{
		if(itemID <=9)
		{
			stringID = "000";
			endID = itemID.ToString();
			stringID = string.Concat(stringID, endID);
		}
		else if(itemID > 9 && itemID < 100)
		{
			stringID = "00";
			endID = itemID.ToString();
			stringID = string.Concat(stringID, endID);
		}
		else if(itemID > 99 && itemID < 1000)
		{
			stringID = "0";
			endID = itemID.ToString();
			stringID = string.Concat(stringID, endID);
		}
		else if(itemID > 999 && itemID < 10000)
		{
			endID = itemID.ToString();
			stringID = endID;
		}
		itemID++;
		items.Add(new Item(name,stringID,desc,maxDura,curDura,damage,speed,slot, value, type, stack));
	}
	public Item returnItem(string id)
	{
		for (int i= 0; i<items.Count; i++)
		{
			if (id == items[i].itemID)
			{
				print ("Returned " + items[i]);
				return items[i];
			}
		}
		return null;
	}
	public Item returnItemByName(string name)
	{
		for (int i= 0; i<items.Count; i++)
		{
			if (name == items[i].itemName)
			{
				return items[i];
			}
		}
		return null;
	}
	
}