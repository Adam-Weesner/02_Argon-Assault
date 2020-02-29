using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonObj<T> : MonoBehaviour where T : MonoBehaviour
{
    private void Awake()
    {
        var sameObjects = FindObjectsOfType<T>();

        if (sameObjects.Length <= 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
