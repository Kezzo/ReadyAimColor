using UnityEngine;

/// <summary>
/// Handles Scene loading.
/// </summary>
public class GameManager : MonoBehaviour {

	public void StartGame()
	{
		Application.LoadLevel ("Gameplay1");
	}
}
