using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameGUIManager : Singleton<GameGUIManager>
{
    public GameObject homeGui;
    public GameObject gameGui;
    //
    public Text scoreCoungtingText;
    public Image powerBarSlider;

    public Dialog achievementDialog;
    public Dialog healpDialog;
    public Dialog gameoverDialog;

    public override void Awake()
    {
        MakeSingleton(false);
    }

    public void ShowGui(bool isShow)
    {
        if (gameGui)
        {
            gameGui.SetActive(isShow);
        }

        if (homeGui)
        {
            homeGui.SetActive(!isShow);
        }

    }

    public void UpdateScoreCountingText(int score)
    {
        if (scoreCoungtingText)
        {
            scoreCoungtingText.text = score.ToString();
        }
    }

    public void UpdatePowerBar(float curVal, float totalVal)
    {
        if (powerBarSlider)
        {
            powerBarSlider.fillAmount = curVal / totalVal;
        }

    }

    public void ShowAchievementDialog()
    {
        if (achievementDialog)
        {
            achievementDialog.Show(true);
        }
    }

    public void ShowHelpDialog()
    {
        if (healpDialog)
        {
            healpDialog.Show(true);
        }
    }

    public void ShowGameoverDialog()
    {
        if (gameoverDialog)
        {
            gameoverDialog.Show(true);
        }
    }


}
