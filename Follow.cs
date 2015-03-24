using UnityEngine;
using System.Collections;
[System.Serializable]
public class Follow : MonoBehaviour {
	public float distance;
	GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	
	}
	void OnGUI(){
		if(GUI.Button(new Rect(Screen.width-200,10,190,25), "Click to close application.")){
			Application.Quit();
		}
	}
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z+distance);
	}
}