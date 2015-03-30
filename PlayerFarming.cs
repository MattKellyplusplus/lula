using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class PlayerFarming : MonoBehaviour {
	public RaycastHit hit;
	public Ray ray;
	public GameObject plot, target;
	public GridSystem gs;
	bool seedPouch, windowOpen, buildMode;
	public List<Seed> seeds = new List<Seed> ();
	public List<Plot> plotsBeingUsed = new List<Plot> ();
	// Use this for initialization
	void Start () {
		plot = Resources.Load<GameObject> ("Models/Plot");
		gs = GameObject.FindGameObjectWithTag ("Grid System").GetComponent<GridSystem> ();
		seeds.Add(new Seed("Carrot", "Plant this seed in a plot of soil!", 15f));
		seeds.Add(new Seed("1", "Plant this seed in a plot of soil!", 15f));
		seeds.Add(new Seed("2", "Plant this seed in a plot of soil!", 15f));
		seeds.Add(new Seed("3", "Plant this seed in a plot of soil!", 15f));
		seeds.Add(new Seed("4", "Plant this seed in a plot of soil!", 15f));
		seeds.Add(new Seed("5", "Plant this seed in a plot of soil!", 15f));
	}

	void OnGUI () {
		if (GUI.Button (new Rect (Screen.width - 110, 10, 105, 30), "Build Mode")) {
			buildMode = !buildMode;
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
		GUI.Box (new Rect (10, 10, 270, 140), "");
		for (int y = 0; y < 2; y++) {
			for (int x = 0; x < 4; x++) {
				GUI.Box (new Rect (15+(65*x),15+(65*y),65,65),"");
				if(c < seeds.Count)
				{
					if(GUI.Button(new Rect (15+(65*x),15+(65*y),65,65), seeds[c].getName())){
						plantSeed(seeds[c]);
						Debug.Log(c + "vs" +seeds.Count);

					}
					c++;
				}
			}
		}
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if(!windowOpen) {
				for(int i = 0; i < plotsBeingUsed.Count; i++){
					if(plotsBeingUsed[i].isSelected())
					{
						plotsBeingUsed.Remove(plotsBeingUsed[i].selectPlot());
					}
				}
			}
			closeWindows ();
			if (buildMode) {
				buildMode = false;
			}
		}
		Ray ray = Camera.main.ViewportPointToRay (new Vector3 (0.5F, 0.5F, 0));
		if (buildMode) {
			if (Physics.Raycast (ray, out hit, 15f)) {
				if (!gs.isOn ()) {
					gs.switchOnOff ();
				}
				gs.selectGrid (gs.getGrid (hit.point.x, hit.point.z));
			} else if (Physics.Raycast (ray, out hit, 16f) && gs.isOn ()) {
				gs.switchOnOff ();
			}

			if (Input.GetKeyDown (KeyCode.Mouse1)) {
				if (Physics.Raycast (ray, out hit, 5f) && hit.transform.tag == "Terrain") {
					gs.placeObject (gs.getGrid (hit.point.x, hit.point.z), plot);
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
		if (!buildMode && gs.isOn()) {
			gs.switchOnOff();
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
