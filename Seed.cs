using UnityEngine;
using System.Collections;

public class Seed {
	/*
     * Frontier
 	 * ********
	 * This script holds all information and carries out any changes to the seed object as well as creating the plant
	 * ********
	 * @Matt
	 * ********
	 */
	string seedName, seedDescription;
	float growTime;
	bool watered, planted;
	Plant plant;
	Texture2D seedIcon;
	
	public Seed (string n, string d, float gt) {
		seedName = n + " Seed";
		seedDescription = d;
		growTime = gt;
		plant = new Plant(n, gt, this);
		seedIcon = Resources.Load<Texture2D>("textures/icons/" + seedName);
	}
	
	
	public Plant getPlant () {
		return plant;
	}
	public bool plantSeed () {
		if (!isPlanted ()) {
			planted = true;
			return true;
		} else {
			return false;
		}
	}
	public bool isPlanted () {
		return planted;
	}
	public bool waterSeed () {
		if(!plant.getGrowing()){
			watered = true;
			return true;
		} else {
			return false;
		}
	}
	public bool isWatered () {
		return watered;
	}
	public float getGrowTime () {
		return growTime;
	}
	public Texture2D getIcon () {
		return seedIcon;
	}
	public string getName () {
		return seedName;
	}
	public string getDescription () {
		return seedDescription;
	}
}