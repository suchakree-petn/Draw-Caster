using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ToNextStage : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject IUBoxPrefab;
    public void NextScene()
    {
        List<string> sceneList = GameController.Instance.scene;
        Scene scene = SceneManager.GetActiveScene();
        int nextScene = scene.buildIndex + 1;
        // if (nextScene < sceneList.Count)
        // {
        //     SceneManager.LoadScene(sceneList[nextScene]);
        // }
        // else
        // {
        //     nextScene = 0;
        //     SceneManager.LoadScene(sceneList[nextScene]);
        // }
        SceneManager.LoadScene(nextScene);

    }
    public void ShowInteractUI()
    {
        IUBoxPrefab.SetActive(true);
        Debug.Log("ShowInteractUI");
    }
    public void HideInteractUI()
    {
        IUBoxPrefab.SetActive(false);
        Debug.Log("HideInteractUI");
    }
    public void Interact(InputAction.CallbackContext context)
    {
        NextScene();
        PlayerInputSystem.Instance.playerAction.Player.Interact.Disable();
        PlayerInputSystem.Instance.playerAction.Player.Interact.performed -= Interact;
    }
}

