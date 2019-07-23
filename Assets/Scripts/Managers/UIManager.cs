using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoSingleton<UIManager>
{
    #region  Variables

    [Header("Panels")]
    [SerializeField] GameObject InGamePanel;
    [SerializeField] GameObject EndGamePanel;
    [Header("Texts")]
    [SerializeField] TextMeshProUGUI OverLevelText;
    [SerializeField] TextMeshProUGUI TapToText;
    [Header("Progress Bar")]
    [SerializeField] TextMeshProUGUI CurrentLevelText;
    [SerializeField] TextMeshProUGUI NextLevelText;
    [SerializeField] Image Bar;
    [SerializeField] [Range(15, 500)] float currentPosition = 15;
    [SerializeField] float startPosition = 15;
    [SerializeField] float finishPosition = 150;

    StageManager stageManager;

    #endregion


    #region  Functions

    void Awake()
    {
        stageManager = StageManager.Instance;
    }

    void Start()
    {
        StartCoroutine("SetProgressBar");
    }

    public void SetOverLevelText(string text)
    {
        OverLevelText.text = text;
    }

    public void SetTapToText(string text)
    {
        TapToText.text = text;
    }

    public void SetInGamePanelActive(bool value)
    {
        InGamePanel.SetActive(value);
        EndGamePanel.SetActive(!value);
    }

    public void SetCurrentPosition(float boatPosZ){
        currentPosition = boatPosZ;
    }

    public void SetFinishPosition(float finishPos){
        finishPosition = finishPos;
    }

    public IEnumerator SetProgressBar()
    {
        currentPosition = startPosition;

        Vector3 pos = Vector3.one;

        CurrentLevelText.text = stageManager.GetLevel().ToString();
        NextLevelText.text = (stageManager.GetLevel() + 1).ToString();

        while (true)
        {
            pos.x = Remapper.Remap(currentPosition, startPosition, finishPosition, 0, 1);
            Bar.transform.localScale = pos;

            yield return new WaitForFixedUpdate();
        }
    }

    #endregion
}

public static class Remapper
{
    public static float Remap(this float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        var fromAbs = value - fromMin;
        var fromMaxAbs = fromMax - fromMin;

        var normal = fromAbs / fromMaxAbs;

        var toMaxAbs = toMax - toMin;
        var toAbs = toMaxAbs * normal;

        var to = toAbs + toMin;

        return to;
    }
}