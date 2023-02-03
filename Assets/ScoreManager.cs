using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI scoreText;

    [SerializeField]
    TextMeshProUGUI lineInfoText;

    public void SetScoreText(double score)
    {
        scoreText.text = $"{score}%";
    }

    public void SetLineInfoText(string text)
    {
        lineInfoText.text = text;
    }

    public string GetLineInfoText()
    {
        return lineInfoText.text;
    }

    public void ClearScoreText()
    {
        scoreText.text = string.Empty;
    }

    public void ClearLineInfoText()
    {
        lineInfoText.text = string.Empty;
    }
}
