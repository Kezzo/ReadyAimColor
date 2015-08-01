using UnityEngine;
using System.Collections;

public class Obstacle {

	private GameObject _obstacleGO;
	public GameObject obstacleGO
	{
		get { return _obstacleGO; }
		set { _obstacleGO = value; } 
	}
	
	private ObstacleState _obstacleStateScript;
	public ObstacleState obstacleStateScript
	{
		get { return _obstacleStateScript; }
		set { _obstacleStateScript = value; } 
	}
	
	public Obstacle(){}
	
	public Obstacle(GameObject obstacle, ObstacleState obstacleState)
	{
		_obstacleGO = obstacle;
		_obstacleStateScript = obstacleState;
	}
}
