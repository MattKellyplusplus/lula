using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class BuildingObject {

	public float width;
	public float length;
	string objName;
	public List<Grid> gridsOn;
	public bool rotated;
	public GameObject objModel;

	void Start () {
		rotated = false;
	}
	public BuildingObject(string n, float w, float l){
		Debug.Log ("Called");
		objName = n;
		width = w;
		length = l;
		objModel = Resources.Load<GameObject> ("Models/" + objName);
	}
	public List<Grid> getGridsObjectIsOn() {
		return gridsOn;
	}
	public void setGridsObjectIsOn(List<Grid> grids) {
		gridsOn = grids;
	}
	public string getName () {
		return objName;
	}
	public float getWidth () {
		return width;
	}
	public float getLength () {
		return length;
	}
	public void rotate () {
		Debug.Log ("Rotated");
		rotated = !rotated;
		Debug.Log (rotated);
	}
	public bool isRotated () {
		return rotated;
	}
	public GameObject getObjectModel () {
		return objModel;
	}
}
