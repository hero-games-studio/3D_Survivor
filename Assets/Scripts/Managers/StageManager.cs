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

    bool firstTap = true;
    bool resetTap = false;
    [SerializeField]
    GameObject boatObject, ringObject, cameraObject;

    #endregion
    #region Functions

    void Awake()
    {
        LevelControl();
        objectPoolManager = ObjectPoolManager.Instance;
        uiManager = UIManager.Instance;
    }

    void Start(){
        CreatePath();
        uiManager.SetInGamePanelActive(false);
        uiManager.SetTapToText("Tap to Start");
        uiManager.SetOverLevelText("");
    }
    void Update(){
        if(Input.GetMouseButtonDown(0) && firstTap){
            boatController.enabled = true;
            firstTap = false;
            uiManager.SetInGamePanelActive(true);
            uiManager.SetTapToText("");
        }
    }
    public void LevelUp()
    {
        currentLevel++;
        PlayerPrefs.SetInt("Level", currentLevel);

        RestartLevel();
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
        objectPoolManager.closeObjects();
        //Call Reset Function From Player
        boatObject.transform.position = Vector3.zero;
        ringObject.transform.position = Vector3.zero;
        cameraObject.transform.position = Vector3.zero;
        tileOffset = tileStep;
        Survivor.isFinished = true;
        CreatePath();
    }

    private void CreatePath()
    {
        //Create the Path with Object Pool Manager
        int tileCount = currentLevel * 5;
        for (int i = 0; i < tileCount; i++)
        {
            int randType = Random.Range(0,6);
            objectPoolManager.spawnObject(randType,tileOffset);
            tileOffset += tileStep;
        }
        objectPoolManager.SpawnEndObject(tileOffset);
        uiManager.SetFinishPosition(tileOffset.z);
        resetTap = false;
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

    #endregion

    #region Set Functions

    public void SetLevel(int level) { this.currentLevel = level; }

    #endregion


    #endregion

}
