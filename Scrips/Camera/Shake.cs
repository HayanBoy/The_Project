using System.Collections;
using UnityEngine;
using Cinemachine;

public class Shake : MonoBehaviour
{
    public static Shake Instance { get; private set; }

    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shakeTime;
    private float shakeTimeTotal;
    private float startingIntensity;

    private void Awake()
    {
        Instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }


    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
           cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        startingIntensity = intensity;
        shakeTimeTotal = time;
        shakeTime = time;
    }

    private void Update()
    {
        // �� �����ϰ� �����ϰ� ��鸮�� ���� 
        //if(shakeTime > 0){
        //    shakeTime -= Time.deltaTime;
        //    if (shakeTime <= 0f)
        //    {
        //        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
        //         cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        //        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
        //    }   
        //}

        // �ε巴�� lerp�� ���� 
        if (shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
             cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain
                = Mathf.Lerp(startingIntensity, 0f, (1 - (shakeTime / shakeTimeTotal)));
        }
    }
}