using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameplayUI : MonoBehaviour {

	[SerializeField]
	private List<GameObject> liveSprites = new List<GameObject>();

	[SerializeField]
	private GameObject shiftArrow;
	private MeshRenderer meshRendArrow;

	[SerializeField]
	private Material deactiveMaterial;

	[SerializeField]
	private PlayerControls playerControls;

	[SerializeField]
	private GameObject pauseMenu;

	[SerializeField]
	private GameObject pauseButton;

	[SerializeField]
	private GameObject gameOverMenu;

	// Use this for initialization
	void Start () 
	{
		meshRendArrow = shiftArrow.GetComponent<MeshRenderer>();
	}

	public void updateLiveUI(int currentlives)
	{
		if (liveSprites.ElementAt (currentlives) != null) {
			liveSprites.ElementAt(currentlives).SetActive(false);
		}
	}

	public void toggleColorSwitchUI(Material activeMaterial)
	{
		meshRendArrow.material = activeMaterial;
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
		gameOverMenu.SetActive (true);
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
		playerControls.pauseGame (true);
		pauseMenu.SetActive (true);
		pauseButton.SetActive (false);
	}

	public void UnPauseGame()
	{
		playerControls.pauseGame (false);
		pauseMenu.SetActive (false);
		pauseButton.SetActive (true);
	}
}
