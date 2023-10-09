using UnityEngine;

public class MissileEngineEffectDestroy2 : MonoBehaviour
{
    public Transform smokePos;

    float particleEmissionRate;
    ParticleSystem FireSmoke;
    ParticleSystem.EmissionModule particleSystemEmissionModule;

    //아래의 내역을 먼저 불러온다.
    void Awake()
    {
        FireSmoke = GetComponent<ParticleSystem>(); //파티클 시스템 가져오기
        particleSystemEmissionModule = FireSmoke.emission; //파티클 분출 요소 지정
    }

    void Update()
    {
        if (smokePos == null)
        {
            particleSystemEmissionModule.rateOverTime = particleEmissionRate;
            particleEmissionRate = 0;
            Destroy(gameObject, 5);
        }
    }
}