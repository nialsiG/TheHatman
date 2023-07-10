using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GameState : MonoBehaviour
{
    //Note: attach to GameManager
    //Currently this script (1) Load the stage (2) Instantiate the player (3) Manage changes in the state of the game
    //To do: separate in different scripts!
    
    //Game objects that need to be dragged down in the Inspector
    [SerializeField] private GameObject[] tileMaps;
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private GameObject[] pnjs;
    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI timerDebugTMPRO;
    [SerializeField] private TextMeshProUGUI scaredPeopleTMPRO;
    [SerializeField] private Image fadeScreen;  //The black screen for transitions

    //Properties
    public float initialTime { get; set; }
    private float nPeople { get; set; }
    private int scaredPeople { get; set; }
    private float timer { get; set; }

    //Singleton
    public static GameState Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;

        //Procedural generation
        //...tilemap
        int map = Random.Range(0, tileMaps.Length);
        Instantiate(tileMaps[map]);

        //...PNJs
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        for (int i = spawnPoints.Length - 1; i >= 0; i--)
        {
            int index = Random.Range(0, pnjs.Length + 1);
            if (index < pnjs.Length)
            {
                Instantiate(pnjs[index], spawnPoints[i].transform);
                nPeople += 1;
            }
            spawnPoints[i].GetComponent<SpriteRenderer>().color = Color.clear;  //Make spawnpoint transparent (note: if removed PNJs don't spawn)
        }

        //Instantiate player
        GameObject startPoint = GameObject.FindGameObjectWithTag("StartPoint");
        Instantiate(player, startPoint.transform);
        startPoint.GetComponent<SpriteRenderer>().color = Color.clear;  //Make startpoint transparent (note: if removed player don't spawn)

        //Initial time
        initialTime = 90f;
    }

    //Methods
    //...for getting initial time
    public float GetInitialTime()
    {
        return (initialTime);
    }

    //...for getting current time
    public float GetTime()
    {
        return (timer);
    }

    //...for getting n of scared people
    public int GetScaredPeople()
    {
        return (scaredPeople);
    }

    //...for updating n of scared people
    public void AddScaredPeople()
    {
        scaredPeople += 1;
        scaredPeopleTMPRO.SetText(scaredPeople.ToString());
        if (scaredPeople >= nPeople) StartCoroutine(EndOfGame(true));
    }

    //Coroutines
    //...for transition
    IEnumerator Transition()
    {
        Color c = fadeScreen.color;
        for (float alpha = 1f; alpha >= 0; alpha -= 0.01f)
        {
            c.a = alpha;
            fadeScreen.color = c;
            yield return null;
        }
    }

    //...for loading the Victory/GameOver scene (with transition)
    IEnumerator EndOfGame(bool isVictory)
    {
        Color c = fadeScreen.color;
        for (float alpha = 0f; alpha <= 1; alpha += 0.01f)
        {
            c.a = alpha;
            fadeScreen.color = c;
            if(fadeScreen.color.a >= 0.99)
            {
                ChangeScene changeScene = gameObject.GetComponentInParent<ChangeScene>();   //Make sure that a ChangeScene object is attached to GameManager
                if (isVictory)
                {
                    changeScene.LoadVictory();
                }
                else if (!isVictory)
                {
                    changeScene.LoadGameOver();
                }
            }
            yield return null;
        }
    }

    private void Start()
    {
        //Initial settings
        timer = initialTime;
        scaredPeople = 0;
        scaredPeopleTMPRO.SetText(scaredPeople.ToString());

        //Once everything is ready, start transition
        StartCoroutine(Transition());
    }

    private void Update()
    {        
        //Update the timer
        timer -= (1 * Time.deltaTime);
        timerDebugTMPRO.SetText(timer.ToString());
        
        /*
        //Debug for navigating between scenes, REMOVE BEFORE BUILDING
        if (Input.GetKeyDown(KeyCode.G))
        {
            gameObject.GetComponentInChildren<ChangeScene>().LoadGameOver();
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            gameObject.GetComponentInChildren<ChangeScene>().LoadVictory();
        }
        */
    }

    private void LateUpdate()
    {
        //Trigger game over at end of timer (but priority to number of scared people, so moved to LateUpdate)
        if (timer <= 0 && scaredPeople < nPeople)
        {
            StartCoroutine(EndOfGame(false));
        }
    }

}
