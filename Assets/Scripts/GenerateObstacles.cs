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
		foreach(Obstacle obstacle in obstaclesFirstRow)
		{
			obstacle.obstacleStateScript.setStateAndActive(0);
		}

		disableAllObstacles(obstaclesSecondRow);

		foreach(Obstacle obstacle in obstaclesSecondRow)
		{
			obstacle.obstacleStateScript.setStateAndActive(1);
		}
	}

	void generateTutorialSequence()
	{

	}

	void generateLevel1Diff()
	{

	}

	void getObstacles()
	{
		foreach(Transform child in obstacleFirstRowParent.transform)
		{
			obstaclesFirstRow.Add(new Obstacle(child.gameObject, child.GetComponent<ObstacleState>()));
			print (obstaclesFirstRow.Last().obstacleStateScript.name);
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
