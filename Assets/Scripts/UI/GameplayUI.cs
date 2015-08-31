using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour {

	[SerializeField]
	private GameObject[] m_liveSprites;

	[SerializeField]
	private PlayerControls m_playerControls;

	[SerializeField]
	private GameObject m_pauseMenu;

	[SerializeField]
	private GameObject m_pauseButton;

	[SerializeField]
	private GameObject m_gameOverMenu;

    [SerializeField]
    private Text m_gameOverHighScoreText;

    private float m_startButtonCD = 0.0f;
    private bool m_decreaseStartCD;

    void Update()
    {
        if(m_decreaseStartCD)
        {
            m_startButtonCD -= Time.deltaTime;
            if(m_startButtonCD < 0)
            {
                m_decreaseStartCD = false;
            }
        }
    }

    /// <summary>
    /// Called to update the energy sprites to fit to the current health left
    /// </summary>
    /// <param name="currentlives"></param>
	public void updateHealthUI(int currentlives)
	{
        if (currentlives < 0)
            return;

		if (m_liveSprites[currentlives] != null) 
			m_liveSprites[currentlives].SetActive(false);
	}

    /// <summary>
    /// Called to toggle the color switch ui from one color to the other.
    /// </summary>
    /// <param name="activeMaterial"></param>
	public void toggleColorSwitchUI(Material activeMaterial)
	{
		//m_meshRendArrow.sharedMaterial = activeMaterial;
	}

    /// <summary>
    /// Called on android when the user closed the app to the background.
    /// </summary>
    /// <param name="isPaused"></param>
	void OnApplicationPause(bool isPaused)
	{
		if (isPaused) {
			PauseGame();
		}
	}

    /// <summary>
    /// Saves the current highscore and displays the game over menu.
    /// </summary>
	public void showGameOverMenu()
	{
        m_startButtonCD = 1.0f;
        m_decreaseStartCD = true;
        
        int currentHighScore = HighScoreController.Instance.GetCurrentHighScore();
        PlayerPrefs.SetInt("LastHighScore", currentHighScore);

        int bestHighScore = PlayerPrefs.GetInt("BestHighScore");
        if(bestHighScore < currentHighScore)
        {
            PlayerPrefs.SetInt("BestHighScore", currentHighScore);
        }

        m_gameOverHighScoreText.text = currentHighScore.ToString();

        m_gameOverMenu.SetActive(true);
    }

    /// <summary>
    /// Starts a new game
    /// </summary>
	public void startNewGame()
	{
        if(m_startButtonCD < 0.0f)
        {
            Application.LoadLevel("Gameplay1");
        }
	}

    /// <summary>
    /// Loads the main menu scene
    /// </summary>
	public void backToMenu()
	{
		Application.LoadLevel ("MainMenu");
	}

    /// <summary>
    /// Pauses the game and show the pause menu.
    /// </summary>
	public void PauseGame()
	{
		m_playerControls.pauseGame (true);
		m_pauseMenu.SetActive (true);
		m_pauseButton.SetActive (false);
	}

    /// <summary>
    /// Unpauses the game and hides the pause menu.
    /// </summary>
	public void UnPauseGame()
	{
		m_playerControls.pauseGame (false);
		m_pauseMenu.SetActive (false);
		m_pauseButton.SetActive (true);
	}
}
