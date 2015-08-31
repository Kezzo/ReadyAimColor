using System.Collections;
using UnityEngine;

public class BulletHandling : MonoBehaviour {

	[SerializeField]
	private Material[] m_stateMaterials;

	[SerializeField]
	private GameObject m_bulletModel;

	[SerializeField]
	private MeshRenderer m_bulletMeshRend;

	private float m_speed = 20.0f;

    private ColorState m_bulletColorState = ColorState.GREEN;

    private HighScoreController m_highScoreController;

    void Start()
    {
        m_highScoreController = HighScoreController.Instance;
    }

    void OnEnable()
    {
        StartCoroutine(DestoryAfter(2.0f));
    }

	// Update is called once per frame
	void Update () 
	{
		this.transform.Translate((Vector3.forward * m_speed) * Time.deltaTime);
		this.transform.localEulerAngles = new Vector3(0.0f, 0.0f, (this.transform.localEulerAngles.z - (120.0f * Time.deltaTime)));
    }

    private IEnumerator DestoryAfter(float secondsToWait)
    {
        yield return new WaitForSeconds(secondsToWait);
        SimplePool.Despawn(this.gameObject);
    }

	void OnTriggerEnter(Collider other)
	{
		//print(other.name);

		switch(other.gameObject.GetComponent<ObstacleState>().getObstacleState())
		{
			case ColorState.RED: redHit();
					break;
			case ColorState.GREEN: greenHit(other.gameObject);
					break;
			case ColorState.YELLOW: yellowHit(other.gameObject);
					break;
		}

		this.gameObject.SetActive(false);
	}

	void redHit()
	{

	}

	void greenHit(GameObject obstacle)
	{
		if(m_bulletColorState == ColorState.GREEN)
		{
			obstacle.SetActive(false);
            m_highScoreController.UpdateHighScoreBy(1);
        }

	}

	void yellowHit(GameObject obstacle)
	{
		if(m_bulletColorState == ColorState.YELLOW)
		{
			obstacle.SetActive(false);
            m_highScoreController.UpdateHighScoreBy(1);
        }
	}

	public void setBulletState(ColorState playerColorState)
	{
		if(playerColorState != m_bulletColorState)
		{
			if(m_bulletMeshRend == null)
			{
				m_bulletMeshRend = m_bulletModel.GetComponent<MeshRenderer>();
			}

			switch(playerColorState)
			{
			    case ColorState.GREEN: m_bulletMeshRend.sharedMaterial = m_stateMaterials[0];
				    break;
			    case ColorState.YELLOW: m_bulletMeshRend.sharedMaterial = m_stateMaterials[1];
				    break;
			}
			m_bulletColorState = playerColorState;
		}
	}
}
