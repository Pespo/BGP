using UnityEngine;
using System;
using System.Collections.Generic; 		//Allows us to use Lists.
using Random = UnityEngine.Random; 		//Tells Random to use the Unity Engine random number generator.
	
public class BoardManager : MonoBehaviour {
	// Using Serializable allows us to embed a class with sub properties in the inspector.
	[Serializable]
	public class Count	{
		public int minimum; 			//Minimum value for our Count class.
		public int maximum; 			//Maximum value for our Count class.

		//Assignment constructor.
		public Count (int min, int max) {
			minimum = min;
			maximum = max;
		}
	}
	
	
	public int columns = 8; 										//Number of columns in our game board.
	public int rows = 8;											//Number of rows in our game board.
	//public Count wallCount = new Count (5, 9);						//Lower and upper limit for our random number of walls per level.
	public GameObject exit;											//Prefab to spawn for exit.
	public GameObject[] floorTiles;									//Array of floor prefabs.
	public GameObject[] wallTiles;									//Array of wall prefabs.
	//public GameObject[] enemyTiles;									//Array of enemy prefabs.
	//public GameObject[] outerWallTiles;								//Array of outer tile prefabs.
	
	private Transform boardHolder;									//A variable to store a reference to the transform of our Board object.
	private List <Vector3> gridPositions = new List <Vector3> ();	//A list of possible locations to place tiles.
	
	
	//Clears our list gridPositions and prepares it to generate a new board.
	void InitialiseList () {
		gridPositions.Clear ();

		for(int x = 1; x < columns-1; x++) {
			for(int y = 1; y < rows-1; y++) {
				gridPositions.Add (new Vector3(x, y, 0f));
			}
		}
	}

	void BoardSetup () {
		boardHolder = new GameObject ("Board").transform;

		for(int x = 0; x < columns; x++) {
			for(int y = 0; y < rows; y++) {
				GameObject toInstantiate = floorTiles[Random.Range (0,floorTiles.Length)];

				//Check if we current position is at board edge, if so choose a random outer wall prefab from our array of outer wall tiles.
				/*if(x == -1 || x == columns || y == -1 || y == rows)
					toInstantiate = outerWallTiles [Random.Range (0, outerWallTiles.Length)];*/
				
				GameObject instance = Instantiate (toInstantiate, new Vector3 (x + ( y * 0.5f), y, 0f), Quaternion.identity) as GameObject;
				instance.name = x + " - " + y;
				instance.transform.SetParent (boardHolder);
			}
		}
	}
	
	Vector3 RandomPosition () {
		int randomIndex = Random.Range (0, gridPositions.Count);
		Vector3 randomPosition = gridPositions[randomIndex];
		gridPositions.RemoveAt (randomIndex);
		return randomPosition;
	}
	
	void LayoutObjectAtRandom (GameObject[] tileArray, int minimum, int maximum) {
		int objectCount = Random.Range (minimum, maximum+1);
		for(int i = 0; i < objectCount; i++) {
			Vector3 randomPosition = RandomPosition();
			GameObject tileChoice = tileArray[Random.Range (0, tileArray.Length)];
			Instantiate(tileChoice, randomPosition, Quaternion.identity);
		}
	}
	
	public void SetupScene (int level) {
		BoardSetup ();
		InitialiseList ();
		//LayoutObjectAtRandom (wallTiles, wallCount.minimum, wallCount.maximum);

		/*
		//Determine number of enemies based on current level number, based on a logarithmic progression
		int enemyCount = (int)Mathf.Log(level, 2f);
		
		//Instantiate a random number of enemies based on minimum and maximum, at randomized positions.
		LayoutObjectAtRandom (enemyTiles, enemyCount, enemyCount);*/
		
		//Instantiate the exit tile in the upper right hand corner of our game board
		//Instantiate (exit, new Vector3 (columns - 1, rows - 1, 0f), Quaternion.identity);
	}
}
