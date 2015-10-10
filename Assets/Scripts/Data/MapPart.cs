using UnityEngine;

namespace Assets.Scripts.Data
{
    /// <summary>
    /// Class to handle a MapPart GameObject, their ObstacleGenerator Script and their endpoint as one Object.
    /// </summary>
    public class MapPart {

        public GameObject MapPartGameObject { get; set; }

        public ObstacleGenerator ObstacleGenScript { get; set; }

        public GameObject EndPointGameObject { get; set; }

        public MapPart(){}

        public MapPart(GameObject mapPartGameObject, ObstacleGenerator genObstacles, GameObject endPointGameObject)
        {
            MapPartGameObject = mapPartGameObject;
            ObstacleGenScript = genObstacles;
            EndPointGameObject = endPointGameObject;
        }
    }
}
