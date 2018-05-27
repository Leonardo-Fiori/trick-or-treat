﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    protected int x;
    protected int y;
    private bool spawned;

    IEnumerator despawnAnimation()
    {
        while (transform.localScale.x >= 0.01f)
        {
            transform.localScale *= 0.9f;
            yield return null;
        }
    }

    IEnumerator spawnAnimation()
    {
        while (transform.localScale.x <= 1f)
        {
            transform.localScale += Vector3.one * 0.1f;

            if (Vector3.Distance(transform.localScale, Vector3.one) <= 0.1f)
            {
                transform.localScale = Vector3.one;
            }

            yield return null;
        }
    }

    protected void Spawn()
    {
        spawned = true;
        StopCoroutine(despawnAnimation());
        StartCoroutine(spawnAnimation());
    }

    protected void Despawn()
    {
        spawned = false;
        StopCoroutine(spawnAnimation());
        StartCoroutine(despawnAnimation());
    }

    public void Think()
    {
        int wX = GameManager.witchInstance.GetX();
        int wY = GameManager.witchInstance.GetY();

        bool witch = (x == wX && y == wY);

        int pX = GameManager.playerInstance.getX();
        int pY = GameManager.playerInstance.getY();

        bool player = (x == pX && y == pY);

        if (witch || player)
        {
            Despawn();
        }
        else
        {
            Spawn();
        }
    }

    private void Start()
    {
        transform.localScale = Vector3.zero;
        spawned = false;
        x = GameManager.mapInstance.GetUscitaX();
        y = GameManager.mapInstance.GetUscitaY();
    }
}