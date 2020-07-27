﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody ballRig;
    [SerializeField] private float speed = 5f;
    [SerializeField] private int damage = 1;
    [SerializeField] private AudioClip audioClip;

    private float baseSpeed;
    private AudioSource audioSource;
    
    private void Start()
    {
        if (audioClip)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = audioClip;
            audioSource.playOnAwake = false;
        }
    }

    private void Reset()
    {
        ballRig = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update

    private void OnCollisionEnter(Collision collision)
    {
        var health = collision.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.SetDamage(damage);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var normalizedVelocity = ballRig.velocity.normalized;
        ballRig.velocity = Vector3.Lerp(ballRig.velocity, normalizedVelocity * speed, Time.deltaTime * 5f);
    }
}
