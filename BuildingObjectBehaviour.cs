using UnityEngine;
using System.Collections;

public class BuildingObjectBehaviour : MonoBehaviour {
	BuildingObject obj;
	// Use this for initialization
	void Start () {
		
	}
	public void assignObj(BuildingObject bObj){
		obj = bObj;
	}
	public BuildingObject getObj(){
		return obj;
	}
	// Update is called once per frame
	void Update () {
	
	}
}
