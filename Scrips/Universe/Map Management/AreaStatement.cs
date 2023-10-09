using System;
using UnityEngine;

public class AreaStatement : MonoBehaviour
{
    UniverseMapSystem UniverseMapSystem;

    //안전도 목록의 int들은 모두 지역의 상태를 의미한다. 1 = 정상, 2 = 침공중, 3 = 점령됨, 4 = 감염됨
    public int StartState = 1;

    [Header("항성 안전도 목록")]
    public int ToropioStarState;
    public int Roro1StarState;
    public int Roro2StarState;
    public int SarisiStarState;
    public int GarixStarState;
    public int SecrosStarState;
    public int TeretosStarState;
    public int MiniPopoStarState;
    public int DeltaD31_4AStarState;
    public int DeltaD31_4BStarState;
    public int JeratoO95_7AStarState;
    public int JeratoO95_7BStarState;
    public int JeratoO95_14CStarState;
    public int JeratoO95_14DStarState;
    public int JeratoO95_OmegaStarState;

    [Header("행성 안전도 목록")]
    public int SatariusGlessiaState;
    public int AposisState;
    public int ToronoState;
    public int Plopa2State;
    public int Vedes4State;
    public int AronPeriState;
    public int Papatus2State;
    public int Papatus3State;
    public int KyepotorosState;
    public int TratosState;
    public int OclasisState;
    public int DeriousHeriState;
    public int VeltrorexyState;
    public int ErixJeoqetaState;
    public int QeepoState;
    public int CrownYosereState;
    public int OrosState;
    public int JapetAgroneState;
    public int Xacro042351State;
    public int DeltaD31_2208State;
    public int DeltaD31_9523State;
    public int DeltaD31_12721State;
    public int JeratoO95_1125State;
    public int JeratoO95_2252State;
    public int JeratoO95_8510State;

    [Header("행성 해방 정보")]
    public int SatariusGlessiaFreeCount;
    public int AposisFreeCount;
    public int ToronoFreeCount;
    public int Plopa2FreeCount;
    public int Vedes4FreeCount;
    public int AronPeriFreeCount;
    public int Papatus2FreeCount;
    public int Papatus3FreeCount;
    public int KyepotorosFreeCount;
    public int TratosFreeCount;
    public int OclasisFreeCount;
    public int DeriousHeriFreeCount;
    public int VeltrorexyFreeCount;
    public int ErixJeoqetaFreeCount;
    public int QeepoFreeCount;
    public int CrownYosereFreeCount;
    public int OrosFreeCount;
    public int JapetAgroneFreeCount;
    public int Xacro042351FreeCount;
    public int DeltaD31_2208FreeCount;
    public int DeltaD31_9523FreeCount;
    public int DeltaD31_12721FreeCount;
    public int JeratoO95_1125FreeCount;
    public int JeratoO95_2252FreeCount;
    public int JeratoO95_8510FreeCount;

    [Header("행성 재침공 정보")]
    public int SatariusGlessiaInvasionCount;
    public int AposisInvasionCount;
    public int ToronoInvasionCount;
    public int Plopa2InvasionCount;
    public int Vedes4InvasionCount;
    public int AronPeriInvasionCount;
    public int Papatus2InvasionCount;
    public int Papatus3InvasionCount;
    public int KyepotorosInvasionCount;
    public int TratosInvasionCount;
    public int OclasisInvasionCount;
    public int DeriousHeriInvasionCount;
    public int VeltrorexyInvasionCount;
    public int ErixJeoqetaInvasionCount;
    public int QeepoInvasionCount;
    public int CrownYosereInvasionCount;
    public int OrosInvasionCount;
    public int JapetAgroneInvasionCount;
    public int Xacro042351InvasionCount;
    public int DeltaD31_2208InvasionCount;
    public int DeltaD31_9523InvasionCount;
    public int DeltaD31_12721InvasionCount;
    public int JeratoO95_1125InvasionCount;
    public int JeratoO95_2252InvasionCount;
    public int JeratoO95_8510InvasionCount;

    [Header("행성 감염 정보")]
    public int SatariusGlessiaInfectionCount;
    public int AposisInfectionCount;
    public int ToronoInfectionCount;
    public int Plopa2InfectionCount;
    public int Vedes4InfectionCount;
    public int AronPeriInfectionCount;
    public int Papatus2InfectionCount;
    public int Papatus3InfectionCount;
    public int KyepotorosInfectionCount;
    public int TratosInfectionCount;
    public int OclasisInfectionCount;
    public int DeriousHeriInfectionCount;
    public int VeltrorexyInfectionCount;
    public int ErixJeoqetaInfectionCount;
    public int QeepoInfectionCount;
    public int CrownYosereInfectionCount;
    public int OrosInfectionCount;
    public int JapetAgroneInfectionCount;
    public int Xacro042351InfectionCount;
    public int DeltaD31_2208InfectionCount;
    public int DeltaD31_9523InfectionCount;
    public int DeltaD31_12721InfectionCount;
    public int JeratoO95_1125InfectionCount;
    public int JeratoO95_2252InfectionCount;
    public int JeratoO95_8510InfectionCount;

    public void BringState(int AreaNumber)
    {
        UniverseMapSystem = FindObjectOfType<UniverseMapSystem>();

        if (AreaNumber == 1)
            UniverseMapSystem.StateNumber = ToropioStarState;
        else if (AreaNumber == 2)
            UniverseMapSystem.StateNumber = Roro1StarState;
        else if (AreaNumber == 3)
            UniverseMapSystem.StateNumber = Roro2StarState;
        else if (AreaNumber == 4)
            UniverseMapSystem.StateNumber = SarisiStarState;
        else if (AreaNumber == 5)
            UniverseMapSystem.StateNumber = GarixStarState;
        else if (AreaNumber == 6)
            UniverseMapSystem.StateNumber = SecrosStarState;
        else if (AreaNumber == 7)
            UniverseMapSystem.StateNumber = TeretosStarState;
        else if (AreaNumber == 8)
            UniverseMapSystem.StateNumber = MiniPopoStarState;
        else if (AreaNumber == 9)
            UniverseMapSystem.StateNumber = DeltaD31_4AStarState;
        else if (AreaNumber == 10)
            UniverseMapSystem.StateNumber = DeltaD31_4BStarState;
        else if (AreaNumber == 11)
            UniverseMapSystem.StateNumber = JeratoO95_7AStarState;
        else if (AreaNumber == 12)
            UniverseMapSystem.StateNumber = JeratoO95_7BStarState;
        else if (AreaNumber == 13)
            UniverseMapSystem.StateNumber = JeratoO95_14CStarState;
        else if (AreaNumber == 14)
            UniverseMapSystem.StateNumber = JeratoO95_14DStarState;
        else if (AreaNumber == 15)
            UniverseMapSystem.StateNumber = JeratoO95_OmegaStarState;

        else if (AreaNumber == 1001)
            UniverseMapSystem.StateNumber = SatariusGlessiaState;
        else if (AreaNumber == 1002)
            UniverseMapSystem.StateNumber = AposisState;
        else if (AreaNumber == 1003)
            UniverseMapSystem.StateNumber = ToronoState;
        else if (AreaNumber == 1004)
            UniverseMapSystem.StateNumber = Plopa2State;
        else if (AreaNumber == 1005)
            UniverseMapSystem.StateNumber = Vedes4State;
        else if (AreaNumber == 1006)
            UniverseMapSystem.StateNumber = AronPeriState;
        else if (AreaNumber == 1007)
            UniverseMapSystem.StateNumber = Papatus2State;
        else if (AreaNumber == 1008)
            UniverseMapSystem.StateNumber = Papatus3State;
        else if (AreaNumber == 1009)
            UniverseMapSystem.StateNumber = KyepotorosState;
        else if (AreaNumber == 1010)
            UniverseMapSystem.StateNumber = TratosState;
        else if (AreaNumber == 1011)
            UniverseMapSystem.StateNumber = OclasisState;
        else if (AreaNumber == 1012)
            UniverseMapSystem.StateNumber = DeriousHeriState;
        else if (AreaNumber == 1013)
            UniverseMapSystem.StateNumber = VeltrorexyState;
        else if (AreaNumber == 1014)
            UniverseMapSystem.StateNumber = ErixJeoqetaState;
        else if (AreaNumber == 1015)
            UniverseMapSystem.StateNumber = QeepoState;
        else if (AreaNumber == 1016)
            UniverseMapSystem.StateNumber = CrownYosereState;
        else if (AreaNumber == 1017)
            UniverseMapSystem.StateNumber = OrosState;
        else if (AreaNumber == 1018)
            UniverseMapSystem.StateNumber = JapetAgroneState;
        else if (AreaNumber == 1019)
            UniverseMapSystem.StateNumber = Xacro042351State;
        else if (AreaNumber == 1020)
            UniverseMapSystem.StateNumber = DeltaD31_2208State;
        else if (AreaNumber == 1021)
            UniverseMapSystem.StateNumber = DeltaD31_9523State;
        else if (AreaNumber == 1022)
            UniverseMapSystem.StateNumber = DeltaD31_12721State;
        else if (AreaNumber == 1023)
            UniverseMapSystem.StateNumber = JeratoO95_1125State;
        else if (AreaNumber == 1024)
            UniverseMapSystem.StateNumber = JeratoO95_2252State;
        else if (AreaNumber == 1025)
            UniverseMapSystem.StateNumber = JeratoO95_8510State;
    }

    public void GetData(SerializableAreaStatement values)
    {
        ToropioStarState = values.ToropioStarState;

        ToropioStarState = values.ToropioStarState;
        Roro1StarState = values.Roro1StarState;
        Roro2StarState = values.Roro2StarState;
        SarisiStarState = values.SarisiStarState;
        GarixStarState = values.GarixStarState;
        SecrosStarState = values.SecrosStarState;
        TeretosStarState = values.TeretosStarState;
        MiniPopoStarState = values.MiniPopoStarState;
        DeltaD31_4AStarState = values.DeltaD31_4AStarState;
        DeltaD31_4BStarState = values.DeltaD31_4BStarState;
        JeratoO95_7AStarState = values.JeratoO95_7AStarState;
        JeratoO95_7BStarState = values.JeratoO95_7BStarState;
        JeratoO95_14CStarState = values.JeratoO95_14CStarState;
        JeratoO95_14DStarState = values.JeratoO95_14DStarState;
        JeratoO95_OmegaStarState = values.JeratoO95_OmegaStarState;

        SatariusGlessiaState = values.SatariusGlessiaState;
        AposisState = values.AposisState;
        ToronoState = values.ToronoState;
        Plopa2State = values.Plopa2State;
        Vedes4State = values.Vedes4State;
        AronPeriState = values.AronPeriState;
        Papatus2State = values.Papatus2State;
        Papatus3State = values.Papatus3State;
        KyepotorosState = values.KyepotorosState;
        TratosState = values.TratosState;
        OclasisState = values.OclasisState;
        DeriousHeriState = values.DeriousHeriState;
        VeltrorexyState = values.VeltrorexyState;
        ErixJeoqetaState = values.ErixJeoqetaState;
        QeepoState = values.QeepoState;
        CrownYosereState = values.CrownYosereState;
        OrosState = values.OrosState;
        JapetAgroneState = values.JapetAgroneState;
        Xacro042351State = values.Xacro042351State;
        DeltaD31_2208State = values.DeltaD31_2208State;
        DeltaD31_9523State = values.DeltaD31_9523State;
        DeltaD31_12721State = values.DeltaD31_12721State;
        JeratoO95_1125State = values.JeratoO95_1125State;
        JeratoO95_2252State = values.JeratoO95_2252State;
        JeratoO95_8510State = values.JeratoO95_8510State;

        SatariusGlessiaFreeCount = values.SatariusGlessiaFreeCount;
        AposisFreeCount = values.AposisFreeCount;
        ToronoFreeCount = values.ToronoFreeCount;
        Plopa2FreeCount = values.Plopa2FreeCount;
        Vedes4FreeCount = values.Vedes4FreeCount;
        AronPeriFreeCount = values.AronPeriFreeCount;
        Papatus2FreeCount = values.Papatus2FreeCount;
        Papatus3FreeCount = values.Papatus3FreeCount;
        KyepotorosFreeCount = values.KyepotorosFreeCount;
        TratosFreeCount = values.TratosFreeCount;
        OclasisFreeCount = values.OclasisFreeCount;
        DeriousHeriFreeCount = values.DeriousHeriFreeCount;
        VeltrorexyFreeCount = values.VeltrorexyFreeCount;
        ErixJeoqetaFreeCount = values.ErixJeoqetaFreeCount;
        QeepoFreeCount = values.QeepoFreeCount;
        CrownYosereFreeCount = values.CrownYosereFreeCount;
        OrosFreeCount = values.OrosFreeCount;
        JapetAgroneFreeCount = values.JapetAgroneFreeCount;
        Xacro042351FreeCount = values.Xacro042351FreeCount;
        DeltaD31_2208FreeCount = values.DeltaD31_2208FreeCount;
        DeltaD31_9523FreeCount = values.DeltaD31_9523FreeCount;
        DeltaD31_12721FreeCount = values.DeltaD31_12721FreeCount;
        JeratoO95_1125FreeCount = values.JeratoO95_1125FreeCount;
        JeratoO95_2252FreeCount = values.JeratoO95_2252FreeCount;
        JeratoO95_8510FreeCount = values.JeratoO95_8510FreeCount;

        SatariusGlessiaInvasionCount = values.SatariusGlessiaInvasionCount;
        AposisInvasionCount = values.AposisInvasionCount;
        ToronoInvasionCount = values.ToronoInvasionCount;
        Plopa2InvasionCount = values.Plopa2InvasionCount;
        Vedes4InvasionCount = values.Vedes4InvasionCount;
        AronPeriInvasionCount = values.AronPeriInvasionCount;
        Papatus2InvasionCount = values.Papatus2InvasionCount;
        Papatus3InvasionCount = values.Papatus3InvasionCount;
        KyepotorosInvasionCount = values.KyepotorosInvasionCount;
        TratosInvasionCount = values.TratosInvasionCount;
        OclasisInvasionCount = values.OclasisInvasionCount;
        DeriousHeriInvasionCount = values.DeriousHeriInvasionCount;
        VeltrorexyInvasionCount = values.VeltrorexyInvasionCount;
        ErixJeoqetaInvasionCount = values.ErixJeoqetaInvasionCount;
        QeepoInvasionCount = values.QeepoInvasionCount;
        CrownYosereInvasionCount = values.CrownYosereInvasionCount;
        OrosInvasionCount = values.OrosInvasionCount;
        JapetAgroneInvasionCount = values.JapetAgroneInvasionCount;
        Xacro042351InvasionCount = values.Xacro042351InvasionCount;
        DeltaD31_2208InvasionCount = values.DeltaD31_2208InvasionCount;
        DeltaD31_9523InvasionCount = values.DeltaD31_9523InvasionCount;
        DeltaD31_12721InvasionCount = values.DeltaD31_12721InvasionCount;
        JeratoO95_1125InvasionCount = values.JeratoO95_1125InvasionCount;
        JeratoO95_2252InvasionCount = values.JeratoO95_2252InvasionCount;
        JeratoO95_8510InvasionCount = values.JeratoO95_8510InvasionCount;

        SatariusGlessiaInfectionCount = values.SatariusGlessiaInfectionCount;
        AposisInfectionCount = values.AposisInfectionCount;
        ToronoInfectionCount = values.ToronoInfectionCount;
        Plopa2InfectionCount = values.Plopa2InfectionCount;
        Vedes4InfectionCount = values.Vedes4InfectionCount;
        AronPeriInfectionCount = values.AronPeriInfectionCount;
        Papatus2InfectionCount = values.Papatus2InfectionCount;
        Papatus3InfectionCount = values.Papatus3InfectionCount;
        KyepotorosInfectionCount = values.KyepotorosInfectionCount;
        TratosInfectionCount = values.TratosInfectionCount;
        OclasisInfectionCount = values.OclasisInfectionCount;
        DeriousHeriInfectionCount = values.DeriousHeriInfectionCount;
        VeltrorexyInfectionCount = values.VeltrorexyInfectionCount;
        ErixJeoqetaInfectionCount = values.ErixJeoqetaInfectionCount;
        QeepoInfectionCount = values.QeepoInfectionCount;
        CrownYosereInfectionCount = values.CrownYosereInfectionCount;
        OrosInfectionCount = values.OrosInfectionCount;
        JapetAgroneInfectionCount = values.JapetAgroneInfectionCount;
        Xacro042351InfectionCount = values.Xacro042351InfectionCount;
        DeltaD31_2208InfectionCount = values.DeltaD31_2208InfectionCount;
        DeltaD31_9523InfectionCount = values.DeltaD31_9523InfectionCount;
        DeltaD31_12721InfectionCount = values.DeltaD31_12721InfectionCount;
        JeratoO95_1125InfectionCount = values.JeratoO95_1125InfectionCount;
        JeratoO95_2252InfectionCount = values.JeratoO95_2252InfectionCount;
        JeratoO95_8510InfectionCount = values.JeratoO95_8510InfectionCount;
    }

    public SerializableAreaStatement GetSerializable()
    {
        var output = new SerializableAreaStatement();

        output.ToropioStarState = this.ToropioStarState;
        output.Roro1StarState = this.Roro1StarState;
        output.Roro2StarState = this.Roro2StarState;
        output.SarisiStarState = this.SarisiStarState;
        output.GarixStarState = this.GarixStarState;
        output.SecrosStarState = this.SecrosStarState;
        output.TeretosStarState = this.TeretosStarState;
        output.MiniPopoStarState = this.MiniPopoStarState;
        output.DeltaD31_4AStarState = this.DeltaD31_4AStarState;
        output.DeltaD31_4BStarState = this.DeltaD31_4BStarState;
        output.JeratoO95_7AStarState = this.JeratoO95_7AStarState;
        output.JeratoO95_7BStarState = this.JeratoO95_7BStarState;
        output.JeratoO95_14CStarState = this.JeratoO95_14CStarState;
        output.JeratoO95_14DStarState = this.JeratoO95_14DStarState;
        output.JeratoO95_OmegaStarState = this.JeratoO95_OmegaStarState;

        output.SatariusGlessiaState = this.SatariusGlessiaState;
        output.AposisState = this.AposisState;
        output.ToronoState = this.ToronoState;
        output.Plopa2State = this.Plopa2State;
        output.Vedes4State = this.Vedes4State;
        output.AronPeriState = this.AronPeriState;
        output.Papatus2State = this.Papatus2State;
        output.Papatus3State = this.Papatus3State;
        output.KyepotorosState = this.KyepotorosState;
        output.TratosState = this.TratosState;
        output.OclasisState = this.OclasisState;
        output.DeriousHeriState = this.DeriousHeriState;
        output.VeltrorexyState = this.VeltrorexyState;
        output.ErixJeoqetaState = this.ErixJeoqetaState;
        output.QeepoState = this.QeepoState;
        output.CrownYosereState = this.CrownYosereState;
        output.OrosState = this.OrosState;
        output.JapetAgroneState = this.JapetAgroneState;
        output.Xacro042351State = this.Xacro042351State;
        output.DeltaD31_2208State = this.DeltaD31_2208State;
        output.DeltaD31_9523State = this.DeltaD31_9523State;
        output.DeltaD31_12721State = this.DeltaD31_12721State;
        output.JeratoO95_1125State = this.JeratoO95_1125State;
        output.JeratoO95_2252State = this.JeratoO95_2252State;
        output.JeratoO95_8510State = this.JeratoO95_8510State;

        output.SatariusGlessiaFreeCount = this.SatariusGlessiaFreeCount;
        output.AposisFreeCount = this.AposisFreeCount;
        output.ToronoFreeCount = this.ToronoFreeCount;
        output.Plopa2FreeCount = this.Plopa2FreeCount;
        output.Vedes4FreeCount = this.Vedes4FreeCount;
        output.AronPeriFreeCount = this.AronPeriFreeCount;
        output.Papatus2FreeCount = this.Papatus2FreeCount;
        output.Papatus3FreeCount = this.Papatus3FreeCount;
        output.KyepotorosFreeCount = this.KyepotorosFreeCount;
        output.TratosFreeCount = this.TratosFreeCount;
        output.OclasisFreeCount = this.OclasisFreeCount;
        output.DeriousHeriFreeCount = this.DeriousHeriFreeCount;
        output.VeltrorexyFreeCount = this.VeltrorexyFreeCount;
        output.ErixJeoqetaFreeCount = this.ErixJeoqetaFreeCount;
        output.QeepoFreeCount = this.QeepoFreeCount;
        output.CrownYosereFreeCount = this.CrownYosereFreeCount;
        output.OrosFreeCount = this.OrosFreeCount;
        output.JapetAgroneFreeCount = this.JapetAgroneFreeCount;
        output.Xacro042351FreeCount = this.Xacro042351FreeCount;
        output.DeltaD31_2208FreeCount = this.DeltaD31_2208FreeCount;
        output.DeltaD31_9523FreeCount = this.DeltaD31_9523FreeCount;
        output.DeltaD31_12721FreeCount = this.DeltaD31_12721FreeCount;
        output.JeratoO95_1125FreeCount = this.JeratoO95_1125FreeCount;
        output.JeratoO95_2252FreeCount = this.JeratoO95_2252FreeCount;
        output.JeratoO95_8510FreeCount = this.JeratoO95_8510FreeCount;

        output.SatariusGlessiaInvasionCount = this.SatariusGlessiaInvasionCount;
        output.AposisInvasionCount = this.AposisInvasionCount;
        output.ToronoInvasionCount = this.ToronoInvasionCount;
        output.Plopa2InvasionCount = this.Plopa2InvasionCount;
        output.Vedes4InvasionCount = this.Vedes4InvasionCount;
        output.AronPeriInvasionCount = this.AronPeriInvasionCount;
        output.Papatus2InvasionCount = this.Papatus2InvasionCount;
        output.Papatus3InvasionCount = this.Papatus3InvasionCount;
        output.KyepotorosInvasionCount = this.KyepotorosInvasionCount;
        output.TratosInvasionCount = this.TratosInvasionCount;
        output.OclasisInvasionCount = this.OclasisInvasionCount;
        output.DeriousHeriInvasionCount = this.DeriousHeriInvasionCount;
        output.VeltrorexyInvasionCount = this.VeltrorexyInvasionCount;
        output.ErixJeoqetaInvasionCount = this.ErixJeoqetaInvasionCount;
        output.QeepoInvasionCount = this.QeepoInvasionCount;
        output.CrownYosereInvasionCount = this.CrownYosereInvasionCount;
        output.OrosInvasionCount = this.OrosInvasionCount;
        output.JapetAgroneInvasionCount = this.JapetAgroneInvasionCount;
        output.Xacro042351InvasionCount = this.Xacro042351InvasionCount;
        output.DeltaD31_2208InvasionCount = this.DeltaD31_2208InvasionCount;
        output.DeltaD31_9523InvasionCount = this.DeltaD31_9523InvasionCount;
        output.DeltaD31_12721InvasionCount = this.DeltaD31_12721InvasionCount;
        output.JeratoO95_1125InvasionCount = this.JeratoO95_1125InvasionCount;
        output.JeratoO95_2252InvasionCount = this.JeratoO95_2252InvasionCount;
        output.JeratoO95_8510InvasionCount = this.JeratoO95_8510InvasionCount;

        output.SatariusGlessiaInfectionCount = this.SatariusGlessiaInfectionCount;
        output.AposisInfectionCount = this.AposisInfectionCount;
        output.ToronoInfectionCount = this.ToronoInfectionCount;
        output.Plopa2InfectionCount = this.Plopa2InfectionCount;
        output.Vedes4InfectionCount = this.Vedes4InfectionCount;
        output.AronPeriInfectionCount = this.AronPeriInfectionCount;
        output.Papatus2InfectionCount = this.Papatus2InfectionCount;
        output.Papatus3InfectionCount = this.Papatus3InfectionCount;
        output.KyepotorosInfectionCount = this.KyepotorosInfectionCount;
        output.TratosInfectionCount = this.TratosInfectionCount;
        output.OclasisInfectionCount = this.OclasisInfectionCount;
        output.DeriousHeriInfectionCount = this.DeriousHeriInfectionCount;
        output.VeltrorexyInfectionCount = this.VeltrorexyInfectionCount;
        output.ErixJeoqetaInfectionCount = this.ErixJeoqetaInfectionCount;
        output.QeepoInfectionCount = this.QeepoInfectionCount;
        output.CrownYosereInfectionCount = this.CrownYosereInfectionCount;
        output.OrosInfectionCount = this.OrosInfectionCount;
        output.JapetAgroneInfectionCount = this.JapetAgroneInfectionCount;
        output.Xacro042351InfectionCount = this.Xacro042351InfectionCount;
        output.DeltaD31_2208InfectionCount = this.DeltaD31_2208InfectionCount;
        output.DeltaD31_9523InfectionCount = this.DeltaD31_9523InfectionCount;
        output.DeltaD31_12721InfectionCount = this.DeltaD31_12721InfectionCount;
        output.JeratoO95_1125InfectionCount = this.JeratoO95_1125InfectionCount;
        output.JeratoO95_2252InfectionCount = this.JeratoO95_2252InfectionCount;
        output.JeratoO95_8510InfectionCount = this.JeratoO95_8510InfectionCount;

        return output;
    }

    [Serializable]
    public class SerializableAreaStatement
    {
        [Header("항성 안전도 목록")]
        public int ToropioStarState;
        public int Roro1StarState;
        public int Roro2StarState;
        public int SarisiStarState;
        public int GarixStarState;
        public int SecrosStarState;
        public int TeretosStarState;
        public int MiniPopoStarState;
        public int DeltaD31_4AStarState;
        public int DeltaD31_4BStarState;
        public int JeratoO95_7AStarState;
        public int JeratoO95_7BStarState;
        public int JeratoO95_14CStarState;
        public int JeratoO95_14DStarState;
        public int JeratoO95_OmegaStarState;

        [Header("행성 안전도 목록")]
        public int SatariusGlessiaState;
        public int AposisState;
        public int ToronoState;
        public int Plopa2State;
        public int Vedes4State;
        public int AronPeriState;
        public int Papatus2State;
        public int Papatus3State;
        public int KyepotorosState;
        public int TratosState;
        public int OclasisState;
        public int DeriousHeriState;
        public int VeltrorexyState;
        public int ErixJeoqetaState;
        public int QeepoState;
        public int CrownYosereState;
        public int OrosState;
        public int JapetAgroneState;
        public int Xacro042351State;
        public int DeltaD31_2208State;
        public int DeltaD31_9523State;
        public int DeltaD31_12721State;
        public int JeratoO95_1125State;
        public int JeratoO95_2252State;
        public int JeratoO95_8510State;

        [Header("행성 해방 정보")]
        public int SatariusGlessiaFreeCount;
        public int AposisFreeCount;
        public int ToronoFreeCount;
        public int Plopa2FreeCount;
        public int Vedes4FreeCount;
        public int AronPeriFreeCount;
        public int Papatus2FreeCount;
        public int Papatus3FreeCount;
        public int KyepotorosFreeCount;
        public int TratosFreeCount;
        public int OclasisFreeCount;
        public int DeriousHeriFreeCount;
        public int VeltrorexyFreeCount;
        public int ErixJeoqetaFreeCount;
        public int QeepoFreeCount;
        public int CrownYosereFreeCount;
        public int OrosFreeCount;
        public int JapetAgroneFreeCount;
        public int Xacro042351FreeCount;
        public int DeltaD31_2208FreeCount;
        public int DeltaD31_9523FreeCount;
        public int DeltaD31_12721FreeCount;
        public int JeratoO95_1125FreeCount;
        public int JeratoO95_2252FreeCount;
        public int JeratoO95_8510FreeCount;

        [Header("행성 재침공 정보")]
        public int SatariusGlessiaInvasionCount;
        public int AposisInvasionCount;
        public int ToronoInvasionCount;
        public int Plopa2InvasionCount;
        public int Vedes4InvasionCount;
        public int AronPeriInvasionCount;
        public int Papatus2InvasionCount;
        public int Papatus3InvasionCount;
        public int KyepotorosInvasionCount;
        public int TratosInvasionCount;
        public int OclasisInvasionCount;
        public int DeriousHeriInvasionCount;
        public int VeltrorexyInvasionCount;
        public int ErixJeoqetaInvasionCount;
        public int QeepoInvasionCount;
        public int CrownYosereInvasionCount;
        public int OrosInvasionCount;
        public int JapetAgroneInvasionCount;
        public int Xacro042351InvasionCount;
        public int DeltaD31_2208InvasionCount;
        public int DeltaD31_9523InvasionCount;
        public int DeltaD31_12721InvasionCount;
        public int JeratoO95_1125InvasionCount;
        public int JeratoO95_2252InvasionCount;
        public int JeratoO95_8510InvasionCount;

        [Header("행성 감염 정보")]
        public int SatariusGlessiaInfectionCount;
        public int AposisInfectionCount;
        public int ToronoInfectionCount;
        public int Plopa2InfectionCount;
        public int Vedes4InfectionCount;
        public int AronPeriInfectionCount;
        public int Papatus2InfectionCount;
        public int Papatus3InfectionCount;
        public int KyepotorosInfectionCount;
        public int TratosInfectionCount;
        public int OclasisInfectionCount;
        public int DeriousHeriInfectionCount;
        public int VeltrorexyInfectionCount;
        public int ErixJeoqetaInfectionCount;
        public int QeepoInfectionCount;
        public int CrownYosereInfectionCount;
        public int OrosInfectionCount;
        public int JapetAgroneInfectionCount;
        public int Xacro042351InfectionCount;
        public int DeltaD31_2208InfectionCount;
        public int DeltaD31_9523InfectionCount;
        public int DeltaD31_12721InfectionCount;
        public int JeratoO95_1125InfectionCount;
        public int JeratoO95_2252InfectionCount;
        public int JeratoO95_8510InfectionCount;
    }
}