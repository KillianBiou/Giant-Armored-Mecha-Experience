using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;


public class ScoreUi : MonoBehaviour
{
    //public RowUi rowUi;
    public List<TextMeshProUGUI> Names;
    public List<TextMeshProUGUI> Scores;
    public ScoreManager scoreManager;

    private void Start()
    {
       /* if (Input.GetKeyDown(KeyCode.A))
        {
            scoreManager.AddScore(new Score("Titouan", 2500));
        }*/

        var scores = scoreManager.GetHighScores().ToArray(); 
        for(int i = 0; i < scores.Length; i++)
        {
            Names[i].text = scores[i].name;
            Scores[i].text = scores[i].score.ToString();
        }
    }
}
