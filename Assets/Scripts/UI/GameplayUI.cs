using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameplayUI : MonoBehaviour {

	public List<GameObject> liveSprites = new List<GameObject>();

	public List<GameObject> shiftArrows = new List<GameObject>();
	MeshRenderer[] meshRendArrows = new MeshRenderer[2];
//	GameObject shiftArrowParent;
	int activeArrowIndex = 1;
	public Material deactiveMaterial;

	public PlayerControls playerControls;
	public GameObject pauseMenu;
	public GameObject pauseButton;
	public GameObject gameOverMenu;

	// Use this for initialization
	void Start () 
	{
	//	shiftArrowParent = shiftArrows [0].transform.parent.gameObject;
		//print (shiftArrowParent.name);

		for(int i=0; i<shiftArrows.Count; i++)
		{
			meshRendArrows[i] = shiftArrows.ElementAt(i).GetComponent<MeshRenderer>();
		}
	}

	public void updateLiveUI(int currentlives)
	{
		if (liveSprites.ElementAt (currentlives) != null) {
			liveSprites.ElementAt(currentlives).SetActive(false);
		}
	}

	public void toggleColorSwitchUI(Material activeMaterial)
	{
		//print (activeArrowIndex);
		meshRendArrows [activeArrowIndex].material = activeMaterial;
		activeArrowIndex = activeArrowIndex == 0 ? 1 : 0;
		meshRendArrows [activeArrowIndex].material = deactiveMaterial;
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
