using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControls : MonoBehaviour {

	public GameObject playerModel;
	public Transform bulletParent;
	public float sensitivity = 0.1f;
	public List<Transform> shootPositionList = new List<Transform>();
	public string currentBulletID;
	Dictionary<string,GameObject> bulletsDict = new Dictionary<string,GameObject>();

	public LayerMask layerMaskWALL = new LayerMask();
	public float shipWidth = 0.0f;

	float timeSinceLastBullet = 0.0f;
	public float shootingCD = 0.0f;

	public enum PlayerColorState{GREEN, YELLOW};
	public PlayerColorState playerColorState = PlayerColorState.GREEN;
	public Material[] stateMaterials;
	MeshRenderer playerMeshRend;

	// Use this for initialization
	void Start () 
	{
		playerMeshRend = playerModel.GetComponent<MeshRenderer>();

		Object[] loadedBullets = Resources.LoadAll("Bullets", typeof(GameObject));
		for(int i=0; i<loadedBullets.Length; i++)
		{
			bulletsDict.Add (loadedBullets[i].name, loadedBullets[i] as GameObject);
//			print ("loaded Bullet: "+loadedBullets[i].name);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		checkWallDistanceOnXAxis(Input.acceleration.x);

		timeSinceLastBullet -= Time.deltaTime;

		if(Input.touchCount > 0)
		{
			if(Input.GetTouch(0).phase == TouchPhase.Moved)
			{
				//Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

				//transform.Translate(touchDeltaPosition.x * sensitivity, touchDeltaPosition.y * sensitivity, 0.0f);
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Began)
			{
				/*Ray touchRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

				RaycastHit hit;
				if(Physics.Raycast(touchRay, out hit, 100.0f, layerMask))
				{
					Debug.DrawLine(touchRay.origin,hit.point);
					Debug.Log(hit.collider.name);*/

				if(timeSinceLastBullet < 0.0f)
				{
					foreach(Transform shootPosition in shootPositionList)
					{
						//shootPosition.LookAt(hit.point);

						GameObject bullet = Instantiate(bulletsDict[currentBulletID], shootPosition.position, shootPosition.rotation) as GameObject;
						bullet.transform.parent = bulletParent;
						bullet.GetComponent<BulletHandling>().setBulletState(playerColorState);
						timeSinceLastBullet = shootingCD;
					}
				}
			}
		}
	}

	void handlePlayerStateChange(PlayerColorState newPlayerColorState)
	{
		if(playerColorState != newPlayerColorState)
		{
			playerColorState = newPlayerColorState;
			
			switch(playerColorState)
			{
			case PlayerColorState.GREEN: playerMeshRend.material = stateMaterials[0];
				break;
			case PlayerColorState.YELLOW: playerMeshRend.material = stateMaterials[1];
				break;
			}
		}
	}

	void checkWallDistanceOnXAxis(float gyroScopeX)
	{
		RaycastHit wallHit;
		if(gyroScopeX > 0.0f)
		{
			if(Physics.Raycast(this.transform.position, Vector3.right, out wallHit, layerMaskWALL))
			{
				Debug.DrawLine(this.transform.position,wallHit.point);
				if(wallHit.distance > shipWidth)
				{
					moveOnXAxis(gyroScopeX);
				}
			}
		}
		else if(gyroScopeX < 0.0f)
		{
			if(Physics.Raycast(this.transform.position, Vector3.left, out wallHit, layerMaskWALL))
			{
				Debug.DrawLine(this.transform.position,wallHit.point);
				if(wallHit.distance > shipWidth)
				{
					moveOnXAxis(gyroScopeX);
				}
			}
		}
		playerModel.transform.localEulerAngles = new Vector3(0.0f, 0.0f, -(gyroScopeX * 20.0f));
	}

	void moveOnXAxis(float gyroScopeX)
	{
		this.transform.Translate((gyroScopeX * sensitivity) * Time.deltaTime, 0.0f, 0.0f);
		this.transform.position = new Vector3(this.transform.position.x, 0.0f, 0.0f);
	}
}
