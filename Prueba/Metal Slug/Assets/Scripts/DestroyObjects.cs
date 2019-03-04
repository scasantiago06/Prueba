using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour, IReactableObject
{
    Color BColor;
    int counter = 0;
    public GameObject explosion;
    [SerializeField]
    int maxHits;

    /// <summary>
    /// A la variable "BColor" se le asigna el sprite de este objeto
    /// </summary>
    void Start()
    {
        BColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    /// <summary>
    /// "React" se llama cuando el objeto es golpeado por la bala y se encarga de de cambiar el color para hacer saber
    /// que fue golpeado
    /// </summary>
    public void React()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(gameObject.GetComponent<SpriteRenderer>().color, Color.white, 1);
        StartCoroutine(ReturnColor());
        counter++;
    }

    /// <summary>
    /// Esta corrutina hace que el color vuelva a ser el de antes después de un tiempo, y tambien es responsable de
    /// desactivarlo cuando la condición se cumpla a la vez que activa el objeto "explosion"
    /// </summary>
    /// <returns></returns>
    IEnumerator ReturnColor()
    {
        yield return new WaitForSeconds(0.05f);
        gameObject.GetComponent<SpriteRenderer>().color = BColor;
        if (counter == maxHits)
        {
            explosion.transform.position = gameObject.transform.position;
            explosion.SetActive(true);
            yield return new WaitForSeconds(0.933f);
            gameObject.SetActive(false);
            explosion.SetActive(false);
        }
    }
}
