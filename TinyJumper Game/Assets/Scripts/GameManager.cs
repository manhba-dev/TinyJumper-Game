using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player playerPrefabs;
    public Platform platformPrefabs;
    public float minSpawnX;
    public float minSpawnY;
    public float maxSpawnX;
    public float maxSpawnY;
    Player m_player;
    int m_Score;
    public CamController mainCam;
    public float powerBarUp;
    bool m_isGameStarted;

    public bool IsGameStarted { get => m_isGameStarted;}

    public override void Awake()
    {
        MakeSingleton(false);
    }

    public override void Start()
    {
        base.Start();

        GameGUIManager.Ins.UpdateScoreCountingText(m_Score);

        GameGUIManager.Ins.UpdatePowerBar(0, 1);
        // ko tat se bi chen hinh nen 
        //GameGUIManager.Ins.ShowGui(false);

        AudioController.Ins.PlayBackgroundMusic();
    }

    public void PlayGame()
    {
        StartCoroutine(PlatformInit());

        GameGUIManager.Ins.ShowGui(true);


    }

    // khởi tạo platform
    IEnumerator PlatformInit()
    {
        Platform platformClone = null;
        if(platformPrefabs != null)
        {
            // x ~ 0 ở giữa màn hình
            platformClone = Instantiate(platformPrefabs,new Vector2(0,Random.Range(minSpawnY,maxSpawnY)),Quaternion.identity);
            platformClone.id = platformClone.gameObject.GetInstanceID();
        }

        yield return new WaitForSeconds(0.5f);

        if (playerPrefabs)
        {
            m_player = Instantiate(playerPrefabs, Vector3.zero, Quaternion.identity);
            m_player.lastFlatformId = platformClone.id;
        }

        if (platformPrefabs)
        {
            float spawnX = m_player.transform.position.x + minSpawnX;

            float spawnY = Random.Range(minSpawnY,maxSpawnY);

            Platform platformClone2 = Instantiate(platformPrefabs,new Vector2(spawnX,spawnY),Quaternion.identity);
            platformClone2.id = platformClone2.gameObject.GetInstanceID();
        }

        yield return new WaitForSeconds(0.5f);

        m_isGameStarted = true;
    }

    // method tổng quát tạo platform
    public void CreatePlatform()
    {
        if(!platformPrefabs && !m_player)
        {
            return;
        }
        float spawnX = Random.Range(m_player.transform.position.x + minSpawnX, m_player.transform.position.x + maxSpawnX);

        float spawnY = Random.Range(minSpawnY, maxSpawnY);

        Platform platformClone = Instantiate(platformPrefabs, new Vector2(spawnX,spawnY), Quaternion.identity);

        platformClone.id = platformClone.gameObject.GetInstanceID();
    }

    // tạo cam và di chuyển
    public void CreatePlatformAndLerp(float playerXPos)
    {
        if(mainCam != null)
        {
            mainCam.LerpTrigger(playerXPos + minSpawnX);

        }

        CreatePlatform();
    }

    public void AddScore()
    {
        m_Score++;

        Prefs.bestScore = m_Score;
        GameGUIManager.Ins.UpdateScoreCountingText(m_Score);
        AudioController.Ins.PlaySound(AudioController.Ins.getScore);
    }
}
