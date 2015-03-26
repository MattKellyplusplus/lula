using UnityEngine;
using System.Collections;

public class Plot : MonoBehaviour {
	string description;
	bool hasSeed, seedSewn, canWater, selected;
	Seed seed;
	Plant plant;
	GameObject obj;
	
	public void selectPlot () {
		selected = !selected;
	}
	public bool plantSeed (Seed s) {
		if (!doesHasSeed ()) {
			seed = s;
			setHasSeed ();
			return true;
		} else {
			return false;
		}
	}
	public bool sowSeed () {
		if(hasSeed){
			seed.plantSeed();
			setSewn();
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
		if(isSewn()){
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
		if(plant.getGrown()){
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