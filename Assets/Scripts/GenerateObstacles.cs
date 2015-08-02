using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GenerateObstacles : MonoBehaviour {

	public GameObject obstacleFirstRowParent;
	List<Obstacle> obstaclesFirstRow = new List<Obstacle>();

	public GameObject obstacleSecondRowParent;
	List<Obstacle> obstaclesSecondRow = new List<Obstacle>();

	public void generateObstacles(int difficultyLevel)
	{
		if(obstaclesFirstRow.Count < 1  || obstaclesSecondRow.Count < 1)
		{
			getObstacles();
		}

		disableAllObstacles(obstaclesFirstRow);
		int[] generatedIDs = new int[6];
		generatedIDs = generateLevel1Diff();

		for(int i=0; i<obstaclesFirstRow.Count; i++)
		{
			obstaclesFirstRow[i].obstacleStateScript.setStateAndActive(generatedIDs[i]);
		}

		disableAllObstacles(obstaclesSecondRow);
		generatedIDs = generateLevel1Diff();

		for(int i=0; i<obstaclesFirstRow.Count; i++)
		{
			obstaclesSecondRow[i].obstacleStateScript.setStateAndActive(generatedIDs[i]);
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
		foreach(Transform child in obstacleFirstRowParent.transform)
		{
			obstaclesFirstRow.Add(new Obstacle(child.gameObject, child.GetComponent<ObstacleState>()));
//			print (obstaclesFirstRow.Last().obstacleStateScript.name);
		}
		
		foreach(Transform child in obstacleSecondRowParent.transform)
		{
			obstaclesSecondRow.Add(new Obstacle(child.gameObject, child.GetComponent<ObstacleState>()));
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
