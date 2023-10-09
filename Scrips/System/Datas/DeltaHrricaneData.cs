using System.Collections.Generic;
using UnityEngine;

public class DeltaHrricaneData : MonoBehaviour
{
    public static DeltaHrricaneData instance = null;

    [Header("주 무기")]
    public List<int> MainWeaponList = new List<int>();
    public int MainWeaponType = 1; //장착된 주무기 유형
    public int SelectedMainWeaponNumber = 1; //선택된 주 무기

    [Header("주 무기 정보")]
    public int DT37AmmoPerMagazine;
    public int DT37AmmoAmount;
    public int DT37MaxAmmoAmount;
    public int DT37GetAmmoAmount;

    public int DS65AmmoPerMagazine;
    public int DS65AmmoAmount;
    public int DS65MaxAmmoAmount;
    public int DS65GetAmmoAmount;

    public int DP9007AmmoPerMagazine;
    public int DP9007AmmoAmount;
    public int DP9007MaxAmmoAmount;
    public int DP9007GetAmmoAmount;

    public int CGD27AmmoPerMagazine;
    public int CGD27AmmoAmount;
    public int CGD27MaxAmmoAmount;
    public int CGD27GetAmmoAmount;

    [Header("중화기")]
    public List<int> HeavyWeaponList = new List<int>();
    public int HeavyWeaponType; //장착된 주무기 유형
    public int SelectedHeavyWeaponNumber = 1; //선택된 주 무기

    [Header("중화기 정보")]
    public int M3078AmmoAmount;
    public int ASC365AmmoAmount;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void GetUpgrade()
    {
        //주 무기 정보 저장
        //DT-37
        instance.DT37AmmoPerMagazine = 30;
        instance.DT37AmmoAmount = 200 * UpgradeDataSystem.instance.SupportAmmoAmount;
        instance.DT37MaxAmmoAmount = 200 * UpgradeDataSystem.instance.SupportAmmoAmount;
        instance.DT37GetAmmoAmount = 100 * UpgradeDataSystem.instance.SupportAmmoAmount;

        //DS-65
        instance.DS65AmmoPerMagazine = 10;
        instance.DS65AmmoAmount = 60 * UpgradeDataSystem.instance.SupportAmmoAmount;
        instance.DS65MaxAmmoAmount = 60 * UpgradeDataSystem.instance.SupportAmmoAmount;
        instance.DS65GetAmmoAmount = 30 * UpgradeDataSystem.instance.SupportAmmoAmount;

        //DP-9007
        instance.DP9007AmmoPerMagazine = 10;
        instance.DP9007AmmoAmount = 40 * UpgradeDataSystem.instance.SupportAmmoAmount;
        instance.DP9007MaxAmmoAmount = 60 * UpgradeDataSystem.instance.SupportAmmoAmount;
        instance.DP9007GetAmmoAmount = 20 * UpgradeDataSystem.instance.SupportAmmoAmount;

        //CGD-27
        instance.CGD27AmmoPerMagazine = 30 * 2;
        instance.CGD27AmmoAmount = 150 * UpgradeDataSystem.instance.SupportAmmoAmount;
        instance.CGD27MaxAmmoAmount = 150 * UpgradeDataSystem.instance.SupportAmmoAmount;
        instance.CGD27GetAmmoAmount = 50 * UpgradeDataSystem.instance.SupportAmmoAmount;

        //중화기 정보 저장
        instance.M3078AmmoAmount = 400;
        instance.ASC365AmmoAmount = 300;
    }
}