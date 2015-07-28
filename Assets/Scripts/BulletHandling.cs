using UnityEngine;
using System.Collections;

public class BulletHandling : MonoBehaviour {

	float speed = 20.0f;
	float activeTime;
	// Update is called once per frame
	void Update () 
	{
		this.transform.Translate((Vector3.forward * speed) * Time.deltaTime);
		this.transform.localEulerAngles = new Vector3(0.0f, 0.0f, (this.transform.localEulerAngles.z - (120.0f * Time.deltaTime)));

		if(activeTime > 2.0f)
		{
			Destroy(this.gameObject);
		}
		else
		{
			activeTime += Time.deltaTime;
		}
	}
}
