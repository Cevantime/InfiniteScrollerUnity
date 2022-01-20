using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreValueScript : MonoBehaviour
{
    private TextMeshProUGUI scoreValueText;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().onScoreChanged += OnScoreChanged;
        scoreValueText = GetComponent<TextMeshProUGUI>();
    }

    void OnScoreChanged(int score)
    {
        scoreValueText.text = score.ToString();
    }
}
