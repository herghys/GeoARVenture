using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreScript : MonoBehaviour
{
    [SerializeField]
    protected List<Text> textScore;

    private List<int> listScore;

    // Start is called before the first frame update
    void Start()
    {
        listScore = new List<int>(5);
        GetHighScore();
    }

    void GetHighScore()
    {
        string score = "Highscore";
        for (int i = 0; i < textScore.Count; i++)
        {
            listScore.Add(0);
            Debug.Log("INdek"+i);
            score = "Highscore" + i;
            listScore[i] = PlayerPrefs.GetInt(score, 0);

            textScore[i].text = "" + listScore[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
