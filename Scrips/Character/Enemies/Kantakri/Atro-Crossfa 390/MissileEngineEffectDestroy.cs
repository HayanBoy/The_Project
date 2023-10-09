using UnityEngine;

public class MissileEngineEffectDestroy : MonoBehaviour
{
    public Transform smokePos;

    float particleEmissionRate;
    ParticleSystem FireSmoke;
    ParticleSystem.EmissionModule particleSystemEmissionModule;

    //�Ʒ��� ������ ���� �ҷ��´�.
    void Awake()
    {
        FireSmoke = GetComponent<ParticleSystem>(); //��ƼŬ �ý��� ��������
        particleSystemEmissionModule = FireSmoke.emission; //��ƼŬ ���� ��� ����
    }

    void Update()
    {
        if (smokePos != null)
        {
            transform.position = smokePos.position;
        }
        else
        {
            particleSystemEmissionModule.rateOverTime = particleEmissionRate;
            particleEmissionRate = 0;
            Destroy(gameObject, 5);
        }
    }
}