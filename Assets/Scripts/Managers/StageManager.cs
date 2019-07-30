using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoSingleton<StageManager>
{

    #region Variables 

    [Header("Level")]
    [SerializeField] int currentLevel;

    ObjectPoolManager objectPoolManager;
    UIManager uiManager;
    Vector3 tileOffset = new Vector3(0,0,25);
    [SerializeField]
    Vector3 tileStep = new Vector3(0,0,25);
    [SerializeField]
    BoatController boatController;
    [SerializeField]
    SurvivarSaver survivarSaver;
    [SerializeField]
    WaterRePlacer waterRePlacer;
    [SerializeField]
    bool gameStarted = true;
    [SerializeField]
    bool gameFailed = false;
    [SerializeField]
    bool gameFinished = false;
    private GameObject[] savedSurvivors;


    [SerializeField]
    GameObject boatObject, hookObject, ropeObject, cameraObject, whaleObject;

    #endregion
    #region Functions

    void Awake()
    {
        LevelControl();
        objectPoolManager = ObjectPoolManager.Instance;
        uiManager = UIManager.Instance;
    }

    void Start()
    {
        SetGameStarted(true);
        SetGameFailed(false);
        SetGameFinished(false);
        boatController.SetKinematic(true);
        CreatePath();
        uiManager.SetInGamePanelActive(false);
        uiManager.SetTapToText("Tap to Start");
        uiManager.SetOverLevelText("");
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && gameStarted){
            boatController.enabled = true;
            boatController.SetKinematic(false);
            gameStarted = false;
            uiManager.SetInGamePanelActive(true);
            uiManager.SetTapToText("");
        }
    }
    public void LevelUp()
    {
        currentLevel++;
        PlayerPrefs.SetInt("Level", currentLevel);
    }

    public void LevelDown()
    {
        if (currentLevel == 1)
            return;

        currentLevel--;
        PlayerPrefs.SetInt("Level", currentLevel);

        RestartLevel();
    }

    public void RestartLevel()
    {
        if(gameStarted)
        {
            survivarSaver.ResetCount();
            boatController.enabled = true;
            boatController.SetKinematic(false);
            uiManager.SetInGamePanelActive(true);
            uiManager.SetTapToText("Tap to Start");
            gameStarted = false;
            Survivor.isFinished = false;
            Survivor.clickedToContinue = false;
            waterRePlacer.ResetPosition();
        }else if(gameFailed)
        {
            savedSurvivors = GameObject.FindGameObjectsWithTag("saved");
            foreach (GameObject saved in savedSurvivors)
            {
                saved.GetComponent<Survivor>().OnGameFinish();
            }
            survivarSaver.ResetCount();
            objectPoolManager.closeObjects();
            ResetPosition();
            tileOffset = tileStep;
            Survivor.isLost = true;
            uiManager.SetOverLevelText("");
            uiManager.SetTapToText("Tap to Restart");
            SetGameStarted(true);
            whaleObject.SetActive(false);
            boatController.RestartTimer();
            waterRePlacer.ResetPosition();
            CreatePath();
        }else if(gameFinished){
            
            savedSurvivors = GameObject.FindGameObjectsWithTag("saved");
            foreach (GameObject saved in savedSurvivors)
            {
                saved.GetComponent<Survivor>().OnGameFinish();
            }


            survivarSaver.ResetCount();
            LevelUp();
            objectPoolManager.closeObjects();
            ResetPosition();
            tileOffset = tileStep;
            Survivor.isFinished = true;
            uiManager.SetOverLevelText("");
            uiManager.SetTapToText("Tap to Start");
            SetGameStarted(true);
            whaleObject.SetActive(false);
            boatController.RestartTimer();
            waterRePlacer.ResetPosition();
            CreatePath();
        }
        
    }
    void ResetPosition(){
        boatController.SetGravity(false);
        boatController.SetKinematic(true);
        boatController.ResetConstraints();
        boatObject.transform.position = Vector3.zero;
        boatObject.transform.forward = Vector3.zero;
        hookObject.transform.position = Vector3.zero;
        hookObject.transform.eulerAngles = Vector3.zero;
        ropeObject.SetActive(false);
        ropeObject.transform.position = Vector3.zero;
        ropeObject.SetActive(true);
        cameraObject.transform.position = Vector3.zero;
        hookObject.transform.SetParent(boatObject.transform);
    }
 

    private void CreatePath()
    {
        int tileCount = currentLevel * 5;
        for (int i = 0; i < tileCount; i++)
        {
            int randType = Random.Range(0,6);
            objectPoolManager.spawnObject(randType,tileOffset);
            tileOffset += tileStep;
        }
        objectPoolManager.SpawnEndObject(tileOffset);
        uiManager.SetFinishPosition(tileOffset.z);
        gameFailed = false;
    }

    private void LevelControl()
    {
        if (PlayerPrefs.GetInt("Level") == 0)
        {
            PlayerPrefs.SetInt("Level", 1);
        }

        currentLevel = PlayerPrefs.GetInt("Level");

    }

    #region Get Functions

    public int GetLevel() { return currentLevel; }
    public bool GetGameStarted(){ return gameStarted; }
    public bool GetGameFailed(){ return gameFailed; }
    public bool GetGameFinished(){ return gameFinished; }

    #endregion

    #region Set Functions

    public void SetLevel(int level) { this.currentLevel = level; }

    public void SetGameStarted(bool gameStarted){
        this.gameStarted = gameStarted;
    }
    
    public void SetGameFailed(bool gameFailed){
        this.gameFailed = gameFailed;
    }
    public void SetGameFinished(bool gameFinished){
        this.gameFinished = gameFinished;
    }
    public void SetClickToContinue(bool click){
        Survivor.clickedToContinue = click;
    }

    #endregion


    #endregion

}
