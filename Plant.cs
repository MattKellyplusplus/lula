using UnityEngine;
using System.Collections;

public class Plant {
	/*
     * Frontier
 	 * ********
	 * This script contails all the information about the plant object and it's behaviour.
	 * ********
	 * @Matt
	 * ********
	 */
	string plantName, description;
	float nutritionalValue, growTime;
	bool growing, grown, watered;
	GameObject plantObject;
	Seed plantSeed;
	Item plantProduce;
	public ItemDatabase idb;
	
	public Plant (string n, float gt, Seed s) {
		plantName = n + " Plant";
		idb = GameObject.FindGameObjectWithTag ("Item Database").GetComponent<ItemDatabase>();
		setProduce(idb.returnItemByName(n));
		growTime = gt;
		plantSeed = s;
		growing = true;
		plantObject = Resources.Load<GameObject>("models/plants/" + plantName);
//		description = "This is a " + getName () + ",give it time to grow and you'll be able to harvest " + getProduce ().getName () + "'s soon.";
	}

	public bool waterPlant () {
		if (isWatered ()) {
			return false;
		} else {
			watered = true;
			growPlant ();
			return true;
		}
	}
	public bool isWatered () {
		return watered;
	}
	public void growPlant () {
		for(int i = 0; i < getGrowTime(); i++){
			// needs an ienunerator to record how long the plant has been growing for feedback to the player
		}
	}
	public void finishedGrowing () {
		setGrowing();
		setGrown();
	}
	public Item harvestPlant () {
		if(getGrown()){
			return getProduce();
		} else {
			return null;
		}
	}
	public void calculateNutritionalValue () {}
	public Seed getSeed () {
		return plantSeed;
	}
	public float getGrowTime () {
		return growTime;
	}
	public Item getProduce () {
		return plantProduce;
	}
	public void setProduce (Item i) {
		plantProduce = i;
	}
	public bool getGrowing () {
		return growing;
	}
	public void setGrowing () {
		growing = !growing;
	}
	public bool getGrown () {
		return grown;
	}
	public void setGrown () {
		grown = true;
		description = "This plant is ready to harvest!";
	}
	public string getName () {
		return plantName;
	}
	public string getDescription () {
		return description;
	}
}