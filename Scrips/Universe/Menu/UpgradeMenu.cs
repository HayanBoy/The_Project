using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    [Header("스크립트")]
    public CameraZoom CameraZoom;
    public SystemMessages SystemMessages;
    public WordPrintMenu WordPrintMenu;
    public MainMenuButtonSystem MainMenuButtonSystem;
    public TutorialSystem TutorialSystem;

    [Header("연구 메뉴 버튼")]
    public GameObject CancelFleetButtonPrefab;
    public bool CancelFleetClick;
    public int CancelNumber; //취소 버튼을 통해 창을 나가는 방식. 1 = 연구 메뉴를 완전히 종료, 2 = 선택한 업그레이드 창을 종료하고 다시 연구 목록으로 돌아가기

    [Header("연구 메뉴 창")]
    public Image LabExitButton; //나가기 버튼

    [Header("메인 탭 목록")]
    public GameObject AllTabPrefab;
    public GameObject FleetGroupPrefab;
    public GameObject DeltaHurricaneGroupTabPrefab;
    public GameObject ShipSupportGroupTabPrefab;

    [Header("서브 탭 목록")]
    public GameObject FleetShipTabPrefab; //함대 탭
    public GameObject FleetWeaponTabPrefab;
    public GameObject FleetSkillTabPrefab;
    public GameObject DeltaHurricaneSuitTabPrefab; //델타 허리케인 탭
    public GameObject MainWeaponsTabPrefab;
    public GameObject DeltaHurricaneSupportWeaponsTabPrefab;
    public GameObject WeaponSupportTabPrefab; //함선 지원 탭
    public GameObject StrikeSupportTabPrefab;

    [Header("탭 목록 클릭")]
    public GameObject FleetPrefabClick;
    public GameObject DeltaHurricaneTabPrefabClick;
    public GameObject ShipSupportTabPrefabClick;

    [Header("서브 탭 목록 클릭")]
    public GameObject ShipTabPrefabClick; //함대 탭
    public GameObject TurretTabPrefabClick;
    public GameObject SkillTabPrefabClick;
    public GameObject DeltaHurricaneSuitTabPrefabClick; //델타 허리케인 탭
    public GameObject MainWeaponsTabPrefabClick;
    public GameObject DeltaHurricaneSupportWeaponsTabPrefabClick;
    public GameObject WeaponSupportTabPrefabClick; //함선 지원 탭
    public GameObject StrikeSupportTabPrefabClick;

    [Header("테이블 리스트")]
    public string TableName;
    public GameObject TabTableWindowPrefab;
    public GameObject UpgradeTable1;
    public GameObject UpgradeTable2;
    public GameObject UpgradeTable3;
    public GameObject UpgradeTable4;
    public GameObject UpgradeTable5;
    public GameObject UpgradeTable6;
    public GameObject UpgradeTable7;
    public GameObject UpgradeTable8;
    public GameObject UpgradeTable9;
    public GameObject UpgradeTable10;

    [Header("테이블 등급 리스트")]
    public Text TableClass1;
    public Text TableClass2;
    public Text TableClass3;
    public Text TableClass4;
    public Text TableClass5;
    public Text TableClass6;
    public Text TableClass7;
    public Text TableClass8;
    public Text TableClass9;
    public Text TableClass10;

    [Header("연구 정보 리스트")]
    public GameObject UpgradelistPrefab;
    public GameObject UpgradelistMainTable1;
    public GameObject UpgradelistSubTable1_1;
    public Image UpgradeIcon;

    [Header("테이블 아이콘")]
    public Sprite FlagshipArmorSystemIcon;
    public Sprite FormationShipArmorSystemIcon;
    public Sprite TacticalShipArmorSystemIcon;
    public Sprite CannonTypeIcon;
    public Sprite MissileTypeIcon;
    public Sprite CarrierBasedAircraftTypeIcon;
    public Sprite FlagshipStrikeIcon;
    public Sprite FleetStrikeIcon;
    public Sprite HitPointIcon;
    public Sprite AssaultRifleTypeIcon;
    public Sprite ShotgunTypeIcon;
    public Sprite SniperRifleTypeIcon;
    public Sprite SubmachineGunTypeIcon;
    public Sprite SubGearTypeIcon;
    public Sprite GrenadeTypeIcon;
    public Sprite ChangeHeavyWeaponIcon;
    public Sprite LogisticsSupportIcon;
    public Sprite HeavyWeaponSupportIcon;
    public Sprite VehicleSupportIcon;
    public Sprite BombardmentSupportIcon;

    [Header("절차")]
    public int LabStep; //메뉴 절차
    public int UnlockStep; //언락 절차
    public int MainTabNumber;
    public int SubTabNumber;

    [Header("사운드")]
    public AudioClip ButtonUIAudio;
    public AudioClip CancelUIAudio;

    //취소 후, 다시 메인메뉴 창으로 돌아가기
    public void CancelFleetButtonClick()
    {
        if (CancelNumber == 1) //완전히 나가기
        {
            MainMenuButtonSystem.MenuButtonAnim.SetActive(true); //메인 메뉴 버튼 활성화
            MainMenuButtonSystem.FlagshipMenuWindow.SetActive(false);
            MainMenuButtonSystem.FlagshipMenuMode = false;
            UpgradeIcon.gameObject.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 0);
            CancelTabClick();
            StartCoroutine(CameraZoom.ExitingAnimation());
            StartCoroutine(Exit());
        }
        else if (CancelNumber == 2) //이전 테이블 리스트로 돌아가기
        {
            CancelNumber = 1;
            AllTabPrefab.SetActive(true);
            TabTableWindowPrefab.SetActive(true);
            UpgradelistPrefab.SetActive(false);
        }
    }
    public void CancelFleetButtonDown()
    {
        CancelFleetClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", CancelUIAudio);
        CancelFleetButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
    }
    public void CancelFleetButtonUp()
    {
        if (CancelFleetClick == true)
        {
            CancelFleetButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
        CancelFleetClick = false;
    }
    public void CancelFleetButtonEnter()
    {
        if (CancelFleetClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", CancelUIAudio);
            CancelFleetButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
        }
    }
    public void CancelFleetButtonExit()
    {
        if (CancelFleetClick == true)
        {
            CancelFleetButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
    }

    //나가기
    IEnumerator Exit()
    {
        SystemMessages.MessageType = 0;
        SystemMessages.MessageNumber = 0;
        yield return new WaitForSecondsRealtime(0.1f);
        MainMenuButtonSystem.LabMenuWindow.SetActive(false);
    }

    //함대 탭 클릭(메인 탭1)
    public void FleetTabClick()
    {
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
        //다른 서브탭 끄기
        DeltaHurricaneSuitTabPrefab.SetActive(false);
        MainWeaponsTabPrefab.SetActive(false);
        DeltaHurricaneSupportWeaponsTabPrefab.SetActive(false);
        WeaponSupportTabPrefab.SetActive(false);
        StrikeSupportTabPrefab.SetActive(false);

        //현재 메인 탭 클릭 프리팹을 활성화하고, 다른 메인 탭 클릭 프리팹을 끄기
        FleetPrefabClick.SetActive(true);
        DeltaHurricaneTabPrefabClick.SetActive(false);
        ShipSupportTabPrefabClick.SetActive(false);

        //현재 메인 탭의 서브탭들을 활성화 하기
        ShipTabPrefabClick.SetActive(true);
        TurretTabPrefabClick.SetActive(false);
        SkillTabPrefabClick.SetActive(false);
        FleetShipTabPrefab.SetActive(true);
        FleetWeaponTabPrefab.SetActive(true);
        FleetSkillTabPrefab.SetActive(true);
        LoadTableList(1, 1);
    }
    //델타 허리케인 탭 클릭(메인 탭2)
    public void DeltaHurricaneTabClick()
    {
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
        FleetShipTabPrefab.SetActive(false);
        FleetWeaponTabPrefab.SetActive(false);
        FleetSkillTabPrefab.SetActive(false);
        WeaponSupportTabPrefab.SetActive(false);
        StrikeSupportTabPrefab.SetActive(false);

        FleetPrefabClick.SetActive(false);
        DeltaHurricaneTabPrefabClick.SetActive(true);
        ShipSupportTabPrefabClick.SetActive(false);

        DeltaHurricaneSuitTabPrefabClick.SetActive(true);
        MainWeaponsTabPrefabClick.SetActive(false);
        DeltaHurricaneSupportWeaponsTabPrefabClick.SetActive(false);
        DeltaHurricaneSuitTabPrefab.SetActive(true);
        MainWeaponsTabPrefab.SetActive(true);
        DeltaHurricaneSupportWeaponsTabPrefab.SetActive(true);
        LoadTableList(2, 1);
    }
    //함선 지원 탭 클릭(메인 탭3)
    public void ShipSupportTabClick()
    {
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
        FleetShipTabPrefab.SetActive(false);
        FleetWeaponTabPrefab.SetActive(false);
        FleetSkillTabPrefab.SetActive(false);
        DeltaHurricaneSuitTabPrefab.SetActive(false);
        MainWeaponsTabPrefab.SetActive(false);
        DeltaHurricaneSupportWeaponsTabPrefab.SetActive(false);

        FleetPrefabClick.SetActive(false);
        DeltaHurricaneTabPrefabClick.SetActive(false);
        ShipSupportTabPrefabClick.SetActive(true);

        WeaponSupportTabPrefabClick.SetActive(true);
        StrikeSupportTabPrefabClick.SetActive(false);
        WeaponSupportTabPrefab.SetActive(true);
        StrikeSupportTabPrefab.SetActive(true);
        LoadTableList(3, 1);
    }

    //서브 탭 클릭
    public void FleetShipTabClick()
    {
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
        ShipTabPrefabClick.SetActive(true);
        TurretTabPrefabClick.SetActive(false);
        SkillTabPrefabClick.SetActive(false);
        LoadTableList(1, 1);
    }
    public void FleetWeaponTabClick()
    {
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
        ShipTabPrefabClick.SetActive(false);
        TurretTabPrefabClick.SetActive(true);
        SkillTabPrefabClick.SetActive(false);
        LoadTableList(1, 2);
    }
    public void FleetSkillTabClick()
    {
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
        ShipTabPrefabClick.SetActive(false);
        TurretTabPrefabClick.SetActive(false);
        SkillTabPrefabClick.SetActive(true);
        LoadTableList(1, 3);
    }
    public void DeltaHurricaneSuitTabClick()
    {
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
        DeltaHurricaneSuitTabPrefabClick.SetActive(true);
        MainWeaponsTabPrefabClick.SetActive(false);
        DeltaHurricaneSupportWeaponsTabPrefabClick.SetActive(false);
        LoadTableList(2, 1);
    }
    public void DeltaHurricaneMainWeaponsTabClick()
    {
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
        DeltaHurricaneSuitTabPrefabClick.SetActive(false);
        MainWeaponsTabPrefabClick.SetActive(true);
        DeltaHurricaneSupportWeaponsTabPrefabClick.SetActive(false);
        LoadTableList(2, 2);
    }
    public void DeltaHurricaneSupportWeaponsTabClick()
    {
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
        DeltaHurricaneSuitTabPrefabClick.SetActive(false);
        MainWeaponsTabPrefabClick.SetActive(false);
        DeltaHurricaneSupportWeaponsTabPrefabClick.SetActive(true);
        LoadTableList(2, 3);
    }
    public void ShipSupportWeaponTabClick()
    {
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
        WeaponSupportTabPrefabClick.SetActive(true);
        StrikeSupportTabPrefabClick.SetActive(false);
        LoadTableList(3, 1);
    }
    public void ShipSupportStrikeTabClick()
    {
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
        WeaponSupportTabPrefabClick.SetActive(false);
        StrikeSupportTabPrefabClick.SetActive(true);
        LoadTableList(3, 2);
    }

    //테이블 목록 가져오기
    public void LoadTableList(int MainTabNumber, int SubTabNumber)
    {
        UpgradeTable1.SetActive(false);
        UpgradeTable2.SetActive(false);
        UpgradeTable3.SetActive(false);
        UpgradeTable4.SetActive(false);

        LabStep = 0;
        UnlockStep = 0;
        this.MainTabNumber = MainTabNumber;
        this.SubTabNumber = SubTabNumber;

        bool PlanetLevel = false;
        int PlanetLevelNumber = 0;

        if (MainTabNumber == 1) //함대 탭
        {
            if (SubTabNumber == 1) //함대 장갑 시스템
            {
                //기함 장갑
                if (UpgradeDataSystem.instance.FlagshipUpgradeLevel == 0)
                {
                    PlanetLevel = WeaponUnlockManager.instance.DeriousHeriLabUnlock;
                    PlanetLevelNumber = 1012;
                }
                else if (UpgradeDataSystem.instance.FlagshipUpgradeLevel == 1)
                {
                    PlanetLevel = WeaponUnlockManager.instance.DeltaD31_2208LabUnlock;
                    PlanetLevelNumber = 1020;
                }
                else if (UpgradeDataSystem.instance.FlagshipUpgradeLevel == 2)
                {
                    PlanetLevel = WeaponUnlockManager.instance.JeratoO95_2252LabUnlock;
                    PlanetLevelNumber = 1024;
                }
                else if (UpgradeDataSystem.instance.FlagshipUpgradeLevel == 3) //연구 완료되었을 때 표시
                {
                    PlanetLevel = true;
                    PlanetLevelNumber = 0;
                }
                WordPrintMenu.UpgradeTableListPrint(MainTabNumber, SubTabNumber, 1);
                TableClass1.text = string.Format("{0}", UpgradeDataSystem.instance.FlagshipUpgradeLevel);
                UpgradeTable1.SetActive(true);
                UpgradeTable1.GetComponent<UpgradeTableInformation1>().TableInformChange(FlagshipArmorSystemIcon, TableName, MainTabNumber, SubTabNumber, PlanetLevel, WeaponUnlockManager.instance.FlagshipUnlock,
                    PlanetLevelNumber, 1000, 0, 1000, "Flagship Unlock", UpgradeDataSystem.instance.FlagshipUpgradeLevel);

                //편대함 장갑
                if (UpgradeDataSystem.instance.FormationUpgradeLevel == 0)
                {
                    PlanetLevel = WeaponUnlockManager.instance.DeriousHeriLabUnlock;
                    PlanetLevelNumber = 1012;
                }
                else if (UpgradeDataSystem.instance.FormationUpgradeLevel == 1)
                {
                    PlanetLevel = WeaponUnlockManager.instance.DeltaD31_2208LabUnlock;
                    PlanetLevelNumber = 1020;
                }
                else if (UpgradeDataSystem.instance.FormationUpgradeLevel == 2)
                {
                    PlanetLevel = WeaponUnlockManager.instance.JeratoO95_2252LabUnlock;
                    PlanetLevelNumber = 1024;
                }
                else if (UpgradeDataSystem.instance.FormationUpgradeLevel == 3)
                {
                    PlanetLevel = true;
                    PlanetLevelNumber = 0;
                }
                WordPrintMenu.UpgradeTableListPrint(MainTabNumber, SubTabNumber, 2);
                TableClass2.text = string.Format("{0}", UpgradeDataSystem.instance.FormationUpgradeLevel);
                UpgradeTable2.SetActive(true);
                UpgradeTable2.GetComponent<UpgradeTableInformation1>().TableInformChange(FormationShipArmorSystemIcon, TableName, MainTabNumber, SubTabNumber, PlanetLevel, WeaponUnlockManager.instance.FormationShipUnlock,
                    PlanetLevelNumber, 500, 0, 500, "Formation Ship Unlock", UpgradeDataSystem.instance.FormationUpgradeLevel);

                //전략함 장갑
                if (UpgradeDataSystem.instance.TacticalShipUpgradeLevel == 0)
                {
                    PlanetLevel = WeaponUnlockManager.instance.DeriousHeriLabUnlock;
                    PlanetLevelNumber = 1012;
                }
                else if (UpgradeDataSystem.instance.TacticalShipUpgradeLevel == 1)
                {
                    PlanetLevel = WeaponUnlockManager.instance.DeltaD31_2208LabUnlock;
                    PlanetLevelNumber = 1020;
                }
                else if (UpgradeDataSystem.instance.TacticalShipUpgradeLevel == 2)
                {
                    PlanetLevel = WeaponUnlockManager.instance.JeratoO95_2252LabUnlock;
                    PlanetLevelNumber = 1024;
                }
                else if (UpgradeDataSystem.instance.TacticalShipUpgradeLevel == 3)
                {
                    PlanetLevel = true;
                    PlanetLevelNumber = 0;
                }
                WordPrintMenu.UpgradeTableListPrint(MainTabNumber, SubTabNumber, 3);
                TableClass3.text = string.Format("{0}", UpgradeDataSystem.instance.TacticalShipUpgradeLevel);
                UpgradeTable3.SetActive(true);
                UpgradeTable3.GetComponent<UpgradeTableInformation1>().TableInformChange(TacticalShipArmorSystemIcon, TableName, MainTabNumber, SubTabNumber, PlanetLevel, WeaponUnlockManager.instance.TacticalShipUnlock,
                    PlanetLevelNumber, 750, 0, 750, "Tactical Ship Unlock", UpgradeDataSystem.instance.TacticalShipUpgradeLevel);
            }
            else if (SubTabNumber == 2) //함대 무기
            {
                //주포
                if (UpgradeDataSystem.instance.ShipCannonUpgradeLevel == 0)
                {
                    PlanetLevel = WeaponUnlockManager.instance.DeriousHeriLabUnlock;
                    PlanetLevelNumber = 1012;
                }
                else if (UpgradeDataSystem.instance.ShipCannonUpgradeLevel == 1)
                {
                    PlanetLevel = WeaponUnlockManager.instance.DeltaD31_2208LabUnlock;
                    PlanetLevelNumber = 1020;
                }
                else if (UpgradeDataSystem.instance.ShipCannonUpgradeLevel == 2)
                {
                    PlanetLevel = WeaponUnlockManager.instance.JeratoO95_2252LabUnlock;
                    PlanetLevelNumber = 1024;
                }
                else if (UpgradeDataSystem.instance.ShipCannonUpgradeLevel == 3) //연구 완료되었을 때 표시
                {
                    PlanetLevel = true;
                    PlanetLevelNumber = 0;
                }
                WordPrintMenu.UpgradeTableListPrint(MainTabNumber, SubTabNumber, 1);
                TableClass1.text = string.Format("{0}", UpgradeDataSystem.instance.ShipCannonUpgradeLevel);
                UpgradeTable1.SetActive(true);
                UpgradeTable1.GetComponent<UpgradeTableInformation1>().TableInformChange(CannonTypeIcon, TableName, MainTabNumber, SubTabNumber, PlanetLevel, WeaponUnlockManager.instance.ShipCannonUnlock,
                    PlanetLevelNumber, 750, 0, 750, "Ship Cannon Unlock", UpgradeDataSystem.instance.ShipCannonUpgradeLevel);

                //미사일
                if (UpgradeDataSystem.instance.ShipMissileUpgradeLevel == 0)
                {
                    PlanetLevel = WeaponUnlockManager.instance.DeriousHeriLabUnlock;
                    PlanetLevelNumber = 1012;
                }
                else if (UpgradeDataSystem.instance.ShipMissileUpgradeLevel == 1)
                {
                    PlanetLevel = WeaponUnlockManager.instance.DeltaD31_2208LabUnlock;
                    PlanetLevelNumber = 1020;
                }
                else if (UpgradeDataSystem.instance.ShipMissileUpgradeLevel == 2)
                {
                    PlanetLevel = WeaponUnlockManager.instance.JeratoO95_2252LabUnlock;
                    PlanetLevelNumber = 1024;
                }
                else if (UpgradeDataSystem.instance.ShipMissileUpgradeLevel == 3)
                {
                    PlanetLevel = true;
                    PlanetLevelNumber = 0;
                }
                WordPrintMenu.UpgradeTableListPrint(MainTabNumber, SubTabNumber, 2);
                TableClass2.text = string.Format("{0}", UpgradeDataSystem.instance.ShipMissileUpgradeLevel);
                UpgradeTable2.SetActive(true);
                UpgradeTable2.GetComponent<UpgradeTableInformation1>().TableInformChange(MissileTypeIcon, TableName, MainTabNumber, SubTabNumber, PlanetLevel, WeaponUnlockManager.instance.ShipMissileUnlock,
                    PlanetLevelNumber, 750, 0, 750, "Ship Missile Unlock", UpgradeDataSystem.instance.ShipMissileUpgradeLevel);

                //함재기
                if (UpgradeDataSystem.instance.ShipFighterUpgradeLevel == 0)
                {
                    PlanetLevel = WeaponUnlockManager.instance.DeriousHeriLabUnlock;
                    PlanetLevelNumber = 1012;
                }
                else if (UpgradeDataSystem.instance.ShipFighterUpgradeLevel == 1)
                {
                    PlanetLevel = WeaponUnlockManager.instance.DeltaD31_2208LabUnlock;
                    PlanetLevelNumber = 1020;
                }
                else if (UpgradeDataSystem.instance.ShipFighterUpgradeLevel == 2)
                {
                    PlanetLevel = WeaponUnlockManager.instance.JeratoO95_2252LabUnlock;
                    PlanetLevelNumber = 1024;
                }
                else if (UpgradeDataSystem.instance.ShipFighterUpgradeLevel == 3)
                {
                    PlanetLevel = true;
                    PlanetLevelNumber = 0;
                }
                WordPrintMenu.UpgradeTableListPrint(MainTabNumber, SubTabNumber, 3);
                TableClass3.text = string.Format("{0}", UpgradeDataSystem.instance.ShipFighterUpgradeLevel);
                UpgradeTable3.SetActive(true);
                UpgradeTable3.GetComponent<UpgradeTableInformation1>().TableInformChange(CarrierBasedAircraftTypeIcon, TableName, MainTabNumber, SubTabNumber, PlanetLevel, WeaponUnlockManager.instance.ShipFighterUnlock,
                    PlanetLevelNumber, 750, 0, 750, "Ship Fighter Unlock", UpgradeDataSystem.instance.ShipFighterUpgradeLevel);
            }
            else if (SubTabNumber == 3) //함대 지원
            {
                //기함 공격
                if (UpgradeDataSystem.instance.FlagshipAttackSkillUpgradeLevel == 0)
                {
                    PlanetLevel = WeaponUnlockManager.instance.JapetAgroneLabUnlock;
                    PlanetLevelNumber = 1018;
                }
                else if (UpgradeDataSystem.instance.FlagshipAttackSkillUpgradeLevel == 1)
                {
                    PlanetLevel = WeaponUnlockManager.instance.DeltaD31_12721LabUnlock;
                    PlanetLevelNumber = 1022;
                }
                else if (UpgradeDataSystem.instance.FlagshipAttackSkillUpgradeLevel == 2)
                {
                    PlanetLevel = WeaponUnlockManager.instance.JeratoO95_8510LabUnlock;
                    PlanetLevelNumber = 1025;
                }
                else if (UpgradeDataSystem.instance.FlagshipAttackSkillUpgradeLevel == 3) //연구 완료되었을 때 표시
                {
                    PlanetLevel = true;
                    PlanetLevelNumber = 0;
                }
                WordPrintMenu.UpgradeTableListPrint(MainTabNumber, SubTabNumber, 1);
                TableClass1.text = string.Format("{0}", UpgradeDataSystem.instance.FlagshipAttackSkillUpgradeLevel);
                UpgradeTable1.SetActive(true);
                UpgradeTable1.GetComponent<UpgradeTableInformation1>().TableInformChange(FlagshipStrikeIcon, TableName, MainTabNumber, SubTabNumber, PlanetLevel, WeaponUnlockManager.instance.FlagshipSkillAttackUnlock,
                    PlanetLevelNumber, 1000, 0, 1000, "Flagship Skill Attack Unlock", UpgradeDataSystem.instance.FlagshipAttackSkillUpgradeLevel);

                //함대 공격
                if (UpgradeDataSystem.instance.FleetAttackSkillUpgradeLevel == 0)
                {
                    PlanetLevel = WeaponUnlockManager.instance.JapetAgroneLabUnlock;
                    PlanetLevelNumber = 1018;
                }
                else if (UpgradeDataSystem.instance.FleetAttackSkillUpgradeLevel == 1)
                {
                    PlanetLevel = WeaponUnlockManager.instance.DeltaD31_12721LabUnlock;
                    PlanetLevelNumber = 1022;
                }
                else if (UpgradeDataSystem.instance.FleetAttackSkillUpgradeLevel == 2)
                {
                    PlanetLevel = WeaponUnlockManager.instance.JeratoO95_8510LabUnlock;
                    PlanetLevelNumber = 1025;
                }
                else if (UpgradeDataSystem.instance.FleetAttackSkillUpgradeLevel == 3)
                {
                    PlanetLevel = true;
                    PlanetLevelNumber = 0;
                }
                WordPrintMenu.UpgradeTableListPrint(MainTabNumber, SubTabNumber, 2);
                TableClass2.text = string.Format("{0}", UpgradeDataSystem.instance.FleetAttackSkillUpgradeLevel);
                UpgradeTable2.SetActive(true);
                UpgradeTable2.GetComponent<UpgradeTableInformation1>().TableInformChange(FleetStrikeIcon, TableName, MainTabNumber, SubTabNumber, PlanetLevel, WeaponUnlockManager.instance.FleetAttackSkillUnlock,
                    PlanetLevelNumber, 1000, 0, 1000, "Fleet Attack Skill Unlock", UpgradeDataSystem.instance.FleetAttackSkillUpgradeLevel);
            }
        }

        else if (MainTabNumber == 2) //델타 허리케인 탭
        {
            if (SubTabNumber == 1) //내구도
            {
                //체력
                if (UpgradeDataSystem.instance.HurricaneHitPointUpgradeLevel == 0)
                {
                    PlanetLevel = WeaponUnlockManager.instance.VedesVILabUnlock;
                    PlanetLevelNumber = 1005;
                }
                else if (UpgradeDataSystem.instance.HurricaneHitPointUpgradeLevel == 1)
                {
                    PlanetLevel = WeaponUnlockManager.instance.CrownYosereLabUnlock;
                    PlanetLevelNumber = 1016;
                }
                else if (UpgradeDataSystem.instance.HurricaneHitPointUpgradeLevel == 2)
                {
                    PlanetLevel = WeaponUnlockManager.instance.DeltaD31_12721LabUnlock;
                    PlanetLevelNumber = 1022;
                }
                else if (UpgradeDataSystem.instance.HurricaneHitPointUpgradeLevel == 3) //연구 완료되었을 때 표시
                {
                    PlanetLevel = true;
                    PlanetLevelNumber = 0;
                }
                WordPrintMenu.UpgradeTableListPrint(MainTabNumber, SubTabNumber, 1);
                TableClass1.text = string.Format("{0}", UpgradeDataSystem.instance.HurricaneHitPointUpgradeLevel);
                UpgradeTable1.SetActive(true);
                UpgradeTable1.GetComponent<UpgradeTableInformation1>().TableInformChange(HitPointIcon, TableName, MainTabNumber, SubTabNumber, PlanetLevel, WeaponUnlockManager.instance.SuitHitPointUnlock,
                    PlanetLevelNumber, 500, 0, 500, "Suit Hit Point Unlock", UpgradeDataSystem.instance.HurricaneHitPointUpgradeLevel);
            }
            else if (SubTabNumber == 2) //기본 무기
            {
                //돌격 소총
                if (UpgradeDataSystem.instance.HurricaneAssaultRifleUpgradeLevel == 0)
                {
                    PlanetLevel = WeaponUnlockManager.instance.CrownYosereLabUnlock;
                    PlanetLevelNumber = 1016;
                }
                else if (UpgradeDataSystem.instance.HurricaneAssaultRifleUpgradeLevel == 1)
                {
                    PlanetLevel = WeaponUnlockManager.instance.DeltaD31_12721LabUnlock;
                    PlanetLevelNumber = 1022;
                }
                else if (UpgradeDataSystem.instance.HurricaneAssaultRifleUpgradeLevel == 2)
                {
                    PlanetLevel = WeaponUnlockManager.instance.JeratoO95_8510LabUnlock;
                    PlanetLevelNumber = 1025;
                }
                else if (UpgradeDataSystem.instance.HurricaneAssaultRifleUpgradeLevel == 3) //연구 완료되었을 때 표시
                {
                    PlanetLevel = true;
                    PlanetLevelNumber = 0;
                }
                WordPrintMenu.UpgradeTableListPrint(MainTabNumber, SubTabNumber, 1);
                TableClass1.text = string.Format("{0}", UpgradeDataSystem.instance.HurricaneAssaultRifleUpgradeLevel);
                UpgradeTable1.SetActive(true);
                UpgradeTable1.GetComponent<UpgradeTableInformation1>().TableInformChange(AssaultRifleTypeIcon, TableName, MainTabNumber, SubTabNumber, PlanetLevel, WeaponUnlockManager.instance.DT37Unlock,
                    PlanetLevelNumber, 500, 0, 500, "DT-37 Unlock", UpgradeDataSystem.instance.HurricaneAssaultRifleUpgradeLevel);

                //샷건
                if (UpgradeDataSystem.instance.HurricaneShotgunUpgradeLevel == 0)
                {
                    PlanetLevel = WeaponUnlockManager.instance.CrownYosereLabUnlock;
                    PlanetLevelNumber = 1016;
                }
                else if (UpgradeDataSystem.instance.HurricaneShotgunUpgradeLevel == 1)
                {
                    PlanetLevel = WeaponUnlockManager.instance.DeltaD31_12721LabUnlock;
                    PlanetLevelNumber = 1022;
                }
                else if (UpgradeDataSystem.instance.HurricaneShotgunUpgradeLevel == 2)
                {
                    PlanetLevel = WeaponUnlockManager.instance.JeratoO95_8510LabUnlock;
                    PlanetLevelNumber = 1025;
                }
                else if (UpgradeDataSystem.instance.HurricaneShotgunUpgradeLevel == 3) //연구 완료되었을 때 표시
                {
                    PlanetLevel = true;
                    PlanetLevelNumber = 0;
                }
                WordPrintMenu.UpgradeTableListPrint(MainTabNumber, SubTabNumber, 2);
                TableClass2.text = string.Format("{0}", UpgradeDataSystem.instance.HurricaneShotgunUpgradeLevel);
                UpgradeTable2.SetActive(true);
                UpgradeTable2.GetComponent<UpgradeTableInformation1>().TableInformChange(ShotgunTypeIcon, TableName, MainTabNumber, SubTabNumber, PlanetLevel, WeaponUnlockManager.instance.DS65Unlock,
                    PlanetLevelNumber, 500, 0, 500, "DS-65 Unlock", UpgradeDataSystem.instance.HurricaneShotgunUpgradeLevel);

                //저격총
                if (UpgradeDataSystem.instance.HurricaneSniperRifleUpgradeLevel == 0)
                {
                    PlanetLevel = WeaponUnlockManager.instance.CrownYosereLabUnlock;
                    PlanetLevelNumber = 1016;
                }
                else if (UpgradeDataSystem.instance.HurricaneSniperRifleUpgradeLevel == 1)
                {
                    PlanetLevel = WeaponUnlockManager.instance.DeltaD31_12721LabUnlock;
                    PlanetLevelNumber = 1022;
                }
                else if (UpgradeDataSystem.instance.HurricaneSniperRifleUpgradeLevel == 2)
                {
                    PlanetLevel = WeaponUnlockManager.instance.JeratoO95_8510LabUnlock;
                    PlanetLevelNumber = 1025;
                }
                else if (UpgradeDataSystem.instance.HurricaneSniperRifleUpgradeLevel == 3) //연구 완료되었을 때 표시
                {
                    PlanetLevel = true;
                    PlanetLevelNumber = 0;
                }
                WordPrintMenu.UpgradeTableListPrint(MainTabNumber, SubTabNumber, 3);
                TableClass3.text = string.Format("{0}", UpgradeDataSystem.instance.HurricaneSniperRifleUpgradeLevel);
                UpgradeTable3.SetActive(true);
                UpgradeTable3.GetComponent<UpgradeTableInformation1>().TableInformChange(SniperRifleTypeIcon, TableName, MainTabNumber, SubTabNumber, PlanetLevel, WeaponUnlockManager.instance.DP9007Unlock,
                    PlanetLevelNumber, 500, 0, 500, "DP-9007 Unlock", UpgradeDataSystem.instance.HurricaneSniperRifleUpgradeLevel);

                //기관단총
                if (UpgradeDataSystem.instance.HurricaneSubmachineGunUpgradeLevel == 0)
                {
                    PlanetLevel = WeaponUnlockManager.instance.CrownYosereLabUnlock;
                    PlanetLevelNumber = 1016;
                }
                else if (UpgradeDataSystem.instance.HurricaneSubmachineGunUpgradeLevel == 1)
                {
                    PlanetLevel = WeaponUnlockManager.instance.DeltaD31_12721LabUnlock;
                    PlanetLevelNumber = 1022;
                }
                else if (UpgradeDataSystem.instance.HurricaneSubmachineGunUpgradeLevel == 2)
                {
                    PlanetLevel = WeaponUnlockManager.instance.JeratoO95_8510LabUnlock;
                    PlanetLevelNumber = 1025;
                }
                else if (UpgradeDataSystem.instance.HurricaneSubmachineGunUpgradeLevel == 3) //연구 완료되었을 때 표시
                {
                    PlanetLevel = true;
                    PlanetLevelNumber = 0;
                }
                WordPrintMenu.UpgradeTableListPrint(MainTabNumber, SubTabNumber, 4);
                TableClass4.text = string.Format("{0}", UpgradeDataSystem.instance.HurricaneSubmachineGunUpgradeLevel);
                UpgradeTable4.SetActive(true);
                UpgradeTable4.GetComponent<UpgradeTableInformation1>().TableInformChange(SubmachineGunTypeIcon, TableName, MainTabNumber, SubTabNumber, PlanetLevel, WeaponUnlockManager.instance.CGD27PillishionUnlock,
                    PlanetLevelNumber, 500, 0, 500, "CGD-27 Pillishion Unlock", UpgradeDataSystem.instance.HurricaneSubmachineGunUpgradeLevel);
            }
            else if (SubTabNumber == 3) //지원 무기
            {
                //보조 장비
                if (UpgradeDataSystem.instance.HurricaneSubGearUpgradeLevel == 0)
                {
                    PlanetLevel = WeaponUnlockManager.instance.PapatusIIILabUnlock;
                    PlanetLevelNumber = 1008;
                }
                else if (UpgradeDataSystem.instance.HurricaneSubGearUpgradeLevel == 1)
                {
                    PlanetLevel = WeaponUnlockManager.instance.JapetAgroneLabUnlock;
                    PlanetLevelNumber = 1018;
                }
                else if (UpgradeDataSystem.instance.HurricaneSubGearUpgradeLevel == 2)
                {
                    PlanetLevel = WeaponUnlockManager.instance.JeratoO95_2252LabUnlock;
                    PlanetLevelNumber = 1024;
                }
                else if (UpgradeDataSystem.instance.HurricaneSubGearUpgradeLevel == 3) //연구 완료되었을 때 표시
                {
                    PlanetLevel = true;
                    PlanetLevelNumber = 0;
                }
                WordPrintMenu.UpgradeTableListPrint(MainTabNumber, SubTabNumber, 1);
                TableClass1.text = string.Format("{0}", UpgradeDataSystem.instance.HurricaneSubGearUpgradeLevel);
                UpgradeTable1.SetActive(true);
                UpgradeTable1.GetComponent<UpgradeTableInformation1>().TableInformChange(SubGearTypeIcon, TableName, MainTabNumber, SubTabNumber, PlanetLevel, WeaponUnlockManager.instance.OSEH302WidowHireUnlock,
                    PlanetLevelNumber, 800, 0, 800, "OSEH-302 Widow Hire Unlock", UpgradeDataSystem.instance.HurricaneSubGearUpgradeLevel);

                //수류탄
                if (UpgradeDataSystem.instance.HurricaneGrenadeUpgradeLevel == 0)
                {
                    PlanetLevel = WeaponUnlockManager.instance.PapatusIIILabUnlock;
                    PlanetLevelNumber = 1008;
                }
                else if (UpgradeDataSystem.instance.HurricaneGrenadeUpgradeLevel == 1)
                {
                    PlanetLevel = WeaponUnlockManager.instance.CrownYosereLabUnlock;
                    PlanetLevelNumber = 1016;
                }
                else if (UpgradeDataSystem.instance.HurricaneGrenadeUpgradeLevel == 2)
                {
                    PlanetLevel = WeaponUnlockManager.instance.DeltaD31_2208LabUnlock;
                    PlanetLevelNumber = 1020;
                }
                else if (UpgradeDataSystem.instance.HurricaneGrenadeUpgradeLevel == 3) //연구 완료되었을 때 표시
                {
                    PlanetLevel = true;
                    PlanetLevelNumber = 0;
                }
                WordPrintMenu.UpgradeTableListPrint(MainTabNumber, SubTabNumber, 2);
                TableClass2.text = string.Format("{0}", UpgradeDataSystem.instance.HurricaneGrenadeUpgradeLevel);
                UpgradeTable2.SetActive(true);
                UpgradeTable2.GetComponent<UpgradeTableInformation1>().TableInformChange(GrenadeTypeIcon, TableName, MainTabNumber, SubTabNumber, PlanetLevel, WeaponUnlockManager.instance.VM5AEGUnlock,
                    PlanetLevelNumber, 800, 0, 800, "VM-5 AEG Unlock", UpgradeDataSystem.instance.HurricaneGrenadeUpgradeLevel);

                //체인지 중화기
                if (UpgradeDataSystem.instance.HurricaneChangeHeavyWeaponUpgradeLevel == 0)
                {
                    PlanetLevel = WeaponUnlockManager.instance.DeriousHeriLabUnlock;
                    PlanetLevelNumber = 1012;
                }
                else if (UpgradeDataSystem.instance.HurricaneChangeHeavyWeaponUpgradeLevel == 1)
                {
                    PlanetLevel = WeaponUnlockManager.instance.DeltaD31_2208LabUnlock;
                    PlanetLevelNumber = 1020;
                }
                else if (UpgradeDataSystem.instance.HurricaneChangeHeavyWeaponUpgradeLevel == 2)
                {
                    PlanetLevel = WeaponUnlockManager.instance.JeratoO95_2252LabUnlock;
                    PlanetLevelNumber = 1024;
                }
                else if (UpgradeDataSystem.instance.HurricaneChangeHeavyWeaponUpgradeLevel == 3) //연구 완료되었을 때 표시
                {
                    PlanetLevel = true;
                    PlanetLevelNumber = 0;
                }
                WordPrintMenu.UpgradeTableListPrint(MainTabNumber, SubTabNumber, 3);
                TableClass3.text = string.Format("{0}", UpgradeDataSystem.instance.HurricaneChangeHeavyWeaponUpgradeLevel);
                UpgradeTable3.SetActive(true);
                UpgradeTable3.GetComponent<UpgradeTableInformation1>().TableInformChange(ChangeHeavyWeaponIcon, TableName, MainTabNumber, SubTabNumber, PlanetLevel, WeaponUnlockManager.instance.ChangeHeavyWeaponTotalUnlock,
                    PlanetLevelNumber, 800, 0, 800, "Change Heavy Weapon Total Unlock", UpgradeDataSystem.instance.HurricaneChangeHeavyWeaponUpgradeLevel);
            }
        }

        else if (MainTabNumber == 3) //함선 지원
        {
            if (SubTabNumber == 1) //무기 지원
            {
                //보급 지원
                if (UpgradeDataSystem.instance.ShipAmmoSupportUpgradeLevel == 0)
                {
                    PlanetLevel = WeaponUnlockManager.instance.AposisLabUnlock;
                    PlanetLevelNumber = 1002;
                }
                else if (UpgradeDataSystem.instance.ShipAmmoSupportUpgradeLevel == 1)
                {
                    PlanetLevel = WeaponUnlockManager.instance.DeriousHeriLabUnlock;
                    PlanetLevelNumber = 1012;
                }
                else if (UpgradeDataSystem.instance.ShipAmmoSupportUpgradeLevel == 2)
                {
                    PlanetLevel = WeaponUnlockManager.instance.DeltaD31_12721LabUnlock;
                    PlanetLevelNumber = 1022;
                }
                else if (UpgradeDataSystem.instance.ShipAmmoSupportUpgradeLevel == 3) //연구 완료되었을 때 표시
                {
                    PlanetLevel = true;
                    PlanetLevelNumber = 0;
                }
                WordPrintMenu.UpgradeTableListPrint(MainTabNumber, SubTabNumber, 1);
                TableClass1.text = string.Format("{0}", UpgradeDataSystem.instance.ShipAmmoSupportUpgradeLevel);
                UpgradeTable1.SetActive(true);
                UpgradeTable1.GetComponent<UpgradeTableInformation1>().TableInformChange(LogisticsSupportIcon, TableName, MainTabNumber, SubTabNumber, PlanetLevel, WeaponUnlockManager.instance.AmmoSupportUnlock,
                    PlanetLevelNumber, 600, 0, 600, "Ammo Support Unlock", UpgradeDataSystem.instance.ShipAmmoSupportUpgradeLevel);

                //중화기 지원
                if (UpgradeDataSystem.instance.ShipHeavyWeaponSupportUpgradeLevel == 0)
                {
                    PlanetLevel = WeaponUnlockManager.instance.CrownYosereLabUnlock;
                    PlanetLevelNumber = 1016;
                }
                else if (UpgradeDataSystem.instance.ShipHeavyWeaponSupportUpgradeLevel == 1)
                {
                    PlanetLevel = WeaponUnlockManager.instance.DeltaD31_2208LabUnlock;
                    PlanetLevelNumber = 1020;
                }
                else if (UpgradeDataSystem.instance.ShipHeavyWeaponSupportUpgradeLevel == 2)
                {
                    PlanetLevel = WeaponUnlockManager.instance.JeratoO95_8510LabUnlock;
                    PlanetLevelNumber = 1025;
                }
                else if (UpgradeDataSystem.instance.ShipHeavyWeaponSupportUpgradeLevel == 3) //연구 완료되었을 때 표시
                {
                    PlanetLevel = true;
                    PlanetLevelNumber = 0;
                }
                WordPrintMenu.UpgradeTableListPrint(MainTabNumber, SubTabNumber, 2);
                TableClass2.text = string.Format("{0}", UpgradeDataSystem.instance.ShipHeavyWeaponSupportUpgradeLevel);
                UpgradeTable2.SetActive(true);
                UpgradeTable2.GetComponent<UpgradeTableInformation1>().TableInformChange(HeavyWeaponSupportIcon, TableName, MainTabNumber, SubTabNumber, PlanetLevel, WeaponUnlockManager.instance.PowerHeavyWeaponTotalUnlock,
                    PlanetLevelNumber, 700, 0, 700, "Power Heavy Weapon Total Unlock", UpgradeDataSystem.instance.ShipHeavyWeaponSupportUpgradeLevel);

                //탑승 차량 지원
                if (UpgradeDataSystem.instance.ShipVehicleSupportUpgradeLevel == 0)
                {
                    PlanetLevel = WeaponUnlockManager.instance.JapetAgroneLabUnlock;
                    PlanetLevelNumber = 1018;
                }
                else if (UpgradeDataSystem.instance.ShipVehicleSupportUpgradeLevel == 1)
                {
                    PlanetLevel = WeaponUnlockManager.instance.DeltaD31_12721LabUnlock;
                    PlanetLevelNumber = 1022;
                }
                else if (UpgradeDataSystem.instance.ShipVehicleSupportUpgradeLevel == 2)
                {
                    PlanetLevel = WeaponUnlockManager.instance.JeratoO95_8510LabUnlock;
                    PlanetLevelNumber = 1025;
                }
                else if (UpgradeDataSystem.instance.ShipVehicleSupportUpgradeLevel == 3) //연구 완료되었을 때 표시
                {
                    PlanetLevel = true;
                    PlanetLevelNumber = 0;
                }
                WordPrintMenu.UpgradeTableListPrint(MainTabNumber, SubTabNumber, 3);
                TableClass3.text = string.Format("{0}", UpgradeDataSystem.instance.ShipVehicleSupportUpgradeLevel);
                UpgradeTable3.SetActive(true);
                UpgradeTable3.GetComponent<UpgradeTableInformation1>().TableInformChange(VehicleSupportIcon, TableName, MainTabNumber, SubTabNumber, PlanetLevel, WeaponUnlockManager.instance.VehicleTotalUnlock,
                    PlanetLevelNumber, 1000, 0, 1000, "Vehicle Total Unlock", UpgradeDataSystem.instance.ShipVehicleSupportUpgradeLevel);
            }
            else if (SubTabNumber == 2) //공격 지원
            {
                //폭격 지원
                if (UpgradeDataSystem.instance.ShipStrikeSupportUpgradeLevel == 0)
                {
                    PlanetLevel = WeaponUnlockManager.instance.DeriousHeriLabUnlock;
                    PlanetLevelNumber = 1012;
                }
                else if (UpgradeDataSystem.instance.ShipStrikeSupportUpgradeLevel == 1)
                {
                    PlanetLevel = WeaponUnlockManager.instance.JapetAgroneLabUnlock;
                    PlanetLevelNumber = 1018;
                }
                else if (UpgradeDataSystem.instance.ShipStrikeSupportUpgradeLevel == 2)
                {
                    PlanetLevel = WeaponUnlockManager.instance.JeratoO95_2252LabUnlock;
                    PlanetLevelNumber = 1024;
                }
                else if (UpgradeDataSystem.instance.ShipStrikeSupportUpgradeLevel == 3) //연구 완료되었을 때 표시
                {
                    PlanetLevel = true;
                    PlanetLevelNumber = 0;
                }
                WordPrintMenu.UpgradeTableListPrint(MainTabNumber, SubTabNumber, 1);
                TableClass1.text = string.Format("{0}", UpgradeDataSystem.instance.ShipStrikeSupportUpgradeLevel);
                UpgradeTable1.SetActive(true);
                UpgradeTable1.GetComponent<UpgradeTableInformation1>().TableInformChange(BombardmentSupportIcon, TableName, MainTabNumber, SubTabNumber, PlanetLevel, WeaponUnlockManager.instance.AirStrikeTotalUnlock,
                    PlanetLevelNumber, 650, 0, 650, "Air Strike Total Unlock", UpgradeDataSystem.instance.ShipStrikeSupportUpgradeLevel);
            }
        }
    }

    //연구 목록 가져오기
    void UpgradeListLoad()
    {
        FleetGroupPrefab.SetActive(true);
        FleetPrefabClick.SetActive(true);
        ShipTabPrefabClick.SetActive(true);
        LoadTableList(1, 1);
    }

    //탭 목록의 클릭된 정보를 해제
    void CancelTabClick()
    {
        if (FleetPrefabClick.activeSelf == true)
            FleetPrefabClick.SetActive(false);
        else if (DeltaHurricaneTabPrefabClick.activeSelf == true)
            DeltaHurricaneTabPrefabClick.SetActive(false);
        else if (ShipSupportTabPrefabClick.activeSelf == true)
            ShipSupportTabPrefabClick.SetActive(false);

        else if (ShipTabPrefabClick.activeSelf == true)
            ShipTabPrefabClick.SetActive(false);
        else if (TurretTabPrefabClick.activeSelf == true)
            TurretTabPrefabClick.SetActive(false);
        else if (SkillTabPrefabClick.activeSelf == true)
            SkillTabPrefabClick.SetActive(false);
        else if (DeltaHurricaneSuitTabPrefabClick.activeSelf == true)
            DeltaHurricaneSuitTabPrefabClick.SetActive(false);
        else if (MainWeaponsTabPrefabClick.activeSelf == true)
            MainWeaponsTabPrefabClick.SetActive(false);
        else if (DeltaHurricaneSupportWeaponsTabPrefabClick.activeSelf == true)
            DeltaHurricaneSupportWeaponsTabPrefabClick.SetActive(false);
        else if (WeaponSupportTabPrefabClick.activeSelf == true)
            WeaponSupportTabPrefabClick.SetActive(false);
        else if (StrikeSupportTabPrefabClick.activeSelf == true)
            StrikeSupportTabPrefabClick.SetActive(false);
    }

    //접속한 연구 테이블의 연구 정보 불러오기
    public void AccessUpgrade(int MainTabNumber, int SubTabNumber, int EnterNumber, Sprite Icon)
    {
        CancelNumber = 2;
        AllTabPrefab.SetActive(false);
        TabTableWindowPrefab.SetActive(false);
        UpgradelistPrefab.SetActive(true);
        UpgradeIcon.GetComponent<Image>().sprite = Icon;
        UpgradeIcon.gameObject.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 3);

        UpgradelistMainTable1.SetActive(true);
        UpgradelistMainTable1.GetComponent<UpgradeTableInformation2>().MainTabNumber = MainTabNumber;
        UpgradelistMainTable1.GetComponent<UpgradeTableInformation2>().SubTabNumber = SubTabNumber;
        UpgradelistMainTable1.GetComponent<UpgradeTableInformation2>().AccessNumber = EnterNumber;
        WordPrintMenu.UpgradeTableExplainPrint(MainTabNumber, SubTabNumber, EnterNumber);
    }

    //업그레이드 메시지 창 출력
    public void UpgradeMessage(int MainTabNumber, int SubTabNumber, int AccessNumber, int UpgradeNumber, int SubUpgradeNumber)
    {
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
        SystemMessages.MessageType = 4;
        SystemMessages.MainTabNumber = MainTabNumber;
        SystemMessages.SubTabNumber = SubTabNumber;
        SystemMessages.AccessNumber = AccessNumber;
        SystemMessages.UpgradeNumber = UpgradeNumber;
        SystemMessages.SubUpgradeNumber = SubUpgradeNumber;
        WordPrintMenu.UpgradeProgress(MainTabNumber, SubTabNumber, AccessNumber);
        WordPrintMenu.UpgradeTextsPrefab.SetActive(true);
        StartCoroutine(SystemMessages.MessageOn(4, 3));
    }

    //연구 메뉴 부팅
    public void StartLabMenu()
    {
        FleetTabClick();
        StartCoroutine(CameraZoom.BootingAnimation());
        StartCoroutine(LabMenuWindowOpen());
        UpgradeListLoad();
    }

    //연구 메뉴 작동
    public IEnumerator LabMenuWindowOpen()
    {
        LabExitButton.raycastTarget = false;
        yield return new WaitForSecondsRealtime(0.55f);
        MainMenuButtonSystem.LabMenuWindow.SetActive(true); //연구 메뉴창 활성화
        CancelNumber = 1;

        if (BattleSave.Save1.LabTutorial == true) //첫 튜토리얼
        {
            StartCoroutine(TutorialSystem.TutorialWindowOpen(103));
        }
        yield return new WaitForSecondsRealtime(1.05f);
        LabExitButton.raycastTarget = true; //연구 메뉴 나가기 버튼 활성화
    }

    //언락 시작
    public void UnlockStart(string UnlockName)
    {
        if (UnlockName == "Flagship Unlock")
            WeaponUnlockManager.instance.FlagshipUnlock = true;
        else if (UnlockName == "Formation Ship Unlock")
            WeaponUnlockManager.instance.FormationShipUnlock = true;
        else if(UnlockName == "Tactical Ship Unlock")
            WeaponUnlockManager.instance.TacticalShipUnlock = true;
        else if (UnlockName == "Ship Cannon Unlock")
            WeaponUnlockManager.instance.ShipCannonUnlock = true;
        else if (UnlockName == "Ship Missile Unlock")
            WeaponUnlockManager.instance.ShipMissileUnlock = true;
        else if (UnlockName == "Ship Fighter Unlock")
            WeaponUnlockManager.instance.ShipFighterUnlock = true;

        else if (UnlockName == "Flagship Skill Attack Unlock")
            WeaponUnlockManager.instance.FlagshipSkillAttackUnlock = true;
        else if (UnlockName == "Fleet Attack Skill Unlock")
            WeaponUnlockManager.instance.FleetAttackSkillUnlock = true;

        else if (UnlockName == "Suit Hit Point Unlock")
            WeaponUnlockManager.instance.SuitHitPointUnlock = true;

        else if (UnlockName == "DT-37 Unlock")
            WeaponUnlockManager.instance.DT37Unlock = true;
        else if (UnlockName == "DS-65 Unlock")
            WeaponUnlockManager.instance.DS65Unlock = true;
        else if (UnlockName == "DP-9007 Unlock")
            WeaponUnlockManager.instance.DP9007Unlock = true;
        else if (UnlockName == "CGD-27 Pillishion Unlock")
            WeaponUnlockManager.instance.CGD27PillishionUnlock = true;

        else if (UnlockName == "OSEH-302 Widow Hire Unlock")
            WeaponUnlockManager.instance.OSEH302WidowHireUnlock = true;
        else if (UnlockName == "VM-5 AEG Unlock")
            WeaponUnlockManager.instance.VM5AEGUnlock = true;
        else if (UnlockName == "Change Heavy Weapon Total Unlock")
            WeaponUnlockManager.instance.ChangeHeavyWeaponTotalUnlock = true;

        else if (UnlockName == "Ammo Support Unlock")
            WeaponUnlockManager.instance.AmmoSupportUnlock = true;
        else if (UnlockName == "Power Heavy Weapon Total Unlock")
            WeaponUnlockManager.instance.PowerHeavyWeaponTotalUnlock = true;
        else if (UnlockName == "Vehicle Total Unlock")
            WeaponUnlockManager.instance.VehicleTotalUnlock = true;
        else if (UnlockName == "Air Strike Total Unlock")
            WeaponUnlockManager.instance.AirStrikeTotalUnlock = true;

        LoadTableList(MainTabNumber, SubTabNumber);
    }
}