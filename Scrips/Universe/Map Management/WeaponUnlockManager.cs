using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUnlockManager : MonoBehaviour
{
    public static WeaponUnlockManager instance = null;

    [Header("�Լ�")]
    public bool FlagshipUnlock = false; //����(����)
    public bool FormationShipUnlock = false; //�����(����)
    public bool TacticalShipUnlock = false; //������(����)
    public bool ShieldShipUnlock = false; //������(����)
    public bool CarrierUnlock = false; //���ָ���(����)

    [Header("�Լ� ����")]
    public bool ShipCannonUnlock = false; //�Լ� ����(����)
    public bool ShipMissileUnlock = false; //�Լ� �̻���(����)
    public bool ShipFighterUnlock = false; //�Լ� �����(����)
    public bool DeltaNeedle42HalistUnlock = false; //Delta Needle-42 Halist ��Ƽ �̻���(�߰�)
    public bool OverJumpUnlock = false; //�ʰ� ���� ����(�߰�)

    [Header("���� ��ų ��ȭ")]
    public bool FlagshipSkillAttackUnlock = false; //���� ���� ��ų(����)
    public bool FleetAttackSkillUnlock = false; //�Դ� ���� ��ų(����)

    [Header("���� ��ų ����")]
    public List<bool> FlagshipFirstSlotUnlock = new List<bool>(); //���� ù ��° ����
    public List<bool> FlagshipSecondSlotUnlock = new List<bool>(); //���� �� ��° ����
    public List<bool> FlagshipThirdSlotUnlock = new List<bool>(); //���� �� ��° ����

    [Header("��Ÿ �㸮���� ������")]
    public bool SuitHitPointUnlock = false; //ü��

    [Header("��Ÿ �㸮���� �⺻ ����")]
    public bool DT37Unlock = false; //DT-37 ���� ����
    public bool DS65Unlock = false; //DS-65 ����
    public bool DP9007Unlock = false; //DP-9007 ������
    public bool CGD27PillishionUnlock = false; //CGD-27 Pillishion �������

    [Header("��Ÿ �㸮���� ����ź")]
    public int GrenadeCountUnlock; //������ ������� ����
    public bool VM5AEGUnlock = false; //VM-5 AEG ����ź

    [Header("��Ÿ �㸮���� �������")]
    public int SubWeaponCountUnlock; //������ ������� ����
    public bool OSEH302WidowHireUnlock = false; //OSEH-302 Widow Hire ���� �̻���

    [Header("��Ÿ �㸮���� ü���� ��ȭ��")]
    public int ChangeWeaponCountUnlock; //������ ü���� ��ȭ�� ����
    public bool ChangeHeavyWeaponTotalUnlock = false; //ü���� ��ȭ�Ⱑ �ϳ� �̻� ��� �Ǿ��� ��(����)
    public bool ChangeWeaponTopSlotUnlock = false; //ü���� ��ȭ�� ���Ժ� ���
    public bool ChangeWeaponDownSlotUnlock = false; //������Ʈ ����, ü���� ��ȭ�� ���� �޴����� ������ ������ true�� ó��, ����ִ� ������ �ڵ����� false�� ó��
    public bool ChangeWeaponRightSlotUnlock = false;
    public bool ChangeWeaponLeftSlotUnlock = false;
    public bool ArthesL775Unlock = false; //Arthes L-775 ���� ������
    public bool Hydra56Unlock = false; //Hydra-56 �и� ö����
    public bool MEAGUnlock = false; //MEAG ���ϰ�
    public bool UGG98Unlock = false; //UGG98 �߷���

    [Header("��Ÿ �㸮���� �Լ����� ����")]
    public bool AmmoSupportUnlock; //ź�� ����

    [Header("��Ÿ �㸮���� �Լ����� ��ȭ ��ȭ��")]
    public int PowerHeavyWeaponCountUnlock; //������ ��ȭ�� ���� ����
    public bool PowerHeavyWeaponTotalUnlock; //�ϳ� �̻� ��ȭ�Ⱑ �����Ǿ��� ���(����)
    public bool M3078Unlock = false; //M3078 �̴ϰ�
    public bool ASC365Unlock = false; //ASC 365 ȭ������

    [Header("��Ÿ �㸮���� �Լ����� ����")]
    public int AirStrikeCountUnlock; //������ �Լ� ���� ���� ����
    public bool AirStrikeTotalUnlock; //�ϳ� �̻� ���������� �����Ǿ��� ���(����)
    public bool PGM1036ScaletHawkUnlock = false; //PGM-1036 Scalet Hawk ���׹̻���

    [Header("��Ÿ �㸮���� �Լ����� ž������")]
    public int VehicleCountUnlock; //������ �Լ� ���� ���� ����
    public bool VehicleTotalUnlock; //�ϳ� �̻� ž�� ������ �����Ǿ��� ���(����)
    public bool MBCA79IronHurricaneUnlock = false; //MBCA-79 Iron Hurricane �����κ�

    [Header("�༺ ������")]
    public bool PlopaIIResidenceUnlock = false; //�÷��� II ������
    public bool KyepotorosResidenceUnlock = false; //Ű������ν� ������
    public bool TratosResidenceUnlock = false; //Ʈ���佺 ������
    public bool DeltaD31_9523ResidenceUnlock = false; //��Ÿ D31-9523 ������
    public bool JeratoO95_1125ResidenceUnlock = false; //������ O95-1125 ������

    [Header("�༺ �ڿ�")]
    public bool SatariusGlessiaCommercialUnlock = false; //��Ÿ���콺 �۷��þ� �ڿ�(���)
    public bool ToronoResourceUnlock = false; //��γ� �ڿ�(����)
    public bool AronPeriCommercialUnlock = false; //�Ʒ� �丮 �ڿ�(���)
    public bool PapatusIIResourceUnlock = false; //�������� II �ڿ�(����)
    public bool OclasisResourceUnlock = false; //��Ŭ��ý� �ڿ�(����)
    public bool VeltrorexyCommercialUnlock = false; //��Ʈ�η��� �ڿ�(���)
    public bool ErixJeoqetaCommercialUnlock = false; //������ ����Ÿ �ڿ�(���)
    public bool QeepoResourceUnlock = false; //������ �ڿ�(����)
    public bool OrosCommercialUnlock = false; //���ν� �ڿ�(���)
    public bool Xacro042351ResourceUnlock = false; //��ũ�� 042351 �ڿ�(����)

    [Header("�༺ ������")]
    public bool AposisLabUnlock = false; //�����ý� ������
    public bool VedesVILabUnlock = false; //������ VI ������
    public bool PapatusIIILabUnlock = false; //�������� III ������
    public bool DeriousHeriLabUnlock = false; //�����콺 �츮 ������
    public bool CrownYosereLabUnlock = false; //ũ��� �似�� ������
    public bool JapetAgroneLabUnlock = false; //���� �Ʊ׷γ� ������
    public bool DeltaD31_2208LabUnlock = false; //��Ÿ D31-2208 ������
    public bool DeltaD31_12721LabUnlock = false; //��Ÿ D31-12721 ������
    public bool JeratoO95_2252LabUnlock = false; //������ O95-2252 ������
    public bool JeratoO95_8510LabUnlock = false; //������ O95-8510 ������

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
        [Header("�Լ�")]
        public bool FlagshipUnlock = false; //����(����)
        public bool FormationShipUnlock = false; //�����(����)
        public bool TacticalShipUnlock = false; //������(����)
        public bool ShieldShipUnlock = false; //������(����)
        public bool CarrierUnlock = false; //���ָ���(����)

        [Header("�Լ� ����")]
        public bool ShipCannonUnlock = false; //�Լ� ����(����)
        public bool ShipMissileUnlock = false; //�Լ� �̻���(����)
        public bool ShipFighterUnlock = false; //�Լ� �����(����)
        public bool DeltaNeedle42HalistUnlock = false; //Delta Needle-42 Halist ��Ƽ �̻���(�߰�)
        public bool OverJumpUnlock = false; //�ʰ� ���� ����(�߰�)

        [Header("���� ��ų ��ȭ")]
        public bool FlagshipSkillAttackUnlock = false; //���� ���� ��ų(����)
        public bool FleetAttackSkillUnlock = false; //�Դ� ���� ��ų(����)

        [Header("���� ��ų ����")]
        public List<bool> FlagshipFirstSlotUnlock = new List<bool>(); //���� ù ��° ����
        public List<bool> FlagshipSecondSlotUnlock = new List<bool>(); //���� �� ��° ����
        public List<bool> FlagshipThirdSlotUnlock = new List<bool>(); //���� �� ��° ����

        [Header("��Ÿ �㸮���� ������")]
        public bool SuitHitPointUnlock = false; //ü��

        [Header("��Ÿ �㸮���� �⺻ ����")]
        public bool DT37Unlock = false; //DT-37 ���� ����
        public bool DS65Unlock = false; //DS-65 ����
        public bool DP9007Unlock = false; //DP-9007 ������
        public bool CGD27PillishionUnlock = false; //CGD-27 Pillishion �������

        [Header("��Ÿ �㸮���� ����ź")]
        public int GrenadeCountUnlock; //������ ������� ����
        public bool VM5AEGUnlock = false; //VM-5 AEG ����ź

        [Header("��Ÿ �㸮���� �������")]
        public int SubWeaponCountUnlock; //������ ������� ����
        public bool OSEH302WidowHireUnlock = false; //OSEH-302 Widow Hire ���� �̻���

        [Header("��Ÿ �㸮���� ü���� ��ȭ��")]
        public int ChangeWeaponCountUnlock; //������ ü���� ��ȭ�� ����
        public bool ChangeHeavyWeaponTotalUnlock = false; //ü���� ��ȭ�Ⱑ �ϳ� �̻� ��� �Ǿ��� ��(����)
        public bool ChangeWeaponTopSlotUnlock = false; //ü���� ��ȭ�� ���Ժ� ���
        public bool ChangeWeaponDownSlotUnlock = false; //������Ʈ ����, ü���� ��ȭ�� ���� �޴����� ������ ������ true�� ó��, ����ִ� ������ �ڵ����� false�� ó��
        public bool ChangeWeaponRightSlotUnlock = false;
        public bool ChangeWeaponLeftSlotUnlock = false;
        public bool ArthesL775Unlock = false; //Arthes L-775 ���� ������
        public bool Hydra56Unlock = false; //Hydra-56 �и� ö����
        public bool MEAGUnlock = false; //MEAG ���ϰ�
        public bool UGG98Unlock = false; //UGG98 �߷���

        [Header("��Ÿ �㸮���� �Լ����� ����")]
        public bool AmmoSupportUnlock; //ź�� ����

        [Header("��Ÿ �㸮���� �Լ����� ��ȭ ��ȭ��")]
        public int PowerHeavyWeaponCountUnlock; //������ ��ȭ�� ���� ����
        public bool PowerHeavyWeaponTotalUnlock; //�ϳ� �̻� ��ȭ�Ⱑ �����Ǿ��� ���(����)
        public bool M3078Unlock = false; //M3078 �̴ϰ�
        public bool ASC365Unlock = false; //ASC 365 ȭ������

        [Header("��Ÿ �㸮���� �Լ����� ����")]
        public int AirStrikeCountUnlock; //������ �Լ� ���� ���� ����
        public bool AirStrikeTotalUnlock; //�ϳ� �̻� ���������� �����Ǿ��� ���(����)
        public bool PGM1036ScaletHawkUnlock = false; //PGM-1036 Scalet Hawk ���׹̻���

        [Header("��Ÿ �㸮���� �Լ����� ž������")]
        public int VehicleCountUnlock; //������ �Լ� ���� ���� ����
        public bool VehicleTotalUnlock; //�ϳ� �̻� ž�� ������ �����Ǿ��� ���(����)
        public bool MBCA79IronHurricaneUnlock = false; //MBCA-79 Iron Hurricane �����κ�

        [Header("�༺ ������")]
        public bool PlopaIIResidenceUnlock = false; //�÷��� II ������
        public bool KyepotorosResidenceUnlock = false; //Ű������ν� ������
        public bool TratosResidenceUnlock = false; //Ʈ���佺 ������
        public bool DeltaD31_9523ResidenceUnlock = false; //��Ÿ D31-9523 ������
        public bool JeratoO95_1125ResidenceUnlock = false; //������ O95-1125 ������

        [Header("�༺ �ڿ�")]
        public bool SatariusGlessiaCommercialUnlock = false; //��Ÿ���콺 �۷��þ� �ڿ�(���)
        public bool ToronoResourceUnlock = false; //��γ� �ڿ�(����)
        public bool AronPeriCommercialUnlock = false; //�Ʒ� �丮 �ڿ�(���)
        public bool PapatusIIResourceUnlock = false; //�������� II �ڿ�(����)
        public bool OclasisResourceUnlock = false; //��Ŭ��ý� �ڿ�(����)
        public bool VeltrorexyCommercialUnlock = false; //��Ʈ�η��� �ڿ�(���)
        public bool ErixJeoqetaCommercialUnlock = false; //������ ����Ÿ �ڿ�(���)
        public bool QeepoResourceUnlock = false; //������ �ڿ�(����)
        public bool OrosCommercialUnlock = false; //���ν� �ڿ�(���)
        public bool Xacro042351ResourceUnlock = false; //��ũ�� 042351 �ڿ�(����)

        [Header("�༺ ������")]
        public bool AposisLabUnlock = false; //�����ý� ������
        public bool VedesVILabUnlock = false; //������ VI ������
        public bool PapatusIIILabUnlock = false; //�������� III ������
        public bool DeriousHeriLabUnlock = false; //�����콺 �츮 ������
        public bool CrownYosereLabUnlock = false; //ũ��� �似�� ������
        public bool JapetAgroneLabUnlock = false; //���� �Ʊ׷γ� ������
        public bool DeltaD31_2208LabUnlock = false; //��Ÿ D31-2208 ������
        public bool DeltaD31_12721LabUnlock = false; //��Ÿ D31-12721 ������
        public bool JeratoO95_2252LabUnlock = false; //������ O95-2252 ������
        public bool JeratoO95_8510LabUnlock = false; //������ O95-8510 ������
    }
}