using UnityEngine;
using System.Collections;

public class MapPart {

	private GameObject _mapPartGO;
	public GameObject mapPartGO
	{
		get { return _mapPartGO; }
		set { _mapPartGO = value; } 
	}

	private GenerateObstacles _obstacleGenScript;
	public GenerateObstacles obstacleGenScript
	{
		get { return _obstacleGenScript; }
		set { _obstacleGenScript = value; } 
	}

	public MapPart(){}

	public MapPart(GameObject mapPart, GenerateObstacles genObstacles)
	{
		_mapPartGO = mapPart;
		_obstacleGenScript = genObstacles;
	}
}
