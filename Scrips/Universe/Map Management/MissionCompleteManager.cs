using System;
using System.Collections.Generic;
using UnityEngine;

public class MissionCompleteManager : MonoBehaviour
{
    public static MissionCompleteManager MCMInstance = null;

    public List<bool> StarFreeList = new List<bool>(); //秦规等 亲己 格废
    public int SatariusGlessiaMissionCompleteCount;
    public List<bool> SatariusGlessiaMission = new List<bool>();
    public int AposisMissionCompleteCount;
    public List<bool> AposisMission = new List<bool>();
    public int ToronoMissionCompleteCount;
    public List<bool> ToronoMission = new List<bool>();
    public int PlopaIIMissionCompleteCount;
    public List<bool> PlopaIIMission = new List<bool>();
    public int VedesVIMissionCompleteCount;
    public List<bool> VedesVIMission = new List<bool>();
    public int AronPeriMissionCompleteCount;
    public List<bool> AronPeriMission = new List<bool>();
    public int PapatusIIMissionCompleteCount;
    public List<bool> PapatusIIMission = new List<bool>();
    public int PapatusIIIMissionCompleteCount;
    public List<bool> PapatusIIIMission = new List<bool>();
    public int KyepotorosMissionCompleteCount;
    public List<bool> KyepotorosMission = new List<bool>();
    public int TratosMissionCompleteCount;
    public List<bool> TratosMission = new List<bool>();
    public int OclasisMissionCompleteCount;
    public List<bool> OclasisMission = new List<bool>();
    public int DeriousHeriMissionCompleteCount;
    public List<bool> DeriousHeriMission = new List<bool>();
    public int VeltrorexyMissionCompleteCount;
    public List<bool> VeltrorexyMission = new List<bool>();
    public int ErixJeoqetaMissionCompleteCount;
    public List<bool> ErixJeoqetaMission = new List<bool>();
    public int QeepoMissionCompleteCount;
    public List<bool> QeepoMission = new List<bool>();
    public int CrownYosereMissionCompleteCount;
    public List<bool> CrownYosereMission = new List<bool>();
    public int OrosMissionCompleteCount;
    public List<bool> OrosMission = new List<bool>();
    public int JapetAgroneMissionCompleteCount;
    public List<bool> JapetAgroneMission = new List<bool>();
    public int Xacro042351MissionCompleteCount;
    public List<bool> Xacro042351Mission = new List<bool>();
    public int DeltaD31_2208MissionCompleteCount;
    public List<bool> DeltaD31_2208Mission = new List<bool>();
    public int DeltaD31_9523MissionCompleteCount;
    public List<bool> DeltaD31_9523Mission = new List<bool>();
    public int DeltaD31_12721MissionCompleteCount;
    public List<bool> DeltaD31_12721Mission = new List<bool>();
    public int JeratoO95_1125MissionCompleteCount;
    public List<bool> JeratoO95_1125Mission = new List<bool>();
    public int JeratoO95_2252MissionCompleteCount;
    public List<bool> JeratoO95_2252Mission = new List<bool>();
    public int JeratoO95_8510MissionCompleteCount;
    public List<bool> JeratoO95_8510Mission = new List<bool>();

    void Awake()
    {
        if (MCMInstance == null)
            MCMInstance = this;
        else if (MCMInstance != this)
            Destroy(gameObject);
    }

    public void GetData(SerializableMissionCompleteManager values)
    {
        StarFreeList = values.StarFreeList;
        SatariusGlessiaMissionCompleteCount = values.SatariusGlessiaMissionCompleteCount;
        SatariusGlessiaMission = values.SatariusGlessiaMission;
        AposisMissionCompleteCount = values.AposisMissionCompleteCount;
        AposisMission = values.AposisMission;
        ToronoMissionCompleteCount = values.ToronoMissionCompleteCount;
        ToronoMission = values.ToronoMission;
        PlopaIIMissionCompleteCount = values.PlopaIIMissionCompleteCount;
        PlopaIIMission = values.PlopaIIMission;
        VedesVIMissionCompleteCount = values.VedesVIMissionCompleteCount;
        VedesVIMission = values.VedesVIMission;
        AronPeriMissionCompleteCount = values.AronPeriMissionCompleteCount;
        AronPeriMission = values.AronPeriMission;
        PapatusIIMissionCompleteCount = values.PapatusIIMissionCompleteCount;
        PapatusIIMission = values.PapatusIIMission;
        PapatusIIIMissionCompleteCount = values.PapatusIIIMissionCompleteCount;
        PapatusIIIMission = values.PapatusIIIMission;
        KyepotorosMissionCompleteCount = values.KyepotorosMissionCompleteCount;
        KyepotorosMission = values.KyepotorosMission;
        TratosMissionCompleteCount = values.TratosMissionCompleteCount;
        TratosMission = values.TratosMission;
        OclasisMissionCompleteCount = values.OclasisMissionCompleteCount;
        OclasisMission = values.OclasisMission;
        DeriousHeriMissionCompleteCount = values.DeriousHeriMissionCompleteCount;
        DeriousHeriMission = values.DeriousHeriMission;
        VeltrorexyMissionCompleteCount = values.VeltrorexyMissionCompleteCount;
        VeltrorexyMission = values.VeltrorexyMission;
        ErixJeoqetaMissionCompleteCount = values.ErixJeoqetaMissionCompleteCount;
        ErixJeoqetaMission = values.ErixJeoqetaMission;
        QeepoMissionCompleteCount = values.QeepoMissionCompleteCount;
        QeepoMission = values.QeepoMission;
        CrownYosereMissionCompleteCount = values.CrownYosereMissionCompleteCount;
        CrownYosereMission = values.CrownYosereMission;
        OrosMissionCompleteCount = values.OrosMissionCompleteCount;
        OrosMission = values.OrosMission;
        JapetAgroneMissionCompleteCount = values.JapetAgroneMissionCompleteCount;
        JapetAgroneMission = values.JapetAgroneMission;
        Xacro042351MissionCompleteCount = values.Xacro042351MissionCompleteCount;
        Xacro042351Mission = values.Xacro042351Mission;
        DeltaD31_2208MissionCompleteCount = values.DeltaD31_2208MissionCompleteCount;
        DeltaD31_2208Mission = values.DeltaD31_2208Mission;
        DeltaD31_9523MissionCompleteCount = values.DeltaD31_9523MissionCompleteCount;
        DeltaD31_9523Mission = values.DeltaD31_9523Mission;
        DeltaD31_12721MissionCompleteCount = values.DeltaD31_12721MissionCompleteCount;
        DeltaD31_12721Mission = values.DeltaD31_12721Mission;
        JeratoO95_1125MissionCompleteCount = values.JeratoO95_1125MissionCompleteCount;
        JeratoO95_1125Mission = values.JeratoO95_1125Mission;
        JeratoO95_2252MissionCompleteCount = values.JeratoO95_2252MissionCompleteCount;
        JeratoO95_2252Mission = values.JeratoO95_2252Mission;
        JeratoO95_8510MissionCompleteCount = values.JeratoO95_8510MissionCompleteCount;
        JeratoO95_8510Mission = values.JeratoO95_8510Mission;
    }

    public SerializableMissionCompleteManager GetSerializable()
    {
        var output = new SerializableMissionCompleteManager();

        output.StarFreeList = this.StarFreeList;
        output.SatariusGlessiaMissionCompleteCount = this.SatariusGlessiaMissionCompleteCount;
        output.SatariusGlessiaMission = this.SatariusGlessiaMission;
        output.AposisMissionCompleteCount = this.AposisMissionCompleteCount;
        output.AposisMission = this.AposisMission;
        output.ToronoMissionCompleteCount = this.ToronoMissionCompleteCount;
        output.ToronoMission = this.ToronoMission;
        output.PlopaIIMissionCompleteCount = this.PlopaIIMissionCompleteCount;
        output.PlopaIIMission = this.PlopaIIMission;
        output.VedesVIMissionCompleteCount = this.VedesVIMissionCompleteCount;
        output.VedesVIMission = this.VedesVIMission;
        output.AronPeriMissionCompleteCount = this.AronPeriMissionCompleteCount;
        output.AronPeriMission = this.AronPeriMission;
        output.PapatusIIMissionCompleteCount = this.PapatusIIMissionCompleteCount;
        output.PapatusIIMission = this.PapatusIIMission;
        output.PapatusIIIMissionCompleteCount = this.PapatusIIIMissionCompleteCount;
        output.PapatusIIIMission = this.PapatusIIIMission;
        output.KyepotorosMissionCompleteCount = this.KyepotorosMissionCompleteCount;
        output.KyepotorosMission = this.KyepotorosMission;
        output.TratosMissionCompleteCount = this.TratosMissionCompleteCount;
        output.TratosMission = this.TratosMission;
        output.OclasisMissionCompleteCount = this.OclasisMissionCompleteCount;
        output.OclasisMission = this.OclasisMission;
        output.DeriousHeriMissionCompleteCount = this.DeriousHeriMissionCompleteCount;
        output.DeriousHeriMission = this.DeriousHeriMission;
        output.VeltrorexyMissionCompleteCount = this.VeltrorexyMissionCompleteCount;
        output.VeltrorexyMission = this.VeltrorexyMission;
        output.ErixJeoqetaMissionCompleteCount = this.ErixJeoqetaMissionCompleteCount;
        output.ErixJeoqetaMission = this.ErixJeoqetaMission;
        output.QeepoMissionCompleteCount = this.QeepoMissionCompleteCount;
        output.QeepoMission = this.QeepoMission;
        output.CrownYosereMissionCompleteCount = this.CrownYosereMissionCompleteCount;
        output.CrownYosereMission = this.CrownYosereMission;
        output.OrosMissionCompleteCount = this.OrosMissionCompleteCount;
        output.OrosMission = this.OrosMission;
        output.JapetAgroneMissionCompleteCount = this.JapetAgroneMissionCompleteCount;
        output.JapetAgroneMission = this.JapetAgroneMission;
        output.Xacro042351MissionCompleteCount = this.Xacro042351MissionCompleteCount;
        output.Xacro042351Mission = this.Xacro042351Mission;
        output.DeltaD31_2208MissionCompleteCount = this.DeltaD31_2208MissionCompleteCount;
        output.DeltaD31_2208Mission = this.DeltaD31_2208Mission;
        output.DeltaD31_9523MissionCompleteCount = this.DeltaD31_9523MissionCompleteCount;
        output.DeltaD31_9523Mission = this.DeltaD31_9523Mission;
        output.DeltaD31_12721MissionCompleteCount = this.DeltaD31_12721MissionCompleteCount;
        output.DeltaD31_12721Mission = this.DeltaD31_12721Mission;
        output.JeratoO95_1125MissionCompleteCount = this.JeratoO95_1125MissionCompleteCount;
        output.JeratoO95_1125Mission = this.JeratoO95_1125Mission;
        output.JeratoO95_2252MissionCompleteCount = this.JeratoO95_2252MissionCompleteCount;
        output.JeratoO95_2252Mission = this.JeratoO95_2252Mission;
        output.JeratoO95_8510MissionCompleteCount = this.JeratoO95_8510MissionCompleteCount;
        output.JeratoO95_8510Mission = this.JeratoO95_8510Mission;

        return output;
    }

    [Serializable]
    public class SerializableMissionCompleteManager
    {
        public List<bool> StarFreeList = new List<bool>(); //秦规等 亲己 格废
        public int SatariusGlessiaMissionCompleteCount;
        public List<bool> SatariusGlessiaMission = new List<bool>();
        public int AposisMissionCompleteCount;
        public List<bool> AposisMission = new List<bool>();
        public int ToronoMissionCompleteCount;
        public List<bool> ToronoMission = new List<bool>();
        public int PlopaIIMissionCompleteCount;
        public List<bool> PlopaIIMission = new List<bool>();
        public int VedesVIMissionCompleteCount;
        public List<bool> VedesVIMission = new List<bool>();
        public int AronPeriMissionCompleteCount;
        public List<bool> AronPeriMission = new List<bool>();
        public int PapatusIIMissionCompleteCount;
        public List<bool> PapatusIIMission = new List<bool>();
        public int PapatusIIIMissionCompleteCount;
        public List<bool> PapatusIIIMission = new List<bool>();
        public int KyepotorosMissionCompleteCount;
        public List<bool> KyepotorosMission = new List<bool>();
        public int TratosMissionCompleteCount;
        public List<bool> TratosMission = new List<bool>();
        public int OclasisMissionCompleteCount;
        public List<bool> OclasisMission = new List<bool>();
        public int DeriousHeriMissionCompleteCount;
        public List<bool> DeriousHeriMission = new List<bool>();
        public int VeltrorexyMissionCompleteCount;
        public List<bool> VeltrorexyMission = new List<bool>();
        public int ErixJeoqetaMissionCompleteCount;
        public List<bool> ErixJeoqetaMission = new List<bool>();
        public int QeepoMissionCompleteCount;
        public List<bool> QeepoMission = new List<bool>();
        public int CrownYosereMissionCompleteCount;
        public List<bool> CrownYosereMission = new List<bool>();
        public int OrosMissionCompleteCount;
        public List<bool> OrosMission = new List<bool>();
        public int JapetAgroneMissionCompleteCount;
        public List<bool> JapetAgroneMission = new List<bool>();
        public int Xacro042351MissionCompleteCount;
        public List<bool> Xacro042351Mission = new List<bool>();
        public int DeltaD31_2208MissionCompleteCount;
        public List<bool> DeltaD31_2208Mission = new List<bool>();
        public int DeltaD31_9523MissionCompleteCount;
        public List<bool> DeltaD31_9523Mission = new List<bool>();
        public int DeltaD31_12721MissionCompleteCount;
        public List<bool> DeltaD31_12721Mission = new List<bool>();
        public int JeratoO95_1125MissionCompleteCount;
        public List<bool> JeratoO95_1125Mission = new List<bool>();
        public int JeratoO95_2252MissionCompleteCount;
        public List<bool> JeratoO95_2252Mission = new List<bool>();
        public int JeratoO95_8510MissionCompleteCount;
        public List<bool> JeratoO95_8510Mission = new List<bool>();
    }
}