using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private int sceneIndex;
    [SerializeField] private LoadSceneMode loadMode;
    public void Load()
    {
        SceneManager.LoadScene(sceneIndex,loadMode);
    }
}
