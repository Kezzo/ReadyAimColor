using UnityEngine;
using System.Collections;

public class BulletHandling : MonoBehaviour {

	float speed = 20.0f;

	float activeTime;

	public Material[] stateMaterials;
	public GameObject bulletModel;
	public MeshRenderer bulletMeshRend;

	PlayerControls.PlayerColorState bulletColorState = PlayerControls.PlayerColorState.GREEN;

	// Update is called once per frame
	void Update () 
	{
		this.transform.Translate((Vector3.up * speed) * Time.deltaTime);

		this.transform.RotateAround (this.transform.position, Vector3.up, -(100.0f*Time.deltaTime));
		//this.transform.localEulerAngles = new Vector3(-90.0f, (this.transform.localEulerAngles.z + (20.0f * Time.deltaTime)), 0.0f);
		//this.transform.Rotate (00.0f, 20.0f * Time.deltaTime, 0.0f);

		if(activeTime > 2.0f)
		{
			Destroy(this.gameObject);
		}
		else
		{
			activeTime += Time.deltaTime;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
//		print(other.name);

		switch(other.gameObject.GetComponent<ObstacleState>().getObstacleState())
		{
			case ObstacleState.ColorState.RED: redHit();
					break;
			case ObstacleState.ColorState.GREEN: greenHit(other.gameObject);
					break;
			case ObstacleState.ColorState.YELLOW: yellowHit(other.gameObject);
					break;
		}

		this.gameObject.SetActive(false);
	}

	void redHit()
	{

	}

	void greenHit(GameObject obstacle)
	{
		if(bulletColorState == PlayerControls.PlayerColorState.GREEN)
		{
			obstacle.SetActive(false);
		}

	}

	void yellowHit(GameObject obstacle)
	{
		if(bulletColorState == PlayerControls.PlayerColorState.YELLOW)
		{
			obstacle.SetActive(false);
		}
	}

	public void setBulletState(PlayerControls.PlayerColorState playerColorState)
	{
		if(playerColorState != bulletColorState)
		{
			if(bulletMeshRend == null)
			{
				bulletMeshRend = bulletModel.GetComponent<MeshRenderer>();
			}

			switch(playerColorState)
			{
			case PlayerControls.PlayerColorState.GREEN: bulletMeshRend.material = stateMaterials[0];
				break;
			case PlayerControls.PlayerColorState.YELLOW: bulletMeshRend.material = stateMaterials[1];
				break;
			}
			bulletColorState = playerColorState;
		}

	}
}
