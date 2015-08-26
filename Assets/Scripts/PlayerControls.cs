using UnityEngine;
using System.Collections.Generic;

public class PlayerControls : MonoBehaviour {

	[SerializeField]
	private GameObject m_playerModel;

	[SerializeField]
	private Transform m_bulletParent;

	[SerializeField]
	private WorldGeneration m_worldGenScript;
    private bool m_gameIsPaused;

	[SerializeField]
	private float m_sensitivity = 0.1f;

	[SerializeField]
	private List<Transform> m_shootPositionList = new List<Transform>();

	[SerializeField]
	private string m_currentBulletID;
    private Dictionary<string,GameObject> m_bulletsDict = new Dictionary<string,GameObject>();

	[SerializeField]
	private LayerMask m_layerMaskWALL;

	[SerializeField]
	private float m_shipWidth = 0.0f;

	[SerializeField]
	private float m_shootingCD = 0.0f;
    private float m_timeSinceLastBullet = 0.0f;

	[SerializeField]
    private ColorState m_playerColorState = ColorState.GREEN;

	[SerializeField]
    private Material[] m_stateMaterials;
    private MeshRenderer m_playerMeshRend;

	[SerializeField]
    private GameplayUI m_gameplayUI;
    private int m_lives = 4;

    [SerializeField]
    private AudioSource m_shootAudioSource;

    private AudioManager m_AudioManager;

	// Use this for initialization
	void Start () 
	{
        m_AudioManager = AudioManager.Instance;

        m_playerMeshRend = m_playerModel.GetComponent<MeshRenderer>();

		Object[] loadedBullets = Resources.LoadAll("Bullets", typeof(GameObject));
		for(int i=0; i<loadedBullets.Length; i++)
		{
			m_bulletsDict.Add (loadedBullets[i].name, loadedBullets[i] as GameObject);
//			print ("loaded Bullet: "+loadedBullets[i].name);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!m_gameIsPaused) {
			checkWallDistanceOnXAxis(Input.acceleration.x);
			m_timeSinceLastBullet -= Time.deltaTime;
		}
	}

	void OnTriggerEnter(Collider other)
	{
//		print (other.name);
		m_lives--;
		m_gameplayUI.updateLiveUI(m_lives);
		//print(lives);
		other.gameObject.SetActive(false);

		if(m_lives < 1)
		{
			pauseGame(true);
			m_gameplayUI.showGameOverMenu();
		}
	}

	public void pauseGame(bool pauseIt)
	{
		m_gameIsPaused = pauseIt;
		m_worldGenScript.toggleWorldGeneration (m_gameIsPaused);
	}

	public void ToggleState()
	{
		if(m_playerColorState == ColorState.GREEN)
		{
			handlePlayerStateChange(ColorState.YELLOW);
		}
		else if(m_playerColorState == ColorState.YELLOW)
		{
			handlePlayerStateChange(ColorState.GREEN);
		}
	}

	public void shoot()
	{
		if(m_timeSinceLastBullet < 0.0f && !m_gameIsPaused)
		{
            m_AudioManager.GetSoundByID(Random.Range(0, 4)).Play();
            
            foreach (Transform shootPosition in m_shootPositionList)
			{
				//shootPosition.LookAt(hit.point);
				
				GameObject bullet = SimplePool.Spawn(m_bulletsDict[m_currentBulletID], shootPosition.position, shootPosition.rotation);
				bullet.transform.parent = m_bulletParent;

				BulletHandling bulletHandlingScript = bullet.GetComponent<BulletHandling>();
				bulletHandlingScript.resetBulletLiveTime();
				bulletHandlingScript.setBulletState(m_playerColorState);

				m_timeSinceLastBullet = m_shootingCD;
			}
		}
	}
	
	void handlePlayerStateChange(ColorState newPlayerColorState)
	{
		if(m_playerColorState != newPlayerColorState)
		{
			Material[] currentMaterials = m_playerMeshRend.sharedMaterials;
			m_playerColorState = newPlayerColorState;
			
			switch(m_playerColorState)
			{
				case ColorState.GREEN: currentMaterials[1] = m_stateMaterials[0];
						m_gameplayUI.toggleColorSwitchUI(m_stateMaterials[0]);
						break;
				case ColorState.YELLOW: currentMaterials[1] = m_stateMaterials[1];
						m_gameplayUI.toggleColorSwitchUI(m_stateMaterials[1]);
						break;
			}

			m_playerMeshRend.sharedMaterials = currentMaterials;
		}
	}

	void checkWallDistanceOnXAxis(float gyroScopeX)
	{
		RaycastHit wallHit;
		if(gyroScopeX > 0.0f)
		{
			if(Physics.Raycast(this.transform.position, Vector3.right, out wallHit, 100.0f,m_layerMaskWALL))
			{
				Debug.DrawLine(this.transform.position,wallHit.point);
				//print (wallHit.collider.name);

				if(wallHit.distance > m_shipWidth)
				{
					moveOnXAxis(gyroScopeX);
				}
			}
		}
		else if(gyroScopeX < 0.0f)
		{
			if(Physics.Raycast(this.transform.position, Vector3.left, out wallHit, 20.0f, m_layerMaskWALL))
			{
				Debug.DrawLine(this.transform.position,wallHit.point);
//				print (wallHit.collider.name);

				if(wallHit.distance > m_shipWidth)
				{
					moveOnXAxis(gyroScopeX);
				}
			}
		}
		m_playerModel.transform.localEulerAngles = new Vector3(0.0f, 0.0f, -(gyroScopeX * 20.0f));
	}

	void moveOnXAxis(float gyroScopeX)
	{
		if(!m_gameIsPaused)
		{
			//print("gyroScopeX: "+Mathf.Abs(gyroScopeX));
			if(Mathf.Abs(gyroScopeX) > 0.02f)
			{
				this.transform.Translate((gyroScopeX * m_sensitivity) * Time.deltaTime, 0.0f, 0.0f);
				this.transform.position = new Vector3(this.transform.position.x, 1.0f, 0.0f);
			}

		}
	}
}
