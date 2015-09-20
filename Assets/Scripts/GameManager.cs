using UnityEngine;

/// <summary>
/// Handles Scene loading.
/// </summary>
public class GameManager : MonoBehaviour {

	public void startGame()
	{
		Application.LoadLevel ("Gameplay1");
	}
}
