using UnityEngine;

public class MapPart {

	private GameObject m_mapPartGO;
	public GameObject MapPartGO
	{
		get { return m_mapPartGO; }
		set { m_mapPartGO = value; } 
	}

	private GenerateObstacles m_obstacleGenScript;
	public GenerateObstacles ObstacleGenScript
	{
		get { return m_obstacleGenScript; }
		set { m_obstacleGenScript = value; } 
	}

	public MapPart(){}

	public MapPart(GameObject mapPart, GenerateObstacles genObstacles)
	{
		m_mapPartGO = mapPart;
		m_obstacleGenScript = genObstacles;
	}
}
