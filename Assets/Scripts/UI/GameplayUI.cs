using System.Collections;
using Assets.Scripts.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    /// <summary>
    /// Class to handle data loading and display of relevant information in the UI.
    /// </summary>
    public class GameplayUI : MonoBehaviour {

        [SerializeField]
        private GameObject[] m_liveSprites;

        [SerializeField]
        private Sprite[] m_colorSwitchSprites;

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

        [SerializeField]
        private Image m_colorToggleButton;

        [SerializeField]
        private GameObject m_highScoreText;

        private bool m_playButtonOnCD;

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

        #region public methods

        /// <summary>
        /// Called to update the energy sprites to fit to the current health left
        /// </summary>
        /// <param name="currentlives"></param>
        public void UpdateHealthUi(int currentlives)
        {
            if (currentlives < 0)
                return;

            if (m_liveSprites[currentlives] != null)
                m_liveSprites[currentlives].SetActive(false);
        }

        /// <summary>
        /// Called to toggle the color switch ui from one color to the other.
        /// </summary>
        public void ToggleColorSwitchUi(ColorState colorState)
        {
            m_colorToggleButton.sprite = colorState == ColorState.Green ? m_colorSwitchSprites[0] : m_colorSwitchSprites[1];
        }

        /// <summary>
        /// Saves the current highscore and displays the game over menu.
        /// </summary>
        public void ShowGameOverMenu()
        {
            m_playButtonOnCD = true;
            StartCoroutine(CountPlayerButtonCdDown(1.0f));

            int currentHighScore = HighScoreController.Instance.GamePlayHighScore;
            PlayerPrefs.SetInt("LastHighScore", currentHighScore);

            int bestHighScore = PlayerPrefs.GetInt("BestHighScore");
            if(bestHighScore < currentHighScore)
            {
                PlayerPrefs.SetInt("BestHighScore", currentHighScore);
            }

            m_gameOverHighScoreText.text = currentHighScore.ToString();

            m_gameOverMenu.SetActive(true);

            m_colorToggleButton.gameObject.SetActive(false);
            m_highScoreText.SetActive(false);
            m_pauseButton.SetActive(false);
        }

        /// <summary>
        /// Starts a new game
        /// </summary>
        public void StartNewGame()
        {
            if(!m_playButtonOnCD)
            {
                Application.LoadLevel("Gameplay1");
            }
        }

        /// <summary>
        /// Loads the main menu scene
        /// </summary>
        public void BackToMenu()
        {
            Application.LoadLevel ("MainMenu");
        }

        /// <summary>
        /// Pauses the game and show the pause menu.
        /// </summary>
        public void PauseGame()
        {
            m_playerControls.PauseGame (true);
            m_pauseMenu.SetActive (true);
            m_pauseButton.SetActive (false);
        }

        /// <summary>
        /// Unpauses the game and hides the pause menu.
        /// </summary>
        public void UnPauseGame()
        {
            m_playerControls.PauseGame (false);
            m_pauseMenu.SetActive (false);
            m_pauseButton.SetActive (true);
        }

        #endregion

        /// <summary>
        /// Counts down the cooldown of the play button when the game over screen is shown.
        /// </summary>
        /// <param name="cooldownInSec">The time in seconds after which the the play button should made interactable.</param>
        /// <returns></returns>
        private IEnumerator CountPlayerButtonCdDown(float cooldownInSec)
        {
            yield return new WaitForSeconds(cooldownInSec);
            m_playButtonOnCD = false;
        }
    }
}
