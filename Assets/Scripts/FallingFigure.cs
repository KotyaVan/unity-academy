using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingFigure : MonoBehaviour
{

	[SerializeField] private NextFigure _nextFigure;
	private GameObject _figure;

	// Update is called once per frame
	void Update () {

		if (_figure)
		{
			_figure.transform.Translate(- Vector3.up * Time.deltaTime);

			if (_figure.transform.position.y < -3)
			{
			    Destroy(_figure);
			}
			
		}
		else
		{
			_figure = Instantiate(_nextFigure.GetFigureForFalling());
			_figure.transform.position = new Vector3(0, 0, 0);
			_nextFigure.UpdateNextFigure();
		}

	}
}
