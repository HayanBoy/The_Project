using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAttackText : MonoBehaviour
{
    //GunAttack
    public void NeedAmmo()
    {
        Debug.Log("ź�� �� by GunAttack");
    }

    public void AmmoGive()
    {
        Debug.Log("ź���� �����մϴ�. by GunAttack");
    }

    public void AmmoGet()
    {
        Debug.Log("ź�� ���� by GunAttack");
    }

    public void AmmoOverGet()
    {
        Debug.Log("ź�� ������ by GunAttack");
    }

    public void AmmoGet2()
    {
        Debug.Log("ź�� �˳� ���� by GunAttack");
    }

    public void AmmoOverGet2()
    {
        Debug.Log("ź�� �˳� ������ by GunAttack");
    }

    public void ReloadAfterFire()
    {
        Debug.Log("SW-06 ������ by GunAttack");
    }

    public void ReloadAfterFireComplete()
    {
        Debug.Log("SW-06 ������ �Ϸ� by GunAttack");
    }

    public void ReloadAfterFire2()
    {
        Debug.Log("SW-06 ������ ������ by GunAttack");
    }

    public void ReloadAfterFireComplete2()
    {
        Debug.Log("SW-06 ������ ������ �Ϸ� by GunAttack");
    }

    public void ReloadAfterFire3()
    {
        Debug.Log("SW-06 ���� ������ by GunAttack");
    }

    public void ReloadAfterFireComplete3()
    {
        Debug.Log("SW-06 ���� ������ �Ϸ� by GunAttack");
    }

    //GunSmoke_SW06
    public void SmokeAfterFire()
    {
        Debug.Log("��� ���� ���� �߻� by GunSmoke_SW06");
    }

    public void SmokeAfterFire2()
    {
        Debug.Log("��ݿϷ��� ���������� ���� �߻� by GunSmoke_SW06");
    }

    public void SmokeAfterFire3()
    {
        Debug.Log("��ݿϷ��� �������������� ���� �߻� by GunSmoke_SW06");
    }
    public void ReloadAfterFireComplete2_1()
    {
        Debug.Log("������ ������ �Ϸ� by GunSmoke_SW06");
    }
}
