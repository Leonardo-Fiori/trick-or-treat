﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFog : MonoBehaviour {
    private int x;
    private int y;

    private bool raised = false;
    private bool active = true;

    public GameObject fogPrefab;

    //public GameObject spotLight;
    //private float lightIntensity;
    //private Light lightComponent;

    public bool GetStatus()
    {
        return active;
    }

    public void SetFog(bool status)
    {
        active = status;
        if (status == false) deactivateFog();
        else if (status == true) activateFog();
    }

    private IEnumerator deactivateAnimation()
    {
        while(fogPrefab.transform.localScale.x > 0.1f)
        {
            fogPrefab.transform.localScale = fogPrefab.transform.localScale * .9f;
            yield return null;
        }
        fogPrefab.GetComponent<ParticleSystem>().Stop();
        fogPrefab.SetActive(false);
        transform.localScale = new Vector3(1f, 0.5f, 1f);
    }

    private void deactivateFog()
    {
        StartCoroutine(deactivateAnimation());

        if(!raised)
            //StartCoroutine(RaiseTile());

        StartCoroutine(InflateTile());

        return;
    }

    private void activateFog()
    {
        fogPrefab.SetActive(true);
        fogPrefab.GetComponent<ParticleSystem>().Play();
        return;
    }

    public IEnumerator RaiseTile()
    {
        raised = true;
        Vector3 destination = transform.position;
        transform.position += Vector3.down * 2;
        
        while(transform.position.y < destination.y)
        {
            //transform.position = (transform.position - destination) / 20;
            transform.position += Vector3.up / 20;
            yield return null;
        }
    }

    public IEnumerator InflateTile()
    {
        Vector3 destination = new Vector3(1f, .5f, 1f);

        while (transform.localScale.x < destination.x)
        {
            transform.localScale += destination / 10f;
            yield return null;
        }
    }

    // Spawna la nebbia
	void Start () {
        x = TileCoords.GetX(gameObject);
        y = TileCoords.GetY(gameObject);

        transform.localScale = new Vector3(0f, 0f, 0f);

        fogPrefab = Instantiate(fogPrefab, transform);

        //spotLight = Instantiate(spotLight, transform);
        //lightComponent = spotLight.GetComponent<Light>();
        //lightIntensity = lightComponent.intensity;
        //lightComponent.intensity = 0f;
	}

}