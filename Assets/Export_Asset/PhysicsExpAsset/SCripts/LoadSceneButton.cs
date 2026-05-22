using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "SecondScene";

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
