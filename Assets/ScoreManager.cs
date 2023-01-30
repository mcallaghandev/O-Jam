using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI scoreText;

    public void SetScoreText(double score)
    {
        scoreText.text = $"{score}%";
    }

    public void SetDebugText(string text)
    {
        scoreText.text = text;
    }

    public void ClearScoreText()
    {
        scoreText.text = string.Empty;
    }
}
