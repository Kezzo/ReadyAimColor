using System.Collections;
using UnityEngine;

/// <summary>
/// Class to handle bullet movement and collision handling.
/// </summary>
public class BulletHandling : MonoBehaviour {

	[SerializeField]
	private Material[] m_stateMaterials;

	[SerializeField]
	private GameObject m_bulletModel;

	[SerializeField]
	private MeshRenderer m_bulletMeshRend;

	private float m_speed = 50.0f;

    private ColorState m_bulletColorState = ColorState.GREEN;

    private HighScoreController m_highScoreController;

    void Start()
    {
        m_highScoreController = HighScoreController.Instance;
    }

    void OnEnable()
    {
        StartCoroutine(DestoryAfter(2.0f));
    }

	// Update is called once per frame
	void Update () 
	{
		transform.Translate((Vector3.forward * m_speed) * Time.deltaTime);
		transform.localEulerAngles = new Vector3(0.0f, 0.0f, (this.transform.localEulerAngles.z - (120.0f * Time.deltaTime)));
    }

    /// <summary>
    /// Despawns the bullet after a given time.
    /// </summary>
    /// <param name="secondsToWait">The time after which the bullet should be despawned.</param>
    /// <returns></returns>
    private IEnumerator DestoryAfter(float secondsToWait)
    {
        yield return new WaitForSeconds(secondsToWait);
        SimplePool.Despawn(this.gameObject);
    }

    /// <summary>
    /// Called when the bullet collides with an obstacle.
    /// </summary>
    /// <param name="other"></param>
	void OnTriggerEnter(Collider other)
	{
        //print(other.name);

        ObstacleState obstacleStateScript = other.gameObject.GetComponent<ObstacleState>();

        switch (obstacleStateScript.ObstacleColorState)
		{
			case ColorState.GREEN: OnGreenObstacleHit(other.gameObject, obstacleStateScript);
					break;
			case ColorState.YELLOW: OnYellowObstacleHit(other.gameObject, obstacleStateScript);
					break;
		}

		gameObject.SetActive(false);
	}

    /// <summary>
    /// Called when the bullet collides with a GREEN obstacle.
    /// </summary>
    /// <param name="obstacle">The obstacle the bullet collided with.</param>
    /// <param name="obstacleStateScript">The obstacle state script of the obstacle</param>
	void OnGreenObstacleHit(GameObject obstacle, ObstacleState obstacleStateScript)
	{
		if(m_bulletColorState == ColorState.GREEN)
		{
            obstacleStateScript.HandleObstacleCollision();
            m_highScoreController.UpdateHighScoreBy(1);
        }
	}

    /// <summary>
    /// Called when the bullet collides with a YELLOW obstacle.
    /// </summary>
    /// <param name="obstacle">The obstacle the bullet collided with.</param>
    /// <param name="obstacleStateScript">The obstacle state script of the obstacle</param>
	void OnYellowObstacleHit(GameObject obstacle, ObstacleState obstacleStateScript)
	{
		if(m_bulletColorState == ColorState.YELLOW)
		{
            obstacleStateScript.HandleObstacleCollision();
            m_highScoreController.UpdateHighScoreBy(1);
        }
	}

    /// <summary>
    /// Changes the bullet state to a certain color.
    /// </summary>
    /// <param name="playerColorState">The color to change the bullet to.</param>
	public void SetBulletState(ColorState playerColorState)
	{
		if(playerColorState != m_bulletColorState)
		{
			if(m_bulletMeshRend == null)
			{
				m_bulletMeshRend = m_bulletModel.GetComponent<MeshRenderer>();
			}

			switch(playerColorState)
			{
			    case ColorState.GREEN: m_bulletMeshRend.sharedMaterial = m_stateMaterials[0];
				    break;
			    case ColorState.YELLOW: m_bulletMeshRend.sharedMaterial = m_stateMaterials[1];
				    break;
			}
			m_bulletColorState = playerColorState;
		}
	}
}
