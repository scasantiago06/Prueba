using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IReactableObject
{
    Instantiator instantiator;
    PlayerController enemy_Player;
    SpriteRenderer enemySprite;
    Transform centerPoint;
    public bool isStatic;
    public bool canShoot;
    bool controller = true;
    float dist;

    /// <summary>
    /// Propiedad con la que se obtiene el sprite del enemigo
    /// </summary>
    public SpriteRenderer ESprite
    {
        get
        {
            return enemySprite;
        }
    }

    /// <summary>
    /// Se llenan las variables "instantiator", "enemy_player" y "enemySprite" con el objeto que se indica en c/u
    /// </summary>
    void Awake()
    {
        instantiator = GameObject.Find("InstanceManager").GetComponent<Instantiator>();
        enemy_Player = GameObject.Find("Player").GetComponent<PlayerController>();
        enemySprite = GetComponent<SpriteRenderer>();
        centerPoint = GameObject.Find("CenterPoint").transform;
    }

    /// <summary>
    /// Se llama constantemente la función "Move" y se controla cuando se llama la corrutina "Shoot"
    /// </summary>
    void Update()
    {
        dist = Vector3.Distance(transform.position, centerPoint.position);
        Move();

        if (dist < 12.7f)
            canShoot = true;
        else if(dist >= 12.7)
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
    /// Esta función se encarga de mover el enemigo segun el booleano y dar el correspondiente flip según la dirección
    /// </summary>
    void Move()
    {
        if(isStatic == false && dist < 20)
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, enemy_Player.transform.position.x, 0.5f * Time.deltaTime), Mathf.Lerp(transform.position.y, enemy_Player.transform.position.y, 0.5f * Time.deltaTime), 0);

        if(enemy_Player.transform.position.x < transform.position.x)
            enemySprite.flipX = false;

        else
            enemySprite.flipX = true;
    }

    /// <summary>
    /// "React" se llama para desactivar el objeto
    /// </summary>
    public void React()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Esta corrutina dispara los misiles cada cierta cantidad de tiempo
    /// </summary>
    /// <returns></returns>
    IEnumerator Shoot()
    {
        instantiator.MissileInstance(gameObject.GetComponent<EnemyController>());
        yield return new WaitForSeconds(2.534f);
        if(canShoot)
            StartCoroutine(Shoot());

    }
}
//1.267