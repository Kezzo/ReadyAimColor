using UnityEngine;

/// <summary>
/// Struct to use the Obstacle GameObject and ObstacleState script with one struct.
/// </summary>
public struct Obstacle
{
    private GameObject m_obstacleGO;
    public GameObject obstacleGO
    {
        get { return m_obstacleGO; }
        set { m_obstacleGO = value; }
    }

    private ObstacleState m_obstacleStateScript;
    public ObstacleState obstacleStateScript
    {
        get { return m_obstacleStateScript; }
        set { m_obstacleStateScript = value; }
    }

    public Obstacle(GameObject obstacle, ObstacleState obstacleState)
    {
        m_obstacleGO = obstacle;
        m_obstacleStateScript = obstacleState;
    }
}
