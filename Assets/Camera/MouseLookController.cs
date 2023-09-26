using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using DrawCaster.Util;
using System.Collections;
using DG.Tweening;

public class MouseLookController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;

    public CinemachineVirtualCamera LookCamera;
    private InputAction holdAttack => PlayerInputSystem.Instance.playerAction.Player.HoldAttack;
    [SerializeField] private float sensitive;
    public float holdTimeToActivate;

    private void Update()
    {
        if (virtualCamera.enabled) { return; }

        // Vector3 vCamPos = LookCamera.transform.position;
        // vCamPos.z = -10;
        Vector3 mousePosition = DrawCasterUtil.GetCurrentMousePosition();
        Vector3 playerPos = transform.root.position;
        Vector3 direction = mousePosition - playerPos;
        direction.z = -10;
        LookCamera.transform.DOMove(direction.normalized, sensitive).SetEase(Ease.InOutSine);
        //LookCamera.transform.position = Vector3.Lerp(vCamPos, mousePose, Time.deltaTime * sensitive);
    }
    IEnumerator CountHoldAttack(float holdTimeToActivate)
    {
        yield return new WaitForSeconds(holdTimeToActivate);
        SwitchMainCam(false);
    }

    private void SwitchMainCam(bool toggle)
    {
        virtualCamera.enabled = toggle;
    }

    private void OnEnable()
    {
        holdAttack.performed += (ctx) => StartCoroutine(CountHoldAttack(holdTimeToActivate));
        holdAttack.canceled += (ctx) =>
        {
            StopCoroutine(CountHoldAttack(holdTimeToActivate));
            virtualCamera.enabled = true;
        };
    }
}