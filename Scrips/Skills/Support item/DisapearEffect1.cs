using System.Collections;
using UnityEngine;

public class DisapearEffect1 : MonoBehaviour
{
    public Transform DT37AmmoPos;
    public Transform DS65AmmoPos;
    public Transform DP9007AmmoPos;
    public Transform CGD27AmmoPos;
    public Transform ChangeWeaponEnergyPos;
    public Transform M3078BodyPos;
    public Transform ASC365BodyPos;
    public int Type;

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
        if (Type == 1)
        {
            if (DT37AmmoPos == null)
            {
                particleSystemEmissionModule.rateOverTime = particleEmissionRate;
                particleEmissionRate = 0;
                Destroy(gameObject, 5);
            }
        }
        else if (Type == 1000)
        {
            if (DS65AmmoPos == null)
            {
                particleSystemEmissionModule.rateOverTime = particleEmissionRate;
                particleEmissionRate = 0;
                Destroy(gameObject, 5);
            }
        }
        else if (Type == 2000)
        {
            if (DP9007AmmoPos == null)
            {
                particleSystemEmissionModule.rateOverTime = particleEmissionRate;
                particleEmissionRate = 0;
                Destroy(gameObject, 5);
            }
        }
        else if (Type == 3000)
        {
            if (CGD27AmmoPos == null)
            {
                particleSystemEmissionModule.rateOverTime = particleEmissionRate;
                particleEmissionRate = 0;
                Destroy(gameObject, 5);
            }
        }
        else if (Type == 4999)
        {
            if (ChangeWeaponEnergyPos == null)
            {
                particleSystemEmissionModule.rateOverTime = particleEmissionRate;
                particleEmissionRate = 0;
                Destroy(gameObject, 5);
            }
        }
        else if (Type == 5000)
        {
            if (M3078BodyPos == null)
            {
                particleSystemEmissionModule.rateOverTime = particleEmissionRate;
                particleEmissionRate = 0;
                Destroy(gameObject, 5);
            }
        }
        else if (Type == 5001)
        {
            if (ASC365BodyPos == null)
            {
                particleSystemEmissionModule.rateOverTime = particleEmissionRate;
                particleEmissionRate = 0;
                Destroy(gameObject, 5);
            }
        }
    }
}