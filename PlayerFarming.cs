using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerFarming : MonoBehaviour {
	RaycastHit hit;
	Ray ray;
	GameObject plot, target;
	GridSystem gridSystem;
	bool seedPouch, windowOpen, farmMode;
	List<Seed> seeds = new List<Seed> ();
	List<Plot> plotsBeingUsed = new List<Plot> ();

	void Start () {
		plot = Resources.Load<GameObject> ("Models/Plot");
		gridSystem = GameObject.FindGameObjectWithTag ("Grid System").GetComponent<GridSystem> ();
		seeds.Add(new Seed("Carrot", "Plant this seed in a plot of soil!", 15f));
	}

	void OnGUI () {
		if (GUI.Button (new Rect (Screen.width - 110, 10, 105, 30), "Farm Mode")) {
			farmMode = !farmMode;
		}
		if (seedPouch) {
			openSeedPouch ();
			if (!windowOpen) {
				gameObject.GetComponent<CharacterController> ().enabled = false;
				windowOpen = true;
			}
		}
	}

	void openSeedPouch (){
		int c = 0;
		for (int y = 0; y < 2; y++) {
			for (int x = 0; x < 4; x++) {
				GUI.Box (new Rect (15+(65*x),15+(65*y),65,65),"");
				if(c < seeds.Count)
				{
					if(GUI.Button(new Rect (15+(65*x),15+(65*y),65,65), seeds[c].getName())){
						plantSeed(seeds[c]);

					}
					c++;
				}
			}
		}
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if(!windowOpen) {
				for(int i = 0; i < plotsBeingUsed.Count; i++){
					if(plotsBeingUsed.Count > 0 && plotsBeingUsed[i].isSelected())
					{
						plotsBeingUsed.Remove(plotsBeingUsed[i].selectPlot());
					}
				}
			}
			closeWindows ();
			if (farmMode) {
				farmMode = false;
			}
		}
		Ray ray = Camera.main.ViewportPointToRay (new Vector3 (0.5F, 0.5F, 0));
		if (farmMode) {
			if (Physics.Raycast (ray, out hit, 15f)) {
				if (!gridSystem.isOn ()) {
					gridSystem.switchOnOff ();
				}
				gridSystem.selectGrid (gridSystem.getGrid (hit.point.x, hit.point.z));
			} else if (Physics.Raycast (ray, out hit, 16f) && gridSystem.isOn ()) {
				gridSystem.switchOnOff ();
			}

			if (Input.GetKeyDown (KeyCode.Mouse1)) {
				if (Physics.Raycast (ray, out hit, 5f) && hit.transform.tag == "Terrain") {
					gridSystem.placeObject (gridSystem.getGrid (hit.point.x, hit.point.z), plot);
				} else if (Physics.Raycast (ray, out hit, 5f) && hit.transform.tag == "Plot") {
					Plot p = hit.transform.GetComponent<Plot> ();
					if(p.isSelected()){
						seedPouch = true;
					} else {
						addPlot(p);
					}
				}
			}
		} 
		if (!farmMode && gridSystem.isOn()) {
			gridSystem.switchOnOff();
		}
		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			if(Physics.Raycast(ray, out hit, 5f) && hit.transform.tag == "Plot"){
				Plot p = hit.transform.GetComponent<Plot>();
				addPlot(p);
			}
		}
	}
	void closeWindows(){
		gameObject.GetComponent<CharacterController>().enabled = true;
		windowOpen = false;
		seedPouch = false;
	}
	void addPlot (Plot p) {
		if (!plotsBeingUsed.Contains (p)) {
			plotsBeingUsed.Add (p.selectPlot ());
		} else {
			p.selectPlot();
			plotsBeingUsed.Remove(p);
		}
	}
	void plantSeed(Seed seed){
		for(int i = 0; i < plotsBeingUsed.Count; i++) {
			plotsBeingUsed[i].plantSeed(seed);
			plotsBeingUsed[i].sowSeed();
		}
	}
}
