using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    Animator playerAnimator;

    /// <summary>
    /// Se llenan las variables "missile_Enemy" y "target" con el objeto que se indica
    /// </summary>
    private void Start()
    {
        playerAnimator = gameObject.GetComponent<Animator>();
    }

    /// <summary>
    /// Esta función da paso a la animación de disparar
    /// </summary>
    public void ShootAnimation()
    {
        playerAnimator.SetBool("IsShooting", true);
    }

    /// <summary>
    /// Esta función da paso a la animación inicial
    /// </summary>
    public void IdleAnimation()
    {
        playerAnimator.SetBool("IsShooting", false);
    }
}
