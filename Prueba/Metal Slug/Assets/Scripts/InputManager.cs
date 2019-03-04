using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerController input_Player;

    /// <summary>
    /// Se llena la variable "input_Player" con el objeto que se indica en el tipo
    /// </summary>
    private void Awake()
    {
        input_Player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        SendToPlayer();
    }

    /// <summary>
    /// Esta función controla los inputs, y llama la función correspondiente segun el input presionado
    /// </summary>
    void SendToPlayer()
    {
        if (Input.GetKey(KeyCode.A))
        {
            input_Player.Move(-1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            input_Player.Move(1);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            input_Player.Grenade();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            input_Player.Jump(1);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            input_Player.Shoot();
        }
    }
}
