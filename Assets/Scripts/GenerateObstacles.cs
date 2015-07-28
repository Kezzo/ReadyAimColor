using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateObstacles : MonoBehaviour {

	public GameObject obstacleFirstRowParent;
	List<GameObject> obstaclesFirstRow = new List<GameObject>();

	public GameObject obstacleSecondRowParent;
	List<GameObject> obstaclesSecondRow = new List<GameObject>();

	void Start()
	{


//		print (obstaclesFirstRow.Count);
	}

	public void generateObstacles(int difficultyLevel)
	{
		if(obstaclesFirstRow.Count < 1  || obstaclesSecondRow.Count < 1)
		{
			getObstacleGOs();
		}

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

	void generateTutorialSequence()
	{

	}

	void generateLevel1Diff()
	{

	}

	void getObstacleGOs()
	{
		foreach(Transform child in obstacleFirstRowParent.transform)
		{
			obstaclesFirstRow.Add(child.gameObject);
		}
		
		foreach(Transform child in obstacleSecondRowParent.transform)
		{
			obstaclesSecondRow.Add(child.gameObject);
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

	void disableAllObstacles(List<GameObject> obstacleList)
	{
		foreach(GameObject obstacles in obstacleList)
		{
			obstacles.SetActive(false);
		}
	}
}
