using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float timeLife;
    public float velocity;

    PlayerController bullet_Player;
    bool isOut = true;

    /// <summary>
    /// Se llena la variable "bullet_Player" con el objeto que se indica en el tipo
    /// </summary>
    private void Awake()
    {
        bullet_Player = FindObjectOfType<PlayerController>();
    }

    /// <summary>
    /// Esta función se llama desde la clase "PoolObjects" que se encarga de llamar la función que reinicia el objeto
    /// </summary>
    void Reinstanciar()
    {
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
    /// "OnCollisionEnter2D" activa la función "React" del objeto con el que colisionó y desactiva este objeto.
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<IReactableObject>() != null && !collision.gameObject.GetComponent<PlayerController>())
        {
            IReactableObject reactObj = collision.gameObject.GetComponent<IReactableObject>();
            reactObj.React();
            gameObject.SetActive(false);
        }   
        if (collision.collider.CompareTag("CameraBorder"))
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// En el "Update" se llama la función de mover el objeto según el flip que tenga el objeto que instancia esta bala
    /// El booleano asegura que no se cambie de dirección una vez el flip cambie.
    /// </summary>
    void Update()
    {
        if (isOut)
        {
            if (bullet_Player.PSprite.flipX == false)
            {
                Invoke("MoveRight", 0);
            }
            else
                Invoke("MoveLeft", 0);
            isOut = false;
        }
    }

    /// <summary>
    /// Esta función mueve el objeto hacia la derecha y se llama a sí misma para que este no se detenga.
    /// </summary>
    void MoveRight()
    {
        transform.position += Vector3.right * velocity * Time.deltaTime;
        InvokeRepeating("MoveRight", 0, 0);
    }
    
    /// <summary>
    /// Esta función mueve el objeto hacia la izquierda y se llama a sí misma para que este no se detenga.
    /// </summary>
    void MoveLeft()
    {
        transform.position += Vector3.left * velocity * Time.deltaTime;
        InvokeRepeating("MoveLeft", 0, 0);
    }

}
