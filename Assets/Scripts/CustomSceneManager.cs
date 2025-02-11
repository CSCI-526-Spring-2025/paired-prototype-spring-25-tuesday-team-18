using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class CustomSceneManager : MonoBehaviour
{
    // Static reference to the instance of our SceneManager
    public static CustomSceneManager instance;
    public GameObject uiPrefab;
    public int killLimit;
    public int totalKills;
    private static int GAME_SCREEN_INDEX = 1;
    private static int UPGRADE_SCREEN_INDEX = 2;
    private List<GameObject> nonDestoryObjects;
    public static bool IsInitialized { get; private set;}
    private GameObject gameOverUI;

    private void Awake()
    {
        // Check if instance already exists
        if (instance == null)
        {
            // If not, set instance to this
            instance = this;
        }
        else if (instance != this)
        {
            // If instance already exists and it's not this, then destroy this to enforce the singleton.
            Destroy(gameObject);
        }
        
        // Set this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        Debug.Log("starting");
        totalKills = 0;
        nonDestoryObjects = new List<GameObject>();
        gameOverUI = GameObject.FindGameObjectWithTag("GameOverUI");
        //gameOverUI = Instantiate(uiPrefab);
        if(gameOverUI == null) Debug.Log("no ui found");
        else Debug.Log(gameOverUI.name);
        gameOverUI.SetActive(false);
        IsInitialized = true;
    }

    private void Update()
    {
        // Check if the user is on a non-main scene and presses the Escape key
        if (SceneManager.GetActiveScene().buildIndex != 0 && Input.GetKeyDown(KeyCode.Escape))
        {
            // Load the main scene (assuming the main scene is at build index 0)
            LoadScene(0);
        }
    }

    public void AddKill()
    {
        totalKills++;
        if (totalKills >= killLimit) LoadScene(UPGRADE_SCREEN_INDEX);
    }

    public void ResetAndLoad(int killLimit)
    {
        totalKills = 0;
        this.killLimit = killLimit;
        LoadScene(GAME_SCREEN_INDEX);
    }

    public void GameOver(){
        Time.timeScale = 0; // Pause the game
        gameOverUI.SetActive(true); // Show the Game Over UI
    }

    public void Restart() {
        foreach(GameObject go in nonDestoryObjects){
            Destroy(go);
        }
        gameOverUI.SetActive(false);
        Time.timeScale = 1; // start the game again
        LoadScene(0);
    }

    public void AddNonDestoryObject(GameObject o) {
        nonDestoryObjects.Add(o);
    }

    // General method to load scenes based on build index
    private void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
