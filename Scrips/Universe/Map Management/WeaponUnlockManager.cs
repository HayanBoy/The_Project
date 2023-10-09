using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUnlockManager : MonoBehaviour
{
    public static WeaponUnlockManager instance = null;

    [Header("함선")]
    public bool FlagshipUnlock = false; //기함(연구)
    public bool FormationShipUnlock = false; //편대함(연구)
    public bool TacticalShipUnlock = false; //전략함(연구)
    public bool ShieldShipUnlock = false; //방패함(건조)
    public bool CarrierUnlock = false; //우주모함(건조)

    [Header("함선 무기")]
    public bool ShipCannonUnlock = false; //함선 주포(연구)
    public bool ShipMissileUnlock = false; //함선 미사일(연구)
    public bool ShipFighterUnlock = false; //함선 함재기(연구)
    public bool DeltaNeedle42HalistUnlock = false; //Delta Needle-42 Halist 멀티 미사일(추가)
    public bool OverJumpUnlock = false; //초과 점프 주포(추가)

    [Header("기함 스킬 강화")]
    public bool FlagshipSkillAttackUnlock = false; //기함 공격 스킬(연구)
    public bool FleetAttackSkillUnlock = false; //함대 공격 스킬(연구)

    [Header("기함 스킬 슬롯")]
    public List<bool> FlagshipFirstSlotUnlock = new List<bool>(); //기함 첫 번째 슬롯
    public List<bool> FlagshipSecondSlotUnlock = new List<bool>(); //기함 두 번째 슬롯
    public List<bool> FlagshipThirdSlotUnlock = new List<bool>(); //기함 세 번째 슬롯

    [Header("델타 허리케인 내구도")]
    public bool SuitHitPointUnlock = false; //체력

    [Header("델타 허리케인 기본 무기")]
    public bool DT37Unlock = false; //DT-37 돌격 소총
    public bool DS65Unlock = false; //DS-65 샷건
    public bool DP9007Unlock = false; //DP-9007 저격총
    public bool CGD27PillishionUnlock = false; //CGD-27 Pillishion 기관단총

    [Header("델타 허리케인 수류탄")]
    public int GrenadeCountUnlock; //해제된 보조장비 갯수
    public bool VM5AEGUnlock = false; //VM-5 AEG 수류탄

    [Header("델타 허리케인 보조장비")]
    public int SubWeaponCountUnlock; //해제된 보조장비 갯수
    public bool OSEH302WidowHireUnlock = false; //OSEH-302 Widow Hire 추적 미사일

    [Header("델타 허리케인 체인지 중화기")]
    public int ChangeWeaponCountUnlock; //해제된 체인지 중화기 갯수
    public bool ChangeHeavyWeaponTotalUnlock = false; //체인지 중화기가 하나 이상 언락 되었을 때(연구)
    public bool ChangeWeaponTopSlotUnlock = false; //체인지 중화기 슬롯별 언락
    public bool ChangeWeaponDownSlotUnlock = false; //업데이트 이후, 체인지 중화기 장착 메뉴에서 장착된 슬롯은 true로 처리, 비어있는 슬롯은 자동으로 false로 처리
    public bool ChangeWeaponRightSlotUnlock = false;
    public bool ChangeWeaponLeftSlotUnlock = false;
    public bool ArthesL775Unlock = false; //Arthes L-775 충전 레이져
    public bool Hydra56Unlock = false; //Hydra-56 분리 철갑포
    public bool MEAGUnlock = false; //MEAG 레일건
    public bool UGG98Unlock = false; //UGG98 중력포

    [Header("델타 허리케인 함선지원 보급")]
    public bool AmmoSupportUnlock; //탄약 지원

    [Header("델타 허리케인 함선지원 강화 중화기")]
    public int PowerHeavyWeaponCountUnlock; //해제된 중화기 지원 갯수
    public bool PowerHeavyWeaponTotalUnlock; //하나 이상 중화기가 해제되었을 경우(연구)
    public bool M3078Unlock = false; //M3078 미니건
    public bool ASC365Unlock = false; //ASC 365 화염방사기

    [Header("델타 허리케인 함선지원 폭격")]
    public int AirStrikeCountUnlock; //해제된 함선 폭격 지원 갯수
    public bool AirStrikeTotalUnlock; //하나 이상 폭격지원이 해제되었을 경우(연구)
    public bool PGM1036ScaletHawkUnlock = false; //PGM-1036 Scalet Hawk 순항미사일

    [Header("델타 허리케인 함선지원 탑승차량")]
    public int VehicleCountUnlock; //해제된 함선 폭격 지원 갯수
    public bool VehicleTotalUnlock; //하나 이상 탑승 차량이 해제되었을 경우(연구)
    public bool MBCA79IronHurricaneUnlock = false; //MBCA-79 Iron Hurricane 전투로봇

    [Header("행성 거주지")]
    public bool PlopaIIResidenceUnlock = false; //플로파 II 거주지
    public bool KyepotorosResidenceUnlock = false; //키예포토로스 거주지
    public bool TratosResidenceUnlock = false; //트라토스 거주지
    public bool DeltaD31_9523ResidenceUnlock = false; //델타 D31-9523 거주지
    public bool JeratoO95_1125ResidenceUnlock = false; //제라토 O95-1125 거주지

    [Header("행성 자원")]
    public bool SatariusGlessiaCommercialUnlock = false; //사타리우스 글래시아 자원(상업)
    public bool ToronoResourceUnlock = false; //토로노 자원(광물)
    public bool AronPeriCommercialUnlock = false; //아론 페리 자원(상업)
    public bool PapatusIIResourceUnlock = false; //파파투스 II 자원(광물)
    public bool OclasisResourceUnlock = false; //오클라시스 자원(광물)
    public bool VeltrorexyCommercialUnlock = false; //벨트로렉시 자원(상업)
    public bool ErixJeoqetaCommercialUnlock = false; //에릭스 제퀘타 자원(상업)
    public bool QeepoResourceUnlock = false; //퀴이포 자원(광물)
    public bool OrosCommercialUnlock = false; //오로스 자원(상업)
    public bool Xacro042351ResourceUnlock = false; //자크로 042351 자원(광물)

    [Header("행성 연구지")]
    public bool AposisLabUnlock = false; //아포시스 연구지
    public bool VedesVILabUnlock = false; //베데스 VI 연구지
    public bool PapatusIIILabUnlock = false; //파파투스 III 연구지
    public bool DeriousHeriLabUnlock = false; //데리우스 헤리 연구지
    public bool CrownYosereLabUnlock = false; //크라운 요세레 연구지
    public bool JapetAgroneLabUnlock = false; //자펫 아그로네 연구지
    public bool DeltaD31_2208LabUnlock = false; //델타 D31-2208 연구지
    public bool DeltaD31_12721LabUnlock = false; //델타 D31-12721 연구지
    public bool JeratoO95_2252LabUnlock = false; //제라토 O95-2252 연구지
    public bool JeratoO95_8510LabUnlock = false; //제라토 O95-8510 연구지

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void GetData(SerializableWeaponUnlockManager values)
    {
        FlagshipUnlock = values.FlagshipUnlock;
        FormationShipUnlock = values.FormationShipUnlock;
        TacticalShipUnlock = values.TacticalShipUnlock;
        ShieldShipUnlock = values.ShieldShipUnlock;
        CarrierUnlock = values.CarrierUnlock;

        ShipCannonUnlock = values.ShipCannonUnlock;
        ShipMissileUnlock = values.ShipMissileUnlock;
        ShipFighterUnlock = values.ShipFighterUnlock;
        DeltaNeedle42HalistUnlock = values.DeltaNeedle42HalistUnlock;
        OverJumpUnlock = values.OverJumpUnlock;

        FlagshipSkillAttackUnlock = values.FlagshipSkillAttackUnlock;
        FleetAttackSkillUnlock = values.FleetAttackSkillUnlock;

        FlagshipFirstSlotUnlock = values.FlagshipFirstSlotUnlock;
        FlagshipSecondSlotUnlock = values.FlagshipSecondSlotUnlock;
        FlagshipThirdSlotUnlock = values.FlagshipThirdSlotUnlock;

        SuitHitPointUnlock = values.SuitHitPointUnlock;

        DT37Unlock = values.DT37Unlock;
        DS65Unlock = values.DS65Unlock;
        DP9007Unlock = values.DP9007Unlock;
        CGD27PillishionUnlock = values.CGD27PillishionUnlock;

        GrenadeCountUnlock = values.GrenadeCountUnlock;
        VM5AEGUnlock = values.VM5AEGUnlock;

        SubWeaponCountUnlock = values.SubWeaponCountUnlock;
        OSEH302WidowHireUnlock = values.OSEH302WidowHireUnlock;

        ChangeWeaponCountUnlock = values.ChangeWeaponCountUnlock;
        ChangeHeavyWeaponTotalUnlock = values.ChangeHeavyWeaponTotalUnlock;
        ChangeWeaponTopSlotUnlock = values.ChangeWeaponTopSlotUnlock;
        ChangeWeaponDownSlotUnlock = values.ChangeWeaponDownSlotUnlock;
        ChangeWeaponRightSlotUnlock = values.ChangeWeaponRightSlotUnlock;
        ChangeWeaponLeftSlotUnlock = values.ChangeWeaponLeftSlotUnlock;
        ArthesL775Unlock = values.ArthesL775Unlock;
        Hydra56Unlock = values.Hydra56Unlock;
        MEAGUnlock = values.MEAGUnlock;
        UGG98Unlock = values.UGG98Unlock;

        AmmoSupportUnlock = values.AmmoSupportUnlock;

        PowerHeavyWeaponCountUnlock = values.PowerHeavyWeaponCountUnlock;
        PowerHeavyWeaponTotalUnlock = values.PowerHeavyWeaponTotalUnlock;
        M3078Unlock = values.M3078Unlock;
        ASC365Unlock = values.ASC365Unlock;

        AirStrikeCountUnlock = values.AirStrikeCountUnlock;
        AirStrikeTotalUnlock = values.AirStrikeTotalUnlock;
        PGM1036ScaletHawkUnlock = values.PGM1036ScaletHawkUnlock;

        VehicleCountUnlock = values.VehicleCountUnlock;
        VehicleTotalUnlock = values.VehicleTotalUnlock;
        MBCA79IronHurricaneUnlock = values.MBCA79IronHurricaneUnlock;

        PlopaIIResidenceUnlock = values.PlopaIIResidenceUnlock;
        KyepotorosResidenceUnlock = values.KyepotorosResidenceUnlock;
        TratosResidenceUnlock = values.TratosResidenceUnlock;
        DeltaD31_9523ResidenceUnlock = values.DeltaD31_9523ResidenceUnlock;
        JeratoO95_1125ResidenceUnlock = values.JeratoO95_1125ResidenceUnlock;

        SatariusGlessiaCommercialUnlock = values.SatariusGlessiaCommercialUnlock;
        ToronoResourceUnlock = values.ToronoResourceUnlock;
        AronPeriCommercialUnlock = values.AronPeriCommercialUnlock;
        PapatusIIResourceUnlock = values.PapatusIIResourceUnlock;
        OclasisResourceUnlock = values.OclasisResourceUnlock;
        VeltrorexyCommercialUnlock = values.VeltrorexyCommercialUnlock;
        ErixJeoqetaCommercialUnlock = values.ErixJeoqetaCommercialUnlock;
        QeepoResourceUnlock = values.QeepoResourceUnlock;
        OrosCommercialUnlock = values.OrosCommercialUnlock;
        Xacro042351ResourceUnlock = values.Xacro042351ResourceUnlock;

        AposisLabUnlock = values.AposisLabUnlock;
        VedesVILabUnlock = values.VedesVILabUnlock;
        PapatusIIILabUnlock = values.PapatusIIILabUnlock;
        DeriousHeriLabUnlock = values.DeriousHeriLabUnlock;
        CrownYosereLabUnlock = values.CrownYosereLabUnlock;
        JapetAgroneLabUnlock = values.JapetAgroneLabUnlock;
        DeltaD31_2208LabUnlock = values.DeltaD31_2208LabUnlock;
        DeltaD31_12721LabUnlock = values.DeltaD31_12721LabUnlock;
        JeratoO95_2252LabUnlock = values.JeratoO95_2252LabUnlock;
        JeratoO95_8510LabUnlock = values.JeratoO95_8510LabUnlock;
    }

    public SerializableWeaponUnlockManager GetSerializable()
    {
        var output = new SerializableWeaponUnlockManager();

        output.FlagshipUnlock = this.FlagshipUnlock;
        output.FormationShipUnlock = this.FormationShipUnlock;
        output.TacticalShipUnlock = this.TacticalShipUnlock;
        output.ShieldShipUnlock = this.ShieldShipUnlock;
        output.CarrierUnlock = this.CarrierUnlock;

        output.ShipCannonUnlock = this.ShipCannonUnlock;
        output.ShipMissileUnlock = this.ShipMissileUnlock;
        output.ShipFighterUnlock = this.ShipFighterUnlock;
        output.DeltaNeedle42HalistUnlock = this.DeltaNeedle42HalistUnlock;
        output.OverJumpUnlock = this.OverJumpUnlock;

        output.FlagshipSkillAttackUnlock = this.FlagshipSkillAttackUnlock;
        output.FleetAttackSkillUnlock = this.FleetAttackSkillUnlock;

        output.FlagshipFirstSlotUnlock = this.FlagshipFirstSlotUnlock;
        output.FlagshipSecondSlotUnlock = this.FlagshipSecondSlotUnlock;
        output.FlagshipThirdSlotUnlock = this.FlagshipThirdSlotUnlock;

        output.SuitHitPointUnlock = this.SuitHitPointUnlock;

        output.DT37Unlock = this.DT37Unlock;
        output.DS65Unlock = this.DS65Unlock;
        output.DP9007Unlock = this.DP9007Unlock;
        output.CGD27PillishionUnlock = this.CGD27PillishionUnlock;

        output.GrenadeCountUnlock = this.GrenadeCountUnlock;
        output.VM5AEGUnlock = this.VM5AEGUnlock;

        output.SubWeaponCountUnlock = this.SubWeaponCountUnlock;
        output.OSEH302WidowHireUnlock = this.OSEH302WidowHireUnlock;

        output.ChangeWeaponCountUnlock = this.ChangeWeaponCountUnlock;
        output.ChangeHeavyWeaponTotalUnlock = this.ChangeHeavyWeaponTotalUnlock;
        output.ChangeWeaponTopSlotUnlock = this.ChangeWeaponTopSlotUnlock;
        output.ChangeWeaponDownSlotUnlock = this.ChangeWeaponDownSlotUnlock;
        output.ChangeWeaponRightSlotUnlock = this.ChangeWeaponRightSlotUnlock;
        output.ChangeWeaponLeftSlotUnlock = this.ChangeWeaponLeftSlotUnlock;
        output.ArthesL775Unlock = this.ArthesL775Unlock;
        output.Hydra56Unlock = this.Hydra56Unlock;
        output.MEAGUnlock = this.MEAGUnlock;
        output.UGG98Unlock = this.UGG98Unlock;

        output.AmmoSupportUnlock = this.AmmoSupportUnlock;

        output.PowerHeavyWeaponCountUnlock = this.PowerHeavyWeaponCountUnlock;
        output.PowerHeavyWeaponTotalUnlock = this.PowerHeavyWeaponTotalUnlock;
        output.M3078Unlock = this.M3078Unlock;
        output.ASC365Unlock = this.ASC365Unlock;

        output.AirStrikeCountUnlock = this.AirStrikeCountUnlock;
        output.AirStrikeTotalUnlock = this.AirStrikeTotalUnlock;
        output.PGM1036ScaletHawkUnlock = this.PGM1036ScaletHawkUnlock;

        output.VehicleCountUnlock = this.VehicleCountUnlock;
        output.VehicleTotalUnlock = this.VehicleTotalUnlock;
        output.MBCA79IronHurricaneUnlock = this.MBCA79IronHurricaneUnlock;

        output.PlopaIIResidenceUnlock = this.PlopaIIResidenceUnlock;
        output.KyepotorosResidenceUnlock = this.KyepotorosResidenceUnlock;
        output.TratosResidenceUnlock = this.TratosResidenceUnlock;
        output.DeltaD31_9523ResidenceUnlock = this.DeltaD31_9523ResidenceUnlock;
        output.JeratoO95_1125ResidenceUnlock = this.JeratoO95_1125ResidenceUnlock;

        output.SatariusGlessiaCommercialUnlock = this.SatariusGlessiaCommercialUnlock;
        output.ToronoResourceUnlock = this.ToronoResourceUnlock;
        output.AronPeriCommercialUnlock = this.AronPeriCommercialUnlock;
        output.PapatusIIResourceUnlock = this.PapatusIIResourceUnlock;
        output.OclasisResourceUnlock = this.OclasisResourceUnlock;
        output.VeltrorexyCommercialUnlock = this.VeltrorexyCommercialUnlock;
        output.ErixJeoqetaCommercialUnlock = this.ErixJeoqetaCommercialUnlock;
        output.QeepoResourceUnlock = this.QeepoResourceUnlock;
        output.OrosCommercialUnlock = this.OrosCommercialUnlock;
        output.Xacro042351ResourceUnlock = this.Xacro042351ResourceUnlock;

        output.AposisLabUnlock = this.AposisLabUnlock;
        output.VedesVILabUnlock = this.VedesVILabUnlock;
        output.PapatusIIILabUnlock = this.PapatusIIILabUnlock;
        output.DeriousHeriLabUnlock = this.DeriousHeriLabUnlock;
        output.CrownYosereLabUnlock = this.CrownYosereLabUnlock;
        output.JapetAgroneLabUnlock = this.JapetAgroneLabUnlock;
        output.DeltaD31_2208LabUnlock = this.DeltaD31_2208LabUnlock;
        output.DeltaD31_12721LabUnlock = this.DeltaD31_12721LabUnlock;
        output.JeratoO95_2252LabUnlock = this.JeratoO95_2252LabUnlock;
        output.JeratoO95_8510LabUnlock = this.JeratoO95_8510LabUnlock;

        return output;
    }

    [Serializable]
    public class SerializableWeaponUnlockManager
    {
        [Header("함선")]
        public bool FlagshipUnlock = false; //기함(연구)
        public bool FormationShipUnlock = false; //편대함(연구)
        public bool TacticalShipUnlock = false; //전략함(연구)
        public bool ShieldShipUnlock = false; //방패함(건조)
        public bool CarrierUnlock = false; //우주모함(건조)

        [Header("함선 무기")]
        public bool ShipCannonUnlock = false; //함선 주포(연구)
        public bool ShipMissileUnlock = false; //함선 미사일(연구)
        public bool ShipFighterUnlock = false; //함선 함재기(연구)
        public bool DeltaNeedle42HalistUnlock = false; //Delta Needle-42 Halist 멀티 미사일(추가)
        public bool OverJumpUnlock = false; //초과 점프 주포(추가)

        [Header("기함 스킬 강화")]
        public bool FlagshipSkillAttackUnlock = false; //기함 공격 스킬(연구)
        public bool FleetAttackSkillUnlock = false; //함대 공격 스킬(연구)

        [Header("기함 스킬 슬롯")]
        public List<bool> FlagshipFirstSlotUnlock = new List<bool>(); //기함 첫 번째 슬롯
        public List<bool> FlagshipSecondSlotUnlock = new List<bool>(); //기함 두 번째 슬롯
        public List<bool> FlagshipThirdSlotUnlock = new List<bool>(); //기함 세 번째 슬롯

        [Header("델타 허리케인 내구도")]
        public bool SuitHitPointUnlock = false; //체력

        [Header("델타 허리케인 기본 무기")]
        public bool DT37Unlock = false; //DT-37 돌격 소총
        public bool DS65Unlock = false; //DS-65 샷건
        public bool DP9007Unlock = false; //DP-9007 저격총
        public bool CGD27PillishionUnlock = false; //CGD-27 Pillishion 기관단총

        [Header("델타 허리케인 수류탄")]
        public int GrenadeCountUnlock; //해제된 보조장비 갯수
        public bool VM5AEGUnlock = false; //VM-5 AEG 수류탄

        [Header("델타 허리케인 보조장비")]
        public int SubWeaponCountUnlock; //해제된 보조장비 갯수
        public bool OSEH302WidowHireUnlock = false; //OSEH-302 Widow Hire 추적 미사일

        [Header("델타 허리케인 체인지 중화기")]
        public int ChangeWeaponCountUnlock; //해제된 체인지 중화기 갯수
        public bool ChangeHeavyWeaponTotalUnlock = false; //체인지 중화기가 하나 이상 언락 되었을 때(연구)
        public bool ChangeWeaponTopSlotUnlock = false; //체인지 중화기 슬롯별 언락
        public bool ChangeWeaponDownSlotUnlock = false; //업데이트 이후, 체인지 중화기 장착 메뉴에서 장착된 슬롯은 true로 처리, 비어있는 슬롯은 자동으로 false로 처리
        public bool ChangeWeaponRightSlotUnlock = false;
        public bool ChangeWeaponLeftSlotUnlock = false;
        public bool ArthesL775Unlock = false; //Arthes L-775 충전 레이져
        public bool Hydra56Unlock = false; //Hydra-56 분리 철갑포
        public bool MEAGUnlock = false; //MEAG 레일건
        public bool UGG98Unlock = false; //UGG98 중력포

        [Header("델타 허리케인 함선지원 보급")]
        public bool AmmoSupportUnlock; //탄약 지원

        [Header("델타 허리케인 함선지원 강화 중화기")]
        public int PowerHeavyWeaponCountUnlock; //해제된 중화기 지원 갯수
        public bool PowerHeavyWeaponTotalUnlock; //하나 이상 중화기가 해제되었을 경우(연구)
        public bool M3078Unlock = false; //M3078 미니건
        public bool ASC365Unlock = false; //ASC 365 화염방사기

        [Header("델타 허리케인 함선지원 폭격")]
        public int AirStrikeCountUnlock; //해제된 함선 폭격 지원 갯수
        public bool AirStrikeTotalUnlock; //하나 이상 폭격지원이 해제되었을 경우(연구)
        public bool PGM1036ScaletHawkUnlock = false; //PGM-1036 Scalet Hawk 순항미사일

        [Header("델타 허리케인 함선지원 탑승차량")]
        public int VehicleCountUnlock; //해제된 함선 폭격 지원 갯수
        public bool VehicleTotalUnlock; //하나 이상 탑승 차량이 해제되었을 경우(연구)
        public bool MBCA79IronHurricaneUnlock = false; //MBCA-79 Iron Hurricane 전투로봇

        [Header("행성 거주지")]
        public bool PlopaIIResidenceUnlock = false; //플로파 II 거주지
        public bool KyepotorosResidenceUnlock = false; //키예포토로스 거주지
        public bool TratosResidenceUnlock = false; //트라토스 거주지
        public bool DeltaD31_9523ResidenceUnlock = false; //델타 D31-9523 거주지
        public bool JeratoO95_1125ResidenceUnlock = false; //제라토 O95-1125 거주지

        [Header("행성 자원")]
        public bool SatariusGlessiaCommercialUnlock = false; //사타리우스 글래시아 자원(상업)
        public bool ToronoResourceUnlock = false; //토로노 자원(광물)
        public bool AronPeriCommercialUnlock = false; //아론 페리 자원(상업)
        public bool PapatusIIResourceUnlock = false; //파파투스 II 자원(광물)
        public bool OclasisResourceUnlock = false; //오클라시스 자원(광물)
        public bool VeltrorexyCommercialUnlock = false; //벨트로렉시 자원(상업)
        public bool ErixJeoqetaCommercialUnlock = false; //에릭스 제퀘타 자원(상업)
        public bool QeepoResourceUnlock = false; //퀴이포 자원(광물)
        public bool OrosCommercialUnlock = false; //오로스 자원(상업)
        public bool Xacro042351ResourceUnlock = false; //자크로 042351 자원(광물)

        [Header("행성 연구지")]
        public bool AposisLabUnlock = false; //아포시스 연구지
        public bool VedesVILabUnlock = false; //베데스 VI 연구지
        public bool PapatusIIILabUnlock = false; //파파투스 III 연구지
        public bool DeriousHeriLabUnlock = false; //데리우스 헤리 연구지
        public bool CrownYosereLabUnlock = false; //크라운 요세레 연구지
        public bool JapetAgroneLabUnlock = false; //자펫 아그로네 연구지
        public bool DeltaD31_2208LabUnlock = false; //델타 D31-2208 연구지
        public bool DeltaD31_12721LabUnlock = false; //델타 D31-12721 연구지
        public bool JeratoO95_2252LabUnlock = false; //제라토 O95-2252 연구지
        public bool JeratoO95_8510LabUnlock = false; //제라토 O95-8510 연구지
    }
}