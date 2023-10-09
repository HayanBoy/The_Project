using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAttackText : MonoBehaviour
{
    //GunAttack
    public void NeedAmmo()
    {
        Debug.Log("탄약 고갈 by GunAttack");
    }

    public void AmmoGive()
    {
        Debug.Log("탄약이 부족합니다. by GunAttack");
    }

    public void AmmoGet()
    {
        Debug.Log("탄약 보급 by GunAttack");
    }

    public void AmmoOverGet()
    {
        Debug.Log("탄약 과보급 by GunAttack");
    }

    public void AmmoGet2()
    {
        Debug.Log("탄약 넉넉 보급 by GunAttack");
    }

    public void AmmoOverGet2()
    {
        Debug.Log("탄약 넉넉 과보급 by GunAttack");
    }

    public void ReloadAfterFire()
    {
        Debug.Log("SW-06 재장전 by GunAttack");
    }

    public void ReloadAfterFireComplete()
    {
        Debug.Log("SW-06 재장전 완료 by GunAttack");
    }

    public void ReloadAfterFire2()
    {
        Debug.Log("SW-06 보급후 재장전 by GunAttack");
    }

    public void ReloadAfterFireComplete2()
    {
        Debug.Log("SW-06 보급후 재장전 완료 by GunAttack");
    }

    public void ReloadAfterFire3()
    {
        Debug.Log("SW-06 전술 재장전 by GunAttack");
    }

    public void ReloadAfterFireComplete3()
    {
        Debug.Log("SW-06 전술 재장전 완료 by GunAttack");
    }

    //GunSmoke_SW06
    public void SmokeAfterFire()
    {
        Debug.Log("사격 직후 연기 발생 by GunSmoke_SW06");
    }

    public void SmokeAfterFire2()
    {
        Debug.Log("사격완료후 재장전에서 연기 발생 by GunSmoke_SW06");
    }

    public void SmokeAfterFire3()
    {
        Debug.Log("사격완료후 전술재장전에서 연기 발생 by GunSmoke_SW06");
    }
    public void ReloadAfterFireComplete2_1()
    {
        Debug.Log("보급후 재장전 완료 by GunSmoke_SW06");
    }
}
