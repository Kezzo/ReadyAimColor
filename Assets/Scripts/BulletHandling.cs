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

	void OnTriggerEnter(Collider other)
	{
		print(other.name);

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
