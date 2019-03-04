using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjects : MonoBehaviour
{
    static Dictionary<int, Queue<GameObject>> pool = new Dictionary<int, Queue<GameObject>>();
    
    /// <summary>
    /// Esta función añade al diccionario el objeto pasado como parametro, y crea la cantidad de veces que se
    /// le ordene, a la vez que el objeto creado se vuelve hijo del transform "bulletFather".
    /// </summary>
    /// <param name="objectToInstance"></param>
    /// <param name="amount"></param>
    /// <param name="bulletFather"></param>
    public static void ObjectInstance(GameObject objectToInstance, int amount, Transform bulletFather)
    {
        pool.Add(objectToInstance.GetInstanceID(), new Queue<GameObject>());

        for(int i = 0; i < amount; i++)
        {
            GameObject go = Instantiate(objectToInstance) as GameObject;
            go.transform.SetParent(bulletFather);
            pool[objectToInstance.GetInstanceID()].Enqueue(go);
            go.SetActive(false);
        }
    }

    /// <summary>
    /// Esta función es la que se emcarga de utilizar el objeto que sigue y luego recolocarlo de nuevo en la lista.
    /// </summary>
    /// <param name="objectToInstance"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    public static void Recicle(GameObject objectToInstance, Vector3 position, Quaternion rotation)
    {
        if (pool.ContainsKey(objectToInstance.GetInstanceID()))
        {
            GameObject go = pool[objectToInstance.GetInstanceID()].Dequeue();
            go.transform.position = position;
            go.transform.rotation = rotation;
            go.SetActive(true);
            go.SendMessage("Reinstanciar", SendMessageOptions.DontRequireReceiver);
            pool[objectToInstance.GetInstanceID()].Enqueue(go);
        }
    }
}
