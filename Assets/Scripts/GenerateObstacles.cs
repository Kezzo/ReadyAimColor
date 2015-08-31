using UnityEngine;
using System.Collections.Generic;

public class GenerateObstacles : MonoBehaviour {

	[SerializeField]
	private GameObject m_obstacleFirstRowParent;
	private List<Obstacle> m_obstaclesFirstRow = new List<Obstacle>();

	[SerializeField]
	private GameObject m_obstacleSecondRowParent;
    private List<Obstacle> m_obstaclesSecondRow = new List<Obstacle>();

    /// <summary>
    /// Generates obstacle in a certain amount and colorstate.
    /// </summary>
    /// <param name="difficultyLevel"></param>
	public void generateObstacles(int difficultyLevel)
	{
		if(m_obstaclesFirstRow.Count < 1  || m_obstaclesSecondRow.Count < 1)
		{
			getObstacles();
		}

		disableAllObstacles(m_obstaclesFirstRow);
		int[] generatedIDs = new int[6];
		generatedIDs = generateLevel1Diff();

		for(int i=0; i<m_obstaclesFirstRow.Count; i++)
		{
			m_obstaclesFirstRow[i].obstacleStateScript.setStateAndActive(generatedIDs[i]);
		}

		disableAllObstacles(m_obstaclesSecondRow);
		generatedIDs = generateLevel1Diff();

		for(int i=0; i<m_obstaclesFirstRow.Count; i++)
		{
			m_obstaclesSecondRow[i].obstacleStateScript.setStateAndActive(generatedIDs[i]);
		}
	}

    private void generateTutorialSequence()
	{
        //TODO: Write generate Tutorial sequence
	}

	private int[] generateLevel1Diff()
	{
		int[] generatedIDs = new int[6];

		int generatedReds = 0;
        int generatedEmpties = 0;

		for(int i=0; i<generatedIDs.Length; i++)
		{
			generatedIDs[i] = Random.Range(0,4);
//			print (generatedIDs[i]);
			if(generatedIDs[i] == 0)
			{
				generatedReds++;
			}

            if (generatedIDs[i] == 3)
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

    private void getObstacles()
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

    private void disableAllObstacles(List<Obstacle> obstacleList)
	{
		foreach(Obstacle obstacles in obstacleList)
		{
			obstacles.obstacleGO.SetActive(false);
		}
	}
}
