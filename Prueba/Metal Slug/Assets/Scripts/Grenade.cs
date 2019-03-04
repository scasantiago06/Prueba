using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float timeLife;
    public float velocity;

    Rigidbody2D grenadeRB;
    PlayerController grenade_Player;
    bool isOut = true;
    
    /// <summary>
    /// Se llena la variable "grenade_Player" y "grenadeRB" con el objeto que se indica
    /// </summary>
    private void Awake()
    {
        grenade_Player = FindObjectOfType<PlayerController>();
        grenadeRB = gameObject.GetComponent<Rigidbody2D>();
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
        if (!collision.collider.GetComponent<PlayerController>() && collision.gameObject.GetComponent<IReactableObject>() != null)
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
            if (grenade_Player.PSprite.flipX == false)
            {
                Invoke("MoveRight", 0);
            }
            else
                Invoke("MoveLeft", 0);
            isOut = false;
            
            grenadeRB.AddForce(transform.up * 300);
        }

    }

    /// <summary>
    /// Esta función mueve el objeto hacia la derecha aplicando una fuerza
    /// </summary>
    void MoveRight()
    {
        grenadeRB.AddForce(transform.right * 150);
    }

    void MoveLeft()
    {
        grenadeRB.AddForce(-transform.right * 150);
    }
}
