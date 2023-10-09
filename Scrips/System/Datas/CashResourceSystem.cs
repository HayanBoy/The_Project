using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class CashResourceSystem : MonoBehaviour
{
    [Header("텍스트")]
    public Text GlopaorosText; //글로파오로스 자산량
    public Text GlopaorosCurrentText; //행성 모드에서의 기존 글로파오로스 자산량
    public Text GlopaorosLimitText; //글로파오로스 한도량
    public Text ConstructionResourceText; //건설 자원량
    public Text ConstructionResourceLimitText; //건설 자원 한도량
    public Text ConstructionResourceCurrentText; //행성 모드에서의 기존 건설 자원량
    public Text TaritronicText; //타리트로닉 자원량

    [Header("자원 조절")]
    public Slider GlopaorosSlider;
    public Slider ConstructionResourceSlider;
    public bool SliderOn; //행성 관리탭을 눌렀을 때에만 발동
    private int PlanetNumber; //행성 번호
    public int HandleNumber; //슬라이더 핸들 번호

    [Header("자원 목록")]
    public float NarihaUnionGlopaoros; //나리하 인류연합 화폐 Glopaoros
    public float ConstructionResource; //건설 재료
    private float NarihaUnionGlopaorosResult;
    private float ConstructionResourceResult;
    private float TaritronicResult;

    [Header("행성 최대 자원")]
    public float MaxGlopaoros; //반영되는 최종 화폐
    public float MaxConstructionResource;
    public int SatariusGlessiaMaxGlopaoros; //행성마다 얻을 수 있는 최대 글로파오로스 화폐량
    public int SatariusGlessiaMaxCR; //행성마다 얻을 수 있는 최대 건설 재료
    public int ToronoMaxGlopaoros;
    public int ToronoMaxCR;
    public int AronPeriMaxGlopaoros;
    public int AronPeriMaxCR;
    public int PapatusIIMaxGlopaoros;
    public int PapatusIIMaxCR;
    public int OclasisMaxGlopaoros;
    public int OclasisMaxCR;
    public int VeltrorexyMaxGlopaoros;
    public int VeltrorexyMaxCR;
    public int ErixJeoqetaMaxGlopaoros;
    public int ErixJeoqetaMaxCR;
    public int QeepoMaxGlopaoros;
    public int QeepoMaxCR;
    public int OrosMaxGlopaoros;
    public int OrosMaxCR;
    public int Xacro042351MaxGlopaoros;
    public int Xacro042351MaxCR;

    private void Start()
    {
        NarihaUnionGlopaorosResult = BattleSave.Save1.NarihaUnionGlopaoros;
        ConstructionResourceResult = BattleSave.Save1.ConstructionResource;
        TaritronicResult = BattleSave.Save1.Taritronic;

        StartCoroutine(GlopaorosAdd());
        StartCoroutine(CRAdd());
    }

    public void ResetCash(int GlopaorosPay, int ConstructionResourcePay, int TaritronicPay)
    {
        BattleSave.Save1.NarihaUnionGlopaoros = BattleSave.Save1.NarihaUnionGlopaoros - GlopaorosPay;
        BattleSave.Save1.ConstructionResource = BattleSave.Save1.ConstructionResource - ConstructionResourcePay;
        BattleSave.Save1.Taritronic = BattleSave.Save1.Taritronic - TaritronicPay;
    }

    //글로파오로스 화폐 및 건설 재료 증가 계산
    IEnumerator GlopaorosAdd()
    {
        if (BattleSave.Save1.NarihaUnionGlopaoros <= BattleSave.Save1.NarihaUnionGlopaoroslimit)
        {
            while (true)
            {
                yield return new WaitForSeconds(5);

                NarihaUnionGlopaoros = 0;
                GlopaorosAddStart();
            }
        }
    }

    void GlopaorosAddStart()
    {
        if (WeaponUnlockManager.instance.SatariusGlessiaCommercialUnlock == true)
        {
            NarihaUnionGlopaoros += PlanetCashAssetDatas.instance.SatariusGlessiaGlopaoros;
        }
        if (WeaponUnlockManager.instance.ToronoResourceUnlock == true)
        {
            NarihaUnionGlopaoros += PlanetCashAssetDatas.instance.ToronoGlopaoros;
        }
        if (WeaponUnlockManager.instance.AronPeriCommercialUnlock == true)
        {
            NarihaUnionGlopaoros += PlanetCashAssetDatas.instance.AronPeriGlopaoros;
        }
        if (WeaponUnlockManager.instance.PapatusIIResourceUnlock == true)
        {
            NarihaUnionGlopaoros += PlanetCashAssetDatas.instance.PapatusIIGlopaoros;
        }
        if (WeaponUnlockManager.instance.OclasisResourceUnlock == true)
        {
            NarihaUnionGlopaoros += PlanetCashAssetDatas.instance.OclasisGlopaoros;
        }
        if (WeaponUnlockManager.instance.VeltrorexyCommercialUnlock == true)
        {
            NarihaUnionGlopaoros += PlanetCashAssetDatas.instance.VeltrorexyGlopaoros;
        }
        if (WeaponUnlockManager.instance.ErixJeoqetaCommercialUnlock == true)
        {
            NarihaUnionGlopaoros += PlanetCashAssetDatas.instance.ErixJeoqetaGlopaoros;
        }
        if (WeaponUnlockManager.instance.QeepoResourceUnlock == true)
        {
            NarihaUnionGlopaoros += PlanetCashAssetDatas.instance.QeepoGlopaoros;
        }
        if (WeaponUnlockManager.instance.OrosCommercialUnlock == true)
        {
            NarihaUnionGlopaoros += PlanetCashAssetDatas.instance.OrosGlopaoros;
        }
        if (WeaponUnlockManager.instance.Xacro042351ResourceUnlock == true)
        {
            NarihaUnionGlopaoros += PlanetCashAssetDatas.instance.Xacro042351Glopaoros;
        }
        BattleSave.Save1.NarihaUnionGlopaoros = BattleSave.Save1.NarihaUnionGlopaoros + NarihaUnionGlopaoros;
    }

    IEnumerator CRAdd()
    {
        if (BattleSave.Save1.ConstructionResource <= BattleSave.Save1.ConstructionResourcelimit)
        {
            while (true)
            {
                yield return new WaitForSeconds(5);

                ConstructionResource = 0;
                ConstructionResourceAddStart();
            }
        }
    }

    void ConstructionResourceAddStart()
    {
        if (WeaponUnlockManager.instance.SatariusGlessiaCommercialUnlock == true)
        {
            ConstructionResource += PlanetCashAssetDatas.instance.SatariusGlessiaConstructionResource;
        }
        if (WeaponUnlockManager.instance.ToronoResourceUnlock == true)
        {
            ConstructionResource += PlanetCashAssetDatas.instance.ToronoConstructionResource;
        }
        if (WeaponUnlockManager.instance.AronPeriCommercialUnlock == true)
        {
            ConstructionResource += PlanetCashAssetDatas.instance.AronPeriConstructionResource;
        }
        if (WeaponUnlockManager.instance.PapatusIIResourceUnlock == true)
        {
            ConstructionResource += PlanetCashAssetDatas.instance.PapatusIIConstructionResource;
        }
        if (WeaponUnlockManager.instance.OclasisResourceUnlock == true)
        {
            ConstructionResource += PlanetCashAssetDatas.instance.OclasisConstructionResource;
        }
        if (WeaponUnlockManager.instance.VeltrorexyCommercialUnlock == true)
        {
            ConstructionResource += PlanetCashAssetDatas.instance.VeltrorexyConstructionResource;
        }
        if (WeaponUnlockManager.instance.ErixJeoqetaCommercialUnlock == true)
        {
            ConstructionResource += PlanetCashAssetDatas.instance.ErixJeoqetaConstructionResource;
        }
        if (WeaponUnlockManager.instance.QeepoResourceUnlock == true)
        {
            ConstructionResource += PlanetCashAssetDatas.instance.QeepoConstructionResource;
        }
        if (WeaponUnlockManager.instance.OrosCommercialUnlock == true)
        {
            ConstructionResource += PlanetCashAssetDatas.instance.OrosConstructionResource;
        }
        if (WeaponUnlockManager.instance.Xacro042351ResourceUnlock == true)
        {
            ConstructionResource += PlanetCashAssetDatas.instance.Xacro042351ConstructionResource;
        }
        BattleSave.Save1.ConstructionResource = BattleSave.Save1.ConstructionResource + ConstructionResource;
    }

    //함선 격침으로 자산 획득
    public void ShipBattleGainStart(float Glopaoros, float CR, float Taritronic)
    {
        BattleSave.Save1.NarihaUnionGlopaoros = BattleSave.Save1.NarihaUnionGlopaoros + Glopaoros;
        BattleSave.Save1.ConstructionResource = BattleSave.Save1.ConstructionResource + CR;
        BattleSave.Save1.Taritronic = BattleSave.Save1.Taritronic + Taritronic;
    }

    public void GlopaorosHandleDown()
    {
        HandleNumber = 1;
    }
    public void ConstructionResourceHandleDown()
    {
        HandleNumber = 2;
    }
    public void HandleUp()
    {
        HandleNumber = 0;
    }

    //선택된 기함이 주둔하고 있는 행성의 재원 정보 불러오기
    public void PlanetResourceBring(int number)
    {
        PlanetNumber = number;

        if (number == 1001)
        {
            MaxGlopaoros = PlanetCashAssetDatas.instance.SatariusGlessiaGlopaoros;
            MaxConstructionResource = PlanetCashAssetDatas.instance.SatariusGlessiaConstructionResource;
            GlopaorosSlider.value = MaxGlopaoros / SatariusGlessiaMaxGlopaoros * 100;
            ConstructionResourceSlider.value = MaxConstructionResource / SatariusGlessiaMaxCR * 100;
        }
        else if (number == 1003)
        {
            MaxGlopaoros = PlanetCashAssetDatas.instance.ToronoGlopaoros;
            MaxConstructionResource = PlanetCashAssetDatas.instance.ToronoConstructionResource;
            GlopaorosSlider.value = MaxGlopaoros / ToronoMaxGlopaoros * 100;
            ConstructionResourceSlider.value = MaxConstructionResource / ToronoMaxCR * 100;
        }
        else if (number == 1006)
        {
            MaxGlopaoros = PlanetCashAssetDatas.instance.AronPeriGlopaoros;
            MaxConstructionResource = PlanetCashAssetDatas.instance.AronPeriConstructionResource;
            GlopaorosSlider.value = MaxGlopaoros / AronPeriMaxGlopaoros * 100;
            ConstructionResourceSlider.value = MaxConstructionResource / AronPeriMaxCR * 100;
        }
        else if (number == 1007)
        {
            MaxGlopaoros = PlanetCashAssetDatas.instance.PapatusIIGlopaoros;
            MaxConstructionResource = PlanetCashAssetDatas.instance.PapatusIIConstructionResource;
            GlopaorosSlider.value = MaxGlopaoros / PapatusIIMaxGlopaoros * 100;
            ConstructionResourceSlider.value = MaxConstructionResource / PapatusIIMaxCR * 100;
        }
        else if (number == 1011)
        {
            MaxGlopaoros = PlanetCashAssetDatas.instance.OclasisGlopaoros;
            MaxConstructionResource = PlanetCashAssetDatas.instance.OclasisConstructionResource;
            GlopaorosSlider.value = MaxGlopaoros / OclasisMaxGlopaoros * 100;
            ConstructionResourceSlider.value = MaxConstructionResource / OclasisMaxCR * 100;
        }
        else if (number == 1013)
        {
            MaxGlopaoros = PlanetCashAssetDatas.instance.VeltrorexyGlopaoros;
            MaxConstructionResource = PlanetCashAssetDatas.instance.VeltrorexyConstructionResource;
            GlopaorosSlider.value = MaxGlopaoros / VeltrorexyMaxGlopaoros * 100;
            ConstructionResourceSlider.value = MaxConstructionResource / VeltrorexyMaxCR * 100;
        }
        else if (number == 1014)
        {
            MaxGlopaoros = PlanetCashAssetDatas.instance.ErixJeoqetaGlopaoros;
            MaxConstructionResource = PlanetCashAssetDatas.instance.ErixJeoqetaConstructionResource;
            GlopaorosSlider.value = MaxGlopaoros / ErixJeoqetaMaxGlopaoros * 100;
            ConstructionResourceSlider.value = MaxConstructionResource / ErixJeoqetaMaxCR * 100;
        }
        else if (number == 1015)
        {
            MaxGlopaoros = PlanetCashAssetDatas.instance.QeepoGlopaoros;
            MaxConstructionResource = PlanetCashAssetDatas.instance.QeepoConstructionResource;
            GlopaorosSlider.value = MaxGlopaoros / QeepoMaxGlopaoros * 100;
            ConstructionResourceSlider.value = MaxConstructionResource / QeepoMaxCR * 100;
        }
        else if (number == 1017)
        {
            MaxGlopaoros = PlanetCashAssetDatas.instance.OrosGlopaoros;
            MaxConstructionResource = PlanetCashAssetDatas.instance.OrosConstructionResource;
            GlopaorosSlider.value = MaxGlopaoros / OrosMaxGlopaoros * 100;
            ConstructionResourceSlider.value = MaxConstructionResource / OrosMaxCR * 100;
        }
        else if (number == 1019)
        {
            MaxGlopaoros = PlanetCashAssetDatas.instance.Xacro042351Glopaoros;
            MaxConstructionResource = PlanetCashAssetDatas.instance.Xacro042351ConstructionResource;
            GlopaorosSlider.value = MaxGlopaoros / Xacro042351MaxGlopaoros * 100;
            ConstructionResourceSlider.value = MaxConstructionResource / Xacro042351MaxCR * 100;
        }

        GlopaorosCurrentText.text = string.Format("{0:F0}", MaxGlopaoros);
        ConstructionResourceCurrentText.text = string.Format("{0:F0}", MaxConstructionResource);
    }

    private void Update()
    {
        GlopaorosText.text = string.Format("{0:F0}", NarihaUnionGlopaorosResult);
        GlopaorosLimitText.text = string.Format("{0:F0}", BattleSave.Save1.NarihaUnionGlopaoroslimit);
        ConstructionResourceText.text = string.Format("{0:F0}", ConstructionResourceResult);
        ConstructionResourceLimitText.text = string.Format("{0:F0}", BattleSave.Save1.ConstructionResourcelimit);
        TaritronicText.text = string.Format("{0:F0}", TaritronicResult);

        AssetSliderPlanet();
        AssetCalculation();
    }

    //행성별 슬라이더 조절에 의한 자원량 분배 계산
    void AssetSliderPlanet()
    {
        if (HandleNumber == 1) //클릭된 핸들에만 슬라이더 조작
        {
            ConstructionResourceSlider.value = 100 - GlopaorosSlider.value;
        }
        else
        {
            GlopaorosSlider.value = 100 - ConstructionResourceSlider.value;
        }

        GlopaorosCurrentText.text = string.Format("{0:F0}", MaxGlopaoros);
        ConstructionResourceCurrentText.text = string.Format("{0:F0}", MaxConstructionResource);

        //행성별 슬라이더 조절
        if (PlanetNumber == 1001)
        {
            PlanetCashAssetDatas.instance.SatariusGlessiaGlopaoros = SatariusGlessiaMaxGlopaoros * GlopaorosSlider.value / 100;
            PlanetCashAssetDatas.instance.SatariusGlessiaConstructionResource = SatariusGlessiaMaxCR * ConstructionResourceSlider.value / 100;
            MaxGlopaoros = PlanetCashAssetDatas.instance.SatariusGlessiaGlopaoros;
            MaxConstructionResource = PlanetCashAssetDatas.instance.SatariusGlessiaConstructionResource;
        }
        else if (PlanetNumber == 1003)
        {
            PlanetCashAssetDatas.instance.ToronoGlopaoros = ToronoMaxGlopaoros * GlopaorosSlider.value / 100;
            PlanetCashAssetDatas.instance.ToronoConstructionResource = ToronoMaxCR * ConstructionResourceSlider.value / 100;
            MaxGlopaoros = PlanetCashAssetDatas.instance.ToronoGlopaoros;
            MaxConstructionResource = PlanetCashAssetDatas.instance.ToronoConstructionResource;
        }
        else if (PlanetNumber == 1006)
        {
            PlanetCashAssetDatas.instance.AronPeriGlopaoros = AronPeriMaxGlopaoros * GlopaorosSlider.value / 100;
            PlanetCashAssetDatas.instance.AronPeriConstructionResource = AronPeriMaxCR * ConstructionResourceSlider.value / 100;
            MaxGlopaoros = PlanetCashAssetDatas.instance.AronPeriGlopaoros;
            MaxConstructionResource = PlanetCashAssetDatas.instance.AronPeriConstructionResource;
        }
        else if (PlanetNumber == 1007)
        {
            PlanetCashAssetDatas.instance.PapatusIIGlopaoros = PapatusIIMaxGlopaoros * GlopaorosSlider.value / 100;
            PlanetCashAssetDatas.instance.PapatusIIConstructionResource = PapatusIIMaxCR * ConstructionResourceSlider.value / 100;
            MaxGlopaoros = PlanetCashAssetDatas.instance.PapatusIIGlopaoros;
            MaxConstructionResource = PlanetCashAssetDatas.instance.PapatusIIConstructionResource;
        }
        else if (PlanetNumber == 1011)
        {
            PlanetCashAssetDatas.instance.OclasisGlopaoros = OclasisMaxGlopaoros * GlopaorosSlider.value / 100;
            PlanetCashAssetDatas.instance.OclasisConstructionResource = OclasisMaxCR * ConstructionResourceSlider.value / 100;
            MaxGlopaoros = PlanetCashAssetDatas.instance.OclasisGlopaoros;
            MaxConstructionResource = PlanetCashAssetDatas.instance.OclasisConstructionResource;
        }
        else if (PlanetNumber == 1013)
        {
            PlanetCashAssetDatas.instance.VeltrorexyGlopaoros = VeltrorexyMaxGlopaoros * GlopaorosSlider.value / 100;
            PlanetCashAssetDatas.instance.VeltrorexyConstructionResource = VeltrorexyMaxCR * ConstructionResourceSlider.value / 100;
            MaxGlopaoros = PlanetCashAssetDatas.instance.VeltrorexyGlopaoros;
            MaxConstructionResource = PlanetCashAssetDatas.instance.VeltrorexyConstructionResource;
        }
        else if (PlanetNumber == 1014)
        {
            PlanetCashAssetDatas.instance.ErixJeoqetaGlopaoros = ErixJeoqetaMaxGlopaoros * GlopaorosSlider.value / 100;
            PlanetCashAssetDatas.instance.ErixJeoqetaConstructionResource = ErixJeoqetaMaxCR * ConstructionResourceSlider.value / 100;
            MaxGlopaoros = PlanetCashAssetDatas.instance.ErixJeoqetaGlopaoros;
            MaxConstructionResource = PlanetCashAssetDatas.instance.ErixJeoqetaConstructionResource;
        }
        else if (PlanetNumber == 1015)
        {
            PlanetCashAssetDatas.instance.QeepoGlopaoros = QeepoMaxGlopaoros * GlopaorosSlider.value / 100;
            PlanetCashAssetDatas.instance.QeepoConstructionResource = QeepoMaxCR * ConstructionResourceSlider.value / 100;
            MaxGlopaoros = PlanetCashAssetDatas.instance.QeepoGlopaoros;
            MaxConstructionResource = PlanetCashAssetDatas.instance.QeepoConstructionResource;
        }
        else if (PlanetNumber == 1017)
        {
            PlanetCashAssetDatas.instance.OrosGlopaoros = OrosMaxGlopaoros * GlopaorosSlider.value / 100;
            PlanetCashAssetDatas.instance.OrosConstructionResource = OrosMaxCR * ConstructionResourceSlider.value / 100;
            MaxGlopaoros = PlanetCashAssetDatas.instance.OrosGlopaoros;
            MaxConstructionResource = PlanetCashAssetDatas.instance.OrosConstructionResource;
        }
        else if (PlanetNumber == 1019)
        {
            PlanetCashAssetDatas.instance.Xacro042351Glopaoros = Xacro042351MaxGlopaoros * GlopaorosSlider.value / 100;
            PlanetCashAssetDatas.instance.Xacro042351ConstructionResource = Xacro042351MaxCR * ConstructionResourceSlider.value / 100;
            MaxGlopaoros = PlanetCashAssetDatas.instance.Xacro042351Glopaoros;
            MaxConstructionResource = PlanetCashAssetDatas.instance.Xacro042351ConstructionResource;
        }
    }

    //자본 획득 및 비용 지불 계산
    void AssetCalculation()
    {
        //자산 한도 관리
        if (BattleSave.Save1.NarihaUnionGlopaoros >= BattleSave.Save1.NarihaUnionGlopaoroslimit) //글로파오로스
        {
            BattleSave.Save1.NarihaUnionGlopaoros = BattleSave.Save1.NarihaUnionGlopaoroslimit;
        }
        if (BattleSave.Save1.ConstructionResource >= BattleSave.Save1.ConstructionResourcelimit) //건설 자원
        {
            BattleSave.Save1.ConstructionResource = BattleSave.Save1.ConstructionResourcelimit;
        }

        //글로파오로스 계산
        if (NarihaUnionGlopaorosResult > BattleSave.Save1.NarihaUnionGlopaoros) //지불
        {
            NarihaUnionGlopaorosResult = Mathf.MoveTowards(NarihaUnionGlopaorosResult, BattleSave.Save1.NarihaUnionGlopaoros, 100);
        }
        else if (NarihaUnionGlopaorosResult < BattleSave.Save1.NarihaUnionGlopaoros) //얻기
        {
            if (BattleSave.Save1.NarihaUnionGlopaoros < 100)
                NarihaUnionGlopaorosResult = Mathf.MoveTowards(NarihaUnionGlopaorosResult, BattleSave.Save1.NarihaUnionGlopaoros, BattleSave.Save1.NarihaUnionGlopaoros / 100);
            else if (BattleSave.Save1.NarihaUnionGlopaoros >= 100 && BattleSave.Save1.NarihaUnionGlopaoros < 250)
                NarihaUnionGlopaorosResult = Mathf.MoveTowards(NarihaUnionGlopaorosResult, BattleSave.Save1.NarihaUnionGlopaoros, BattleSave.Save1.NarihaUnionGlopaoros / 300);
            else if (BattleSave.Save1.NarihaUnionGlopaoros >= 250 && BattleSave.Save1.NarihaUnionGlopaoros < 500)
                NarihaUnionGlopaorosResult = Mathf.MoveTowards(NarihaUnionGlopaorosResult, BattleSave.Save1.NarihaUnionGlopaoros, BattleSave.Save1.NarihaUnionGlopaoros / 600);
            else if (BattleSave.Save1.NarihaUnionGlopaoros >= 500)
                NarihaUnionGlopaorosResult = Mathf.MoveTowards(NarihaUnionGlopaorosResult, BattleSave.Save1.NarihaUnionGlopaoros, BattleSave.Save1.NarihaUnionGlopaoros / 1000);
        }
        else if (NarihaUnionGlopaorosResult == BattleSave.Save1.NarihaUnionGlopaoros) //유지
        {
            NarihaUnionGlopaorosResult = BattleSave.Save1.NarihaUnionGlopaoros;
        }

        //건설 자원 계산
        if (ConstructionResourceResult > BattleSave.Save1.ConstructionResource)
        {
            ConstructionResourceResult = Mathf.MoveTowards(ConstructionResourceResult, BattleSave.Save1.ConstructionResource, 100);
        }
        else if (ConstructionResourceResult < BattleSave.Save1.ConstructionResource)
        {
            if (BattleSave.Save1.ConstructionResource < 100)
                ConstructionResourceResult = Mathf.MoveTowards(ConstructionResourceResult, BattleSave.Save1.ConstructionResource, BattleSave.Save1.ConstructionResource / 100);
            else if (BattleSave.Save1.ConstructionResource >= 100 && BattleSave.Save1.ConstructionResource < 250)
                ConstructionResourceResult = Mathf.MoveTowards(ConstructionResourceResult, BattleSave.Save1.ConstructionResource, BattleSave.Save1.ConstructionResource / 300);
            else if (BattleSave.Save1.ConstructionResource >= 250 && BattleSave.Save1.ConstructionResource < 500)
                ConstructionResourceResult = Mathf.MoveTowards(ConstructionResourceResult, BattleSave.Save1.ConstructionResource, BattleSave.Save1.ConstructionResource / 600);
            else if (BattleSave.Save1.ConstructionResource >= 500)
                ConstructionResourceResult = Mathf.MoveTowards(ConstructionResourceResult, BattleSave.Save1.ConstructionResource, BattleSave.Save1.ConstructionResource / 1000);
        }
        else if (ConstructionResourceResult == BattleSave.Save1.ConstructionResource)
        {
            ConstructionResourceResult = BattleSave.Save1.ConstructionResource;
        }

        //타리트로닉 계산
        if (TaritronicResult > BattleSave.Save1.Taritronic)
        {
            TaritronicResult = Mathf.MoveTowards(TaritronicResult, BattleSave.Save1.Taritronic, 100);
        }
        if (TaritronicResult < BattleSave.Save1.Taritronic)
        {
            TaritronicResult = Mathf.MoveTowards(TaritronicResult, BattleSave.Save1.Taritronic, BattleSave.Save1.Taritronic / 1000);
        }
        else if (TaritronicResult == BattleSave.Save1.Taritronic)
        {
            TaritronicResult = BattleSave.Save1.Taritronic;
        }
    }
}