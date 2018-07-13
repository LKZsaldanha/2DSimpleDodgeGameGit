using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    public int score = 0;
    public Text scoreText;

    private void Start()
    {
        UpdateInterface();
    }

    public void AddPoints(int _value)
    {
        score += _value;
        UpdateInterface();
    }

    public void RemovePoints(int _value)
    {
        score -= _value;
        UpdateInterface();
    }

    private void UpdateInterface()
    {
        scoreText.text = score.ToString();
    }
}
