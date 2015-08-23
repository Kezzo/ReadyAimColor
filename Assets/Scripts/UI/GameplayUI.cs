using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour {

	[SerializeField]
	private List<GameObject> m_liveSprites = new List<GameObject>();

	[SerializeField]
	private GameObject m_shiftArrow;
	private MeshRenderer m_meshRendArrow;

	[SerializeField]
	private Material m_deactiveMaterial;

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

    private HighScoreController m_highScoreController;

	// Use this for initialization
	void Start () 
	{
		m_meshRendArrow = m_shiftArrow.GetComponent<MeshRenderer>();
        m_highScoreController = HighScoreController.Instance;
    }

	public void updateLiveUI(int currentlives)
	{
		if (m_liveSprites.ElementAt (currentlives) != null) {
			m_liveSprites.ElementAt(currentlives).SetActive(false);
		}
	}

	public void toggleColorSwitchUI(Material activeMaterial)
	{
		m_meshRendArrow.material = activeMaterial;
	}

	void OnApplicationPause(bool isPaused)
	{
		//print (pauseStatus);
		if (isPaused) {
			PauseGame();
		}
	}

	public void showGameOverMenu()
	{
        int currentHighScore = m_highScoreController.GetCurrentHighScore();
        PlayerPrefs.SetInt("LastHighScore", currentHighScore);

        int bestHighScore = PlayerPrefs.GetInt("BestHighScore");
        if(bestHighScore < currentHighScore)
        {
            PlayerPrefs.SetInt("BestHighScore", currentHighScore);
        }

        m_gameOverHighScoreText.text = currentHighScore.ToString();

        m_gameOverMenu.SetActive(true);
    }

	public void startNewGame()
	{
		Application.LoadLevel ("Gameplay1");
	}

	public void backToMenu()
	{
		Application.LoadLevel ("MainMenu");
	}

	public void PauseGame()
	{
		m_playerControls.pauseGame (true);
		m_pauseMenu.SetActive (true);
		m_pauseButton.SetActive (false);
	}

	public void UnPauseGame()
	{
		m_playerControls.pauseGame (false);
		m_pauseMenu.SetActive (false);
		m_pauseButton.SetActive (true);
	}
}
