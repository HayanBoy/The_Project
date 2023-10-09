using System;
using System.Collections;
using UnityEngine;

public class UpgradeDataSystem : MonoBehaviour
{
    public static UpgradeDataSystem instance = null;
    UpgradeMenu UpgradeMenu;

    [Header("함선 장갑 레벨 정보")]
    public int FlagshipUpgradeLevel; //기함
    public int FormationUpgradeLevel; //편대함
    public int TacticalShipUpgradeLevel; //전략함

    [Header("함선 무기 레벨 정보")]
    public int ShipCannonUpgradeLevel; //함포
    public int ShipMissileUpgradeLevel; //미사일
    public int ShipFighterUpgradeLevel; //함재기

    [Header("함대 지원 레벨 정보")]
    public int FlagshipAttackSkillUpgradeLevel; //기함 공격 지원
    public int FleetAttackSkillUpgradeLevel; //함대 공격 지원

    [Header("델타 허리케인 내구도 레벨 정보")]
    public int HurricaneHitPointUpgradeLevel; //체력

    [Header("델타 허리케인 기본 무기 레벨 정보")]
    public int HurricaneAssaultRifleUpgradeLevel; //돌격 소총
    public int HurricaneShotgunUpgradeLevel; //샷건
    public int HurricaneSniperRifleUpgradeLevel; //저격총
    public int HurricaneSubmachineGunUpgradeLevel; //기관단총

    [Header("델타 허리케인 지원 무기 레벨 정보")]
    public int HurricaneSubGearUpgradeLevel; //보조 장비
    public int HurricaneGrenadeUpgradeLevel; //수류탄
    public int HurricaneChangeHeavyWeaponUpgradeLevel; //체인지 중화기

    [Header("함선 지원 레벨 정보")]
    public int ShipAmmoSupportUpgradeLevel; //보급 지원
    public int ShipHeavyWeaponSupportUpgradeLevel; //중화기 지원
    public int ShipVehicleSupportUpgradeLevel; //탑승 차량 지원
    public int ShipStrikeSupportUpgradeLevel; //공격 지원

    [Header("함선 장갑 업그레이드 정보")]
    public float FlagshipHitPoints; //기함 선체
    public float FormationHitPoints; //편대함 선체
    public float ShieldShipHitPoints; //방패함 선체(전략함)
    public float CarrierHitPoints; //우주모함 선체(전략함)

    [Header("함선 무기 업그레이드 정보")]
    public int FlagshipSilenceSistDamage; //사일런스 시스트 연발 주포 데미지(기함)
    public int FlagshipOverJumpDamage; //초과점프 단발 주포 데미지(기함)
    public int FlagshipSadLilly345Damage; //세드 릴리-345 단발 미사일 데미지(기함)
    public int FlagshipDeltaNeedle42HalistDamage; //델타 니들-42 할리스트 멀티 미사일 데미지(기함)
    public int FlagshipGF12DeltaWingBomberDamage; //GF-12 델타 윙 폭격기 데미지(기함)
    public int FormationSilenceSistDamage; //사일런스 시스트 연발 주포 데미지(편대함)
    public int FormationOverJumpDamage; //초과점프 단발 주포 데미지(편대함)
    public int FormationSadLilly345Damage; //세드 릴리-345 단발 미사일 데미지(편대함)
    public int FormationDeltaNeedle42HalistDamage; //델타 니들-42 할리스트 멀티 미사일 데미지(편대함)
    public int CarrierGF12DeltaWingBomberDamage; //GF-12 델타 윙 폭격기 데미지(우주모함)

    [Header("기함 공격 업그레이드 정보(함대 지원)")]
    public int SikroClassCruiseMissileDamage; //시크로급 순항 미사일 데미지

    [Header("함대 공격 업그레이드 정보(함대 지원)")]
    public int Cysiro47PatriotMissileDamage; //사이시로-47 페트리엇 미사일 데미지

    [Header("델타 허리케인 내구도 업그레이드 정보")]
    public float HurricaneHitPoint; //델타 허리케인 체력
    public float HurricaneHitArmor; //델타 허리케인 방어구

    [Header("델타 허리케인 기본 무기 업그레이드 정보")]
    public int DT37Damage; //DT-37 돌격 소총 데미지
    public int DS65Damage; //DS-65 샷건 데미지
    public int DP9007Damage; //DP-9007 저격총 데미지
    public int CGD27PillishionDamage; //CGD-27 필리시온 기관단총 데미지

    [Header("델타 허리케인 보조 장비 업그레이드 정보(지원 무기)")]
    public int OSEH302WidowHireDamage; //OSEH-302 Widow Hire 추적 미사일 데미지

    [Header("델타 허리케인 수류탄 업그레이드 정보(지원 무기)")]
    public int VM5AEGDamage; //VM-5 AEG 수류탄 데미지

    [Header("델타 허리케인 체인지 중화기 업그레이드 정보(지원 무기)")]
    public int ArthesL775Step1Damage; //Arthes L-775 충전 레이져 1단계 데미지
    public int ArthesL775Step2Damage; //Arthes L-775 충전 레이져 2단계 데미지
    public int ArthesL775Step3Damage; //Arthes L-775 충전 레이져 3단계 데미지
    public int ArthesL775Step4Damage; //Arthes L-775 충전 레이져 4단계 데미지

    public int Hydra56Damage; //Hydra-56 분리 철갑포 데미지
    public int MEAGDamage; //MEAG 레일건 데미지
    public int MEAGAddDamage; //MEAG 레일건 초당 상승 데미지
    public int UGG98Step1Damage; //UGG 98 중력포 1단계 데미지
    public int UGG98Step2Damage; //UGG 98 중력포 2단계 데미지

    [Header("함선 보급 지원 업그레이드 정보")]
    public int SupportAmmoAmount; //탄약 지원량

    [Header("함선 중화기 지원 업그레이드 정보")]
    public int M3078Damage; //M3078 미니건 데미지
    public int ASC365Damage; //ASC 365 화염방사기 데미지

    [Header("함선 탑승 차량 지원 업그레이드 정보")]
    public float MBCA79IronHurricaneHitPoint; //MBCA-79 Iron Hurricane 체력
    public float MBCA79IronHurricaneArmor; //MBCA-79 Iron Hurricane 방어
    public int MBCA79IronHurricaneHTACDamage; //HTAC 주포 데미지
    public int MBCA79IronHurricaneAPCDamage; //APC 플라즈마 포 데미지
    public int MBCA79IronHurricaneOSEHSDamage; //OSEHS 추적 미사일 데미지
    public int MBCA79IronHurricaneFBWSIrisDamage; //FBWS Iris 게틀링포 데미지

    [Header("함선 폭격 지원 업그레이드 정보")]
    public int PGM1036ScaletHawkDamage; //Planet Guided Missile 1036 Scalet Hawk 데미지

    [Header("연구 비용")]
    public int GlopaorosCost; //비용을 지불하기 위해 전송된 자금
    public int ConstructionResourceCost;
    public int TaritronicCost;

    public bool isNextPlanetOpen; //업그레이드가 한 번 끝날 때마다 해당 연구목록의 다음 행성 해방 여부를 확인한다.

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    /// <summary>
    /// 업그레이드 레벨 및 비용
    /// </summary>
    //아래의 "업그레이드 레벨 및 비용"이라는 주석이 있는 모든 메서드(void 이름()으로 이루어짐)들은 서로 동일한 구조이므로 아래의 화살표로 표기된 설명란들을 참고할 것.
    //밸런스 작업은 아래 표기된 업그레이드 대상과 비용들 값만 조절하면 된다. 구조를 절때 바꾸려하지 말것.
    //기함 계급 업그레이드 레벨 및 비용
    public void FlagshipClassLevel()
    {
        if (FlagshipUpgradeLevel == 0) //<-- 업그레이드를 아예 하지 않은 상태. 등급 0에 해당하며, 이 상태는 보통 비용을 주고 언락을 요구하는 단계이다.
        {
            instance.FlagshipHitPoints = 45000; //기함 선체 <-- 업그레이드 대상이다. 연구를 아예 안한 등급 0에 해당하는 값. 보통 제일 먼저 이렇게 표기된다.

            //여기는 등급 0 -> 등급 1로 업그레이드하기 위한 비용. 보통 비용 내역은 업그레이드 대상 다음으로 이렇게 표기된다.
            GlopaorosCost = 8000; //<-- 필요한 글로파오로스 비용
            ConstructionResourceCost = 0; //<-- 필요한 건설 자원 비용
            TaritronicCost = 6000; //<--필요한 타리트로닉 비용
        }
        else if (FlagshipUpgradeLevel == 1) //<-- 업그레이드 등급 1. 첫 번째 연구를 한 상태
        {
            instance.FlagshipHitPoints = 67500; //<-- 등급 1에 해당하는 값

            //여기는 등급 1 -> 등급 2로 업그레이드하기 위한 비용
            GlopaorosCost = 12000;
            ConstructionResourceCost = 0;
            TaritronicCost = 8000;
        }
        else if (FlagshipUpgradeLevel == 2) //<-- 업그레이드 등급 2. 두 번째 연구를 한 상태
        {
            instance.FlagshipHitPoints = 90000; //<-- 등급 2에 해당하는 값

            //여기는 등급 2 -> 등급 3로 업그레이드하기 위한 비용(여기가 현재 마지막 비용)
            GlopaorosCost = 15000;
            ConstructionResourceCost = 0;
            TaritronicCost = 13000;
        }
        else if (FlagshipUpgradeLevel == 3) //<-- 업그레이드 등급 3. 세 번째 연구를 한 상태(현재 등급 3가 마지막). 이 후 더 이상 연구가 불가능
        {
            instance.FlagshipHitPoints = 112500; //<-- 등급 3에 해당하는 값. 여기는 이미 등급 3로 연구를 진행하는 구간이므로 더 이상 비용이 표기되지 않으며, 업그레이드 대상만 표기된다.
        }
    }

    //편대함 계급 업그레이드 레벨 및 비용
    public void FormationShipClassLevel()
    {
        if (FormationUpgradeLevel == 0)
        {
            instance.FormationHitPoints = 13500; //편대함 선체

            GlopaorosCost = 3200;
            ConstructionResourceCost = 0;
            TaritronicCost = 2500;
        }
        else if (FormationUpgradeLevel == 1)
        {
            instance.FormationHitPoints = 20250;

            GlopaorosCost = 5600;
            ConstructionResourceCost = 0;
            TaritronicCost = 4200;
        }
        else if (FormationUpgradeLevel == 2)
        {
            instance.FormationHitPoints = 27000;

            GlopaorosCost = 7600;
            ConstructionResourceCost = 0;
            TaritronicCost = 6100;
        }
        else if (FormationUpgradeLevel == 3)
        {
            instance.FormationHitPoints = 33750;
        }
    }

    //전략함 계급 업그레이드 레벨 및 비용
    public void TacticalShipClassLevel()
    {
        if (TacticalShipUpgradeLevel == 0)
        {
            instance.ShieldShipHitPoints = 22500; //방패함 선체
            instance.CarrierHitPoints = 18000; //우주모함 선체

            GlopaorosCost = 4000;
            ConstructionResourceCost = 0;
            TaritronicCost = 3500;
        }
        else if (TacticalShipUpgradeLevel == 1)
        {
            instance.ShieldShipHitPoints = 33750;
            instance.CarrierHitPoints = 27000;

            GlopaorosCost = 6500;
            ConstructionResourceCost = 0;
            TaritronicCost = 5500;
        }
        else if (TacticalShipUpgradeLevel == 2)
        {
            instance.ShieldShipHitPoints = 45000;
            instance.CarrierHitPoints = 36000;

            GlopaorosCost = 9000;
            ConstructionResourceCost = 0;
            TaritronicCost = 8000;
        }
        else if (TacticalShipUpgradeLevel == 3)
        {
            instance.ShieldShipHitPoints = 56250;
            instance.CarrierHitPoints = 45000;
        }
    }

    //함포 업그레이드 레벨 및 비용
    public void ShipCannonClassLevel()
    {
        if (ShipCannonUpgradeLevel == 0)
        {
            instance.FlagshipSilenceSistDamage = 90; //사일런스 시스트 연발 주포 데미지
            instance.FlagshipOverJumpDamage = 350; //초과점프 단발 주포 데미지

            instance.FormationSilenceSistDamage = 45;
            instance.FormationOverJumpDamage = 160;

            GlopaorosCost = 4000;
            ConstructionResourceCost = 0;
            TaritronicCost = 3000;
        }
        else if (ShipCannonUpgradeLevel == 1)
        {
            instance.FlagshipSilenceSistDamage = 117;
            instance.FlagshipOverJumpDamage = 455;

            instance.FormationSilenceSistDamage = 59;
            instance.FormationOverJumpDamage = 208;

            GlopaorosCost = 7000;
            ConstructionResourceCost = 0;
            TaritronicCost = 5000;
        }
        else if (ShipCannonUpgradeLevel == 2)
        {
            instance.FlagshipSilenceSistDamage = 144;
            instance.FlagshipOverJumpDamage = 560;

            instance.FormationSilenceSistDamage = 72;
            instance.FormationOverJumpDamage = 256;

            GlopaorosCost = 9500;
            ConstructionResourceCost = 0;
            TaritronicCost = 7500;
        }
    }

    //미사일 업그레이드 레벨 및 비용
    public void ShipMissileClassLevel()
    {
        if (ShipMissileUpgradeLevel == 0)
        {
            instance.FlagshipSadLilly345Damage = 210; //세드 릴리-345 단발 미사일 데미지
            instance.FlagshipDeltaNeedle42HalistDamage = 75; //델타 니들-42 할리스트 멀티 미사일 데미지

            instance.FormationSadLilly345Damage = 100;
            instance.FormationDeltaNeedle42HalistDamage = 32;

            GlopaorosCost = 4000;
            ConstructionResourceCost = 0;
            TaritronicCost = 3000;
        }
        else if (ShipMissileUpgradeLevel == 1)
        {
            instance.FlagshipSadLilly345Damage = 273;
            instance.FlagshipDeltaNeedle42HalistDamage = 98;

            instance.FormationSadLilly345Damage = 130;
            instance.FormationDeltaNeedle42HalistDamage = 42;

            GlopaorosCost = 7000;
            ConstructionResourceCost = 0;
            TaritronicCost = 5000;
        }
        else if (ShipMissileUpgradeLevel == 2)
        {
            instance.FlagshipSadLilly345Damage = 336;
            instance.FlagshipDeltaNeedle42HalistDamage = 120;

            instance.FormationSadLilly345Damage = 160;
            instance.FormationDeltaNeedle42HalistDamage = 51;

            GlopaorosCost = 9500;
            ConstructionResourceCost = 0;
            TaritronicCost = 7500;
        }
        else if (ShipMissileUpgradeLevel == 3)
        {
            instance.FlagshipSadLilly345Damage = 378;
            instance.FlagshipDeltaNeedle42HalistDamage = 135;

            instance.FormationSadLilly345Damage = 180;
            instance.FormationDeltaNeedle42HalistDamage = 58;
        }
    }

    //함재기 업그레이드 레벨 및 비용
    public void ShipFighterClassLevel()
    {
        if (ShipFighterUpgradeLevel == 0)
        {
            instance.FlagshipGF12DeltaWingBomberDamage = 210; //현재 미구현(이건 밸런스 작업할 필요없다)

            instance.CarrierGF12DeltaWingBomberDamage = 65; //GF-12 델타 윙 폭격기 데미지(우주모함에서 발진)

            GlopaorosCost = 4000;
            ConstructionResourceCost = 0;
            TaritronicCost = 3500;
        }
        else if (ShipFighterUpgradeLevel == 1)
        {
            instance.FlagshipGF12DeltaWingBomberDamage = 273;

            instance.CarrierGF12DeltaWingBomberDamage = 85;

            GlopaorosCost = 7000;
            ConstructionResourceCost = 0;
            TaritronicCost = 5500;
        }
        else if (ShipFighterUpgradeLevel == 2)
        {
            instance.FlagshipGF12DeltaWingBomberDamage = 336;

            instance.CarrierGF12DeltaWingBomberDamage = 105;

            GlopaorosCost = 9500;
            ConstructionResourceCost = 0;
            TaritronicCost = 8000;
        }
        else if (ShipFighterUpgradeLevel == 3)
        {
            instance.FlagshipGF12DeltaWingBomberDamage = 378;

            instance.CarrierGF12DeltaWingBomberDamage = 125;
        }
    }

    //기함 공격 스킬 업그레이드 레벨 및 비용
    public void FlagshipAttackSkillClassLevel()
    {
        if (FlagshipAttackSkillUpgradeLevel == 0)
        {
            instance.SikroClassCruiseMissileDamage = 9500; //시크로급 순항 미사일 데미지(기함 공격 스킬)

            GlopaorosCost = 4000;
            ConstructionResourceCost = 0;
            TaritronicCost = 2500;
        }
        else if (FlagshipAttackSkillUpgradeLevel == 1)
        {
            instance.SikroClassCruiseMissileDamage = 19000;

            GlopaorosCost = 7000;
            ConstructionResourceCost = 0;
            TaritronicCost = 5000;
        }
        else if (FlagshipAttackSkillUpgradeLevel == 2)
        {
            instance.SikroClassCruiseMissileDamage = 27500;

            GlopaorosCost = 12000;
            ConstructionResourceCost = 0;
            TaritronicCost = 8000;
        }
        else if (FlagshipAttackSkillUpgradeLevel == 3)
        {
            instance.SikroClassCruiseMissileDamage = 37000;
        }
    }

    //함대 공격 스킬 업그레이드 레벨 및 비용
    public void FleetAttackSkillClassLevel()
    {
        if (FleetAttackSkillUpgradeLevel == 0)
        {
            instance.Cysiro47PatriotMissileDamage = 200; //사이시로-47 페트리엇 미사일 데미지(함대 공격 스킬)

            GlopaorosCost = 4000;
            ConstructionResourceCost = 0;
            TaritronicCost = 3000;
        }
        else if (FleetAttackSkillUpgradeLevel == 1)
        {
            instance.Cysiro47PatriotMissileDamage = 366;

            GlopaorosCost = 7000;
            ConstructionResourceCost = 0;
            TaritronicCost = 6000;
        }
        else if (FleetAttackSkillUpgradeLevel == 2)
        {
            instance.Cysiro47PatriotMissileDamage = 533;

            GlopaorosCost = 12000;
            ConstructionResourceCost = 0;
            TaritronicCost = 9000;
        }
        else if (FleetAttackSkillUpgradeLevel == 3)
        {
            instance.Cysiro47PatriotMissileDamage = 700;
        }
    }

    //델타 허리케인 체력 업그레이드 레벨 및 비용
    public void DeltaHurricaneHitPointLevel()
    {
        if (HurricaneHitPointUpgradeLevel == 0)
        {
            instance.HurricaneHitPoint = 1000; //허리케인 체력

            GlopaorosCost = 3500;
            ConstructionResourceCost = 0;
            TaritronicCost = 6000;
        }
        else if (HurricaneHitPointUpgradeLevel == 1)
        {
            instance.HurricaneHitPoint = 1400;

            GlopaorosCost = 6000;
            ConstructionResourceCost = 0;
            TaritronicCost = 8000;
        }
        else if (HurricaneHitPointUpgradeLevel == 2)
        {
            instance.HurricaneHitPoint = 1800;

            GlopaorosCost = 8000;
            ConstructionResourceCost = 0;
            TaritronicCost = 12000;
        }
        else if (HurricaneHitPointUpgradeLevel == 3)
        {
            instance.HurricaneHitPoint = 2200;
        }
    }

    //델타 허리케인 돌격 소총 업그레이드 레벨 및 비용
    public void DeltaHurricaneAssaultRifleLevel()
    {
        if (HurricaneAssaultRifleUpgradeLevel == 0)
        {
            instance.DT37Damage = 35; //DT-37 돌격 소총 데미지

            GlopaorosCost = 1750;
            ConstructionResourceCost = 0;
            TaritronicCost = 3000;
        }
        else if (HurricaneAssaultRifleUpgradeLevel == 1)
        {
            instance.DT37Damage = 49;

            GlopaorosCost = 3000;
            ConstructionResourceCost = 0;
            TaritronicCost = 5000;
        }
        else if (HurricaneAssaultRifleUpgradeLevel == 2)
        {
            instance.DT37Damage = 63;

            GlopaorosCost = 4000;
            ConstructionResourceCost = 0;
            TaritronicCost = 6000;
        }
        else if (HurricaneAssaultRifleUpgradeLevel == 3)
        {
            instance.DT37Damage = 77;
        }
    }

    //델타 허리케인 샷건 업그레이드 레벨 및 비용
    public void DeltaHurricaneShotgunLevel()
    {
        if (HurricaneShotgunUpgradeLevel == 0)
        {
            instance.DS65Damage = 31; //DS-65 샷건 한 발당 데미지

            GlopaorosCost = 1750;
            ConstructionResourceCost = 0;
            TaritronicCost = 3000;
        }
        else if (HurricaneShotgunUpgradeLevel == 1)
        {
            instance.DS65Damage = 42;

            GlopaorosCost = 3000;
            ConstructionResourceCost = 0;
            TaritronicCost = 5000;
        }
        else if (HurricaneShotgunUpgradeLevel == 2)
        {
            instance.DS65Damage = 55;

            GlopaorosCost = 4000;
            ConstructionResourceCost = 0;
            TaritronicCost = 6000;
        }
        else if (HurricaneShotgunUpgradeLevel == 3)
        {
            instance.DS65Damage = 68;
        }
    }

    //델타 허리케인 저격총 업그레이드 레벨 및 비용
    public void DeltaHurricaneSniperRifleLevel()
    {
        if (HurricaneSniperRifleUpgradeLevel == 0)
        {
            instance.DP9007Damage = 230; //DP-9007 저격총 데미지

            GlopaorosCost = 1750;
            ConstructionResourceCost = 0;
            TaritronicCost = 3000;
        }
        else if (HurricaneSniperRifleUpgradeLevel == 1)
        {
            instance.DP9007Damage = 322;

            GlopaorosCost = 3000;
            ConstructionResourceCost = 0;
            TaritronicCost = 5000;
        }
        else if (HurricaneSniperRifleUpgradeLevel == 2)
        {
            instance.DP9007Damage = 414;

            GlopaorosCost = 4000;
            ConstructionResourceCost = 0;
            TaritronicCost = 6000;
        }
        else if (HurricaneSniperRifleUpgradeLevel == 3)
        {
            instance.DP9007Damage = 506;
        }
    }

    //델타 허리케인 기관단총 업그레이드 레벨 및 비용
    public void DeltaHurricaneSubmachineGunLevel()
    {
        if (HurricaneSubmachineGunUpgradeLevel == 0)
        {
            instance.CGD27PillishionDamage = 20; //CGD-27 필리시온 기관단총 데미지

            GlopaorosCost = 1750;
            ConstructionResourceCost = 0;
            TaritronicCost = 3000;
        }
        else if (HurricaneSubmachineGunUpgradeLevel == 1)
        {
            instance.CGD27PillishionDamage = 28;

            GlopaorosCost = 3000;
            ConstructionResourceCost = 0;
            TaritronicCost = 5000;
        }
        else if (HurricaneSubmachineGunUpgradeLevel == 2)
        {
            instance.CGD27PillishionDamage = 36;

            GlopaorosCost = 4000;
            ConstructionResourceCost = 0;
            TaritronicCost = 6000;
        }
        else if (HurricaneSubmachineGunUpgradeLevel == 3)
        {
            instance.CGD27PillishionDamage = 44;
        }
    }

    //델타 허리케인 보조장비 업그레이드 레벨 및 비용
    public void DeltaHurricaneSubGearLevel()
    {
        if (HurricaneSubGearUpgradeLevel == 0)
        {
            instance.OSEH302WidowHireDamage = 30; //OSEH-302 Widow Hire 추적 미사일 한 발당 데미지

            GlopaorosCost = 1000;
            ConstructionResourceCost = 0;
            TaritronicCost = 2500;
        }
        else if (HurricaneSubGearUpgradeLevel == 1)
        {
            instance.OSEH302WidowHireDamage = 42;

            GlopaorosCost = 1500;
            ConstructionResourceCost = 0;
            TaritronicCost = 3500;
        }
        else if (HurricaneSubGearUpgradeLevel == 2)
        {
            instance.OSEH302WidowHireDamage = 54;

            GlopaorosCost = 2000;
            ConstructionResourceCost = 0;
            TaritronicCost = 4500;
        }
        else if (HurricaneSubGearUpgradeLevel == 3)
        {
            instance.OSEH302WidowHireDamage = 66;
        }
    }

    //델타 허리케인 수류탄 업그레이드 레벨 및 비용
    public void DeltaHurricaneGrenadeLevel()
    {
        if (HurricaneGrenadeUpgradeLevel == 0)
        {
            instance.VM5AEGDamage = 300; //VM-5 AEG 수류탄 데미지

            GlopaorosCost = 1000;
            ConstructionResourceCost = 0;
            TaritronicCost = 2500;
        }
        else if (HurricaneGrenadeUpgradeLevel == 1)
        {
            instance.VM5AEGDamage = 420;

            GlopaorosCost = 1500;
            ConstructionResourceCost = 0;
            TaritronicCost = 3500;
        }
        else if (HurricaneGrenadeUpgradeLevel == 2)
        {
            instance.VM5AEGDamage = 540;

            GlopaorosCost = 2000;
            ConstructionResourceCost = 0;
            TaritronicCost = 4500;
        }
        else if (HurricaneGrenadeUpgradeLevel == 3)
        {
            instance.VM5AEGDamage = 660;
        }
    }

    //델타 허리케인 체인지 중화기 업그레이드 레벨 및 비용
    public void DeltaHurricaneChangeHeavyWeaponLevel()
    {
        if (HurricaneChangeHeavyWeaponUpgradeLevel == 0)
        {
            instance.ArthesL775Step1Damage = 10; //Arthes L-775 충전 레이져 1단계 데미지
            instance.ArthesL775Step2Damage = 15; //Arthes L-775 충전 레이져 2단계 데미지
            instance.ArthesL775Step3Damage = 20; //Arthes L-775 충전 레이져 3단계 데미지
            instance.ArthesL775Step4Damage = 25; //Arthes L-775 충전 레이져 4단계 데미지
            instance.Hydra56Damage = 70; //Hydra-56 분리 철갑포 한 발 당 데미지
            instance.MEAGDamage = 150; //MEAG 레일건 데미지
            instance.MEAGAddDamage = 50; //MEAG 레일건 초당 상승 데미지
            instance.UGG98Step1Damage = 60; //UGG 98 중력포 1단계 데미지
            instance.UGG98Step2Damage = 125; //UGG 98 중력포 2단계 데미지

            GlopaorosCost = 7000;
            ConstructionResourceCost = 0;
            TaritronicCost = 6500;
        }
        else if (HurricaneChangeHeavyWeaponUpgradeLevel == 1)
        {
            instance.ArthesL775Step1Damage = 14;
            instance.ArthesL775Step2Damage = 21;
            instance.ArthesL775Step3Damage = 28;
            instance.ArthesL775Step4Damage = 35;
            instance.Hydra56Damage = 84;
            instance.MEAGDamage = 210;
            instance.MEAGAddDamage = 60;
            instance.UGG98Step1Damage = 84;
            instance.UGG98Step2Damage = 175;

            GlopaorosCost = 12500;
            ConstructionResourceCost = 0;
            TaritronicCost = 10000;
        }
        else if (HurricaneChangeHeavyWeaponUpgradeLevel == 2)
        {
            instance.ArthesL775Step1Damage = 18;
            instance.ArthesL775Step2Damage = 27;
            instance.ArthesL775Step3Damage = 36;
            instance.ArthesL775Step4Damage = 45;
            instance.Hydra56Damage = 98;
            instance.MEAGDamage = 270;
            instance.MEAGAddDamage = 70;
            instance.UGG98Step1Damage = 108;
            instance.UGG98Step2Damage = 225;

            GlopaorosCost = 17000;
            ConstructionResourceCost = 0;
            TaritronicCost = 14500;
        }
        else if (HurricaneChangeHeavyWeaponUpgradeLevel == 3)
        {
            instance.ArthesL775Step1Damage = 22;
            instance.ArthesL775Step2Damage = 33;
            instance.ArthesL775Step3Damage = 44;
            instance.ArthesL775Step4Damage = 55;
            instance.Hydra56Damage = 112;
            instance.MEAGDamage = 330;
            instance.MEAGAddDamage = 80;
            instance.UGG98Step1Damage = 132;
            instance.UGG98Step2Damage = 275;
        }
    }

    //함선 보급 지원 업그레이드 레벨 및 비용
    public void ShipAmmoSupportLevel()
    {
        if (ShipAmmoSupportUpgradeLevel == 0)
        {
            instance.SupportAmmoAmount = 2; //탄약 지원량. 기본 지원량에다가 곱하는 방식이다. 따라서, 최종 지원량이 절대로 아니다.(예시 : 기본 지원량 x "탄약 지원량" = 최종 지원량)

            GlopaorosCost = 1500;
            ConstructionResourceCost = 0;
            TaritronicCost = 2000;
        }
        else if (ShipAmmoSupportUpgradeLevel == 1)
        {
            instance.SupportAmmoAmount = 3;

            GlopaorosCost = 3000;
            ConstructionResourceCost = 0;
            TaritronicCost = 4000;
        }
        else if (ShipAmmoSupportUpgradeLevel == 2)
        {
            instance.SupportAmmoAmount = 4;

            GlopaorosCost = 5000;
            ConstructionResourceCost = 0;
            TaritronicCost = 8000;
        }
        else if (ShipAmmoSupportUpgradeLevel == 3)
        {
            instance.SupportAmmoAmount = 5;
        }
    }

    //함선 중화기 지원 업그레이드 레벨 및 비용
    public void ShipHeavyWeaponSupportLevel()
    {
        if (ShipHeavyWeaponSupportUpgradeLevel == 0)
        {
            instance.M3078Damage = 20; //M3078 미니건 데미지
            instance.ASC365Damage = 20; //ASC 365 화염방사기 데미지

            GlopaorosCost = 2000;
            ConstructionResourceCost = 0;
            TaritronicCost = 3000;
        }
        else if (ShipHeavyWeaponSupportUpgradeLevel == 1)
        {
            instance.M3078Damage = 30;
            instance.ASC365Damage = 25;

            GlopaorosCost = 4000;
            ConstructionResourceCost = 0;
            TaritronicCost = 5000;
        }
        else if (ShipHeavyWeaponSupportUpgradeLevel == 2)
        {
            instance.M3078Damage = 40;
            instance.ASC365Damage = 35;

            GlopaorosCost = 6500;
            ConstructionResourceCost = 0;
            TaritronicCost = 7500;
        }
        else if (ShipHeavyWeaponSupportUpgradeLevel == 3)
        {
            instance.M3078Damage = 50;
            instance.ASC365Damage = 45;
        }
    }

    //함선 탑승 차량 지원 업그레이드 레벨 및 비용
    public void ShipVehicleSupportLevel()
    {
        if (ShipVehicleSupportUpgradeLevel == 0)
        {
            instance.MBCA79IronHurricaneHitPoint = 3000; //MBCA-79 Iron Hurricane 체력
            instance.MBCA79IronHurricaneArmor = 1; //MBCA-79 Iron Hurricane 방어
            instance.MBCA79IronHurricaneHTACDamage = 330; //HTAC 주포 데미지
            instance.MBCA79IronHurricaneAPCDamage = 450; //APC 플라즈마 포 데미지
            instance.MBCA79IronHurricaneOSEHSDamage = 95; //OSEHS 추적 미사일 데미지
            instance.MBCA79IronHurricaneFBWSIrisDamage = 25; //FBWS Iris 게틀링포 데미지

            GlopaorosCost = 8000;
            ConstructionResourceCost = 0;
            TaritronicCost = 7000;
        }
        else if (ShipVehicleSupportUpgradeLevel == 1)
        {
            instance.MBCA79IronHurricaneHitPoint = 3500;
            instance.MBCA79IronHurricaneArmor = 1.8f;
            instance.MBCA79IronHurricaneHTACDamage = 550;
            instance.MBCA79IronHurricaneAPCDamage = 600;
            instance.MBCA79IronHurricaneOSEHSDamage = 140;
            instance.MBCA79IronHurricaneFBWSIrisDamage = 30;

            GlopaorosCost = 13000;
            ConstructionResourceCost = 0;
            TaritronicCost = 10000;
        }
        else if (ShipVehicleSupportUpgradeLevel == 2)
        {
            instance.MBCA79IronHurricaneHitPoint = 4000;
            instance.MBCA79IronHurricaneArmor = 2;
            instance.MBCA79IronHurricaneHTACDamage = 770;
            instance.MBCA79IronHurricaneAPCDamage = 840;
            instance.MBCA79IronHurricaneOSEHSDamage = 196;
            instance.MBCA79IronHurricaneFBWSIrisDamage = 42;

            GlopaorosCost = 18000;
            ConstructionResourceCost = 0;
            TaritronicCost = 17000;
        }
        else if (ShipVehicleSupportUpgradeLevel == 3)
        {
            instance.MBCA79IronHurricaneHitPoint = 4500;
            instance.MBCA79IronHurricaneArmor = 2.2f;
            instance.MBCA79IronHurricaneHTACDamage = 990;
            instance.MBCA79IronHurricaneAPCDamage = 1080;
            instance.MBCA79IronHurricaneOSEHSDamage = 252;
            instance.MBCA79IronHurricaneFBWSIrisDamage = 54;
        }
    }

    //함선 공격 지원 업그레이드 레벨 및 비용
    public void ShipStrikeSupportLevel()
    {
        if (ShipStrikeSupportUpgradeLevel == 0)
        {
            instance.PGM1036ScaletHawkDamage = 350; //Planet Guided Missile 1036 Scalet Hawk 한 발 당 데미지(폭격 미사일1)

            GlopaorosCost = 3000;
            ConstructionResourceCost = 0;
            TaritronicCost = 2000;
        }
        else if (ShipStrikeSupportUpgradeLevel == 1)
        {
            instance.PGM1036ScaletHawkDamage = 700;

            GlopaorosCost = 6000;
            ConstructionResourceCost = 0;
            TaritronicCost = 4000;
        }
        else if (ShipStrikeSupportUpgradeLevel == 2)
        {
            instance.PGM1036ScaletHawkDamage = 1050;

            GlopaorosCost = 9000;
            ConstructionResourceCost = 0;
            TaritronicCost = 6000;
        }
        else if (ShipStrikeSupportUpgradeLevel == 3)
        {
            instance.PGM1036ScaletHawkDamage = 1500;
        }
    }

    /// <summary>
    /// 업그레이드 값 적용
    /// </summary>
    //기함 장갑값을 다시 재설정한다.
    void ResetFlagshipData()
    {
        for (int i = 0; i < ShipManager.instance.FlagShipList.Count; i++)
        {
            ShipManager.instance.FlagShipList[i].GetComponent<HullSloriusFlagship1>().UpgradePatch();
        }
    }
    void LoadFlagshipData() //업그레이드 값만 불러오기
    {
        for (int i = 0; i < ShipManager.instance.FlagShipList.Count; i++)
        {
            ShipManager.instance.FlagShipList[i].GetComponent<HullSloriusFlagship1>().LoadPatch();
        }
    }

    //편대함 장갑값을 다시 재설정한다.
    void ResetFormationData()
    {
        for (int i = 0; i < ShipManager.instance.FlagShipList.Count; i++)
        {
            for (int j = 0; j < ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList.Count; j++)
            {
                if (ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<ShipRTS>().ShipNumber == 2)
                {
                    ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<HullSloriusFormationShip1>().UpgradePatch();
                }
            }
        }
    }
    void LoadFormationData()
    {
        for (int i = 0; i < ShipManager.instance.FlagShipList.Count; i++)
        {
            for (int j = 0; j < ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList.Count; j++)
            {
                if (ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<ShipRTS>().ShipNumber == 2)
                {
                    ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<HullSloriusFormationShip1>().LoadPatch();
                }
            }
        }
    }

    //방패함 장갑값을 다시 재설정한다.
    void ResetShieldShipData()
    {
        for (int i = 0; i < ShipManager.instance.FlagShipList.Count; i++)
        {
            for (int j = 0; j < ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList.Count; j++)
            {
                if (ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<ShipRTS>().ShipNumber == 3)
                {
                    ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<HullSloriusFormationShip1>().UpgradePatch();
                }
            }
        }
    }
    void LoadShieldShipData()
    {
        for (int i = 0; i < ShipManager.instance.FlagShipList.Count; i++)
        {
            for (int j = 0; j < ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList.Count; j++)
            {
                if (ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<ShipRTS>().ShipNumber == 3)
                {
                    ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<HullSloriusFormationShip1>().LoadPatch();
                }
            }
        }
    }

    //우주모함 장갑값을 다시 재설정한다.
    void ResetCarrierData()
    {
        for (int i = 0; i < ShipManager.instance.FlagShipList.Count; i++)
        {
            for (int j = 0; j < ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList.Count; j++)
            {
                if (ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<ShipRTS>().ShipNumber == 4)
                {
                    ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<HullSloriusFormationShip1>().UpgradePatch();
                }
            }
        }
    }
    void LoadCarrierData()
    {
        for (int i = 0; i < ShipManager.instance.FlagShipList.Count; i++)
        {
            for (int j = 0; j < ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList.Count; j++)
            {
                if (ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<ShipRTS>().ShipNumber == 4)
                {
                    ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<HullSloriusFormationShip1>().LoadPatch();
                }
            }
        }
    }

    //함선 무기 공격값을 다시 재설정한다.
    void ResetShipWeaponData()
    {
        for (int i = 0; i < ShipManager.instance.FlagShipList.Count; i++)
        {
            ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().CannonReinput();
            ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().CannonReinput();
            ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().Turret3.GetComponent<NarihaTurretAttackSystem>().CannonReinput();
            ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().Turret4.GetComponent<NarihaTurretAttackSystem>().CannonReinput();
            ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().Turret5.GetComponent<NarihaTurretAttackSystem>().CannonReinput();
            ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().Turret6.GetComponent<NarihaTurretAttackSystem>().CannonReinput();

            for (int j = 0; j < ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList.Count; j++)
            {
                if (ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<ShipRTS>().ShipNumber == 2)
                {
                    ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().CannonReinput();
                }
            }
        }
    }

    //함선 함재기 공격값을 다시 재설정한다.
    void ResetShipFighterData()
    {
        for (int i = 0; i < ShipManager.instance.FlagShipList.Count; i++)
        {
            for (int j = 0; j < ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList.Count; j++)
            {
                if (ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<ShipRTS>().ShipNumber == 4)
                {
                    ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].transform.Find("Carrier Borne Aircraft System").GetComponent<NarihaFighterSystem>().BomberDamage = CarrierGF12DeltaWingBomberDamage;
                }
            }
        }
    }

    //기함 공격 스킬 데미지값을 다시 재설정한다.
    void ResetFlagshipAttackSkillData()
    {
        for (int i = 0; i < ShipManager.instance.FlagShipList.Count; i++)
        {
            ShipManager.instance.FlagShipList[i].GetComponent<FlagshipAttackSkill>().SikroClassCruiseMissileDamage = SikroClassCruiseMissileDamage;
        }
    }

    //함대 공격 스킬 데미지값을 다시 재설정한다.
    void ResetFleetAttackSkillData()
    {
        for (int i = 0; i < ShipManager.instance.FlagShipList.Count; i++)
        {
            ShipManager.instance.FlagShipList[i].GetComponent<FlagshipAttackSkill>().Cysiro47PatriotMissileDamage = Cysiro47PatriotMissileDamage;
        }
    }

    //델타 허리케인 보급 지원량을 다시 재설정한다.
    void GetAmmoData()
    {
        DeltaHrricaneData.instance.GetUpgrade();
    }

    /// <summary>
    /// 업그레이드 시작 영역
    /// </summary>
    //업그레이드 시작
    public void UpgradeStart(int MainTabNumber, int SubTabNumber, int AccessNumber, int UpgradeNumber, int SubUpgradeNumber)
    {
        if (MainTabNumber == 1) //함대 탭
        {
            if (SubTabNumber == 1) //함선 장갑 시스템
            {
                if (AccessNumber == 1) //기함
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0) //장갑 업그레이드
                    {
                        instance.FlagshipUpgradeLevel++;
                        FlagshipClassLevel();
                        CheckNextPlanet(MainTabNumber, SubTabNumber, AccessNumber, UpgradeNumber, SubUpgradeNumber);
                        ResetFlagshipData();
                    }
                }
                else if (AccessNumber == 2) //편대함
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0) //장갑 업그레이드
                    {
                        instance.FormationUpgradeLevel++;
                        FormationShipClassLevel();
                        CheckNextPlanet(MainTabNumber, SubTabNumber, AccessNumber, UpgradeNumber, SubUpgradeNumber);
                        ResetFormationData();
                    }
                }
                else if (AccessNumber == 3) //전략함
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0) //장갑 업그레이드
                    {
                        instance.TacticalShipUpgradeLevel++;
                        TacticalShipClassLevel();
                        CheckNextPlanet(MainTabNumber, SubTabNumber, AccessNumber, UpgradeNumber, SubUpgradeNumber);
                        ResetShieldShipData();
                        ResetCarrierData();
                    }
                }
            }
            else if (SubTabNumber == 2) //함선 무기
            {
                if (AccessNumber == 1) //함포 계열
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0)
                    {
                        instance.ShipCannonUpgradeLevel++;
                        ShipCannonClassLevel();
                        CheckNextPlanet(MainTabNumber, SubTabNumber, AccessNumber, UpgradeNumber, SubUpgradeNumber);
                        ResetShipWeaponData();
                    }
                }
                else if (AccessNumber == 2) //미사일 계열
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0)
                    {
                        instance.ShipMissileUpgradeLevel++;
                        ShipMissileClassLevel();
                        CheckNextPlanet(MainTabNumber, SubTabNumber, AccessNumber, UpgradeNumber, SubUpgradeNumber);
                        ResetShipWeaponData();
                    }
                }
                else if (AccessNumber == 3) //함재기 계열
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0)
                    {
                        instance.ShipFighterUpgradeLevel++;
                        ShipFighterClassLevel();
                        CheckNextPlanet(MainTabNumber, SubTabNumber, AccessNumber, UpgradeNumber, SubUpgradeNumber);
                        ResetShipFighterData();
                    }
                }
            }
            else if (SubTabNumber == 3) //함대 지원
            {
                if (AccessNumber == 1) //기함 공격
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0)
                    {
                        instance.FlagshipAttackSkillUpgradeLevel++;
                        FlagshipAttackSkillClassLevel();
                        CheckNextPlanet(MainTabNumber, SubTabNumber, AccessNumber, UpgradeNumber, SubUpgradeNumber);
                        ResetFlagshipAttackSkillData();
                    }
                }
                else if (AccessNumber == 2) //함대 공격
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0)
                    {
                        instance.FleetAttackSkillUpgradeLevel++;
                        FleetAttackSkillClassLevel();
                        CheckNextPlanet(MainTabNumber, SubTabNumber, AccessNumber, UpgradeNumber, SubUpgradeNumber);
                        ResetFleetAttackSkillData();
                    }
                }
            }
        }

        else if (MainTabNumber == 2) //델타 허리케인 탭
        {
            if (SubTabNumber == 1) //내구도
            {
                if (AccessNumber == 1)
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0)
                    {
                        instance.HurricaneHitPointUpgradeLevel++;
                        DeltaHurricaneHitPointLevel();
                        CheckNextPlanet(MainTabNumber, SubTabNumber, AccessNumber, UpgradeNumber, SubUpgradeNumber);
                    }
                }
            }
            else if (SubTabNumber == 2) //기본 무기
            {
                if (AccessNumber == 1) //돌격 소총
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0)
                    {
                        instance.HurricaneAssaultRifleUpgradeLevel++;
                        DeltaHurricaneAssaultRifleLevel();
                        CheckNextPlanet(MainTabNumber, SubTabNumber, AccessNumber, UpgradeNumber, SubUpgradeNumber);
                    }
                }
                else if (AccessNumber == 2) //샷건
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0)
                    {
                        instance.HurricaneShotgunUpgradeLevel++;
                        DeltaHurricaneShotgunLevel();
                        CheckNextPlanet(MainTabNumber, SubTabNumber, AccessNumber, UpgradeNumber, SubUpgradeNumber);
                    }
                }
                else if (AccessNumber == 3) //저격총
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0)
                    {
                        instance.HurricaneSniperRifleUpgradeLevel++;
                        DeltaHurricaneSniperRifleLevel();
                        CheckNextPlanet(MainTabNumber, SubTabNumber, AccessNumber, UpgradeNumber, SubUpgradeNumber);
                    }
                }
                else if (AccessNumber == 4) //기관단총
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0)
                    {
                        instance.HurricaneSubmachineGunUpgradeLevel++;
                        DeltaHurricaneSubmachineGunLevel();
                        CheckNextPlanet(MainTabNumber, SubTabNumber, AccessNumber, UpgradeNumber, SubUpgradeNumber);
                    }
                }
            }
            else if (SubTabNumber == 3) //지원 무기
            {
                if (AccessNumber == 1) //보조 장비
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0)
                    {
                        instance.HurricaneSubGearUpgradeLevel++;
                        DeltaHurricaneSubGearLevel();
                        CheckNextPlanet(MainTabNumber, SubTabNumber, AccessNumber, UpgradeNumber, SubUpgradeNumber);
                    }
                }
                else if (AccessNumber == 2) //수류탄
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0)
                    {
                        instance.HurricaneGrenadeUpgradeLevel++;
                        DeltaHurricaneGrenadeLevel();
                        CheckNextPlanet(MainTabNumber, SubTabNumber, AccessNumber, UpgradeNumber, SubUpgradeNumber);
                    }
                }
                else if (AccessNumber == 3) //체인지 중화기
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0)
                    {
                        instance.HurricaneChangeHeavyWeaponUpgradeLevel++;
                        DeltaHurricaneChangeHeavyWeaponLevel();
                        CheckNextPlanet(MainTabNumber, SubTabNumber, AccessNumber, UpgradeNumber, SubUpgradeNumber);
                    }
                }
            }
        }
        else if (MainTabNumber == 3) //함선 지원
        {
            if (SubTabNumber == 1) //무기 지원
            {
                if (AccessNumber == 1) //보급 지원
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0)
                    {
                        instance.ShipAmmoSupportUpgradeLevel++;
                        ShipAmmoSupportLevel();
                        CheckNextPlanet(MainTabNumber, SubTabNumber, AccessNumber, UpgradeNumber, SubUpgradeNumber);
                    }
                }
                else if (AccessNumber == 2) //중화기 지원
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0)
                    {
                        instance.ShipHeavyWeaponSupportUpgradeLevel++;
                        ShipHeavyWeaponSupportLevel();
                        CheckNextPlanet(MainTabNumber, SubTabNumber, AccessNumber, UpgradeNumber, SubUpgradeNumber);
                    }
                }
                else if (AccessNumber == 3) //탑승 차량 지원
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0)
                    {
                        instance.ShipVehicleSupportUpgradeLevel++;
                        ShipVehicleSupportLevel();
                        CheckNextPlanet(MainTabNumber, SubTabNumber, AccessNumber, UpgradeNumber, SubUpgradeNumber);
                    }
                }
            }
            else if (SubTabNumber == 2) //공격 지원
            {
                if (AccessNumber == 1) //폭격 지원
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0)
                    {
                        instance.ShipStrikeSupportUpgradeLevel++;
                        ShipStrikeSupportLevel();
                        CheckNextPlanet(MainTabNumber, SubTabNumber, AccessNumber, UpgradeNumber, SubUpgradeNumber);
                    }
                }
            }
        }
    }

    //업그레이드가 한 번 끝날 때마다 해당 연구 항목의 다음 연구 행성의 해방 정보를 확인, 해방되었을 경우 뒤로 가지 않지만, 미해방되었을 경우 자동으로 뒤로가서 연구를 연속으로 하지 못하게 막는다.
    public void CheckNextPlanet(int MainTabNumber, int SubTabNumber, int AccessNumber, int UpgradeNumber, int SubUpgradeNumber)
    {
        UpgradeMenu = FindObjectOfType<UpgradeMenu>();
        if (MainTabNumber == 1) //함대 탭
        {
            if (SubTabNumber == 1) //함선 장갑 시스템
            {
                if (AccessNumber == 1) //기함 장갑
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0)
                    {
                        if (FlagshipUpgradeLevel == 1)
                        {
                            if (WeaponUnlockManager.instance.DeltaD31_2208LabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        else if (FlagshipUpgradeLevel == 2)
                        {
                            if (WeaponUnlockManager.instance.JeratoO95_2252LabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        //업그레이드 한도가 다 찼을 경우 혹은 다음 연구 행성이 미해방되었을 경우, 자동으로 테이블 리스트로 되돌아가기
                        if (FlagshipUpgradeLevel == 3 || isNextPlanetOpen == false)
                        {
                            UpgradeMenu.CancelFleetButtonClick();
                            UpgradeMenu.LoadTableList(UpgradeMenu.MainTabNumber, UpgradeMenu.SubTabNumber);
                        }
                    }
                }
                else if (AccessNumber == 2) //편대함 장갑
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0)
                    {
                        if (FormationUpgradeLevel == 1)
                        {
                            if (WeaponUnlockManager.instance.DeltaD31_2208LabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        else if (FormationUpgradeLevel == 2)
                        {
                            if (WeaponUnlockManager.instance.JeratoO95_2252LabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        if (FormationUpgradeLevel == 3 || isNextPlanetOpen == false)
                        {
                            UpgradeMenu.CancelFleetButtonClick();
                            UpgradeMenu.LoadTableList(UpgradeMenu.MainTabNumber, UpgradeMenu.SubTabNumber);
                        }
                    }
                }
                else if (AccessNumber == 3) //전략함 장갑
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0)
                    {
                        if (TacticalShipUpgradeLevel == 1)
                        {
                            if (WeaponUnlockManager.instance.DeltaD31_2208LabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        else if (TacticalShipUpgradeLevel == 2)
                        {
                            if (WeaponUnlockManager.instance.JeratoO95_2252LabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        if (TacticalShipUpgradeLevel == 3 || isNextPlanetOpen == false)
                        {
                            UpgradeMenu.CancelFleetButtonClick();
                            UpgradeMenu.LoadTableList(UpgradeMenu.MainTabNumber, UpgradeMenu.SubTabNumber);
                        }
                    }
                }
            }
            else if (SubTabNumber == 2) //함선 무기
            {
                if (AccessNumber == 1) //함포 계열
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0)
                    {
                        if (ShipCannonUpgradeLevel == 1)
                        {
                            if (WeaponUnlockManager.instance.DeltaD31_2208LabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        else if (ShipCannonUpgradeLevel == 2)
                        {
                            if (WeaponUnlockManager.instance.JeratoO95_2252LabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        //업그레이드 한도가 다 찼을 경우 혹은 다음 연구 행성이 미해방되었을 경우, 자동으로 테이블 리스트로 되돌아가기
                        if (ShipCannonUpgradeLevel == 3 || isNextPlanetOpen == false)
                        {
                            UpgradeMenu.CancelFleetButtonClick();
                            UpgradeMenu.LoadTableList(UpgradeMenu.MainTabNumber, UpgradeMenu.SubTabNumber);
                        }
                    }
                }
                else if (AccessNumber == 2) //미사일 계열
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0)
                    {
                        if (ShipMissileUpgradeLevel == 1)
                        {
                            if (WeaponUnlockManager.instance.DeltaD31_2208LabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        else if (ShipMissileUpgradeLevel == 2)
                        {
                            if (WeaponUnlockManager.instance.JeratoO95_2252LabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        //업그레이드 한도가 다 찼을 경우 혹은 다음 연구 행성이 미해방되었을 경우, 자동으로 테이블 리스트로 되돌아가기
                        if (ShipMissileUpgradeLevel == 3 || isNextPlanetOpen == false)
                        {
                            UpgradeMenu.CancelFleetButtonClick();
                            UpgradeMenu.LoadTableList(UpgradeMenu.MainTabNumber, UpgradeMenu.SubTabNumber);
                        }
                    }
                }
                else if (AccessNumber == 3) //미사일 계열
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0)
                    {
                        if (ShipFighterUpgradeLevel == 1)
                        {
                            if (WeaponUnlockManager.instance.DeltaD31_2208LabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        else if (ShipFighterUpgradeLevel == 2)
                        {
                            if (WeaponUnlockManager.instance.JeratoO95_2252LabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        //업그레이드 한도가 다 찼을 경우 혹은 다음 연구 행성이 미해방되었을 경우, 자동으로 테이블 리스트로 되돌아가기
                        if (ShipFighterUpgradeLevel == 3 || isNextPlanetOpen == false)
                        {
                            UpgradeMenu.CancelFleetButtonClick();
                            UpgradeMenu.LoadTableList(UpgradeMenu.MainTabNumber, UpgradeMenu.SubTabNumber);
                        }
                    }
                }
            }
            else if (SubTabNumber == 3) //함대 지원
            {
                if (AccessNumber == 1) //기함 공격
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0)
                    {
                        if (FlagshipAttackSkillUpgradeLevel == 1)
                        {
                            if (WeaponUnlockManager.instance.DeltaD31_12721LabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        else if (FlagshipAttackSkillUpgradeLevel == 2)
                        {
                            if (WeaponUnlockManager.instance.JeratoO95_8510LabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        //업그레이드 한도가 다 찼을 경우 혹은 다음 연구 행성이 미해방되었을 경우, 자동으로 테이블 리스트로 되돌아가기
                        if (FlagshipAttackSkillUpgradeLevel == 3 || isNextPlanetOpen == false)
                        {
                            UpgradeMenu.CancelFleetButtonClick();
                            UpgradeMenu.LoadTableList(UpgradeMenu.MainTabNumber, UpgradeMenu.SubTabNumber);
                        }
                    }
                }
                else if (AccessNumber == 2) //함대 공격
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0)
                    {
                        if (FleetAttackSkillUpgradeLevel == 1)
                        {
                            if (WeaponUnlockManager.instance.DeltaD31_12721LabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        else if (FleetAttackSkillUpgradeLevel == 2)
                        {
                            if (WeaponUnlockManager.instance.JeratoO95_8510LabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        //업그레이드 한도가 다 찼을 경우 혹은 다음 연구 행성이 미해방되었을 경우, 자동으로 테이블 리스트로 되돌아가기
                        if (FleetAttackSkillUpgradeLevel == 3 || isNextPlanetOpen == false)
                        {
                            UpgradeMenu.CancelFleetButtonClick();
                            UpgradeMenu.LoadTableList(UpgradeMenu.MainTabNumber, UpgradeMenu.SubTabNumber);
                        }
                    }
                }
            }
        }

        else if (MainTabNumber == 2) //델타 허리케인 탭
        {
            if (SubTabNumber == 1) //내구도
            {
                if (AccessNumber == 1) //체력
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0)
                    {
                        if (HurricaneHitPointUpgradeLevel == 1)
                        {
                            if (WeaponUnlockManager.instance.CrownYosereLabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        else if (HurricaneHitPointUpgradeLevel == 2)
                        {
                            if (WeaponUnlockManager.instance.DeltaD31_12721LabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        //업그레이드 한도가 다 찼을 경우 혹은 다음 연구 행성이 미해방되었을 경우, 자동으로 테이블 리스트로 되돌아가기
                        if (HurricaneHitPointUpgradeLevel == 3 || isNextPlanetOpen == false)
                        {
                            UpgradeMenu.CancelFleetButtonClick();
                            UpgradeMenu.LoadTableList(UpgradeMenu.MainTabNumber, UpgradeMenu.SubTabNumber);
                        }
                    }
                }
            }
            else if (SubTabNumber == 2) //기본 무기
            {
                if (AccessNumber == 1) //돌격 소총
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0)
                    {
                        if (HurricaneAssaultRifleUpgradeLevel == 1)
                        {
                            if (WeaponUnlockManager.instance.DeltaD31_12721LabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        else if (HurricaneAssaultRifleUpgradeLevel == 2)
                        {
                            if (WeaponUnlockManager.instance.JeratoO95_8510LabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        //업그레이드 한도가 다 찼을 경우 혹은 다음 연구 행성이 미해방되었을 경우, 자동으로 테이블 리스트로 되돌아가기
                        if (HurricaneAssaultRifleUpgradeLevel == 3 || isNextPlanetOpen == false)
                        {
                            UpgradeMenu.CancelFleetButtonClick();
                            UpgradeMenu.LoadTableList(UpgradeMenu.MainTabNumber, UpgradeMenu.SubTabNumber);
                        }
                    }
                }
                else if (AccessNumber == 2) //샷건
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0)
                    {
                        if (HurricaneShotgunUpgradeLevel == 1)
                        {
                            if (WeaponUnlockManager.instance.DeltaD31_12721LabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        else if (HurricaneShotgunUpgradeLevel == 2)
                        {
                            if (WeaponUnlockManager.instance.JeratoO95_8510LabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        //업그레이드 한도가 다 찼을 경우 혹은 다음 연구 행성이 미해방되었을 경우, 자동으로 테이블 리스트로 되돌아가기
                        if (HurricaneShotgunUpgradeLevel == 3 || isNextPlanetOpen == false)
                        {
                            UpgradeMenu.CancelFleetButtonClick();
                            UpgradeMenu.LoadTableList(UpgradeMenu.MainTabNumber, UpgradeMenu.SubTabNumber);
                        }
                    }
                }
                else if (AccessNumber == 3) //저격총
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0)
                    {
                        if (HurricaneSniperRifleUpgradeLevel == 1)
                        {
                            if (WeaponUnlockManager.instance.DeltaD31_12721LabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        else if (HurricaneSniperRifleUpgradeLevel == 2)
                        {
                            if (WeaponUnlockManager.instance.JeratoO95_8510LabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        //업그레이드 한도가 다 찼을 경우 혹은 다음 연구 행성이 미해방되었을 경우, 자동으로 테이블 리스트로 되돌아가기
                        if (HurricaneSniperRifleUpgradeLevel == 3 || isNextPlanetOpen == false)
                        {
                            UpgradeMenu.CancelFleetButtonClick();
                            UpgradeMenu.LoadTableList(UpgradeMenu.MainTabNumber, UpgradeMenu.SubTabNumber);
                        }
                    }
                }
                else if (AccessNumber == 4) //기관단총
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0)
                    {
                        if (HurricaneSubmachineGunUpgradeLevel == 1)
                        {
                            if (WeaponUnlockManager.instance.DeltaD31_12721LabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        else if (HurricaneSubmachineGunUpgradeLevel == 2)
                        {
                            if (WeaponUnlockManager.instance.JeratoO95_8510LabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        //업그레이드 한도가 다 찼을 경우 혹은 다음 연구 행성이 미해방되었을 경우, 자동으로 테이블 리스트로 되돌아가기
                        if (HurricaneSubmachineGunUpgradeLevel == 3 || isNextPlanetOpen == false)
                        {
                            UpgradeMenu.CancelFleetButtonClick();
                            UpgradeMenu.LoadTableList(UpgradeMenu.MainTabNumber, UpgradeMenu.SubTabNumber);
                        }
                    }
                }
            }
            else if (SubTabNumber == 3) //지원 무기
            {
                if (AccessNumber == 1) //보조 장비
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0)
                    {
                        if (HurricaneSubGearUpgradeLevel == 1)
                        {
                            if (WeaponUnlockManager.instance.JapetAgroneLabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        else if (HurricaneSubGearUpgradeLevel == 2)
                        {
                            if (WeaponUnlockManager.instance.JeratoO95_2252LabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        //업그레이드 한도가 다 찼을 경우 혹은 다음 연구 행성이 미해방되었을 경우, 자동으로 테이블 리스트로 되돌아가기
                        if (HurricaneSubGearUpgradeLevel == 3 || isNextPlanetOpen == false)
                        {
                            UpgradeMenu.CancelFleetButtonClick();
                            UpgradeMenu.LoadTableList(UpgradeMenu.MainTabNumber, UpgradeMenu.SubTabNumber);
                        }
                    }
                }
                else if (AccessNumber == 2) //수류탄
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0)
                    {
                        if (HurricaneGrenadeUpgradeLevel == 1)
                        {
                            if (WeaponUnlockManager.instance.CrownYosereLabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        else if (HurricaneGrenadeUpgradeLevel == 2)
                        {
                            if (WeaponUnlockManager.instance.DeltaD31_2208LabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        //업그레이드 한도가 다 찼을 경우 혹은 다음 연구 행성이 미해방되었을 경우, 자동으로 테이블 리스트로 되돌아가기
                        if (HurricaneGrenadeUpgradeLevel == 3 || isNextPlanetOpen == false)
                        {
                            UpgradeMenu.CancelFleetButtonClick();
                            UpgradeMenu.LoadTableList(UpgradeMenu.MainTabNumber, UpgradeMenu.SubTabNumber);
                        }
                    }
                }
                else if (AccessNumber == 3) //체인지 중화기
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0)
                    {
                        if (HurricaneChangeHeavyWeaponUpgradeLevel == 1)
                        {
                            if (WeaponUnlockManager.instance.DeltaD31_2208LabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        else if (HurricaneChangeHeavyWeaponUpgradeLevel == 2)
                        {
                            if (WeaponUnlockManager.instance.JeratoO95_2252LabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        //업그레이드 한도가 다 찼을 경우 혹은 다음 연구 행성이 미해방되었을 경우, 자동으로 테이블 리스트로 되돌아가기
                        if (HurricaneChangeHeavyWeaponUpgradeLevel == 3 || isNextPlanetOpen == false)
                        {
                            UpgradeMenu.CancelFleetButtonClick();
                            UpgradeMenu.LoadTableList(UpgradeMenu.MainTabNumber, UpgradeMenu.SubTabNumber);
                        }
                    }
                }
            }
        }

        else if (MainTabNumber == 3) //함선 지원
        {
            if (SubTabNumber == 1) //무기 지원
            {
                if (AccessNumber == 1) //보급 지원
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0)
                    {
                        if (ShipAmmoSupportUpgradeLevel == 1)
                        {
                            if (WeaponUnlockManager.instance.DeriousHeriLabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        else if (ShipAmmoSupportUpgradeLevel == 2)
                        {
                            if (WeaponUnlockManager.instance.DeltaD31_12721LabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        //업그레이드 한도가 다 찼을 경우 혹은 다음 연구 행성이 미해방되었을 경우, 자동으로 테이블 리스트로 되돌아가기
                        if (ShipAmmoSupportUpgradeLevel == 3 || isNextPlanetOpen == false)
                        {
                            UpgradeMenu.CancelFleetButtonClick();
                            UpgradeMenu.LoadTableList(UpgradeMenu.MainTabNumber, UpgradeMenu.SubTabNumber);
                        }
                    }
                }
                else if (AccessNumber == 2) //중화기 지원
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0)
                    {
                        if (ShipHeavyWeaponSupportUpgradeLevel == 1)
                        {
                            if (WeaponUnlockManager.instance.DeltaD31_2208LabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        else if (ShipHeavyWeaponSupportUpgradeLevel == 2)
                        {
                            if (WeaponUnlockManager.instance.JeratoO95_8510LabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        //업그레이드 한도가 다 찼을 경우 혹은 다음 연구 행성이 미해방되었을 경우, 자동으로 테이블 리스트로 되돌아가기
                        if (ShipHeavyWeaponSupportUpgradeLevel == 3 || isNextPlanetOpen == false)
                        {
                            UpgradeMenu.CancelFleetButtonClick();
                            UpgradeMenu.LoadTableList(UpgradeMenu.MainTabNumber, UpgradeMenu.SubTabNumber);
                        }
                    }
                }
                else if (AccessNumber == 3) //탑승 차량 지원
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0)
                    {
                        if (ShipVehicleSupportUpgradeLevel == 1)
                        {
                            if (WeaponUnlockManager.instance.DeltaD31_12721LabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        else if (ShipVehicleSupportUpgradeLevel == 2)
                        {
                            if (WeaponUnlockManager.instance.JeratoO95_8510LabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        //업그레이드 한도가 다 찼을 경우 혹은 다음 연구 행성이 미해방되었을 경우, 자동으로 테이블 리스트로 되돌아가기
                        if (ShipVehicleSupportUpgradeLevel == 3 || isNextPlanetOpen == false)
                        {
                            UpgradeMenu.CancelFleetButtonClick();
                            UpgradeMenu.LoadTableList(UpgradeMenu.MainTabNumber, UpgradeMenu.SubTabNumber);
                        }
                    }
                }
            }
            else if (SubTabNumber == 2) //공격 지원
            {
                if (AccessNumber == 1) //폭격 지원
                {
                    if (UpgradeNumber == 1 && SubUpgradeNumber == 0)
                    {
                        if (ShipStrikeSupportUpgradeLevel == 1)
                        {
                            if (WeaponUnlockManager.instance.JapetAgroneLabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        else if (ShipStrikeSupportUpgradeLevel == 2)
                        {
                            if (WeaponUnlockManager.instance.JeratoO95_2252LabUnlock == false)
                                isNextPlanetOpen = false;
                            else
                                isNextPlanetOpen = true;
                        }
                        //업그레이드 한도가 다 찼을 경우 혹은 다음 연구 행성이 미해방되었을 경우, 자동으로 테이블 리스트로 되돌아가기
                        if (ShipStrikeSupportUpgradeLevel == 3 || isNextPlanetOpen == false)
                        {
                            UpgradeMenu.CancelFleetButtonClick();
                            UpgradeMenu.LoadTableList(UpgradeMenu.MainTabNumber, UpgradeMenu.SubTabNumber);
                        }
                    }
                }
            }
        }
    }

    //함대전으로 불러온 이후 업그레이드 상태 적용
    public void LoadUpgradePatch()
    {
        //함선
        LoadFlagshipData();
        LoadFormationData();
        LoadShieldShipData();
        LoadCarrierData();

        //함선 무기
        ResetShipWeaponData();
        ResetShipFighterData();

        //함선 지원
        GetAmmoData();
        ResetFlagshipAttackSkillData();
        ResetFleetAttackSkillData();
    }

    public void GetData(SerializableUpgradeDataSystem values)
    {
        FlagshipUpgradeLevel = values.FlagshipUpgradeLevel;
        FormationUpgradeLevel = values.FormationUpgradeLevel;
        TacticalShipUpgradeLevel = values.TacticalShipUpgradeLevel;

        ShipCannonUpgradeLevel = values.ShipCannonUpgradeLevel;
        ShipMissileUpgradeLevel = values.ShipMissileUpgradeLevel;
        ShipFighterUpgradeLevel = values.ShipFighterUpgradeLevel;

        FlagshipAttackSkillUpgradeLevel = values.FlagshipAttackSkillUpgradeLevel;
        FleetAttackSkillUpgradeLevel = values.FleetAttackSkillUpgradeLevel;

        HurricaneHitPointUpgradeLevel = values.HurricaneHitPointUpgradeLevel;

        HurricaneAssaultRifleUpgradeLevel = values.HurricaneAssaultRifleUpgradeLevel;
        HurricaneShotgunUpgradeLevel = values.HurricaneShotgunUpgradeLevel;
        HurricaneSniperRifleUpgradeLevel = values.HurricaneSniperRifleUpgradeLevel;
        HurricaneSubmachineGunUpgradeLevel = values.HurricaneSubmachineGunUpgradeLevel;

        HurricaneSubGearUpgradeLevel = values.HurricaneSubGearUpgradeLevel;
        HurricaneGrenadeUpgradeLevel = values.HurricaneGrenadeUpgradeLevel;
        HurricaneChangeHeavyWeaponUpgradeLevel = values.HurricaneChangeHeavyWeaponUpgradeLevel;

        ShipAmmoSupportUpgradeLevel = values.ShipAmmoSupportUpgradeLevel;
        ShipHeavyWeaponSupportUpgradeLevel = values.ShipHeavyWeaponSupportUpgradeLevel;
        ShipVehicleSupportUpgradeLevel = values.ShipVehicleSupportUpgradeLevel;
        ShipStrikeSupportUpgradeLevel = values.ShipStrikeSupportUpgradeLevel;

        FlagshipHitPoints = values.FlagshipHitPoints;
        FormationHitPoints = values.FormationHitPoints;
        ShieldShipHitPoints = values.ShieldShipHitPoints;
        CarrierHitPoints = values.CarrierHitPoints;

        FlagshipSilenceSistDamage = values.FlagshipSilenceSistDamage;
        FlagshipOverJumpDamage = values.FlagshipOverJumpDamage;
        FlagshipSadLilly345Damage = values.FlagshipSadLilly345Damage;
        FlagshipDeltaNeedle42HalistDamage = values.FlagshipDeltaNeedle42HalistDamage;
        FlagshipGF12DeltaWingBomberDamage = values.FlagshipGF12DeltaWingBomberDamage;
        FormationSilenceSistDamage = values.FormationSilenceSistDamage;
        FormationOverJumpDamage = values.FormationOverJumpDamage;
        FormationSadLilly345Damage = values.FormationSadLilly345Damage;
        FormationDeltaNeedle42HalistDamage = values.FormationDeltaNeedle42HalistDamage;
        CarrierGF12DeltaWingBomberDamage = values.CarrierGF12DeltaWingBomberDamage;

        SikroClassCruiseMissileDamage = values.SikroClassCruiseMissileDamage;

        Cysiro47PatriotMissileDamage = values.Cysiro47PatriotMissileDamage;

        HurricaneHitPoint = values.HurricaneHitPoint;
        HurricaneHitArmor = values.HurricaneHitArmor;

        DT37Damage = values.DT37Damage;
        DS65Damage = values.DS65Damage;
        DP9007Damage = values.DP9007Damage;
        CGD27PillishionDamage = values.CGD27PillishionDamage;

        OSEH302WidowHireDamage = values.OSEH302WidowHireDamage;

        VM5AEGDamage = values.VM5AEGDamage;

        ArthesL775Step1Damage = values.ArthesL775Step1Damage;
        ArthesL775Step2Damage = values.ArthesL775Step2Damage;
        ArthesL775Step3Damage = values.ArthesL775Step3Damage;
        ArthesL775Step4Damage = values.ArthesL775Step4Damage;
        Hydra56Damage = values.Hydra56Damage;
        MEAGDamage = values.MEAGDamage;
        MEAGAddDamage = values.MEAGAddDamage;
        UGG98Step1Damage = values.UGG98Step1Damage;
        UGG98Step2Damage = values.UGG98Step2Damage;

        SupportAmmoAmount = values.SupportAmmoAmount;

        M3078Damage = values.M3078Damage;
        ASC365Damage = values.ASC365Damage;

        MBCA79IronHurricaneHitPoint = values.MBCA79IronHurricaneHitPoint;
        MBCA79IronHurricaneArmor = values.MBCA79IronHurricaneArmor;
        MBCA79IronHurricaneHTACDamage = values.MBCA79IronHurricaneHTACDamage;
        MBCA79IronHurricaneAPCDamage = values.MBCA79IronHurricaneAPCDamage;
        MBCA79IronHurricaneOSEHSDamage = values.MBCA79IronHurricaneOSEHSDamage;
        MBCA79IronHurricaneFBWSIrisDamage = values.MBCA79IronHurricaneFBWSIrisDamage;

        PGM1036ScaletHawkDamage = values.PGM1036ScaletHawkDamage;
    }

    public SerializableUpgradeDataSystem GetSerializable()
    {
        var output = new SerializableUpgradeDataSystem();

        output.FlagshipUpgradeLevel = this.FlagshipUpgradeLevel;
        output.FormationUpgradeLevel = this.FormationUpgradeLevel;
        output.TacticalShipUpgradeLevel = this.TacticalShipUpgradeLevel;

        output.ShipCannonUpgradeLevel = this.ShipCannonUpgradeLevel;
        output.ShipMissileUpgradeLevel = this.ShipMissileUpgradeLevel;
        output.ShipFighterUpgradeLevel = this.ShipFighterUpgradeLevel;

        output.FlagshipAttackSkillUpgradeLevel = this.FlagshipAttackSkillUpgradeLevel;
        output.FleetAttackSkillUpgradeLevel = this.FleetAttackSkillUpgradeLevel;

        output.HurricaneHitPointUpgradeLevel = this.HurricaneHitPointUpgradeLevel;

        output.HurricaneAssaultRifleUpgradeLevel = this.HurricaneAssaultRifleUpgradeLevel;
        output.HurricaneShotgunUpgradeLevel = this.HurricaneShotgunUpgradeLevel;
        output.HurricaneSniperRifleUpgradeLevel = this.HurricaneSniperRifleUpgradeLevel;
        output.HurricaneSubmachineGunUpgradeLevel = this.HurricaneSubmachineGunUpgradeLevel;

        output.HurricaneSubGearUpgradeLevel = this.HurricaneSubGearUpgradeLevel;
        output.HurricaneGrenadeUpgradeLevel = this.HurricaneGrenadeUpgradeLevel;
        output.HurricaneChangeHeavyWeaponUpgradeLevel = this.HurricaneChangeHeavyWeaponUpgradeLevel;

        output.ShipAmmoSupportUpgradeLevel = this.ShipAmmoSupportUpgradeLevel;
        output.ShipHeavyWeaponSupportUpgradeLevel = this.ShipHeavyWeaponSupportUpgradeLevel;
        output.ShipVehicleSupportUpgradeLevel = this.ShipVehicleSupportUpgradeLevel;
        output.ShipStrikeSupportUpgradeLevel = this.ShipStrikeSupportUpgradeLevel;

        output.FlagshipHitPoints = this.FlagshipHitPoints;
        output.FormationHitPoints = this.FormationHitPoints;
        output.ShieldShipHitPoints = this.ShieldShipHitPoints;
        output.CarrierHitPoints = this.CarrierHitPoints;

        output.FlagshipSilenceSistDamage = this.FlagshipSilenceSistDamage;
        output.FlagshipOverJumpDamage = this.FlagshipOverJumpDamage;
        output.FlagshipSadLilly345Damage = this.FlagshipSadLilly345Damage;
        output.FlagshipDeltaNeedle42HalistDamage = this.FlagshipDeltaNeedle42HalistDamage;
        output.FlagshipGF12DeltaWingBomberDamage = this.FlagshipGF12DeltaWingBomberDamage;
        output.FormationSilenceSistDamage = this.FormationSilenceSistDamage;
        output.FormationOverJumpDamage = this.FormationOverJumpDamage;
        output.FormationSadLilly345Damage = this.FormationSadLilly345Damage;
        output.FormationDeltaNeedle42HalistDamage = this.FormationDeltaNeedle42HalistDamage;
        output.CarrierGF12DeltaWingBomberDamage = this.CarrierGF12DeltaWingBomberDamage;

        output.SikroClassCruiseMissileDamage = this.SikroClassCruiseMissileDamage;

        output.Cysiro47PatriotMissileDamage = this.Cysiro47PatriotMissileDamage;

        output.HurricaneHitPoint = this.HurricaneHitPoint;
        output.HurricaneHitArmor = this.HurricaneHitArmor;

        output.DT37Damage = this.DT37Damage;
        output.DS65Damage = this.DS65Damage;
        output.DP9007Damage = this.DP9007Damage;
        output.CGD27PillishionDamage = this.CGD27PillishionDamage;

        output.OSEH302WidowHireDamage = this.OSEH302WidowHireDamage;

        output.VM5AEGDamage = this.VM5AEGDamage;

        output.ArthesL775Step1Damage = this.ArthesL775Step1Damage;
        output.ArthesL775Step2Damage = this.ArthesL775Step2Damage;
        output.ArthesL775Step3Damage = this.ArthesL775Step3Damage;
        output.ArthesL775Step4Damage = this.ArthesL775Step4Damage;
        output.Hydra56Damage = this.Hydra56Damage;
        output.MEAGDamage = this.MEAGDamage;
        output.MEAGAddDamage = this.MEAGAddDamage;
        output.UGG98Step1Damage = this.UGG98Step1Damage;
        output.UGG98Step2Damage = this.UGG98Step2Damage;

        output.SupportAmmoAmount = this.SupportAmmoAmount;

        output.M3078Damage = this.M3078Damage;
        output.ASC365Damage = this.ASC365Damage;

        output.MBCA79IronHurricaneHitPoint = this.MBCA79IronHurricaneHitPoint;
        output.MBCA79IronHurricaneArmor = this.MBCA79IronHurricaneArmor;
        output.MBCA79IronHurricaneHTACDamage = this.MBCA79IronHurricaneHTACDamage;
        output.MBCA79IronHurricaneAPCDamage = this.MBCA79IronHurricaneAPCDamage;
        output.MBCA79IronHurricaneOSEHSDamage = this.MBCA79IronHurricaneOSEHSDamage;
        output.MBCA79IronHurricaneFBWSIrisDamage = this.MBCA79IronHurricaneFBWSIrisDamage;

        output.PGM1036ScaletHawkDamage = this.PGM1036ScaletHawkDamage;

        return output;
    }

    [Serializable]
    public class SerializableUpgradeDataSystem
    {
        [Header("함선 장갑 레벨 정보")]
        public int FlagshipUpgradeLevel; //기함
        public int FormationUpgradeLevel; //편대함
        public int TacticalShipUpgradeLevel; //전략함

        [Header("함선 무기 레벨 정보")]
        public int ShipCannonUpgradeLevel; //함포
        public int ShipMissileUpgradeLevel; //미사일
        public int ShipFighterUpgradeLevel; //함재기

        [Header("함대 지원 레벨 정보")]
        public int FlagshipAttackSkillUpgradeLevel; //기함 공격 지원
        public int FleetAttackSkillUpgradeLevel; //함대 공격 지원

        [Header("델타 허리케인 내구도 레벨 정보")]
        public int HurricaneHitPointUpgradeLevel; //체력

        [Header("델타 허리케인 기본 무기 레벨 정보")]
        public int HurricaneAssaultRifleUpgradeLevel; //돌격 소총
        public int HurricaneShotgunUpgradeLevel; //샷건
        public int HurricaneSniperRifleUpgradeLevel; //저격총
        public int HurricaneSubmachineGunUpgradeLevel; //기관단총

        [Header("델타 허리케인 지원 무기 레벨 정보")]
        public int HurricaneSubGearUpgradeLevel; //보조 장비
        public int HurricaneGrenadeUpgradeLevel; //수류탄
        public int HurricaneChangeHeavyWeaponUpgradeLevel; //체인지 중화기

        [Header("함선 지원 레벨 정보")]
        public int ShipAmmoSupportUpgradeLevel; //보급 지원
        public int ShipHeavyWeaponSupportUpgradeLevel; //중화기 지원
        public int ShipVehicleSupportUpgradeLevel; //탑승 차량 지원
        public int ShipStrikeSupportUpgradeLevel; //공격 지원

        [Header("함선 장갑 업그레이드 정보")]
        public float FlagshipHitPoints; //기함 선체
        public float FormationHitPoints; //편대함 선체
        public float ShieldShipHitPoints; //방패함 선체(전략함)
        public float CarrierHitPoints; //우주모함 선체(전략함)

        [Header("함선 무기 업그레이드 정보")]
        public int FlagshipSilenceSistDamage; //사일런스 시스트 연발 주포 데미지(기함)
        public int FlagshipOverJumpDamage; //초과점프 단발 주포 데미지(기함)
        public int FlagshipSadLilly345Damage; //세드 릴리-345 단발 미사일 데미지(기함)
        public int FlagshipDeltaNeedle42HalistDamage; //델타 니들-42 할리스트 멀티 미사일 데미지(기함)
        public int FlagshipGF12DeltaWingBomberDamage; //GF-12 델타 윙 폭격기 데미지(기함)
        public int FormationSilenceSistDamage; //사일런스 시스트 연발 주포 데미지(편대함)
        public int FormationOverJumpDamage; //초과점프 단발 주포 데미지(편대함)
        public int FormationSadLilly345Damage; //세드 릴리-345 단발 미사일 데미지(편대함)
        public int FormationDeltaNeedle42HalistDamage; //델타 니들-42 할리스트 멀티 미사일 데미지(편대함)
        public int CarrierGF12DeltaWingBomberDamage; //GF-12 델타 윙 폭격기 데미지(우주모함)

        [Header("기함 공격 업그레이드 정보(함대 지원)")]
        public int SikroClassCruiseMissileDamage; //시크로급 순항 미사일 데미지

        [Header("함대 공격 업그레이드 정보(함대 지원)")]
        public int Cysiro47PatriotMissileDamage; //사이시로-47 페트리엇 미사일 데미지

        [Header("델타 허리케인 내구도 업그레이드 정보")]
        public float HurricaneHitPoint; //델타 허리케인 체력
        public float HurricaneHitArmor; //델타 허리케인 방어구

        [Header("델타 허리케인 기본 무기 업그레이드 정보")]
        public int DT37Damage; //DT-37 돌격 소총 데미지
        public int DS65Damage; //DS-65 샷건 데미지
        public int DP9007Damage; //DP-9007 저격총 데미지
        public int CGD27PillishionDamage; //CGD-27 필리시온 기관단총 데미지

        [Header("델타 허리케인 보조 장비 업그레이드 정보(지원 무기)")]
        public int OSEH302WidowHireDamage; //OSEH-302 Widow Hire 추적 미사일 데미지

        [Header("델타 허리케인 수류탄 업그레이드 정보(지원 무기)")]
        public int VM5AEGDamage; //VM-5 AEG 수류탄 데미지

        [Header("델타 허리케인 체인지 중화기 업그레이드 정보(지원 무기)")]
        public int ArthesL775Step1Damage; //Arthes L-775 충전 레이져 1단계 데미지
        public int ArthesL775Step2Damage; //Arthes L-775 충전 레이져 2단계 데미지
        public int ArthesL775Step3Damage; //Arthes L-775 충전 레이져 3단계 데미지
        public int ArthesL775Step4Damage; //Arthes L-775 충전 레이져 4단계 데미지

        public int Hydra56Damage; //Hydra-56 분리 철갑포 데미지
        public int MEAGDamage; //MEAG 레일건 데미지
        public int MEAGAddDamage; //MEAG 레일건 초당 상승 데미지
        public int UGG98Step1Damage; //UGG 98 중력포 1단계 데미지
        public int UGG98Step2Damage; //UGG 98 중력포 2단계 데미지

        [Header("함선 보급 지원 업그레이드 정보")]
        public int SupportAmmoAmount; //탄약 지원량

        [Header("함선 중화기 지원 업그레이드 정보")]
        public int M3078Damage; //M3078 미니건 데미지
        public int ASC365Damage; //ASC 365 화염방사기 데미지

        [Header("함선 탑승 차량 지원 업그레이드 정보")]
        public float MBCA79IronHurricaneHitPoint; //MBCA-79 Iron Hurricane 체력
        public float MBCA79IronHurricaneArmor; //MBCA-79 Iron Hurricane 방어
        public int MBCA79IronHurricaneHTACDamage; //HTAC 주포 데미지
        public int MBCA79IronHurricaneAPCDamage; //APC 플라즈마 포 데미지
        public int MBCA79IronHurricaneOSEHSDamage; //OSEHS 추적 미사일 데미지
        public int MBCA79IronHurricaneFBWSIrisDamage; //FBWS Iris 게틀링포 데미지

        [Header("함선 폭격 지원 업그레이드 정보")]
        public int PGM1036ScaletHawkDamage; //Planet Guided Missile 1036 Scalet Hawk 데미지
    }
}