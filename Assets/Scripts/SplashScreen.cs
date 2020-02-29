using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(ChangeScene), 2.0f);
    }

    private void ChangeScene()
    {
        SceneManagement.Instance.NextScene();
    }
}
