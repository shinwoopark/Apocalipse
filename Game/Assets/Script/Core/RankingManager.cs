using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    public Canvas RankingCanvas;
    public Canvas SetRankCanvas;

    private List<RankingEntry> rankingEntries = new List<RankingEntry>();
    public TextMeshProUGUI[] Rankings = new TextMeshProUGUI[5];
    public TextMeshProUGUI CurrentPlayerScore;
    public TextMeshProUGUI InitialInputFieldText;

    private string CurrentPlayerInitial;

    public void SetInitial()
    {
        SetRankCanvas.gameObject.SetActive(false);
        RankingCanvas.gameObject.SetActive(true);

        CurrentPlayerInitial = InitialInputFieldText.text;

        SetCurrentScore();
        SortRanking();
        UpdateRankingUI();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void MainMenuRanking()
    {
        RankingCanvas.gameObject.SetActive(true);

        for (int i = 0; i < 5; i++)
        {
            int currentScore = PlayerPrefs.GetInt(i + "BestScore");
            string currentName = PlayerPrefs.GetString(i + "BestName");
            if (currentName == "")
                currentName = "None";

            rankingEntries.Add(new RankingEntry(currentScore, currentName));
        }

        SortRanking();

        for (int i = 0; i < Rankings.Length; i++)
        {
            if (i < rankingEntries.Count)
            {
                Rankings[i].text = $"{i + 1} {rankingEntries[i].Name} : {rankingEntries[i].Score}";
            }
            else
            {
                Rankings[i].text = $"{i + 1} -";
            }
        }
    }

    void SetCurrentScore()
    {
        rankingEntries.Clear();

        for (int i = 0; i < 5; i++)
        {
            int currentScore = PlayerPrefs.GetInt(i + "BestScore");
            string currentName = PlayerPrefs.GetString(i + "BestName");
            if (currentName == "")
                currentName = "None";

            rankingEntries.Add(new RankingEntry(currentScore, currentName));
        }

        // 현재 플레이어의 점수와 이름을 가져와 랭킹에 등록
        int currentPlayerScore = GameInstance.instance.Score;
        string currentPlayerName = CurrentPlayerInitial;

        // 현재 플레이어의 점수가 랭킹에 등록 가능한지 확인
        if (IsScoreEligibleForRanking(currentPlayerScore))
        {
            rankingEntries.Add(new RankingEntry(currentPlayerScore, currentPlayerName));
        }
    }

    bool IsScoreEligibleForRanking(int currentPlayerScore)
    {
        // 랭킹에 등록 가능한지 확인 (예: 상위 5위까지만 등록 가능하도록 설정)
        return rankingEntries.Count < 5 || currentPlayerScore > rankingEntries.Min(entry => entry.Score);
    }

    void SortRanking()
    {
        // 내림차순으로 정렬
        rankingEntries = rankingEntries.OrderByDescending(entry => entry.Score).ToList();

        // 랭킹이 5개를 초과하면 가장 낮은 점수를 가진 항목을 제거
        if (rankingEntries.Count > 5)
        {
            rankingEntries.RemoveAt(rankingEntries.Count - 1);
        }
    }

    void UpdateRankingUI()
    {
        CurrentPlayerScore.text = $"{CurrentPlayerInitial} {GameInstance.instance.Score}";

        for (int i = 0; i < Rankings.Length; i++)
        {
            if (i < rankingEntries.Count)
            {
                Rankings[i].text = $"{i + 1} {rankingEntries[i].Name} : {rankingEntries[i].Score}";
            }
            else
            {
                Rankings[i].text = $"{i + 1} -";
            }
        }

        // PlayerPrefs 업데이트
        for (int i = 0; i < rankingEntries.Count; i++)
        {
            PlayerPrefs.SetInt(i + "BestScore", rankingEntries[i].Score);
            PlayerPrefs.SetString(i + "BestName", rankingEntries[i].Name);
        }
    }
}
public class RankingEntry
{
    public int Score { get; set; }
    public string Name { get; set; }

    public RankingEntry(int score, string name)
    {
        Score = score;
        Name = name;
    }
}
