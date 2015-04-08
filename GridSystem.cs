using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
/*
 * Frontier
 * ********
 * This script carries out the generation and storage of grid squares.
 * ********
 * @Matt
 * ********
 */
public class GridSystem : MonoBehaviour {
	
	public TerrainData td; //Set in the inspector, gets the length of the terrain to be used for calculated the amount of grid squares.
	public float gridSize; //Set in the inspector, will change the size of the grid squares, and therefore the ammount of grid squares.
	float gridAmount; //Used to hold the max X and Y values.
	Projector projector;
	List<Grid> grid = new List<Grid> (); //Holds all the individual grids squares.
	public Material red, green; //For the colour of the projection over the terrain.
	int a;
	void Start () {
		projector = GameObject.FindGameObjectWithTag ("Projector").GetComponent<Projector>();
		gridAmount = td.size.x / gridSize;
		for (int y = 0; y < gridAmount; y++) {
			for (int x = 0; x < gridAmount; x++) {
				grid.Add(new Grid(x * gridSize, y * gridSize, gridSize));
			}
		}
	}
	void Update(){
	}
	/*
	 * Given any co-ords, returns the grid that contains those co-ordinates.
	 * Does this by chcking first if the 'x' co-ord is within each Grid(in the 'grid' List<Grid>)'s lowest ...
	 * ... 'x' co-ord and its highest.
	 * If it is within the min-max 'x' range, it will then check the 'y' in the same way.
	 * Then returns the grid that has those co-ords.
	*/
	public Grid getGrid (float x, float y) {
		int i;
		Vector2 v1 = new Vector2 (grid [0].getStartX (), grid [0].getStartY ());
		Vector2 v2 = new Vector2 (grid [grid.Count-1].getStartX (), grid [grid.Count-1].getStartY ());
		if(Vector2.Distance((new Vector2(x,y)), v1) > Vector2.Distance((new Vector2(x,y)), v2)){
		//if(Vector2.Distance(new Vector3(x,y)), new Vector2(grid[0].getStartX(), new Vector2(grid[0].getStartY())) > Vector2.Distance(new Vector3(x,y)), new Vector2(grid[grid.Count].getStartX(), new Vector2(grid[grid.Count].getStartY()))){
			i = grid.Count/2;
		}
		for (i = 0; i < grid.Count; i++) {
			if(grid[i].getStartX() <= x && grid[i].getEndX () > x){
				if(grid[i].getStartY() <= y && grid[i].getEndY () > y){
					return grid[i];
				}
			}
		}
		return null;
	}
	
	/*
	 * Should only be called after getGrid has pulled the Grid that the player is looking at/hovering over.
	 * It provides feedback to the player by bringing the projector over to that grid, then seeing if the ...
	 * ... grid is occupied by an object.
	 * Projects a green square if the grid square is not occupied and a red square if it is. 
	 */
	public void selectGrid (Grid g) {
		projector.transform.position = new Vector3 (g.getMiddle ("x"), td.GetHeight((int)g.getMiddle ("x"), (int)g.getMiddle ("y"))+0.9f, g.getMiddle ("y"));
		setActive (g);
		if (g.isOccupied()) {
			projector.material = red;
		} else if (!g.isOccupied()) {
			projector.material = green;
		}
	}
	public void switchOnOff(){
		projector.enabled = !projector.enabled;
	}
	public bool isOn(){
		return projector.enabled;
	}
	/*
	 * Should be called from the player building script 
	 * Eg. 
	 * 		if(gridsystem.placeObject (someGrid, someGameObject)) {
	 * 			buildingInventory.Remove(someGameObject);
	 * 		}
	 * That will remove the item from the players building inventory, only if it is possible to put the building where the player ...
	 * ... choose to put it.
	 */
	public bool placeObject (Grid g, GameObject go) {
		if (!g.isOccupied()) {
			g.addObject(Instantiate (go, new Vector3 (g.getMiddle ("x"), td.GetHeight((int)g.getMiddle ("x"), (int)g.getMiddle ("y")), g.getMiddle ("y")), Quaternion.identity) as GameObject);
			g.setOccupied();
			return true;
		} else {
			return false;
		}
	}
	
	/* 
	 * This method will need another argument to give the array or list that is being used to hold the players building material, ...
	 * ... so that the object on the Grid will be given back to the player. 
	 */
	public bool removeObject (Grid g) {
		/*
		 * So we should have here
		 * if (g.getObject !=null) {
		 * 		someArray.Add(g.getObject());
		 * }
		 */
		if (g.clearObject()) {
			return true;
		} else {
			return false;
		}
	}
	
	// Just gives the object that is currently on the grid square, returns 'null' if there is none.
	public GameObject getGridObject (Grid g) {
		return g.getObject ();
	}
	
	/*
	 * First turns off all grid squares that are currently active.
	 * Then, sets the required grid square to active.
	 */
	public void setActive (Grid g) {
		for (int i = 0; i < grid.Count; i++) {
			if(grid[i].getActive()){
				grid[i].setActive(); 
			}
		}
		g.setActive();
	}
}