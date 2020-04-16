using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class HopTrack : MonoBehaviour
{

	[SerializeField] private HopePlatform m_Platform;
	private  List<GameObject> platforms = new List<GameObject>();
	
	// Use this for initialization
	void Start () {
		
		Random.InitState(1010);
		
		platforms.Add(m_Platform.gameObject);

		for (int i = 0; i < 25; i++)
		{
			var obj = Instantiate(m_Platform.gameObject, transform);
			Vector3 pos = Vector3.zero;

			pos.z = 2 * (i + 1);
			pos.x = Random.Range(-1, 2);
			obj.transform.position = pos;

			// obj.name = $"Platform {i}";
			obj.name = "Platform " + i;

		}
	}

	public bool IsBallOnPlatform(Vector3 position)
	{
		position.y = 0f;

		GameObject nearestPlatform = platforms[0];

		for (int i = 0; i < platforms.Count; i++)
		{
			if (platforms[i].transform.position.z + 0.5f < position.z)
			{
				continue;
			}
            
			if (platforms[i].transform.position.z - position.z > 1f)
			{
				continue;
			}

			nearestPlatform = platforms[i];
			break;
		}

		float minX = nearestPlatform.transform.position.x - 0.5f;
		float maxX = nearestPlatform.transform.position.x + 0.5f;

		bool isDone =  position.x > minX && position.x < maxX;

		if (isDone)
		{
			HopePlatform platform = nearestPlatform.GetComponent<HopePlatform>();
		}

		return isDone;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
