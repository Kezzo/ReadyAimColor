using UnityEngine;

namespace Assets.Scripts.Data
{
    /// <summary>
    /// Struct to use the Obstacle GameObject and ObstacleState script with one struct.
    /// </summary>
    public struct Obstacle
    {
        public GameObject ObstacleGo { get; set; }

        public ObstacleState ObstacleStateScript { get; set; }

        public Obstacle(GameObject obstacle, ObstacleState obstacleState)
        {
            ObstacleGo = obstacle;
            ObstacleStateScript = obstacleState;
        }
    }
}
