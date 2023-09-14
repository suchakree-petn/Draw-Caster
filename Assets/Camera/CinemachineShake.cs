using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance;
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    float time;

    private void Awake()
    {
        Instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();


    }
    void Start()
    {
    }

    public void Shake(GameObject target)
    {
        CinemachineBasicMultiChannelPerlin channelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        channelPerlin.m_AmplitudeGain = 6;
        time = 0.1f;
        StartCoroutine(ShakeDuration(time));
    }
    // Update is called once per frame
    void Update()
    {
        // if (time > 0)
        // {
        //     time -= Time.deltaTime;
        //     if (time <= 0)
        //     {
        //         channelPerlin = GetComponent<CinemachineBasicMultiChannelPerlin>();
        //         channelPerlin.m_AmplitudeGain = 0f;
        //     }
        // }
    }
    IEnumerator ShakeDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        CinemachineBasicMultiChannelPerlin channelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        channelPerlin.m_AmplitudeGain = 0f;

    }

    private void OnEnable() {
        //GameController.OnEnemyTakeDamage += Shake;
    }
}
