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

	void generateTutorialSequence()
	{

	}

	int[] generateLevel1Diff()
	{
		int[] generatedIDs = new int[6];

		int generatedReds = 0;
		for(int i=0; i<generatedIDs.Length; i++)
		{
			generatedIDs[i] = Random.Range(0,4);
//			print (generatedIDs[i]);
			if(generatedIDs[i] == 0)
			{
				generatedReds++;
			}
		}

//		print (generatedReds);

		if (generatedReds > 5) {
			print ("new id generation cause of 6 reds");
			generatedIDs = generateLevel1Diff();
		}

		return generatedIDs;
	}

	void getObstacles()
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

	bool testGeneration()
	{

		int rnd = Random.Range(0,2);

		bool returnVal = false;

		if(rnd == 1)
		{
			returnVal = true;
		}

		//print(returnVal);

		return returnVal;
	}

	void disableAllObstacles(List<Obstacle> obstacleList)
	{
		foreach(Obstacle obstacles in obstacleList)
		{
			obstacles.obstacleGO.SetActive(false);
		}
	}
}
