using UnityEngine;
using System.Collections;

public class Plot : MonoBehaviour {
	/*
     * Frontier
 	 * ********
	 * This script handles all script interaction and player interaction involved in growing crops.
	 * ********
	 * @Matt
	 * ********
	 */
	string description;
	bool hasSeed, seedSewn, canWater, selected;
	Seed seed;
	Plant plant;
	GameObject obj;
	
	public Plot selectPlot () {
		selected = !selected;
		if (selected) {
			gameObject.GetComponent<Renderer>().material.color = Color.green;
			return this;
		} else {
			gameObject.GetComponent<Renderer>().material.color = Color.white;
			return null;
		}
	}
	public bool isSelected() {
		return selected;
	}
	public bool plantSeed (Seed s) {
		if (!doesHasSeed ()) {
			seed = s;
			plant = seed.getPlant ();
			setHasSeed ();
			return true;
		} else {
			return false;
		}
	}
	public bool sowSeed () {
		if(hasSeed && !isSewn()) {
			seed.plantSeed();
			setSewn();
			Debug.Log(seed.getName() +" was planted in a plot.");
			return true;
		} else {
			return false;
		}
	}
	public bool doesHasSeed () {
		return hasSeed;
	}
	public void setHasSeed () {
		hasSeed = !hasSeed;
	}
	public bool waterPlot () {
		if(isSewn()) {
			seed.waterSeed();
			return true;
		} else {
			return false;
		}
	}
	public void setSewn () {
		seedSewn = !seedSewn;
	}
	public bool isSewn() {
		return seedSewn;
	}
	public Item harvestPlot () {
		if(plant.getGrown()) {
			return plant.harvestPlant();
		} else {
			return null;
		}
	}
	public void destroyPlot() {
		Destroy (gameObject);
	}
	public void setDescription () {
		if (doesHasSeed () && !isSewn ()) {
			description = "This plot has a " + seed.getName () + " set down, but it needs to be sewn.";
		} else if (isSewn ()) {
			description = "This plot has a " + seed.getName () + " sewn into the soil, now it needs to be watered.";
		} else if (seed.isWatered ()) {
			description = "This plot has a " + plant.getName () + " growing in it, wait until it's fully grown before you harvest it.";
		} else if (plant.getGrown ()) {
			description = "This plot has a fully grown and ready to harvest " + plant.getName () + ".";
		} else {
			description = "This plot has is empty.";
		}
	}
	public string getDescription () {
		return description;
	}
}