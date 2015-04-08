using UnityEngine;
using System.Collections;

public class Grid {
	/*
     * Frontier
 	 * ********
	 * This script holds all the getters and setters for the information held about each Grid.
	 * ********
	 * @Matt
	 * ********
	 */
	float gridSize;
	float startX;
	float startY;
	bool occupied;
	bool active;
	GameObject obj;
	
	public Grid (float x, float y, float size) {
		setStartX (x);
		setStartY (y);
		setGridSize (size);
	}
	
	public void setStartX (float x) {
		startX = x;
	}
	
	public float getStartX () {
		return startX;
	}
	
	public void setStartY (float y) {
		startY = y;
	}
	
	public float getStartY () {
		return startY;
	}
	
	public float getEndX () {
		return getStartX () + gridSize;
	}
	
	public float getEndY () {
		return getStartY () + gridSize;
	}
	
	public bool isOccupied () {
		return occupied;
	}
	
	public void setOccupied () {
		occupied = !occupied;
		Debug.Log ("Set Occupied");
	}
	
	public void setGridSize (float s) {
		gridSize = s;
	}
	
	public void setActive () {
		active = !active;
	}
	
	public bool getActive () {
		return active;
	}
	
	public bool addObject (GameObject o) {
		if (obj == null) {
			obj = o;
			isOccupied();
			return true;
		} else {
			return false;
		}
	}
	
	public bool clearObject () {
		if (obj != null) {
			obj = null;
			isOccupied();
			return true;
		} else {
			return false;
		}
	}
	
	public GameObject getObject () {
		return obj;
	}
	
	// Depending on wether this method is passed x, or y, it will return the mid-point of each grid.
	public float getMiddle (string XorY) {
		if (XorY == "x" || XorY == "X") {
			return (getStartX () + getEndX ()) / 2;
		} else if (XorY == "y" || XorY == "Y") {
			return (getStartY () + getEndY ()) / 2;
		} else {
			return 0f;
		}
	}
}