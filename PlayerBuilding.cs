using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBuilding : MonoBehaviour {
	bool buildMode;
	RaycastHit hit;
	Ray ray;
	GameObject currentObject;
	BuildingObject b;
	List<Grid> grids = new List<Grid>();
	List<BuildingObject> objects = new List<BuildingObject>();
	GridSystem gridSystem;

	void Start () {
		gridSystem = GameObject.FindGameObjectWithTag ("Grid System").GetComponent<GridSystem> ();
		objects.Add(new BuildingObject("Block", 3f, 3f));
		objects.Add(new BuildingObject("Block", 3f, 3f));
		objects.Add(new BuildingObject("Small Block", 1f, 1f));
		objects.Add(new BuildingObject("Stairs", 1f, 1f));
		objects.Add (new BuildingObject ("Wood Wall", 3f, 0.2f));
		objects.Add (new BuildingObject ("Wood Wall", 3f, 0.2f));

	}
	
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			buildMode = false;
			if(gridSystem.isOn()){
				gridSystem.switchOnOff();
			}
		}
		if (buildMode) {

			ray = Camera.main.ViewportPointToRay (new Vector3 (0.5F, 0.5F, 0));
			if (Physics.Raycast (ray, out hit, 15f)) {
				if(hit.transform.tag == "Terrain"){
					if(!hasObject()){
						gridSystem.selectGrid(gridSystem.getGrid(hit.point.x,hit.point.z));
					}
					if(hasObject()){
						if(getCurrentObject().tag == "Building Block") {
							getCurrentObject().transform.position = new Vector3(gridSystem.getGrid(hit.point.x,hit.point.z).getMiddle("x"), hit.point.y, gridSystem.getGrid(hit.point.x,hit.point.z).getMiddle("Y"));
						}
						if(getCurrentObject().tag == "Furniture") {
							getCurrentObject().transform.position = new Vector3(gridSystem.getGrid(hit.point.x,hit.point.z).getStartX(), hit.point.y, gridSystem.getGrid(hit.point.x,hit.point.z).getStartY());
						}
						if(gridSystem.isOn()){
							gridSystem.switchOnOff();
						}
					}
				}
				else if(hit.transform.tag == "Building Block" && hasObject()){
					if(gridSystem.isOn()){
						gridSystem.switchOnOff();
					}
					if(getCurrentObject().tag == "Wall"){
						getCurrentObject().transform.position = new Vector3(hit.transform.position.x + 1f, hit.transform.position.y + 1.735f, hit.transform.position.z+1f);
					}
					if(getCurrentObject().tag == "Furniture"){
						getCurrentObject().transform.position = new Vector3(gridSystem.getGrid(hit.point.x,hit.point.z).getStartX(), hit.point.y, gridSystem.getGrid(hit.point.x,hit.point.z).getStartY());
					}
				}
				if(getCurrentObject () != null) {
					if(getCurrentObject().tag == "Building Block"){
						if(Input.GetAxis("Mouse ScrollWheel") < 0){
							getCurrentObject().transform.Rotate(0f,90f,0f);
							b.rotate();
						} else if(Input.GetAxis("Mouse ScrollWheel") > 0) {
							getCurrentObject().transform.Rotate(0f,-90,0);
							b.rotate();
						}
					}
					if(getCurrentObject().tag == "Wall"){
						if(Input.GetAxis("Mouse ScrollWheel") < 0){
							getCurrentObject().transform.Rotate(0f,90f,0f);
						} else if (Input.GetAxis("Mouse ScrollWheel") > 0) {
							getCurrentObject().transform.Rotate(0f,-90f,0f);
						}
					}
					if(getCurrentObject().transform.GetChild(0).gameObject.layer == 0){
						getCurrentObject().transform.GetChild(0).gameObject.layer = 2;
					}
					if(Input.GetKeyDown(KeyCode.Mouse1)){
						if(hasObject()){
							if(getCurrentObject().tag == "Building Block"){
								if(canPlace()) {
									getCurrentObject().layer = 6;
									getCurrentObject().transform.GetChild(0).gameObject.layer = 6;
									for(int i = 0; i < grids.Count; i++){
										grids[i].setOccupied();
									}
									grids = new List<Grid>();
									objects.Remove(currentObject.GetComponent<BuildingObjectBehaviour>().getObj());
									setCurrentObject(null, new Vector3(0,0,0));
									gridSystem.switchOnOff();
								}
							}
							else if(getCurrentObject().tag == "Wall"){
								if(getCurrentObject().transform.position != new Vector3(0,-100,0)){
									getCurrentObject().layer = 6;
									getCurrentObject().transform.GetChild(0).gameObject.layer = 6;
									objects.Remove(currentObject.GetComponent<BuildingObjectBehaviour>().getObj());
									setCurrentObject(null, new Vector3(0,0,0));
									gridSystem.switchOnOff();
								}
							}
						}
					}
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
		if (obj == null) {
			currentObject = null;
			b = null;
		}		
		if (obj != null) {
			b = obj;
			currentObject = Instantiate (obj.getObjectModel(), new Vector3 (0,-100,0), Quaternion.identity) as GameObject;
			b.objModel = currentObject;
			b.objModel.GetComponent<BuildingObjectBehaviour>().assignObj(b);
		}
	}
	bool hasObject(){
		return currentObject != null;
	}
	bool canPlace () {
		if (getCurrentObject().tag == "Wall") {
			return true;
		}
		for (int i = 0; i < b.getWidth(); i++) {
			for(int j = 0; j < b.getLength(); j++){
				if(b.isRotated()) {
					grids.Add(gridSystem.getGrid(getCurrentObject().transform.position.x-(j+0.1f), getCurrentObject().transform.position.z+i));
				}
				else if (!b.isRotated()) {
					grids.Add(gridSystem.getGrid(getCurrentObject().transform.position.x+i, getCurrentObject().transform.position.z+j));
				}
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
