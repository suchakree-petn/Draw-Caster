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
        List<string> scenes = GameController.Instance.scene;
        int nextScene = scenes.IndexOf(scene.name) + 1;
        SceneManager.LoadScene(scenes[nextScene]);
        GameController.Instance.currentState = GameState.InstantiateUI;
        GameController.Instance.doorToNextStage.SetActive(false);
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

