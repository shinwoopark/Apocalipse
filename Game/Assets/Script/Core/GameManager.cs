using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;

    public MapManager MapManager;
    public EnemySpawnManager EnemySpawnManager;
    public ItemManager ItemManager;
    public SoundManager SoundManager;

    public Canvas StageResultCanvas;
    public GameObject StageResultCanvas_gb;
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
        {
            //Destroy(this.gameObject);
        }
            
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

    private void Update()
    {
        MouseSound();
    }

    public void GameStart()
    {
        SceneManager.LoadScene("Stage1");
        InitInstance();
    }

    public void MouseSound()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SoundManager.PlaySFX(0);
        }
    }
    public void EnemDies()
    {
        SoundManager.PlaySFX(10);
        AddScore(10);
    }

    public void StageClear()
    {
        SoundManager.StopBGM();
        SoundManager.PlaySFX(9);

        AddScore(500);

        float gameStartTime = GameInstance.instance.GameStartTime;
        int score = GameInstance.instance.Score;

        int elapsedTime = Mathf.FloorToInt(Time.time - gameStartTime);

        StageResultCanvas_gb.SetActive(true);
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
                SceneManager.LoadScene("Stage2");
                GameInstance.instance.CurrentStageLevel = 2;
                break;
            case 2:
                SceneManager.LoadScene("Stage3");
                GameInstance.instance.CurrentStageLevel = 3;
                break;
            case 3:
                SceneManager.LoadScene("Result");
                break;
        }
    }

    public void AddScore(int score)
    {
        GameInstance.instance.Score += score;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
