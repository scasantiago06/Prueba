using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour, IReactableObject
{
    public float timeLife;
    public float velocity;

    public EnemyController missile_Enemy;
    GameObject target;
    bool isOut = true;
    float Temporaldist;
    float dist;
    int counter = 5;

    /// <summary>
    /// Se llenan las variables "missile_Enemy" y "target" con el objeto que se indica
    /// </summary>
    private void Awake()
    {
        target = GameObject.Find("Player");
    }

    /// <summary>
    /// Esta función se llama desde la clase "PoolObjects" que se encarga de llamar la función que reinicia el objeto
    /// </summary>
    void Reinstanciar()
    {
        missile_Enemy = FindObjectOfType<EnemyController>();
        Invoke("DesactiveGO", timeLife);
    }

    /// <summary>
    /// La función cancela el funcionamiento de la bala para disponerla a una nueva instancia
    /// </summary>
    void DesactiveGO()
    {
        isOut = true;
        CancelInvoke("MoveRight");
        CancelInvoke("MoveLeft");
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Esta funcion es llamada para desactivar el objeto
    /// </summary>
    public void React()
    {
        DesactiveGO();
    }

    /// <summary>
    /// "OnCollisionEnter2D" activa la función "React" del objeto con el que colisionó y desactiva este objeto.
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<IReactableObject>() != null)
        {
            IReactableObject reactObj = collision.gameObject.GetComponent<IReactableObject>();
            reactObj.React();
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// En el "Update" se llama la función de mover el objeto según el flip que tenga el objeto que instancia este misil
    /// El booleano asegura que no se cambie de dirección una vez el flip cambie.
    /// </summary>
    void Update()
    {
        if (isOut)
        {
            if (missile_Enemy.ESprite.flipX == true)
            {
                Invoke("MoveRight", 0);
            }
            else
            {
                Invoke("MoveLeft", 0);
            }
            isOut = false;
        }
    }

    /// <summary>
    /// Esta función mueve el objeto hacia la derecha y se llama a sí misma para que este no se detenga
    /// y se asegura que el flip este correcto según la dirección a la que va.
    /// </summary>
    void MoveRight()
    {
        gameObject.GetComponent<SpriteRenderer>().flipX = true;
        transform.position += Vector3.right * velocity * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, target.transform.position, .075f * Time.deltaTime);
        InvokeRepeating("MoveRight", 0, 0);
    }

    /// <summary>
    /// Esta función mueve el objeto hacia la izquierda y se llama a sí misma para que este no se detenga
    /// y se asegura que el flip este correcto según la dirección a la que va.
    /// </summary>
    void MoveLeft()
    {

        gameObject.GetComponent<SpriteRenderer>().flipX = false;
        transform.position += Vector3.left * velocity * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, target.transform.position, .075f * Time.deltaTime);
        InvokeRepeating("MoveLeft", 0, 0);
    }
}
