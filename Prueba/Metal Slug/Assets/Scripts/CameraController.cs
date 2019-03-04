using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static bool canMove = true;
    public Vector2 maxXAndY;
    public Vector2 minXAndY;
    public Transform playerT;

    /// <summary>
    /// Se llenan las variables "missile_Enemy" y "target" con el objeto que se indica
    /// </summary>
    private void Awake()
    {
        playerT = GameObject.Find("Player").transform;
    }

    /// <summary>
    /// Se llama constantemente la función "FollowPlayer"
    /// </summary>
    private void Update()
    {
        if (canMove)
            FollowPlayer();
    }

    /// <summary>
    /// El booleano que se retorna será verdadero si el valor absoluto mayor que cero
    /// </summary>
    ///// <returns></returns>
    bool CheckXMargin()
    {
        return Mathf.Abs(transform.position.x - playerT.position.x) > 0;
    }

    /// <summary>
    /// /// El booleano que se retorna será verdadero si el valor absoluto mayor que cero
    /// </summary>
    /// <returns></returns>
    bool CheckYMargin()
    {
        return Mathf.Abs(transform.position.y - playerT.position.y) > 0;
    }

    /// <summary>
    /// Esta función mueve la cámara a la posicion del jugador, con unos limitantes definidos con
    /// el Math.Clamp
    /// </summary>
    void FollowPlayer()
    {
        float targetX = transform.position.x;
        float targetY = transform.position.y;

        if (CheckXMargin())
        {
            targetX = Mathf.Lerp(transform.position.x, playerT.position.x, 1);
        }

        if (CheckYMargin())
        {
            targetY = Mathf.Lerp(transform.position.y, playerT.position.y, 1);
        }

        targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
        targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);

        if(transform.position.x == maxXAndY.x)
        {
            canMove = false;
        }
            transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
}
