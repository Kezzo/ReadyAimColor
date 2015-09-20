using UnityEngine;

/// <summary>
/// Each object reprents and handles the state of an obstacle
/// </summary>
public class ObstacleState : MonoBehaviour {
    
    [SerializeField]
	private Material[] m_stateMaterials = new Material[3];

    [SerializeField]
    private GameObject m_obstacleModel;

    [SerializeField]
    private ParticleSystem m_obstacleDestructionPFX;

    private MeshRenderer m_meshRenderer;

    private ColorState m_colorState = ColorState.DISABLED;
    public ColorState ObstacleColorState { get { return m_colorState; } }

    /// <summary>
    /// Activates the Obstacle and sets a certain stage depending on the input parameter.
    /// </summary>
    /// <param name="stateID">The id for the stagethe obstacle should have.</param>
	public void setStateAndActive(int stateID)
	{
		if(m_meshRenderer == null)
		{
			m_meshRenderer = m_obstacleModel.GetComponent<MeshRenderer>();
		}

		if(stateID > 0)
		{
            if(m_meshRenderer != null)
			    m_meshRenderer.sharedMaterial = m_stateMaterials[stateID-1];

			gameObject.SetActive(true);
			
			switch(stateID)
			{
			    case 1: m_colorState = ColorState.GREEN;
				    break;
			    case 2: m_colorState = ColorState.YELLOW;
				    break;
                case 3:
                    m_colorState = ColorState.RED;
                    break;
            }
		}
		else
		{
			gameObject.SetActive(false);
		}
	}

    /// <summary>
    /// Handles the a collision with this obstacle.
    /// Plays PFX and deactives the obstacle.
    /// </summary>
    /// <param name="obstacle"></param>
    public void HandleObstacleCollision()
    {
        gameObject.SetActive(false);

        ParticleSystemRenderer particleSystemRenderer = m_obstacleDestructionPFX.GetComponent<ParticleSystemRenderer>();

        if (particleSystemRenderer != null)
            particleSystemRenderer.material = m_stateMaterials[(int) m_colorState];

        GameObject obstacleDestructionPFXGO = SimplePool.Spawn(m_obstacleDestructionPFX.gameObject, gameObject.transform.position, Quaternion.identity) as GameObject;
        obstacleDestructionPFXGO.transform.SetParent(gameObject.transform.parent.transform);

        m_obstacleDestructionPFX.Play();
    }
}
