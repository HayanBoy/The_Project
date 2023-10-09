using System;
using UnityEngine;

public class PlanetCashAssetDatas : MonoBehaviour
{
    public static PlanetCashAssetDatas instance = null;

    [Header("青己 包府等 磊盔")]
    public float SatariusGlessiaGlopaoros;
    public float SatariusGlessiaConstructionResource;
    public float ToronoGlopaoros;
    public float ToronoConstructionResource;
    public float AronPeriGlopaoros;
    public float AronPeriConstructionResource;
    public float PapatusIIGlopaoros;
    public float PapatusIIConstructionResource;
    public float OclasisGlopaoros;
    public float OclasisConstructionResource;
    public float VeltrorexyGlopaoros;
    public float VeltrorexyConstructionResource;
    public float ErixJeoqetaGlopaoros;
    public float ErixJeoqetaConstructionResource;
    public float QeepoGlopaoros;
    public float QeepoConstructionResource;
    public float OrosGlopaoros;
    public float OrosConstructionResource;
    public float Xacro042351Glopaoros;
    public float Xacro042351ConstructionResource;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void GetData(SerializablePlanetCashAssetDatas values)
    {
        SatariusGlessiaGlopaoros = values.SatariusGlessiaGlopaoros;
        SatariusGlessiaConstructionResource = values.SatariusGlessiaConstructionResource;
        ToronoGlopaoros = values.ToronoGlopaoros;
        ToronoConstructionResource = values.ToronoConstructionResource;
        AronPeriGlopaoros = values.AronPeriGlopaoros;
        AronPeriConstructionResource = values.AronPeriConstructionResource;
        PapatusIIGlopaoros = values.PapatusIIGlopaoros;
        PapatusIIConstructionResource = values.PapatusIIConstructionResource;
        OclasisGlopaoros = values.OclasisGlopaoros;
        OclasisConstructionResource = values.OclasisConstructionResource;
        VeltrorexyGlopaoros = values.VeltrorexyGlopaoros;
        VeltrorexyConstructionResource = values.VeltrorexyConstructionResource;
        ErixJeoqetaGlopaoros = values.ErixJeoqetaGlopaoros;
        ErixJeoqetaConstructionResource = values.ErixJeoqetaConstructionResource;
        QeepoGlopaoros = values.QeepoGlopaoros;
        QeepoConstructionResource = values.QeepoConstructionResource;
        OrosGlopaoros = values.OrosGlopaoros;
        OrosConstructionResource = values.OrosConstructionResource;
        Xacro042351Glopaoros = values.Xacro042351Glopaoros;
        Xacro042351ConstructionResource = values.Xacro042351ConstructionResource;
    }

    public SerializablePlanetCashAssetDatas GetSerializable()
    {
        var output = new SerializablePlanetCashAssetDatas();

        output.SatariusGlessiaGlopaoros = this.SatariusGlessiaGlopaoros;
        output.SatariusGlessiaConstructionResource = this.SatariusGlessiaConstructionResource;
        output.ToronoGlopaoros = this.ToronoGlopaoros;
        output.ToronoConstructionResource = this.ToronoConstructionResource;
        output.AronPeriGlopaoros = this.AronPeriGlopaoros;
        output.AronPeriConstructionResource = this.AronPeriConstructionResource;
        output.PapatusIIGlopaoros = this.PapatusIIGlopaoros;
        output.PapatusIIConstructionResource = this.PapatusIIConstructionResource;
        output.OclasisGlopaoros = this.OclasisGlopaoros;
        output.OclasisConstructionResource = this.OclasisConstructionResource;
        output.VeltrorexyGlopaoros = this.VeltrorexyGlopaoros;
        output.VeltrorexyConstructionResource = this.VeltrorexyConstructionResource;
        output.ErixJeoqetaGlopaoros = this.ErixJeoqetaGlopaoros;
        output.ErixJeoqetaConstructionResource = this.ErixJeoqetaConstructionResource;
        output.QeepoGlopaoros = this.QeepoGlopaoros;
        output.QeepoConstructionResource = this.QeepoConstructionResource;
        output.OrosGlopaoros = this.OrosGlopaoros;
        output.OrosConstructionResource = this.OrosConstructionResource;
        output.Xacro042351Glopaoros = this.Xacro042351Glopaoros;
        output.Xacro042351ConstructionResource = this.Xacro042351ConstructionResource;

        return output;
    }

    [Serializable]
    public class SerializablePlanetCashAssetDatas
    {
        [Header("青己 包府等 磊盔")]
        public float SatariusGlessiaGlopaoros;
        public float SatariusGlessiaConstructionResource;
        public float ToronoGlopaoros;
        public float ToronoConstructionResource;
        public float AronPeriGlopaoros;
        public float AronPeriConstructionResource;
        public float PapatusIIGlopaoros;
        public float PapatusIIConstructionResource;
        public float OclasisGlopaoros;
        public float OclasisConstructionResource;
        public float VeltrorexyGlopaoros;
        public float VeltrorexyConstructionResource;
        public float ErixJeoqetaGlopaoros;
        public float ErixJeoqetaConstructionResource;
        public float QeepoGlopaoros;
        public float QeepoConstructionResource;
        public float OrosGlopaoros;
        public float OrosConstructionResource;
        public float Xacro042351Glopaoros;
        public float Xacro042351ConstructionResource;
    }
}