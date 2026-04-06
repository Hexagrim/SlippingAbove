using Unity.Cinemachine;
using UnityEngine;


public class CamShake : MonoBehaviour
{

    private CinemachineCamera vcam;
    private CinemachineBasicMultiChannelPerlin noise;

    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntensity;

    void Awake()
    {
        vcam = GetComponent<CinemachineCamera>();
        noise = vcam.GetComponent<CinemachineBasicMultiChannelPerlin>();
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;

            noise.AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, 1f - (shakeTimer / shakeTimerTotal));
        }
        else
        {
            noise.AmplitudeGain = 0f;
            noise.FrequencyGain = 0;
        }
    }

    public void Shake(float intensity, float time)
    {
        noise.AmplitudeGain = intensity;
        noise.FrequencyGain = 2f; 

        startingIntensity = intensity;
        shakeTimer = time;
        shakeTimerTotal = time;
    }
}
