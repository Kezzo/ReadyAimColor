using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// Class to handle MapPart/World generation.
/// </summary>
public class WorldGeneration : MonoBehaviour {

    [SerializeField]
    private GameObject m_playerGameObject;

    [SerializeField]
	private bool m_gameIsPaused;

	[SerializeField]
	private GameObject m_mapPartsParent;
	Vector3 m_mapPartSpawnPosition;

	[SerializeField]
	private GameObject m_currentMapPrefab;

	[SerializeField]
	private float m_mapPartLength;

    [SerializeField]
    private float m_mapPartMargin;

    [SerializeField]
	private int m_generalMapPartCount;

	[SerializeField]
	private bool m_generateObstacles;

    [SerializeField]
    private bool m_updateHighScore;

    List<MapPart> m_mapParts = new List<MapPart>();

    private GameObject m_lastMapPart;

    private HighScoreController m_highScoreController;

    private bool m_generatedTutorialSequence;
    private int m_tutorialSequenceIndex = 0;
    private int m_generatedMapPartsThisSession;

	// Use this for initialization
	void Start () 
	{
        //Clean up maptiles left from editor.
        foreach(Transform child in m_mapPartsParent.transform)
        {
            Destroy(child.gameObject);
        }

        m_highScoreController = HighScoreController.Instance;

        m_mapPartSpawnPosition = new Vector3(0.0f, 0.0f, (m_generalMapPartCount - 1) * m_mapPartLength);
        m_generatedMapPartsThisSession = m_generalMapPartCount;

        for (int i=0; i<m_generalMapPartCount; i++)
		{
			SpawnMapPart(new Vector3(0.0f, 0.0f, i * m_mapPartLength), null);
		}

        StartCoroutine(CheckMapPartPositioning(0.5f));
    }

    /// <summary>
    /// Checks the MapPart positioning in a certain interval and respositions MapParts if necessary.
    /// Also increases the highscore when a MapPart is respawned.
    /// </summary>
    /// <returns></returns>
    private IEnumerator CheckMapPartPositioning(float delayBetweenEachCheck)
    {
        while(!m_gameIsPaused)
        {
            foreach (MapPart mapPart in m_mapParts.ToArray())
            {
                if ((mapPart.EndPointGO.transform.position.z + m_mapPartMargin) < m_playerGameObject.transform.position.z)
                {
                    m_generatedMapPartsThisSession++;
                    m_mapPartSpawnPosition = new Vector3(0.0f, 0.0f, (m_generatedMapPartsThisSession - 1) * m_mapPartLength);

                    SpawnMapPart(m_mapPartSpawnPosition, mapPart);
                    if (m_updateHighScore)
                    {
                        m_highScoreController.UpdateHighScoreBy(3);
                    }
                }
            }

            yield return new WaitForSeconds(delayBetweenEachCheck);
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

        ObstacleGenerator generateObstaclesScript = createdMapPartGO.GetComponent<ObstacleGenerator>();

        MapPart createdMapPart = new MapPart(createdMapPartGO, generateObstaclesScript, generateObstaclesScript.MapPartEndPoint);
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
		if(m_mapParts.Count < m_generalMapPartCount)
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
                if (m_generatedTutorialSequence)
                    currentMapPart.ObstacleGenScript.GenerateObstacles(3);
                else
                {
                    currentMapPart.ObstacleGenScript.GenerateObstacles(m_tutorialSequenceIndex);
                    m_tutorialSequenceIndex++;

                    if(m_tutorialSequenceIndex == 3)
                        m_generatedTutorialSequence = true;
                }
                    
            }

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
