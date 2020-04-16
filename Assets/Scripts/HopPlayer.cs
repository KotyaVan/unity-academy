using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HopPlayer : MonoBehaviour
{

	[SerializeField] private AnimationCurve jumpCurve;
	[SerializeField] private float m_JumpHeight = 1f;
	[SerializeField] private float m_JumpDistance = 2f;

	[SerializeField] private float m_BallSpeed = 1f;
	[SerializeField] private HopInput m_Input;
	[SerializeField] private HopTrack m_Track;

	private float iteration;
	private float startZ;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 pos = transform.position;

		pos.x = Mathf.Lerp(pos.x, m_Input.Strafe, Time.deltaTime * 5f);

		pos.y = jumpCurve.Evaluate(iteration) * m_JumpHeight;

		pos.z = startZ + iteration * m_JumpDistance;

		transform.position = pos;
		
		iteration += Time.deltaTime * m_BallSpeed;

		if (iteration < 1f)
		{
			return;
		}

		iteration = 0f;
		startZ += m_JumpDistance;

		if ( m_Track.IsBallOnPlatform(transform.position))
		{
			return;
		}
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
