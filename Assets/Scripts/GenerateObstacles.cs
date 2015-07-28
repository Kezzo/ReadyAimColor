using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateObstacles : MonoBehaviour {

	public List<GameObject> obstaclesFirstRow = new List<GameObject>();
	public List<GameObject> obstaclesSecondRow = new List<GameObject>();

	public void generateObstacles(int difficultyLevel)
	{
		disableAllObstacles(obstaclesFirstRow);
		foreach(GameObject obstacle in obstaclesFirstRow)
		{
			obstacle.SetActive(testGeneration());
		}

		disableAllObstacles(obstaclesSecondRow);

		foreach(GameObject obstacle in obstaclesSecondRow)
		{
			obstacle.SetActive(testGeneration());
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

		print(returnVal);

		return returnVal;
	}

	void disableAllObstacles(List<GameObject> obstacleList)
	{
		foreach(GameObject obstacles in obstacleList)
		{
			obstacles.SetActive(false);
		}
	}
}
