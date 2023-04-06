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

    public void AddScore(Score score)
    {
        sd.scores.Add(score);
        SaveScoreToJson();
    }

    private void OnDestroy()
    {
        SaveScoreToJson();
    }

    public void SaveScoreToJson()
    {
        string LeaderboardData = JsonUtility.ToJson(sd);
        string filePath = Application.persistentDataPath + "/LeaderboardData.json";
        Debug.Log(filePath);
        System.IO.File.WriteAllText(filePath, LeaderboardData);
        Debug.Log("Sauvegarde effectué");
    }

    public void LoadScoreFromJson()
    {
        string filePath = Application.persistentDataPath + "/LeaderboardData.json";
        string LeaderboardData = System.IO.File.ReadAllText(filePath);
        sd = JsonUtility.FromJson<ScoreData>(LeaderboardData);
        Debug.Log("Chargement effectué");
    }
}
