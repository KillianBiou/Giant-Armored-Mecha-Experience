using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScoreManager : MonoBehaviour
{
    private ScoreData sd;

    void Awake()
    {
        LoadScoreFromJson();
    }

    public IEnumerable<Score> GetHighScores()
    {
        return sd.scores.OrderByDescending(x => x.score);
    }

    public void AddScore(string name, int score)
    {
        sd.scores.Add(new Score(name, score));
        ScoreUi.instance.ReloadScoreboard();
    }

    private void OnDestroy()
    {
        SaveScoreToJson();
    }

    public void SaveScoreToJson()
    {
        string LeaderboardData = JsonUtility.ToJson(sd);
        string filePath = Application.persistentDataPath + "/LeaderboardData.json";
        System.IO.File.WriteAllText(filePath, LeaderboardData);
    }

    public void LoadScoreFromJson()
    {
        string filePath = Application.persistentDataPath + "/LeaderboardData.json";
        string LeaderboardData = System.IO.File.ReadAllText(filePath);
        sd = JsonUtility.FromJson<ScoreData>(LeaderboardData);
        Debug.Log(Application.persistentDataPath + "/LeaderboardData.json");
    }
}
