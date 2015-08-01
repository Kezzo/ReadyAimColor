using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WorldGeneration : MonoBehaviour {

	public bool playerIsDead;
	public float speed = 1.0f;
	List<MapPart> mapParts = new List<MapPart>();
	public GameObject mapPartsParent;
	Vector3 mapPartSpawnPosition;

	public GameObject currentMapPrefab;
	public float mapPartLength;
	public int mapPartCount;

	public bool generateObstacles;

	GameObject lastMapPart;

	// Use this for initialization
	void Start () 
	{
		mapPartSpawnPosition = new Vector3(0.0f, 0.0f, (mapPartCount-1) * mapPartLength);

		for(int i=0; i<mapPartCount; i++)
		{
			SpawnMapPart(new Vector3(0.0f, 0.0f, i * mapPartLength), null);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		foreach(MapPart mapPart in mapParts.ToArray())
		{
			if(mapPart.mapPartGO.transform.position.z < -mapPartLength)
			{
				SpawnMapPart(mapPartSpawnPosition, mapPart);
			}
			//mapPart.transform.Translate(0.0f, 0.0f, -(1.0f * speed * Time.deltaTime));
		}

		if(!playerIsDead)
		{
			mapPartsParent.transform.Translate(0.0f, 0.0f, -(1.0f * speed * Time.deltaTime));
		}
	}

	MapPart CreateMapPart(Vector3 spawnPosition)
	{
		GameObject createdMapPartGO = Instantiate(currentMapPrefab, spawnPosition, Quaternion.identity) as GameObject;
		createdMapPartGO.transform.parent = mapPartsParent.transform;

		MapPart createdMapPart = new MapPart(createdMapPartGO, createdMapPartGO.GetComponent<GenerateObstacles>());
		mapParts.Add(createdMapPart);

		return createdMapPart;
	}

	void SpawnMapPart(Vector3 spawnPosition, MapPart mapPartToSpawn)
	{
		MapPart currentMapPart;

		GameObject currentMapPartGO;
		if(mapParts.Count < mapPartCount)
		{
			currentMapPart = CreateMapPart(spawnPosition);
			currentMapPartGO = currentMapPart.mapPartGO;
		}
		else
		{
			//Reposition rather than destroying and instantiating
			currentMapPart = mapPartToSpawn;
			currentMapPartGO = currentMapPart.mapPartGO;
			currentMapPartGO.transform.position = spawnPosition;
		}

		if(mapParts.Count > 1)
		{
			if(generateObstacles)
			{
				currentMapPart.obstacleGenScript.generateObstacles(0);
			}

			//print ("generatedObstacles");

			// To eliminated small displacements of mapParts
			float lastMapPartPosZ = lastMapPart.transform.position.z;
			if((currentMapPartGO.transform.position.z - lastMapPartPosZ) > mapPartLength)
			{
				float correctedZPosition = currentMapPartGO.transform.position.z - ((currentMapPartGO.transform.position.z - lastMapPartPosZ) - mapPartLength);
				currentMapPartGO.transform.position = new Vector3(0.0f,0.0f, correctedZPosition);
			}
		}

		lastMapPart = currentMapPartGO;
	}
}
