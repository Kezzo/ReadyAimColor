using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldGeneration : MonoBehaviour {

	public float speed = 1.0f;
	List<GameObject> mapParts = new List<GameObject>();
	public GameObject mapPartsParent;
	Vector3 mapPartSpawnPosition;

	public GameObject currentMapPrefab;
	public float mapPartLength;
	public int mapPartCount;

	// Use this for initialization
	void Start () 
	{
		mapPartSpawnPosition = new Vector3(0.0f, 0.0f, (mapPartCount-1) * mapPartLength);

		for(int i=0; i<mapPartCount; i++)
		{
			SpawnNewMapPart(new Vector3(0.0f, 0.0f, i * mapPartLength));
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		mapPartsParent.transform.Translate(0.0f, 0.0f, -(1.0f * speed * Time.deltaTime));
		foreach(GameObject mapPart in mapParts.ToArray())
		{
			if(mapPart.transform.position.z < -mapPartLength)
			{
				mapParts.Remove(mapPart);
				Destroy(mapPart);
				SpawnNewMapPart(mapPartSpawnPosition);
			}
		}
	}

	void SpawnNewMapPart(Vector3 spawnPosition)
	{
		GameObject newMapPart = SimplePool.Spawn(currentMapPrefab, spawnPosition, Quaternion.identity);
		mapParts.Add(newMapPart);
		newMapPart.transform.parent = mapPartsParent.transform;
	}
	
}
