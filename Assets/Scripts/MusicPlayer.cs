using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Invoke(nameof(LoadFirstScene), 2.0f);
    }


    void LoadFirstScene()
    {
        SceneManagement.Instance.NextScene();
    }
}
