using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tips : MonoBehaviour
{
    public void ClickTips()
    {
        string currentSceneName = GetSceneName();
        if (currentSceneName == "Tutorial 1")
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (currentSceneName == "Tutorial 2")
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (currentSceneName == "Tutorial 3")
        {
            transform.GetChild(2).gameObject.SetActive(true);
        }
    }
    private string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
}
