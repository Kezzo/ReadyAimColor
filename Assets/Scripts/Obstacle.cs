using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	public Material[] materials = new Material[3];
	public GameObject model;
	MeshRenderer meshRend;

	void Start()
	{
		meshRend = model.GetComponent<MeshRenderer>();
	}

	public void setStateAndActive(int stateID)
	{
		meshRend.material = materials[stateID];
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
