using UnityEngine;
using System.Collections;
[System.Serializable]
public class MovmentTest : MonoBehaviour {
	public float horizontalVelocity;
	public float verticalVelocity;
	public float speedLimit, speedHolder;
	public float accelerationSpeed;
	public Vector2 pos;
	public bool verticalChanging;
	public bool horizontalChanging;
	public bool moving, diagonal;
	public GUIStyle gui;

	void Start(){
		accelerationSpeed = 0.01f;
		speedLimit = 0.2f;
		speedHolder = speedLimit;
		//verticalVelocity = 0.1f;
		//transform.LookAt(new Vector2(transform.position.x, transform.position.y+5));
		this.GetComponent<Renderer> ().material.color = Color.blue;
	}
	void OnGUI(){
		drawPanel ();
	}
	void drawPanel(){
		GUI.Box (new Rect (10, 10, 330, 135), "");
		if (GUI.Button (new Rect (15, 25, 25, 25), "-")) {
			setSpeedLimit (speedLimit - 0.01f);
			speedHolder = speedLimit;
		}
		if (GUI.Button (new Rect (310, 25, 25, 25), "+")) {
			setSpeedLimit (speedLimit + 0.01f);
			speedHolder = speedLimit;
		}
		GUI.Box (new Rect (45, 15, 260, 45), "");
		GUI.Box (new Rect (45, 15, 260, 45), "Changes the Speed Limit which is currently: " + speedHolder + ".", gui);
		if (GUI.Button (new Rect (15, 75, 25, 25), "-")) {
			setAccelerationSpeed (accelerationSpeed - 0.0025f);
		}
		if (GUI.Button (new Rect (310, 75, 25, 25), "+")) {
			setAccelerationSpeed (accelerationSpeed + 0.0025f);
		}
		GUI.Box (new Rect (45, 65, 260, 45), "");
		GUI.Box (new Rect (45, 65, 260, 45), "Changes the acceleration speed which is currently: " + accelerationSpeed + ".", gui);
		if (GUI.Button (new Rect (130, 115, 80, 25), "Reset")) {
			Application.LoadLevel(0);
		}
	}
	void Update(){	
		if (diagonal && speedLimit == speedHolder) {
			speedLimit /= 1.5f;
		}
		if (!diagonal && speedLimit != speedHolder) {
			speedLimit = speedHolder;
		}
		if (Input.GetKey (KeyCode.W) && verticalVelocity >= 0) {
			verticalChanging = true;
			verticalVelocity += accelerationSpeed; 
		}
		if (Input.GetKeyUp (KeyCode.W)) {	
			verticalChanging = false;
		}
		if (Input.GetKey (KeyCode.S) && verticalVelocity <= 0) {
			verticalChanging = true;
			verticalVelocity -= accelerationSpeed; 
		}
		if (Input.GetKeyUp (KeyCode.S)) {
			verticalChanging = false;
		}
		if (!verticalChanging) {
			if (verticalVelocity > 0) {
				verticalVelocity -= accelerationSpeed * 2;
			}
			if (verticalVelocity < 0) {			
				verticalVelocity += accelerationSpeed * 2;
			}
		}
		if (Input.GetKey (KeyCode.A) && horizontalVelocity <= 0) {
			horizontalChanging = true;
			horizontalVelocity -= accelerationSpeed; 
		}
		if (Input.GetKeyUp (KeyCode.A)) {
			horizontalChanging = false;
		}
		if (Input.GetKey (KeyCode.D) && horizontalVelocity >= 0) {
			horizontalChanging = true;		
			horizontalVelocity += accelerationSpeed; 
		}
		if (Input.GetKeyUp (KeyCode.D)) {	
			horizontalChanging = false; 
		}

		if (!verticalChanging) {
			if (verticalVelocity > 0) {
				verticalVelocity -= accelerationSpeed * 20;
				if(verticalVelocity < 0.1){
					verticalVelocity = 0;
				}
			}
			if (verticalVelocity < 0) {
				verticalVelocity += accelerationSpeed * 20;
				if(verticalVelocity > -0.1){
					verticalVelocity = 0;
				}
			}
		}
		if (!horizontalChanging) {
			if (horizontalVelocity > 0) {
				horizontalVelocity -= accelerationSpeed * 20;
				if(horizontalVelocity < 0.1){
					horizontalVelocity = 0;
				}
			}
			if (horizontalVelocity < 0) {
				horizontalVelocity += accelerationSpeed * 20;
				if(horizontalVelocity > -0.1){
					horizontalVelocity = 0;
				}
			}
		}

		if (verticalVelocity >= speedLimit) {
			verticalVelocity = speedLimit;
		}
		if (verticalVelocity <= speedLimit * -1) {
			verticalVelocity = speedLimit * -1;
		}
		
		if (horizontalVelocity >= speedLimit) {
			horizontalVelocity = speedLimit;
		}
		if (horizontalVelocity <= speedLimit * -1) {
			horizontalVelocity = speedLimit * -1;
		}
		
		if (horizontalVelocity != 0 || verticalVelocity != 0) {
			moving = true;
		} else {
			moving = false;
		}

		if (pos != getPos ()) {	
			pos = getPos ();
		}

		if (moving) {	
			pos = new Vector2 (pos.x + horizontalVelocity, pos.y + verticalVelocity);
			setPos (pos);
		}
		if (verticalVelocity > 0 && !horizontalChanging) {
			transform.LookAt(new Vector2(transform.position.x, transform.position.y+5));
			diagonal = false;
		}//Looking up
		if (verticalVelocity > 0 && horizontalVelocity > 0) {
			diagonal = true;
			transform.LookAt(new Vector2(transform.position.x+5, transform.position.y+5));
		}//Looking up+right
		if (horizontalVelocity > 0 && !verticalChanging) {
			transform.LookAt(new Vector2(transform.position.x+5, transform.position.y));
			diagonal = false;
		}//Looking right
		if (verticalVelocity < 0 && horizontalVelocity > 0) {
			transform.LookAt(new Vector2(transform.position.x+5, transform.position.y-5));
			diagonal = true;
		}//Looking down+right
		if (verticalVelocity < 0 && !horizontalChanging) {
			transform.LookAt(new Vector2(transform.position.x, transform.position.y-5));
			diagonal = false;
		}//Looking down
		if (verticalVelocity < 0 && horizontalVelocity < 0) {
			transform.LookAt(new Vector2(transform.position.x-5, transform.position.y-5));
			diagonal = true;
		}//Looking down+left
		if (horizontalVelocity < 0 && !verticalChanging) {
			transform.LookAt(new Vector2(transform.position.x-5, transform.position.y));
			diagonal = false;
		}//Looking left
		if (horizontalVelocity < 0 && verticalVelocity > 0) {
			transform.LookAt(new Vector2(transform.position.x-5, transform.position.y+5));
			diagonal = true;
		}//Looking up+left
	}

	Vector2 getPos(){	
		return gameObject.transform.position;
	}
	

	void setPos(Vector2 position){	
		gameObject.transform.position = position; 
	}
		
	void setSpeedLimit(float limit){	
		speedLimit = limit;
	}

	void setAccelerationSpeed(float speed){	
		accelerationSpeed = speed;
	}
	
}
