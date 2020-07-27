using System;
using System.Collections;
using System.Collections.Generic;
using ECS;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class EffectController : MonoBehaviour
{
    public static EntityManager EntityManager;

    [SerializeField] private GameObject[] effectPrefabs;
    [SerializeField] private float minSpeed = 1.5f;
    [SerializeField] private float maxSpeed = 2.5f;
    [SerializeField] private uint effectIterations = 40;
    [SerializeField] private float effectStep = 0.1f;
    [SerializeField] private float effectWidth = 0.5f;
    [SerializeField] private int objectsInLine = 3;

    private GameObject[] effectObjects;

    private Coroutine effectCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        effectObjects = new GameObject[effectPrefabs.Length];
        for (int i = 0; i < effectObjects.Length; i++)
        {
            effectObjects[i] = Instantiate(effectPrefabs[i]);
            effectPrefabs[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowEffect(new Vector3(0, 1.2f, -6));
        }
    }

    private void ShowEffect(Vector3 startPoint)
    {
        if (effectCoroutine != null)
        {
            return;
        }

        effectCoroutine = StartCoroutine(EffectProcess(startPoint));
    }

    private IEnumerator EffectProcess(Vector3 startPoint)
    {
        var counter = 0;

        while (counter < effectIterations)
        {
            foreach (var effectObject in effectObjects)
            {
                effectObject.SetActive(true);
            }

            for (int i = 0; i < objectsInLine; i++)
            {
                var x = Random.Range(-effectWidth, effectWidth) + startPoint.x;
                var z = counter * effectStep + startPoint.z;

                var position = new Vector3(x, 0, z);
                var direction = new float3(0, 1, 0);
                var speed = Random.Range(minSpeed, maxSpeed);

                var obj = effectObjects[Random.Range(0, effectObjects.Length)];
                obj.transform.position = position;
                obj.transform.rotation = quaternion.identity;

                SetupObject(obj, direction, speed);
            }

            counter++;

            foreach (var effectObject in effectObjects)
            {
                effectObject.SetActive(false);
            }
            
            yield return null;

        }

        effectCoroutine = null;
    }

    private void SetupObject(GameObject effectObject, float3 direction, float speed)
    {
        var conversionSettings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        var effectEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(effectObject, conversionSettings);

        var movementData = new EffectMovementComponent()
        {
            Direction = direction,
            Speed = speed
        };

        EntityManager.AddComponent<EffectMovementComponent>(effectEntity);
        EntityManager.SetComponentData(effectEntity, movementData);

    }
}