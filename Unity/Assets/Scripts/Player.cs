using UnityEngine;
using System.Collections;
using UnityEngine.UI;	//Allows us to use UI.

//Player inherits from MovingObject, our base class for objects that can move, Enemy also inherits from this.
public class Player : MonoBehaviour {
	
	private Animator animator;					//Used to store a reference to the Player's animator component.
	private Vector2 touchOrigin = -Vector2.one;	//Used to store location of screen touch origin for mobile controls.
	private BoxCollider2D boxCollider; 		//The BoxCollider2D component attached to this object.
	private Rigidbody2D rb2D;				//The Rigidbody2D component attached to this object.

	protected void Start () {
		animator = GetComponent<Animator>();

		boxCollider = GetComponent <BoxCollider2D> ();
		rb2D = GetComponent <Rigidbody2D> ();
	}	
	
	private void Update () {
		//If it's not the player's turn, exit the function.
		if(!GameManager.instance.playersTurn) return;
		
		int horizontal = 0;  	//Used to store the horizontal move direction.
		int vertical = 0;		//Used to store the vertical move direction.
	}
	
	protected void move() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if(Physics.Raycast (ray, out hit)) {
			if(hit.transform.name == "Player") {
				Debug.Log ("This is a Player");
			} else {
				Debug.Log ("This isn't a Player");                
			}
		}
	}
	
	//OnTriggerEnter2D is sent when another object enters a trigger collider attached to this object (2D physics only).
	private void OnTriggerEnter2D (Collider2D other)
	{
		
	}
	
	
	//Restart reloads the scene when called.
	private void Restart ()
	{
		//Load the last scene loaded, in this case Main, the only scene in the game.
		Application.LoadLevel (Application.loadedLevel);
	}
	
	//CheckIfGameOver checks if the player is out of food points and if so, ends the game.
	private void CheckIfGameOver () {
		
	}
}

