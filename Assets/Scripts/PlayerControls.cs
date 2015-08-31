using UnityEngine;
using System.Collections.Generic;

public class PlayerControls : MonoBehaviour {

	[SerializeField]
	private GameObject m_playerModel;

	[SerializeField]
	private Transform m_bulletParent;

    [SerializeField]
    private Transform m_playerLerpPositionLeft;

    [SerializeField]
    private Transform m_playerLerpPositionRight;

    [SerializeField]
	private WorldGeneration m_worldGenScript;
    
	[SerializeField]
	private float m_sensitivity = 0.1f;

	[SerializeField]
	private Transform[] m_shootPositionList;

    [SerializeField]
    private GameObject m_bulletPrefab;

    [SerializeField]
    private Material[] m_stateMaterials;

    [SerializeField]
    private MeshRenderer m_playerMeshRend;

	[SerializeField]
    private GameplayUI m_gameplayUI;
    
    [SerializeField]
    private AudioSource m_shootAudioSource;

    private float m_movementFraction = 0.5f;
    private int m_lives = 4;
    private bool m_gameIsPaused;

    private AudioManager m_AudioManager;
    private ColorState m_playerColorState = ColorState.GREEN;

    // Use this for initialization
    void Start () 
	{
        m_AudioManager = AudioManager.Instance;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!m_gameIsPaused) {
            MoveOnXAxis(Input.acceleration.x);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		m_lives--;
		m_gameplayUI.updateHealthUI(m_lives);
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
		if(!m_gameIsPaused)
		{
            m_AudioManager.PlayShootSound();

            foreach (Transform shootPosition in m_shootPositionList)
			{
				GameObject bullet = SimplePool.Spawn(m_bulletPrefab, shootPosition.position, shootPosition.rotation);
				bullet.transform.parent = m_bulletParent;

				BulletHandling bulletHandlingScript = bullet.GetComponent<BulletHandling>();
				bulletHandlingScript.setBulletState(m_playerColorState);
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

	private void MoveOnXAxis(float gyroScopeX)
	{
		if(!m_gameIsPaused)
		{
            m_playerModel.transform.localEulerAngles = new Vector3(0.0f, 0.0f, -(gyroScopeX * 20.0f));

            float fractionModifier = (gyroScopeX * m_sensitivity) * Time.deltaTime;

            if (m_movementFraction < 0.0f)
                m_movementFraction += fractionModifier;
            else
                m_movementFraction -= fractionModifier;

            m_movementFraction = Mathf.Clamp(m_movementFraction, 0.0f, 1.0f);

            if (Mathf.Abs(gyroScopeX) > 0.02f)
            {
                this.transform.position = Vector3.Lerp(m_playerLerpPositionLeft.position, m_playerLerpPositionRight.position, m_movementFraction);
            }
		}
	}
}
