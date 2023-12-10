using UnityEngine;
using Cinemachine;

public class ScreenShakeManager : Singleton<ScreenShakeManager>
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin noise;

    private void GetCamera()
    {
        if (GameObject.Find("CM vcam1").TryGetComponent<CinemachineVirtualCamera>(out virtualCamera))
        {
            noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
        else
        {
            Debug.LogError("CinemachineVirtualCamera component not found!");
        }
    }

    public void Shake(float duration, float amplitude)
    {
        if (virtualCamera == null)
        {
            GetCamera();
        }
        if (noise != null)
        {
            noise.m_AmplitudeGain = amplitude;
            StartCoroutine(StopShake(duration));
        }
    }

    private System.Collections.IEnumerator StopShake(float duration)
    {
        yield return new WaitForSeconds(duration);

        if (noise != null)
        {
            noise.m_AmplitudeGain = 0f;
        }
    }

    protected override void InitAfterAwake()
    {
    }
}
