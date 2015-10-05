using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to handle player movement and interaction like shooting and color switching.
/// </summary>
public class PlayerControls : MonoBehaviour {

    [SerializeField]
    private float m_forwardSpeed;

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

    [SerializeField]
    private ParticleSystemRenderer[] m_engineParticleRenderer;

    private List<ParticleSystem> m_engineParticleSystems;

    private float m_movementFraction = 0.5f;
    private int m_lives = 4;
    private bool m_gameIsPaused;

    private AudioManager m_AudioManager;
    private ColorState m_playerColorState = ColorState.GREEN;

    // Use this for initialization
    void Start () 
	{
        m_AudioManager = AudioManager.Instance;

        m_engineParticleSystems = new List<ParticleSystem>();

        foreach (ParticleSystemRenderer particleRenderer in m_engineParticleRenderer)
        {
            ParticleSystem particleSystem = particleRenderer.GetComponent<ParticleSystem>();

            if(particleRenderer != null)
            {
                m_engineParticleSystems.Add(particleSystem);
            }
        }
    }
	
	// Update is called once per frame
	void Update () 
	{
		if (!m_gameIsPaused) {
            MoveOnXAxis(Input.acceleration.x);
            MoveOnZAxis();
        }
	}

    /// <summary>
    /// Called when the player collides with an obstacle.
    /// </summary>
    /// <param name="other">The collider the player collided with.</param>
	void OnTriggerEnter(Collider other)
	{
		m_lives--;
		m_gameplayUI.UpdateHealthUI(m_lives);

        ObstacleState obstacleStateScript = other.GetComponent<ObstacleState>();
        if (obstacleStateScript != null)
            obstacleStateScript.HandleObstacleCollision();

        if (m_lives < 1)
		{
			PauseGame(true);
			m_gameplayUI.ShowGameOverMenu();
		}
	}

    /// <summary>
    /// Called when the game was paused.
    /// Pauses the player movement and the worldgeneration.
    /// </summary>
    /// <param name="pauseIt"></param>
	public void PauseGame(bool pauseIt)
	{
		m_gameIsPaused = pauseIt;
		m_worldGenScript.toggleWorldGeneration (m_gameIsPaused);
	}

    /// <summary>
    /// Called when the player switches their color.
    /// Calls the need methods to fulfill the color switch.
    /// </summary>
	public void ToggleState()
	{
		if(m_playerColorState == ColorState.GREEN)
		{
			HandlePlayerStateChange(ColorState.YELLOW);
		}
		else if(m_playerColorState == ColorState.YELLOW)
		{
			HandlePlayerStateChange(ColorState.GREEN);
		}
	}

    /// <summary>
    /// Called when the player pressed the shoot button.
    /// Handles bullet spawning and sound trigger.
    /// </summary>
	public void HandlePlayerShot()
	{
		if(!m_gameIsPaused)
		{
            m_AudioManager.PlayShootSound();

            foreach (Transform shootPosition in m_shootPositionList)
			{
				GameObject bullet = SimplePool.Spawn(m_bulletPrefab, shootPosition.position, shootPosition.rotation);
				bullet.transform.parent = m_bulletParent;

				BulletHandling bulletHandlingScript = bullet.GetComponent<BulletHandling>();
				bulletHandlingScript.SetBulletState(m_playerColorState);
			}
		}
	}

    /// <summary>
    /// Handles the player state change. 
    /// Changes the material for the player, the engine-pfx and the color switch UI.
    /// </summary>
    /// <param name="newPlayerColorState">The new player color state.</param>
    private void HandlePlayerStateChange(ColorState newPlayerColorState)
	{
		if(m_playerColorState != newPlayerColorState)
		{
			Material[] currentMaterials = m_playerMeshRend.sharedMaterials;
			m_playerColorState = newPlayerColorState;
			
			switch(m_playerColorState)
			{
				case ColorState.GREEN:
                    currentMaterials[1] = m_stateMaterials[0];
					m_gameplayUI.ToggleColorSwitchUI(newPlayerColorState);

                    m_engineParticleRenderer[0].material = m_stateMaterials[0];
                    m_engineParticleRenderer[1].material = m_stateMaterials[0];
                    break;

				case ColorState.YELLOW:
                    currentMaterials[1] = m_stateMaterials[1];
					m_gameplayUI.ToggleColorSwitchUI(newPlayerColorState);

                    m_engineParticleRenderer[0].material = m_stateMaterials[1];
                    m_engineParticleRenderer[1].material = m_stateMaterials[1];
                    break;
			}

			m_playerMeshRend.sharedMaterials = currentMaterials;
		}
	}

    /// <summary>
    /// Controls the movement on the Z-Axis.
    /// </summary>
    private void MoveOnZAxis()
    {
        transform.Translate(new Vector3(0f, 0f, m_forwardSpeed * Time.deltaTime));

        m_playerLerpPositionLeft.position = new Vector3(m_playerLerpPositionLeft.position.x, m_playerLerpPositionLeft.position.y, transform.position.z);
        m_playerLerpPositionRight.position = new Vector3(m_playerLerpPositionRight.position.x, m_playerLerpPositionRight.position.y, transform.position.z);
    }

    /// <summary>
    /// Controls the movement on the X-Axis, based on the gyroscope input.
    /// </summary>
    /// <param name="gyroScopeX">The current gyroscope x value.</param>
	private void MoveOnXAxis(float gyroScopeX)
	{
        m_playerModel.transform.localEulerAngles = new Vector3(0.0f, 0.0f, -(gyroScopeX * 20.0f));

        float fractionModifier = gyroScopeX * m_sensitivity * Time.deltaTime;

        if (m_movementFraction < 0.0f)
            m_movementFraction += fractionModifier;
        else
            m_movementFraction -= fractionModifier;

        m_movementFraction = Mathf.Clamp(m_movementFraction, 0.0f, 1.0f);

        if (Mathf.Abs(gyroScopeX) > 0.02f)
        {
            transform.position = Vector3.Lerp(m_playerLerpPositionRight.position, m_playerLerpPositionLeft.position, m_movementFraction);
        }
	}

    /// <summary>
    /// Toggles the engine particles effects.
    /// </summary>
    /// <param name="setActive">To state if the pfx should be toggled on or off.</param>
    private void ToggleEnginePFX(bool setActive)
    {
        foreach (ParticleSystem particleSystem in m_engineParticleSystems)
        {
            if(setActive)
                particleSystem.Play();
            else
                particleSystem.Stop();
        }
    }
}
