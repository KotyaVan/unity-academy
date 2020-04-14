using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;


public class NextFigure : MonoBehaviour
{
    private Random _random = new Random();
    private GameObject _figure;


    // Use this for initialization
    void Start()
    {
        GenerateFigure();
    }

    private void GenerateFigure()
    {
        int figureName = _random.Next(4);

        Debug.Log("Random " + figureName);

        switch (figureName)
        {
            case 0:
                _figure = GameObject.CreatePrimitive(PrimitiveType.Cube);
                break;
            case 1:
                _figure = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                break;
            case 2:
                _figure = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                break;
            case 3:
                _figure = GameObject.CreatePrimitive(PrimitiveType.Quad);
                break;
            case 4:
                _figure = GameObject.CreatePrimitive(PrimitiveType.Plane);
                break;
                
        }
    }

    public GameObject GetFigureForFalling()
    {
        return _figure;
    }

    public void UpdateNextFigure()
    {
        Destroy(_figure);
        GenerateFigure();
    }
}