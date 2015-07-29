using UnityEngine;
using System.Collections;

public class ObstacleState : MonoBehaviour {

	public Material[] materials = new Material[3];
	public GameObject model;
	MeshRenderer meshRend;

	public enum ColorState {RED, GREEN, YELLOW, DISABLED};

	ColorState colorState = ColorState.DISABLED;

	public void setStateAndActive(int stateID)
	{
		if(meshRend == null)
		{
			meshRend = GetMeshRenderer();
		}

		if(stateID < 3)
		{
			meshRend.material = materials[stateID];

			this.gameObject.SetActive(true);
			
			switch(stateID)
			{
			case 0: colorState = ColorState.RED;
				break;
			case 1: colorState = ColorState.GREEN;
				break;
			case 2: colorState = ColorState.YELLOW;
				break;
			}
		}
		else
		{
			this.gameObject.SetActive(false);
		}

	}

	public ColorState getObstacleState()
	{
		ColorState returnColorState = colorState;
		return returnColorState;
	}

	MeshRenderer GetMeshRenderer()
	{
		return model.GetComponent<MeshRenderer>();
	}
}
