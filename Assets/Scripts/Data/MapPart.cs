using UnityEngine;

/// <summary>
/// Class to handle a MapPart GameObject, their ObstacleGenerator Script and their endpoint as one Object.
/// </summary>
public class MapPart {

    /// <summary>
    /// The MapPart GameObject.
    /// </summary>
	private GameObject m_mapPartGO;
	public GameObject MapPartGO
	{
		get { return m_mapPartGO; }
		set { m_mapPartGO = value; } 
	}

    /// <summary>
    /// The MapPart ObstacleGenerator Script
    /// </summary>
	private ObstacleGenerator m_obstacleGenScript;
	public ObstacleGenerator ObstacleGenScript
	{
		get { return m_obstacleGenScript; }
		set { m_obstacleGenScript = value; } 
	}

    /// <summary>
    /// The MapPart Endpoint, needed for worldgeneration.
    /// </summary>
    private GameObject m_endPointGO;
    public GameObject EndPointGO
    {
        get { return m_endPointGO; }
        set { m_endPointGO = value; }
    }

    public MapPart(){}

	public MapPart(GameObject mapPartGO, ObstacleGenerator genObstacles, GameObject endPointGO)
	{
		m_mapPartGO = mapPartGO;
		m_obstacleGenScript = genObstacles;
        m_endPointGO = endPointGO;
    }
}
