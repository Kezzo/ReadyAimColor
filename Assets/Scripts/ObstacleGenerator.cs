using System.Collections.Generic;
using Assets.Scripts.Data;
using UnityEngine;

/// <summary>
/// Class to handle obstacle generation.
/// </summary>
public class ObstacleGenerator : MonoBehaviour {

	[SerializeField]
	private GameObject m_obstacleFirstRowParent;
	private List<Obstacle> m_obstaclesFirstRow = new List<Obstacle>();

	[SerializeField]
	private GameObject m_obstacleSecondRowParent;
    private List<Obstacle> m_obstaclesSecondRow = new List<Obstacle>();

    [SerializeField]
    private GameObject m_mapPartEndPoint;
    public GameObject MapPartEndPoint { get { return m_mapPartEndPoint; } }

    /// <summary>
    /// Generates obstacle in a certain amount and colorstate.
    /// </summary>
    /// <param name="difficultyLevel">The difficulty level to generate</param>
	public void GenerateObstacles(int difficultyLevel)
	{
		if(m_obstaclesFirstRow.Count < 1  || m_obstaclesSecondRow.Count < 1)
		{
			GetObstacles();
		}

		DisableAllObstacles(m_obstaclesFirstRow);
		int[] generatedIDs = new int[6];

        if(difficultyLevel >= 3)
		    generatedIDs = generateLevel1Diff();
        else
            generatedIDs = generateTutorialSequence(difficultyLevel);

        for (int i=0; i<m_obstaclesFirstRow.Count; i++)
		{
			m_obstaclesFirstRow[i].ObstacleStateScript.SetStateAndActive(generatedIDs[i]);
		}

		DisableAllObstacles(m_obstaclesSecondRow);

        generatedIDs = new int[6];

        if (difficultyLevel >= 3)
		    generatedIDs = generateLevel1Diff();

        for (int i=0; i<m_obstaclesFirstRow.Count; i++)
		{
			m_obstaclesSecondRow[i].ObstacleStateScript.SetStateAndActive(generatedIDs[i]);
		}
	}

    /// <summary>
    /// Generates the prebuild tutorial sequence.
    /// </summary>
    /// <param name="generationIndex">The current generation index.</param>
    /// <returns>Returns the current generation sequence.</returns>
    private int[] generateTutorialSequence(int generationIndex)
	{
        int[] generatedIDs = null;

        switch(generationIndex)
        {
            case 0:
                generatedIDs = new int[] { 1, 1, 1, 1, 1, 1 };
                break;

            case 1:
                generatedIDs = new int[] { 2, 2, 2, 2, 2, 2 };
                break;

            case 2:
                generatedIDs = new int[] { 0, 3, 3, 0, 3, 3 };
                break;
        }

        return generatedIDs;
    }

    /// <summary>
    /// Generates a Level 1 Difficulty Sequence.
    /// </summary>
    /// <returns>Returns a generated Level 1 Difficulty Sequence.</returns>
	private int[] generateLevel1Diff()
	{
		int[] generatedIDs = new int[6];

		int generatedReds = 0;
        int generatedEmpties = 0;

		for(int i=0; i<generatedIDs.Length; i++)
		{
			generatedIDs[i] = Random.Range(0,4);
//			print (generatedIDs[i]);
			if(generatedIDs[i] == 3)
			{
				generatedReds++;
			}

            if (generatedIDs[i] == 0)
            {
                generatedEmpties++;
            }
        }

//		print (generatedReds);

		if (generatedReds > 5 || generatedEmpties > 4) {
			print ("new id generation cause of 6 reds or 5+ empties");
			generatedIDs = generateLevel1Diff();
		}

		return generatedIDs;
	}

    /// <summary>
    /// Gets the obstalcle state scripts from the current obstacles to generate for.
    /// </summary>
    private void GetObstacles()
	{
		//print ("getObstacles");
		foreach(Transform child in m_obstacleFirstRowParent.transform)
		{
			m_obstaclesFirstRow.Add(new Obstacle(child.gameObject, child.GetComponent<ObstacleState>()));
//			print (obstaclesFirstRow.Last().obstacleStateScript.name);
		}
		
		foreach(Transform child in m_obstacleSecondRowParent.transform)
		{
			m_obstaclesSecondRow.Add(new Obstacle(child.gameObject, child.GetComponent<ObstacleState>()));
		}
	}

    /// <summary>
    /// Disables all current obstacles.
    /// </summary>
    /// <param name="obstacleList"></param>
    private void DisableAllObstacles(List<Obstacle> obstacleList)
	{
		foreach(Obstacle obstacles in obstacleList)
		{
			obstacles.ObstacleGo.SetActive(false);
		}
	}
}
