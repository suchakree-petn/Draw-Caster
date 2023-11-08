using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tips : MonoBehaviour
{
    public void ClickTips()
    {
        string currentSceneName = GetSceneName();
        if (currentSceneName == "Stage_1_Learning")
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (currentSceneName == "Stage_2_Learning")
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (currentSceneName == "Stage_3_Learning")
        {
            transform.GetChild(2).gameObject.SetActive(true);
        }
    }
    private string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
}
