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

    float particleEmissionRate = 0; //게임 시작할 때 연기가 일단 발생하지 않도록 처리
    
    ParticleSystem FireSmoke;
    ParticleSystem.EmissionModule particleSystemEmissionModule;

    //아래의 내역을 먼저 불러온다.
    void Awake()
    {
        FireSmoke = GetComponent<ParticleSystem>(); //파티클 시스템 가져오기
        particleSystemEmissionModule = FireSmoke.emission; //파티클 분출 요소 지정
        particleSystemEmissionModule.rateOverTime = 0; //분출시간을 0으로 처리
    }

    void Update()
    {
        timeStamp += Time.deltaTime;

        particleEmissionRate = Mathf.Lerp(particleEmissionRate, 0, Time.deltaTime * 10f); //연기가 발생하고 나서 사라지는 데 걸리는 시간
        particleSystemEmissionModule.rateOverTime = particleEmissionRate; //위의 계산된 결과를 파티클 분출 시간으로 처리

        StartCoroutine(Firing());

        //탄약 보급(탄약이 고갈되지 않았을 경우)
        if (Input.GetKeyDown(KeyCode.E) && NeedAddAmmo == 0 && AmmoAmount > 0)
        {
            if (AmmoAmount < MaxAmmoAmount)
            {
                tempAmmo = MaxAmmoAmount - AmmoAmount;

                //보급후 최대 탄약량을 넘지 못하는 경우
                if (tempAmmo > Magazines)
                {
                    AmmoAmount += Magazines;
                }
                //보급후 최대 탄약량을 넘을 경우
                else
                {
                    for (int i = 1; i <= tempAmmo; i++)
                    {
                        AmmoAmount += 1;
                    }
                }
            }
        }

        //탄약 고갈
        if (AmmoAmount == 0)
        {
            NeedAddAmmo = 1;

            //탄약 보급(여길 거쳐야 연기가 정상으로 나올 수 있음)
            if(Input.GetKeyDown(KeyCode.E) && NeedAddAmmo == 1)
            {
                if (AmmoAmount < MaxAmmoAmount)
                {
                    tempAmmo = MaxAmmoAmount - AmmoAmount;

                    //보급후 최대 탄약량을 넘지 못하는 경우
                    if (tempAmmo > Magazines)
                    {
                        AmmoAmount += Magazines;
                        AfterAddAmmo = 1;
                    }
                    //보급후 최대 탄약량을 넘을 경우
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
            particleEmissionRate = 0; //총을 쏘기 시작할 때에는 연기 분출량을 0으로 처리

            if (timeStamp >= fireRate && reloading == 0)
            {
                timeStamp = 0;
                ammo += 1;
                AmmoAmount -= 1;
            }
        }
        if (Input.GetKeyUp(KeyCode.C) && reloading == 0 && NeedAddAmmo == 0 && AfterAddAmmo == 0)
        {
            particleEmissionRate = 500; //총을 다 쏘고나서의 연기 분출량
            this.gameObject.AddComponent<GunAttackText>().SmokeAfterFire(); //사격직후 연기 발생
        }

        //탄약 고갈 후 연기 발생
        if(AmmoAmount == 0 && timeStamp == 0)
        {
            particleEmissionRate = 500;
        }

        //사격후 재장전
        if (ammo == Magazines && reloading == 0 && timeStamp >= 0.01)
        {
            reloading = 1;
            AfterAddAmmo = 0;
            NeedAddAmmo = 0;

            yield return new WaitForSeconds(0.1f);

            particleEmissionRate = 500;
            this.gameObject.AddComponent<GunAttackText>().SmokeAfterFire2(); //사격완료후 재장전에서 연기 발생

            yield return new WaitForSeconds(3f);

            ammo = 0;
            reloading = 0;
            AfterAddAmmo = 0;
            NeedAddAmmo = 0;
        }

        //보급후 재장전
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
            this.gameObject.AddComponent<GunAttackText>().ReloadAfterFireComplete2_1(); //Debug 보급후 재장전 완료
        }

        //전술 재장전
        if (Input.GetKeyDown(KeyCode.R) && reloading == 0)
        {
            reloading = 1;
            AfterAddAmmo = 0;
            NeedAddAmmo = 0;

            if (Input.GetKey(KeyCode.C) && reloading == 1)
            {
                particleEmissionRate = 500;
                this.gameObject.AddComponent<GunAttackText>().SmokeAfterFire3(); //사격완료후 전술재장전에서 연기 발생
            }

            yield return new WaitForSeconds(3.25f);

            ammo = 0;
            reloading = 0;
            AfterAddAmmo = 0;
            NeedAddAmmo = 0;
        }
    }
}
