using UnityEngine;
using System.Collections;

public class BulletAcceleration : MonoBehaviour {

	public float speed = 1.0f;
	// Update is called once per frame
	void Update () 
	{
		this.transform.Translate(Vector3.forward * speed);
	}
}
