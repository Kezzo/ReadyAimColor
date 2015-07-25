using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControls : MonoBehaviour {

	public float sensitivity = 0.1f;
	public List<Transform> shootPositionList = new List<Transform>();
	public string currentBulletID;
	Dictionary<string,GameObject> bulletsDict = new Dictionary<string,GameObject>();

	// Use this for initialization
	void Start () 
	{
		Object[] loadedBullets = Resources.LoadAll("Bullets", typeof(GameObject));
		for(int i=0; i<loadedBullets.Length; i++)
		{
			bulletsDict.Add (loadedBullets[i].name, loadedBullets[i] as GameObject);
			print ("loaded Bullet: "+loadedBullets[i].name);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.touchCount > 0)
		{
			if(Input.GetTouch(0).phase == TouchPhase.Moved)
			{
				Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

				transform.Translate(-touchDeltaPosition.x * sensitivity, -touchDeltaPosition.y * sensitivity, 0.0f);
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Began)
			{
				foreach(Transform shootPosition in shootPositionList)
				{
					SimplePool.Spawn(bulletsDict[currentBulletID], shootPosition.position, Quaternion.identity);
				}
			}
		}
	}
}
