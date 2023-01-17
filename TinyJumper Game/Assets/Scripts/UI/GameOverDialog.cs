using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverDialog : Dialog
{
    public Text bestScoreText;
    bool m_replayBtnClicked;

    // khi dialog hien thi len thi lam gi
    private void OnEnable()
    {
        // scene da load xong thi se goi su kien OnSceneLoaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void Show(bool isShow)
    {
        base.Show(isShow);

        if (bestScoreText)
        {
            bestScoreText.text = Prefs.bestScore.ToString();
        }
    }
    // su dung bien is_gameStarted
    public void Replay()
    {
        m_replayBtnClicked = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BacktoHome()
    {
        GameGUIManager.Ins.ShowGui(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (m_replayBtnClicked)
        {
            GameGUIManager.Ins.ShowGui(true);
            GameManager.Ins.PlayGame();
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
