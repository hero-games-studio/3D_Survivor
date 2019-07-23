using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorManager 
{
    #if UNITY_EDITOR
   

    [MenuItem("Survivor 3D/Level Up")]
    public static void LevelUp()
    {
        int currentLevel = PlayerPrefs.GetInt("Level");
        if (currentLevel == 0)
        {
            currentLevel = 2;
        }

        PlayerPrefs.SetInt("Level", currentLevel);
    }

    [MenuItem("Survivor 3D/Level Down")]
    public static void LevelDown()
    {
        int currentLevel = PlayerPrefs.GetInt("Level");
        if (currentLevel == 1)
        {
            currentLevel = 1;
        }

        PlayerPrefs.SetInt("Level", currentLevel);
    }

    [MenuItem("Survivor 3D/Reset Level")]
    public static void ResetLevel()
    {
        PlayerPrefs.SetInt("Level", 1);
    }


#endif

}
