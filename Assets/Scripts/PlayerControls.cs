using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerControls : MonoBehaviour {

	public GameObject playerModel;
	public Transform bulletParent;

	public WorldGeneration worldGenScript;
	bool gameIsPaused;

	public float sensitivity = 0.1f;
	public List<Transform> shootPositionList = new List<Transform>();
	public string currentBulletID;
	Dictionary<string,GameObject> bulletsDict = new Dictionary<string,GameObject>();

	public LayerMask layerMaskWALL;
	public float shipWidth = 0.0f;

	float timeSinceLastBullet = 0.0f;
	public float shootingCD = 0.0f;

	public enum PlayerColorState{GREEN, YELLOW};
	public PlayerColorState playerColorState = PlayerColorState.GREEN;
	public Material[] stateMaterials;
	MeshRenderer playerMeshRend;

	public GameplayUI gameplayUI;
	int lives = 4;

	// Use this for initialization
	void Start () 
	{
		playerMeshRend = playerModel.GetComponent<MeshRenderer>();

		Object[] loadedBullets = Resources.LoadAll("Bullets", typeof(GameObject));
		for(int i=0; i<loadedBullets.Length; i++)
		{
			bulletsDict.Add (loadedBullets[i].name, loadedBullets[i] as GameObject);
//			print ("loaded Bullet: "+loadedBullets[i].name);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!gameIsPaused) {
			checkWallDistanceOnXAxis(Input.acceleration.x);
			timeSinceLastBullet -= Time.deltaTime;
		}
	}

	void OnTriggerEnter(Collider other)
	{
//		print (other.name);
		lives--;
		gameplayUI.updateLiveUI(lives);
		//print(lives);
		other.gameObject.SetActive(false);

		if(lives < 1)
		{
			pauseGame(true);
			gameplayUI.showGameOverMenu();
		}
	}

	public void pauseGame(bool pauseIt)
	{
		gameIsPaused = pauseIt;
		worldGenScript.toggleWorldGeneration (gameIsPaused);
	}

	public void ToggleState()
	{
		if(playerColorState == PlayerColorState.GREEN)
		{
			handlePlayerStateChange(PlayerColorState.YELLOW);
		}
		else if(playerColorState == PlayerColorState.YELLOW)
		{
			handlePlayerStateChange(PlayerColorState.GREEN);
		}
	}

	public void shoot()
	{
		if(timeSinceLastBullet < 0.0f && !gameIsPaused)
		{
			foreach(Transform shootPosition in shootPositionList)
			{
				//shootPosition.LookAt(hit.point);
				
				GameObject bullet = SimplePool.Spawn(bulletsDict[currentBulletID], shootPosition.position, shootPosition.rotation);
				bullet.transform.parent = bulletParent;

				BulletHandling bulletHandlingScript = bullet.GetComponent<BulletHandling>();
				bulletHandlingScript.resetBulletLiveTime();
				bulletHandlingScript.setBulletState(playerColorState);

				timeSinceLastBullet = shootingCD;
			}
		}
	}
	
	void handlePlayerStateChange(PlayerColorState newPlayerColorState)
	{
		if(playerColorState != newPlayerColorState)
		{
			Material[] currentMaterials = playerMeshRend.materials;
			playerColorState = newPlayerColorState;
			
			switch(playerColorState)
			{
			case PlayerColorState.GREEN: currentMaterials[1] = stateMaterials[0];
					gameplayUI.toggleColorSwitchUI(stateMaterials[1]);
					break;
			case PlayerColorState.YELLOW: currentMaterials[1] = stateMaterials[1];
					gameplayUI.toggleColorSwitchUI(stateMaterials[0]);
					break;
			}

			playerMeshRend.materials = currentMaterials;
		}
	}

	void checkWallDistanceOnXAxis(float gyroScopeX)
	{
		RaycastHit wallHit;
		if(gyroScopeX > 0.0f)
		{
			if(Physics.Raycast(this.transform.position, Vector3.right, out wallHit, 100.0f,layerMaskWALL))
			{
				Debug.DrawLine(this.transform.position,wallHit.point);
				//print (wallHit.collider.name);

				if(wallHit.distance > shipWidth)
				{
					moveOnXAxis(gyroScopeX);
				}
			}
		}
		else if(gyroScopeX < 0.0f)
		{
			if(Physics.Raycast(this.transform.position, Vector3.left, out wallHit, 20.0f, layerMaskWALL))
			{
				Debug.DrawLine(this.transform.position,wallHit.point);
//				print (wallHit.collider.name);

				if(wallHit.distance > shipWidth)
				{
					moveOnXAxis(gyroScopeX);
				}
			}
		}
		playerModel.transform.localEulerAngles = new Vector3(0.0f, 0.0f, -(gyroScopeX * 20.0f));
	}

	void moveOnXAxis(float gyroScopeX)
	{
		if(!gameIsPaused)
		{
			this.transform.Translate((gyroScopeX * sensitivity) * Time.deltaTime, 0.0f, 0.0f);
			this.transform.position = new Vector3(this.transform.position.x, 1.0f, 0.0f);
		}
	}
}
