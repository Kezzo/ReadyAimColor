using UnityEngine;
using System.Collections.Generic;

public class WorldGeneration : MonoBehaviour {

	[SerializeField]
	private bool m_gameIsPaused;

	[SerializeField]
	private float m_worldSpeed = 1.0f;
	List<MapPart> m_mapParts = new List<MapPart>();

	[SerializeField]
	private GameObject m_mapPartsParent;
	Vector3 m_mapPartSpawnPosition;

	[SerializeField]
	private GameObject m_currentMapPrefab;

	[SerializeField]
	private float m_mapPartLength;

	[SerializeField]
	private int m_mapPartCount;

	[SerializeField]
	private bool m_generateObstacles;

    [SerializeField]
    private bool m_updateHighScore;

    private GameObject m_lastMapPart;

    private HighScoreController m_highScoreController;

	// Use this for initialization
	void Start () 
	{
        m_highScoreController = HighScoreController.Instance;

        m_mapPartSpawnPosition = new Vector3(0.0f, 0.0f, (m_mapPartCount-1) * m_mapPartLength);

		for(int i=0; i<m_mapPartCount; i++)
		{
			SpawnMapPart(new Vector3(0.0f, 0.0f, i * m_mapPartLength), null);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		foreach(MapPart mapPart in m_mapParts.ToArray())
		{
			if(mapPart.MapPartGO.transform.position.z < -m_mapPartLength)
			{
				SpawnMapPart(m_mapPartSpawnPosition, mapPart);
                if(m_updateHighScore)
                {
                    m_highScoreController.UpdateHighScoreBy(300);
                }
                
            }
			//mapPart.transform.Translate(0.0f, 0.0f, -(1.0f * speed * Time.deltaTime));
		}

		if(!m_gameIsPaused)
		{
			m_mapPartsParent.transform.Translate(0.0f, 0.0f, -(1.0f * m_worldSpeed * Time.deltaTime));
		}
	}

    /// <summary>
    /// Can be called to pause or unpause the world generation.
    /// </summary>
    /// <param name="newPauseStatus"></param>
	public void toggleWorldGeneration(bool newPauseStatus)
	{
		m_gameIsPaused = newPauseStatus;
	}

    /// <summary>
    /// Can be called to instantiate and set up a new map part at a certain position.
    /// </summary>
    /// <param name="spawnPosition"></param>
    /// <returns></returns>
	MapPart CreateMapPart(Vector3 spawnPosition)
	{
		GameObject createdMapPartGO = Instantiate(m_currentMapPrefab, spawnPosition, Quaternion.identity) as GameObject;
		createdMapPartGO.transform.parent = m_mapPartsParent.transform;

		MapPart createdMapPart = new MapPart(createdMapPartGO, createdMapPartGO.GetComponent<GenerateObstacles>());
		m_mapParts.Add(createdMapPart);

		return createdMapPart;
	}

    /// <summary>
    /// Handles Mappart pooling and positioning.
    /// </summary>
    /// <param name="spawnPosition">The position the mappart parameter should be set to</param>
    /// <param name="mapPartToSpawn">The map part object that shall be spawned</param>
	private void SpawnMapPart(Vector3 spawnPosition, MapPart mapPartToSpawn)
	{
		MapPart currentMapPart;

		GameObject currentMapPartGO;
		if(m_mapParts.Count < m_mapPartCount)
		{
			currentMapPart = CreateMapPart(spawnPosition);
			currentMapPartGO = currentMapPart.MapPartGO;
		}
		else
		{
			//Reposition rather than destroying and instantiating
			currentMapPart = mapPartToSpawn;
			currentMapPartGO = currentMapPart.MapPartGO;
			currentMapPartGO.transform.position = spawnPosition;
		}

		if(m_mapParts.Count > 1)
		{
			if(m_generateObstacles)
			{
				currentMapPart.ObstacleGenScript.generateObstacles(0);
			}

			//print ("generatedObstacles");

			// To eliminated small displacements of mapParts
			float lastMapPartPosZ = m_lastMapPart.transform.position.z;
			if((currentMapPartGO.transform.position.z - lastMapPartPosZ) > m_mapPartLength)
			{
				float correctedZPosition = currentMapPartGO.transform.position.z - ((currentMapPartGO.transform.position.z - lastMapPartPosZ) - m_mapPartLength);
				currentMapPartGO.transform.position = new Vector3(0.0f,0.0f, correctedZPosition);
			}
		}

		m_lastMapPart = currentMapPartGO;
	}
}
