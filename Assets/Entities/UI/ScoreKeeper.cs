using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{

    private static int _score = 0;
    private static Text _scoreTextBox;

    void Awake()
    {
        _scoreTextBox = gameObject.GetComponent<Text>();
        Reset();
    }

    void Update()
    {
        _scoreTextBox.text = _score.ToString();
    }

    public static int GetScore()
    {
        return _score;
    }

    public void UpdateScore(int newScore)
    {
        _score += newScore;
    }

    private static void Reset()
    {
        _score = 0;
    }
}
