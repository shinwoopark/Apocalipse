using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;

    public MapManager MapManager;
    public EnemySpawnManager EnemySpawnManager;
    public ItemManager ItemManager;

    public Canvas StageResultCanvas;
    public TMP_Text CurrentScoreText;
    public TMP_Text TimeText;

    protected PlayerCharacter _playerCharacter;
    public PlayerCharacter PlayerCharacter;

    public virtual void Init(PlayerCharacter playerCharacter)
    {
        _playerCharacter = playerCharacter;
    }

    [HideInInspector] public bool bStageCleared = false;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this.gameObject);
    }

    public void InitInstance()
    {
        if (GameInstance.instance == null)
        {
            return;
        }
        GameInstance.instance.GameStartTime = 0;
        GameInstance.instance.Score = 0;
        GameInstance.instance.CurrentStageLevel = 1;
        GameInstance.instance.CurrentPlayerWeaponLevel = 0;
        GameInstance.instance.CurrentPlayerHp = 3;
        GameInstance.instance.CurrentPlayerFuel = 100f;
        GameInstance.instance.CurrentAddOnLevel = 0;
    }

    void Start()
    {
        if (EnemySpawnManager == null) { return; }
        EnemySpawnManager.Init(this);

        MapManager.Init(this);
    }

    public void GameStart()
    {
        SceneManager.LoadScene("Stage1");
    }
    public void EnemDies()
    {
        AddScore(10);
    }

    public void StageClear()
    {
        AddScore(500);

        float gameStartTime = GameInstance.instance.GameStartTime;
        int score = GameInstance.instance.Score;

        int elapsedTime = Mathf.FloorToInt(Time.time - gameStartTime);

        StageResultCanvas.gameObject.SetActive(true);
        CurrentScoreText.text = "CurrentScore : " + score;
        TimeText.text = "ElapsedTime : " + elapsedTime;

        bStageCleared = true;

        StartCoroutine(LoadNextStageAfterDelay(5f));
    }


    IEnumerator LoadNextStageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        switch (GameInstance.instance.CurrentStageLevel)
        {
            case 1:
                break;
        }
    }

    public void AddScore(int score)
    {
        GameInstance.instance.Score =+ score;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {

        }
        if (Input.GetKeyDown(KeyCode.F2))
        {

        }
        if (Input.GetKeyDown(KeyCode.F3))
        {

        }
        if (Input.GetKeyDown(KeyCode.F4))
        {

        }
        if (Input.GetKeyDown(KeyCode.F5))
        {

        }
        if (Input.GetKeyDown(KeyCode.F6))
        {

        }
    }
}
