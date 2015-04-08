using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class PlayerBuilding : MonoBehaviour {
	bool buildMode;
	public RaycastHit hit;
	Ray ray;
	public GameObject currentObject;
	public BuildingObject b;
	List<Grid> grids = new List<Grid>();
	public List<BuildingObject> objects = new List<BuildingObject>();
	GridSystem gridSystem;
	// Use this for initialization
	void Start () {
		gridSystem = GameObject.FindGameObjectWithTag ("Grid System").GetComponent<GridSystem> ();
		objects.Add(new BuildingObject("Block", 3f, 3f));
		objects.Add(new BuildingObject("Small Block", 1f, 1f));
		objects.Add(new BuildingObject("Stairs", 1f, 1f));
		//setCurrentObject (objects [0]);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			buildMode = false;
			gridSystem.switchOnOff();
		}
		if (buildMode) {
			ray = Camera.main.ViewportPointToRay (new Vector3 (0.5F, 0.5F, 0));
			if (Physics.Raycast (ray, out hit, 15f)) {
				if(hit.transform.tag == "Terrain") {
					gridSystem.selectGrid(gridSystem.getGrid(hit.point.x,hit.point.z));
				}	
				if(!gridSystem.isOn()){
					gridSystem.switchOnOff();
				}
				if(getCurrentObject () != null) {
					if(b.isRotated() && Input.GetAxis("Mouse ScrollWheel") < 0){
						getCurrentObject().transform.Rotate(0f,90f,0f);
						b.rotate();
					} else if(!b.isRotated() && Input.GetAxis("Mouse ScrollWheel") > 0) {
						getCurrentObject().transform.Rotate(0f,-90,0);
						b.rotate();
					}
					/*if(getCurrentObject().transform.position != new Vector3(gridSystem.getGrid(hit.point.x,hit.point.z).getStartX(), hit.point.y, gridSystem.getGrid(hit.point.x,hit.point.z).getStartX())){
						if(canPlace()){
							getCurrentObject().transform.GetChild(0).GetComponent<Renderer>().material.color = Color.white;
						} else {*/
							getCurrentObject().transform.GetChild(0).GetComponent<Renderer>().sharedMaterial.color = Color.white;
					//	}
					//}
					getCurrentObject().transform.GetChild(0).gameObject.layer = 2;
					//if(!gridSystem.getGrid(hit.point.x,hit.point.z).isOccupied()) {
						getCurrentObject().transform.position = new Vector3(gridSystem.getGrid(hit.point.x,hit.point.z).getStartX(), hit.point.y, gridSystem.getGrid(hit.point.x,hit.point.z).getStartY());
					//}
					if(Input.GetKeyDown(KeyCode.Mouse1)){
						if(canPlace()) {
							Debug.Log("Got to here");
							getCurrentObject().transform.GetChild(0).gameObject.layer = 0;
							for(int i = 0; i < grids.Count; i++){
								grids[i].setOccupied();
							}
							grids = new List<Grid>();
							objects.Remove(currentObject.GetComponent<BuildingObjectBehaviour>().getObj());
							setCurrentObject(null, new Vector3(0,0,0));
						}
					}
					//}
				}
			}
		} 
		if (!buildMode && getCurrentObject () != null) {
			GameObject.DestroyObject(getCurrentObject());
			setCurrentObject(null, new Vector3(0,0,0));
		}
	}

	void OnGUI () {
		if (GUI.Button (new Rect (Screen.width - 110, 45, 105, 30), "Build Mode")) {
			buildMode = !buildMode;
		}
		if (buildMode) {
			int c = 0;
			for (int y = 0; y < 2; y++) {
				for (int x = 0; x < 4; x++) {
					GUI.Box (new Rect (15+(65*x),15+(65*y),65,65),"");
					if(c < objects.Count)
					{
						if(GUI.Button(new Rect (15+(65*x),15+(65*y),65,65), objects[c].getName())){
							setCurrentObject(objects[c], hit.point);
						}
					}
					c++;
				}
			}
		}
	}
	GameObject getCurrentObject () {
		return currentObject;
	}
	void setCurrentObject (BuildingObject obj, Vector3 pos) {
		//currentObject = obj.getObjectModel ();
		if (obj == null) {
			currentObject = null;
			b = null;
		}		
		if (obj != null) {
			b = obj;
			currentObject = Instantiate (obj.getObjectModel(), new Vector3 (gridSystem.getGrid (pos.x, pos.z).getStartX (), pos.y, gridSystem.getGrid (pos.x, pos.z).getStartY ()), Quaternion.identity) as GameObject;
			b.objModel = currentObject;
			b.objModel.GetComponent<BuildingObjectBehaviour>().assignObj(b);
		}
	}
	bool canPlace () {
		for (int i = 0; i < b.getWidth(); i++) {
			for(int j = 0; j < b.getLength(); j++){
				if(b.isRotated()) {
					grids.Add(gridSystem.getGrid(getCurrentObject().transform.position.x-(j+0.1f), getCurrentObject().transform.position.z+i));
				}
				else if (!b.isRotated()) {
					grids.Add(gridSystem.getGrid(getCurrentObject().transform.position.x+i, getCurrentObject().transform.position.z+j));
				}
				Debug.Log(grids[grids.Count-1].getStartX()+","+grids[grids.Count-1].getStartY());
				Debug.Log(getCurrentObject().transform.position.x+i+","+ getCurrentObject().transform.position.z+j);
				Debug.Log(grids.Count);
			}
		}
		for (int k = 0; k < grids.Count; k++) {
			if(grids[k].isOccupied()){
				return false;
			}
		}
		return true;
	}
}
