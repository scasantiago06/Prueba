using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IReactableObject
{
    [SerializeField]
    LayerMask floorMask;

    public int speedToMove;
    public int jumpForce;
    float radioTester = 0.04f;
    public bool onGround;

    Instantiator instantiator;
    PlayerAnimationController pAController;
    Rigidbody2D playerRigid;
    SpriteRenderer playerSprite;
    CapsuleCollider2D playerCollider;
    Transform floorTester;
    public Transform bulletSpawn;
    public Transform grenadeSpawn;

    /// <summary>
    /// Propiedad con la que se obtiene el sprite del jugador
    /// </summary>
    public SpriteRenderer PSprite
    {
        get
        {
            return playerSprite;
        }
    }

    /// <summary>
    /// Propiedad con la que se obtiene el transform del "bulletSpawn"
    /// </summary>
    public Transform BSapwn
    {
        get
        {
            return bulletSpawn;
        }
    }

    /// <summary>
    /// Propiedad con la que se obtiene el transform del "grenadeSpawn"
    /// </summary>
    public Transform GSapwn
    {
        get
        {
            return grenadeSpawn;
        }
    }

    /// <summary>
    /// Se llenan las variables con el objeto que se indica
    /// </summary>
    void Awake()
    {
        playerSprite = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        playerCollider = GameObject.Find("Player").GetComponent<CapsuleCollider2D>();
        playerRigid = GetComponent<Rigidbody2D>();
        instantiator = GameObject.Find("InstanceManager").GetComponent<Instantiator>();
        bulletSpawn = GameObject.Find("BulletSpawn").transform;
        pAController = GetComponent<PlayerAnimationController>();
        floorTester = GameObject.Find("FloorTester").GetComponent<Transform>();
    }

    /// <summary>
    /// "FixedUpdate" crea un radio alrededor de "floorTester"
    /// </summary>
    void FixedUpdate()
    {
        onGround = Physics2D.OverlapCircle(floorTester.position, radioTester, floorMask);
    }

    /// <summary>
    /// Se detecta la colision con objetos que tengan el tag de "Danger"
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Danger"))
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// "React" se llama para desactivar el objeto
    /// </summary>
    public void React()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Esta función se encarga de mover el jugador según el parámetro enviado por el "inputManager" y dar el correspondiente flip según la dirección
    /// También mueve el collider y el "bulletSpawn"
    /// </summary>
    public void Move(int dir)
    {
        if (dir > 0)
        {
            playerSprite.flipX = false;
            playerCollider.offset = new Vector2(-0.1f, playerCollider.offset.y);
            bulletSpawn.localPosition = new Vector3(0.3f, 0, 0);
        }
        else
        {
            playerSprite.flipX = true;
            playerCollider.offset = new Vector2(0.1f, playerCollider.offset.y);
            bulletSpawn.localPosition = new Vector3(-0.3f, 0, 0);
        }

        transform.position += new Vector3((dir * speedToMove) * Time.deltaTime, 0);
    }

    /// <summary>
    /// "Jump" añade fuerza el vertical al jugador para generar el salto
    /// </summary>
    /// <param name="jump"></param>
    public void Jump(int jump)
    {
        if(onGround == true)
        {
            playerRigid.AddForce(new Vector2(0, (jump * jumpForce)));
        }
    }

    /// <summary>
    /// Esta función genera la bala, también llama función para activar animación de disparo, a la vez
    /// que es llamada la corrutina para volver de nuevo a la animación inicial
    /// </summary>
    public void Shoot()
    {
        instantiator.BulletInstance();
        pAController.ShootAnimation();
        StopCoroutine(PassToIdle());
        StartCoroutine(PassToIdle());
    }

    /// <summary>
    /// Esta función genera la granada
    /// </summary>
    public void Grenade()
    {
        instantiator.GrenadeInstance();
    }

    /// <summary>
    /// La corrutina "PassToIdle" se encarga de llamar la función que pasa a la animación inicial
    /// </summary>
    /// <returns></returns>
    IEnumerator PassToIdle()
    {
        yield return new WaitForSeconds(0.267f);
        pAController.IdleAnimation();
    }
}
