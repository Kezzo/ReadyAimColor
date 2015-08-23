using UnityEngine;

/// <summary>
/// Each object reprents and handles the state of an obstacle
/// </summary>
public class ObstacleState : MonoBehaviour {
    
    [SerializeField]
	private Material[] m_materials = new Material[3];

    [SerializeField]
    private GameObject m_model;

    private MeshRenderer m_meshRend;

    private ColorState m_colorState = ColorState.DISABLED;

    /// <summary>
    /// Activates the Obstacle and sets a certain stage depending on the input parameter.
    /// </summary>
    /// <param name="stateID">The id for the stagethe obstacle should have.</param>
	public void setStateAndActive(int stateID)
	{
		if(m_meshRend == null)
		{
			m_meshRend = GetMeshRenderer();
		}

		if(stateID < 3)
		{
			m_meshRend.material = m_materials[stateID];

			this.gameObject.SetActive(true);
			
			switch(stateID)
			{
			case 0: m_colorState = ColorState.RED;
				break;
			case 1: m_colorState = ColorState.GREEN;
				break;
			case 2: m_colorState = ColorState.YELLOW;
				break;
			}
		}
		else
		{
			this.gameObject.SetActive(false);
		}

	}

	public ColorState getObstacleState()
	{
		ColorState returnColorState = m_colorState;
		return returnColorState;
	}

	MeshRenderer GetMeshRenderer()
	{
		return m_model.GetComponent<MeshRenderer>();
	}
}
