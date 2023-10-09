using System.Collections;
using UnityEngine;

public class GunSmoke_SW06 : MonoBehaviour
{
    public float fireRate;
    public int Magazines;
    public int AmmoAmount;
    public int MaxAmmoAmount;
    private float timeStamp = 0.0f;
    int NeedAddAmmo = 0;
    int AfterAddAmmo = 0;
    int reloading = 0;
    int ammo = 0;
    int tempAmmo = 0;

    float particleEmissionRate = 0; //���� ������ �� ���Ⱑ �ϴ� �߻����� �ʵ��� ó��
    
    ParticleSystem FireSmoke;
    ParticleSystem.EmissionModule particleSystemEmissionModule;

    //�Ʒ��� ������ ���� �ҷ��´�.
    void Awake()
    {
        FireSmoke = GetComponent<ParticleSystem>(); //��ƼŬ �ý��� ��������
        particleSystemEmissionModule = FireSmoke.emission; //��ƼŬ ���� ��� ����
        particleSystemEmissionModule.rateOverTime = 0; //����ð��� 0���� ó��
    }

    void Update()
    {
        timeStamp += Time.deltaTime;

        particleEmissionRate = Mathf.Lerp(particleEmissionRate, 0, Time.deltaTime * 10f); //���Ⱑ �߻��ϰ� ���� ������� �� �ɸ��� �ð�
        particleSystemEmissionModule.rateOverTime = particleEmissionRate; //���� ���� ����� ��ƼŬ ���� �ð����� ó��

        StartCoroutine(Firing());

        //ź�� ����(ź���� ������ �ʾ��� ���)
        if (Input.GetKeyDown(KeyCode.E) && NeedAddAmmo == 0 && AmmoAmount > 0)
        {
            if (AmmoAmount < MaxAmmoAmount)
            {
                tempAmmo = MaxAmmoAmount - AmmoAmount;

                //������ �ִ� ź�෮�� ���� ���ϴ� ���
                if (tempAmmo > Magazines)
                {
                    AmmoAmount += Magazines;
                }
                //������ �ִ� ź�෮�� ���� ���
                else
                {
                    for (int i = 1; i <= tempAmmo; i++)
                    {
                        AmmoAmount += 1;
                    }
                }
            }
        }

        //ź�� ��
        if (AmmoAmount == 0)
        {
            NeedAddAmmo = 1;

            //ź�� ����(���� ���ľ� ���Ⱑ �������� ���� �� ����)
            if(Input.GetKeyDown(KeyCode.E) && NeedAddAmmo == 1)
            {
                if (AmmoAmount < MaxAmmoAmount)
                {
                    tempAmmo = MaxAmmoAmount - AmmoAmount;

                    //������ �ִ� ź�෮�� ���� ���ϴ� ���
                    if (tempAmmo > Magazines)
                    {
                        AmmoAmount += Magazines;
                        AfterAddAmmo = 1;
                    }
                    //������ �ִ� ź�෮�� ���� ���
                    else
                    {
                        for (int i = 1; i <= tempAmmo; i++)
                        {
                            AmmoAmount += 1;
                        }
                        AfterAddAmmo = 1;
                    }
                }
            }
        }
    }

    public IEnumerator Firing()
    {
        if (Input.GetKey(KeyCode.C) && reloading == 0 && AmmoAmount > 0 && NeedAddAmmo == 0 && AfterAddAmmo == 0)
        {
            particleEmissionRate = 0; //���� ��� ������ ������ ���� ���ⷮ�� 0���� ó��

            if (timeStamp >= fireRate && reloading == 0)
            {
                timeStamp = 0;
                ammo += 1;
                AmmoAmount -= 1;
            }
        }
        if (Input.GetKeyUp(KeyCode.C) && reloading == 0 && NeedAddAmmo == 0 && AfterAddAmmo == 0)
        {
            particleEmissionRate = 500; //���� �� ������� ���� ���ⷮ
            this.gameObject.AddComponent<GunAttackText>().SmokeAfterFire(); //������� ���� �߻�
        }

        //ź�� �� �� ���� �߻�
        if(AmmoAmount == 0 && timeStamp == 0)
        {
            particleEmissionRate = 500;
        }

        //����� ������
        if (ammo == Magazines && reloading == 0 && timeStamp >= 0.01)
        {
            reloading = 1;
            AfterAddAmmo = 0;
            NeedAddAmmo = 0;

            yield return new WaitForSeconds(0.1f);

            particleEmissionRate = 500;
            this.gameObject.AddComponent<GunAttackText>().SmokeAfterFire2(); //��ݿϷ��� ���������� ���� �߻�

            yield return new WaitForSeconds(3f);

            ammo = 0;
            reloading = 0;
            AfterAddAmmo = 0;
            NeedAddAmmo = 0;
        }

        //������ ������
        if (Input.GetKeyDown(KeyCode.R) && reloading == 0 && NeedAddAmmo == 1)
        {
            reloading = 1;
            AfterAddAmmo = 0;
            NeedAddAmmo = 0;

            yield return new WaitForSeconds(3f);

            ammo = 0;
            timeStamp = 0;
            reloading = 0;
            AfterAddAmmo = 0;
            NeedAddAmmo = 0;
            this.gameObject.AddComponent<GunAttackText>().ReloadAfterFireComplete2_1(); //Debug ������ ������ �Ϸ�
        }

        //���� ������
        if (Input.GetKeyDown(KeyCode.R) && reloading == 0)
        {
            reloading = 1;
            AfterAddAmmo = 0;
            NeedAddAmmo = 0;

            if (Input.GetKey(KeyCode.C) && reloading == 1)
            {
                particleEmissionRate = 500;
                this.gameObject.AddComponent<GunAttackText>().SmokeAfterFire3(); //��ݿϷ��� �������������� ���� �߻�
            }

            yield return new WaitForSeconds(3.25f);

            ammo = 0;
            reloading = 0;
            AfterAddAmmo = 0;
            NeedAddAmmo = 0;
        }
    }
}
