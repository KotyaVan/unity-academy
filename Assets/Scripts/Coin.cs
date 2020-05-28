using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            GameManager.Coins++;
            StartCoroutine(MoveUp());
            GetComponent<Collider2D>().enabled = false;
        }
    }

    private IEnumerator MoveUp()
    {
        var timer = 1f;

        while (timer > 0f)
        {
            transform.Translate(Vector2.up * Time.deltaTime);
            timer -= Time.deltaTime;
            yield return null;
        }
        
        Destroy(gameObject);
    }
}