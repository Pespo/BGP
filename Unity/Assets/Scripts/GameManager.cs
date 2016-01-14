using UnityEngine;
using System.Collections;

using System.Collections.Generic;		//Allows us to use Lists. 
using UnityEngine.UI;					//Allows us to use UI.
	
public class GameManager : MonoBehaviour {
	public float levelStartDelay = 2f;						//Time to wait before starting level, in seconds.
	public float turnDelay = 0.1f;							//Delay between each Player turn.
	public static GameManager instance = null;				//Static instance of GameManager which allows it to be accessed by any other script.
	[HideInInspector] public bool playersTurn = true;		//Boolean to check if it's players turn, hidden in inspector but public.

	private Text levelText;									//Text to display current level number.
	private GameObject levelImage;							//Image to block out level as levels are being set up, background for levelText.
	private BoardManager boardScript;						//Store a reference to our BoardManager which will set up the level.
	private int level = 1;									//Current level number, expressed in game as "Day 1".
	//private List<Enemy> enemies;							//List of all Enemy units, used to issue them move commands.
	//private bool enemiesMoving;							//Boolean to check if enemies are moving.
	private bool doingSetup = true;							//Boolean to check if we're setting up board, prevent Player from moving during setup.
	private Player player;
   
	//Awake is always called before any Start functions
	void Awake() {
		if (instance == null) instance = this;
		else if (instance != this) Destroy(gameObject);	

		DontDestroyOnLoad(gameObject); //Sets this to not be destroyed when reloading scene
		
		//enemies = new List<Enemy>();
		boardScript = GetComponent<BoardManager>();
		InitGame();

		GameObject playerGO = Instantiate ( Resources.Load ("Player") as GameObject );
        player = playerGO.GetComponent<Player>();
        if (player == null) Debug.Log("FUCK");

        player.move(boardScript.startGO);
    }
	
	//This is called each time a scene is loaded.
	void OnLevelWasLoaded( int index ) {
		level++;
		InitGame();
	}
	
	//Initializes the game for each level.
	void InitGame()	{
		doingSetup = true;

		levelImage = GameObject.Find("LevelImage");
		levelText = GameObject.Find("LevelText").GetComponent<Text>();
		levelText.text = "Day " + level;
		levelImage.SetActive(true);
		
		//Call the HideLevelImage function with a delay in seconds of levelStartDelay.
		Invoke("HideLevelImage", levelStartDelay);
		
		//Clear any Enemy objects in our List to prepare for next level.
		//enemies.Clear();

		boardScript.SetupScene(level);		
	}
	
	//Hides black image used between levels
	void HideLevelImage() {
		levelImage.SetActive(false);
		doingSetup = false;
	}
	
	//Update is called every frame.
	void Update() {
		if( doingSetup ) return;

		if( Input.GetMouseButtonDown ( 0 ) )
			click();
		//StartCoroutine (MoveEnemies ());
	}

	protected void click() {
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		 
		if ( hit.collider ) {
			Debug.Log ("Hit : " + hit.collider.gameObject.name );
            player.move( hit.collider.gameObject );
            Debug.Log(" exit GO : " + boardScript.exitGO.transform.position);
            Debug.Log(" hit GO : " + hit.collider.gameObject.transform.position);
            if (boardScript.exitGO.transform.position == hit.collider.gameObject.transform.position ) {
                Debug.Log("WIN !!");  
            }
        } else { 
			Debug.Log ("Miss !");
		}
	}
	
	//Call this to add the passed in Enemy to the List of Enemy objects.
	/*public void AddEnemyToList(Enemy script)
	{
		//Add Enemy to List enemies.
		enemies.Add(script);
	}*/
	
	
	//GameOver is called when the player reaches 0 food points
	public void GameOver() {
		levelText.text = "After " + level + " days, you starved.";
		levelImage.SetActive(true);
		enabled = false;
	}
	
	//Coroutine to move enemies in sequence.
	/*IEnumerator MoveEnemies() {
		//While enemiesMoving is true player is unable to move.
		enemiesMoving = true;
		
		//Wait for turnDelay seconds, defaults to .1 (100 ms).
		yield return new WaitForSeconds(turnDelay);
		
		//If there are no enemies spawned (IE in first level):
		if (enemies.Count == 0) 
		{
			//Wait for turnDelay seconds between moves, replaces delay caused by enemies moving when there are none.
			yield return new WaitForSeconds(turnDelay);
		}

		//Loop through List of Enemy objects.
		for (int i = 0; i < enemies.Count; i++)
		{
			//Call the MoveEnemy function of Enemy at index i in the enemies List.
			enemies[i].MoveEnemy ();
			
			//Wait for Enemy's moveTime before moving next Enemy, 
			yield return new WaitForSeconds(enemies[i].moveTime);
		}
		//Once Enemies are done moving, set playersTurn to true so player can move.
		playersTurn = true;
		
		//Enemies are done moving, set enemiesMoving to false.
		enemiesMoving = false;
	}*/
}

