using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Transform bigMissileSpawn;
    Instantiator instantiator;

    Transform centerPoint;
    public bool canShoot;
    bool controller = true;
    float dist;

    /// <summary>
    /// Se llenan las variables "instantiator", "centerPoint" y "bigMissileSpawn" con el objeto que se indica en c/u
    /// </summary>
    void Awake()
    {
        instantiator = GameObject.Find("InstanceManager").GetComponent<Instantiator>();
        centerPoint = GameObject.Find("CenterPoint").transform;
        bigMissileSpawn = GameObject.Find("DogHeadSpawn").transform;
    }
    
    /// <summary>
    /// Se controla cuando se llama la corrutina "Shoot"
    /// </summary>
    void Update()
    {
        dist = Vector3.Distance(transform.position, centerPoint.position);

        if (dist < 12.7f)
            canShoot = true;
        else if (dist >= 12.7)
        {
            canShoot = false;
        }

        if (canShoot && controller)
        {
            StartCoroutine(Shoot());
            controller = false;
        }
        else if (!canShoot && !controller)
        {
            StopCoroutine(Shoot());
            controller = true;
        }
    }


    /// <summary>
    /// Esta corrutina dispara los misiles cada cierta cantidad de tiempo
    /// </summary>
    /// <returns></returns>
    IEnumerator Shoot()
    {
        instantiator.DogHeadInstance(bigMissileSpawn);
        yield return new WaitForSeconds(6);
        if (canShoot)
            StartCoroutine(Shoot());

    }
}
