using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldGeneration : MonoBehaviour {

	public float speed = 1.0f;
	public List<GameObject> mapParts = new List<GameObject>();

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		foreach(GameObject mapPart in mapParts)
		{
			mapPart.transform.Translate(0.0f, 0.0f, -(1.0f * speed * Time.deltaTime));
		}
	}
}
