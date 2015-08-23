using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighScoreController : MonoBehaviour {

    [SerializeField]
    Text m_gameplayHighScoreText;

    private int m_gameplayHighScore = 0;

    /// <summary>
    /// Call to increase the HighScore
    /// </summary>
    /// <param name="highScoreToAdd">The amount the highscore should be increased.</param>
    public void UpdateHighScoreBy(int highScoreToAdd)
    {
        m_gameplayHighScore += highScoreToAdd;
        m_gameplayHighScoreText.text = m_gameplayHighScore.ToString();
    }
}
