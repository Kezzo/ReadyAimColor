using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Singleton to handle highscores in the Game.
/// </summary>
public class HighScoreController : MonoBehaviour {

    [SerializeField]
    private Text m_gameplayHighScoreText;

    private int m_gameplayHighScore = 0;
    public int GamePlayHighScore { get { return m_gameplayHighScore; } }

    public static HighScoreController Instance { get; private set; }

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Call to change the HighScore
    /// </summary>
    /// <param name="highScoreToAdd">The amount the highscore should be increased.</param>
    public void UpdateHighScoreBy(int highScoreToAdd)
    {
        m_gameplayHighScore += highScoreToAdd;
        m_gameplayHighScoreText.text = m_gameplayHighScore.ToString();
    }
}
