using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour
{
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    GameObject missilePrefab;
    [SerializeField]
    GameObject grenadePrefab;
    [SerializeField]
    GameObject dogHead;
    PlayerController inst_Player;

    /// <summary>
    /// Se llena la variable "inst_Player" con el objeto que se indica
    /// </summary>
    private void Awake()
    {
        inst_Player = FindObjectOfType<PlayerController>();
    }

    /// <summary>
    /// La función "Start" crea los objetos para añadirlos al diccionario.
    /// </summary>
    void Start()
    {
        PoolObjects.ObjectInstance(bulletPrefab, 25, GameObject.Find("BulletsFather").transform);
        PoolObjects.ObjectInstance(missilePrefab, 15, GameObject.Find("MissileFather").transform);
        PoolObjects.ObjectInstance(grenadePrefab, 25, GameObject.Find("GrenadeFather").transform);
        PoolObjects.ObjectInstance(dogHead, 10, GameObject.Find("DogHeadMissile").transform);
    }

    /// <summary>
    /// Esta función utiliza el objeto que sigue en la lista con la llave de las balas
    /// </summary>
    public void BulletInstance()
    {
        PoolObjects.Recicle(bulletPrefab, inst_Player.BSapwn.position, Quaternion.identity);
    }

    /// <summary>
    /// Esta función utiliza el objeto que sigue en la lista con la llave de los misiles
    /// </summary>
    public void MissileInstance(EnemyController inst_Enemy)
    {
        PoolObjects.Recicle(missilePrefab, inst_Enemy.transform.position, Quaternion.identity);
    }

    /// <summary>
    /// Esta función utiliza el objeto que sigue en la lista con la llave de las granadas
    /// </summary>
    public void GrenadeInstance()
    {
        PoolObjects.Recicle(grenadePrefab, inst_Player.GSapwn.position, Quaternion.identity);
    }

    /// <summary>
    /// Esta función utiliza el objeto que sigue en la lista con la llave de las granadas
    /// </summary>
    public void DogHeadInstance(Transform spawn)
    {
        PoolObjects.Recicle(dogHead, spawn.position, Quaternion.identity);
    }
}
