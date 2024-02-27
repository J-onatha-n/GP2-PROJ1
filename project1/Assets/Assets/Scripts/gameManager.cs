using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    [Header("Global vars")]
    public GameObject myPlayer;
    public float timer;
    public float timeLimit;
    public int score;
    public playerController PC;

    [Header("NPC vars")]
    public GameObject projectile;
    public GameObject collectible;
    public float projectileTimer;
    public float projectileInterval;
    public float collectibleTimer;
    public float collectibleInterval;   
    public Vector2 spawnXBounds1;
    public Vector2 spawnYBounds1;
    public Vector2 spawnXBounds2;
    public Vector2 spawnYBounds2;
    [Header("UI/UX Vars")]
    public TextMeshProUGUI TitleText;
    public TextMeshProUGUI TimerText;
    public TextMeshProUGUI CollectedText; 

    //to design a state machine, first we need to define a subclass of enum - GameState
    public enum GameState
    {
        GAMESTART, PLAYING, GAMEOVER, LOSTSCREEN //we can pass it as many states as we want here
    };
    //declare an actual instance of GameState for the gameManager to use
    public GameState myGameState;

    // Start is called before the first frame update
    void Start()
    {
        myGameState = GameState.GAMESTART;
        myPlayer.SetActive(false);
        collectible.SetActive(false);
        TitleText.text = "Use [A] [S] to move. Press [SPACE] to start";
        
        

    }

    // Update is called once per frame
    void Update()
    {
        //switch statements work kind of like a lightswitch with 2 or more positions
        switch (myGameState)
        {
            //each case can be seen as a single unique position of the lightswitch
            case GameState.GAMESTART:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    EnterPlaying();
                }
                break;

            //this code only executes when our myGameState enum is currently in the matching state
            //to the case in question
            case GameState.PLAYING:
                //timer is global, spawnTimer tracks collectibles
                #region PLAYING_code
                timer -= Time.deltaTime;
                collectibleTimer += Time.deltaTime;
                projectileTimer += Time.deltaTime;
                TimerText.text = timer.ToString("F2");
                CollectedText.text = "Collected: " + PC.collected.ToString(); 
                
                if (timer < 15)
                {
                    projectileInterval = .30f; 
                }
                if (timer < 10)
                {
                    projectileInterval = .20f;
                }
                if (timer < 5)
                {
                    projectileInterval = .10f;
                }

                //check against timeLimit, end the game if we're at time
                if (timer < timeLimit)
                {
                    EnterFinale();
                }

                //this is the world position where our collectible spawns
                float x1 = Random.Range(spawnXBounds1.x, spawnXBounds1.y);
                float y1 = Random.Range(spawnYBounds1.x, spawnYBounds1.y);
                float x2 = Random.Range(spawnXBounds2.x, spawnXBounds2.y);
                float y2 = Random.Range(spawnYBounds2.x, spawnYBounds2.y);
                Vector3 targetPos1 = new Vector3(x1, y1, 0);
                Vector3 targetPos2 = new Vector3(x2, y2, 0); 
                

                //instantiate and reset timer when condition is met
                if (collectibleTimer > collectibleInterval)
                {
                    Instantiate(collectible, targetPos1, Quaternion.identity);
                    collectibleTimer = 0;
                }
                if (projectileTimer > projectileInterval)
                {
                    Instantiate(projectile, targetPos2, Quaternion.identity);
                    projectileTimer = 0;
                }
                if (PC.isAlive == false) {
                    LostScreen();
                }
                // destroys excess objects 
                /*GameObject[] enemyObj = GameObject.FindGameObjectsWithTag("collectible");

                for (int i = 0; 20  < enemyObj.Length; i++)
                {
                    Destroy(enemyObj[i]);
                }
                */
                #endregion
                break;
            
            case GameState.LOSTSCREEN:
                //code for losing the game
                Destroy(GameObject.FindWithTag("collectible"));
                Destroy(GameObject.FindWithTag("projectile"));
                collectible.SetActive(false);
                projectile.SetActive(false);
                if (Input.GetKey(KeyCode.Space))
                {
                    EnterStart();
                }
                break;

            case GameState.GAMEOVER:
                //code for the ending goes here
                if (Input.GetKey(KeyCode.Space))
                {
                    EnterStart();
                }
                break;

        }
    } 

    //state change for playing mode, turn on players, disable any start menu logic
    void EnterStart()
    {
        myGameState = GameState.GAMESTART;
        collectible.SetActive(false);
        projectile.SetActive(false);
        myPlayer.SetActive(false);
        TitleText.text = "Use [A] [S] to move. Press [SPACE] to start";
    }
    void EnterPlaying()
    {
        myGameState = GameState.PLAYING;
        myPlayer.transform.position = new Vector3(0f, -3.5f, 0f);

        timer = 20f;
        projectileInterval = .5f;

        PC.collected = 0; 
        PC.isAlive = true;

        
        collectible.SetActive(true);
        projectile.SetActive(true);
        myPlayer.SetActive(true);


        TitleText.enabled = false;
        TimerText.enabled = true;
        CollectedText.enabled = true;

       
    }

    void LostScreen()
    {
        TitleText.enabled = true;
        TimerText.enabled = false;
        CollectedText.enabled = false;
        TitleText.text = "YOU LOST. Press [SPACE] to try again.";
        myPlayer.SetActive(false);
        myGameState = GameState.LOSTSCREEN;
        
         

    }

    void EnterFinale()
    {
        myPlayer.SetActive(false);
        TitleText.enabled = true;
        TimerText.enabled = false;
        CollectedText.enabled = false;
        myGameState = GameState.GAMEOVER;
        TitleText.text = "CONGRATS, You Survived. Press [SPACE] to restart" + "<br>Items Collected: " + PC.collected;
        
        
    }
}
