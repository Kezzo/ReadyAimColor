using UnityEngine;
using System.Collections;

public class ObstacleState : MonoBehaviour {

	public Material[] materials = new Material[3];
	public GameObject model;
	MeshRenderer meshRend;

	public void setStateAndActive(int stateID)
	{
		if(meshRend == null)
		{
			meshRend = GetMeshRenderer();
		}

		meshRend.material = materials[stateID];
		this.gameObject.SetActive(true);

		switch(stateID)
		{
			case 0: setStateRED();
				break;
			case 1: setStateGREEN();
				break;
			case 2: setStateYELLOW();
				break;
		}
	}

	MeshRenderer GetMeshRenderer()
	{
		return model.GetComponent<MeshRenderer>();
	}

	void setStateRED()
	{
		print("RED");
	}

	void setStateGREEN()
	{
		print("GREEN");
	}

	void setStateYELLOW()
	{
		print("YELLOW");
	}
}
