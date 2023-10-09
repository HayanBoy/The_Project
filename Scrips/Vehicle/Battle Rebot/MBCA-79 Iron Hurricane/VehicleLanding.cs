using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class VehicleLanding : MonoBehaviour
{
    RobotPlayer robotPlayer;
    Movement movement;

    public Transform LandingPos;
    public Transform OutAreaPos;
    Vector3 Landing;
    Vector3 OutArea;

    public float DropSpeed;
    private float OutSpeed;
    private float OutSpeedAccelerater;
    public bool StartLanding = false; //처음에는 강하를 하지 않도록 설정
    public bool StartOut = false;
    public bool ReadyForBattle = false; //전투 탑승 준비. 플레이어가 탑승하지 않은 상태에서만 작동
    private bool Click;
    public bool HTACOnline = false;
    public bool APCOnline = false;
    public bool FBWSOnline = false;

    public GameObject LandingSmokePrefab;
    public Transform LandingSmokePos;
    public GameObject OutSmokePrefab;
    public Transform OutSmokePos;
    private float OneEffect;

    public GameObject FBWSAmmo;

    public GameObject VehicleCall;
    public GameObject VehicleRecall;
    public GameObject Player;
    public GameObject RecallEffect;
    public Image VehicleCallActive;
    public Image VehicleRecallActive;
    public Image VehicleTake;

    public bool EnteringShuttle = false; //미션 완료 후, 수송기에 탑승하기 위해 이동하는 스위치
    private float EnteringOneTime;

    public AudioClip LandingRokectBoom1;
    public AudioClip LandingRokect1;
    public AudioClip LandingRokectLanding1;
    public AudioClip LandingRokectLanding2;
    float SoundBar1;

    public AudioClip Beep1;
    public AudioClip Beep2;

    public void TakeOffUp()
    {
        if (Click == true)
            VehicleRecall.GetComponent<Animator>().SetBool("Click, Vehicle recall", false);
        Click = false;
    }

    public void TakeOffDown()
    {
        Click = true;
        SoundManager.instance.SFXPlay2("Sound", Beep1);
        SoundManager.instance.SFXPlay2("Sound", Beep2);
        VehicleRecall.GetComponent<Animator>().SetBool("Click, Vehicle recall", true);
    }

    public void TakeOffEnter()
    {
        if (Click == true)
        {
            SoundManager.instance.SFXPlay2("Sound", Beep1);
            SoundManager.instance.SFXPlay2("Sound", Beep2);
            VehicleRecall.GetComponent<Animator>().SetBool("Click, Vehicle recall", true);
        }
    }

    public void TakeOffExit()
    {
        if (Click == true)
            VehicleRecall.GetComponent<Animator>().SetBool("Click, Vehicle recall", false);
    }

    public void TakeOffClick()
    {
        VehicleRecall.GetComponent<Animator>().SetBool("Activated, Vehicle recall", true);
        VehicleRecallActive.raycastTarget = false;
        OutArea = new Vector3(transform.position.x, 100, transform.position.z);
        StartCoroutine(TheAir());
    }

    public void GetStartBooster()
    {
        SoundBar1 = 0;
        OneEffect = 0;
        GetComponent<Animator>().SetBool("Landing, MBCA-79", true);
        transform.position = LandingPos.position;
        Landing = transform.position - new Vector3(-1, 47f, 0);
        StartLanding = true;
    }

    private void Start()
    {
        robotPlayer = FindObjectOfType<RobotPlayer>();
        movement = FindObjectOfType<Movement>();
    }

    void Update()
    {
        if (StartLanding == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, Landing, DropSpeed * Time.deltaTime);

            if (SoundBar1 == 0)
            {
                SoundBar1 += Time.deltaTime;
                SoundManager.instance.SFXPlay9("Sound", LandingRokectBoom1);
            }
        }

        if (StartLanding == true && transform.position.y <= Landing.y)
        {
            if (OneEffect == 0)
            {
                OneEffect += Time.deltaTime;
                StartLanding = false;
                SoundManager.instance.SFXPlay9("Sound", LandingRokect1);
                GameObject Landingeffect = Instantiate(LandingSmokePrefab, LandingSmokePos.position, LandingSmokePos.rotation);
                VehicleCall.GetComponent<Animator>().SetBool("Activated, Vehicle call", false);
                VehicleCall.GetComponent<Animator>().SetBool("Dropping, Vehicle call", false);
                VehicleCall.GetComponent<Animator>().SetBool("Turn off, Vehicle call", true);
                VehicleCallActive.raycastTarget = false;
                VehicleRecall.GetComponent<Animator>().SetBool("Turn off, Vehicle recall", false);
                VehicleRecall.GetComponent<Animator>().SetBool("Swap, Vehicle recall", true);
                VehicleRecallActive.raycastTarget = true;
                this.gameObject.layer = 18;
                VehicleTake.raycastTarget = true;
                ReadyForBattle = true;
            }
        }

        if (StartOut == true)
        {
            OutSpeed += OutSpeedAccelerater * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, OutArea, OutSpeed * Time.deltaTime);
        }

        //플레이어가 임무를 완료하고 수송기에 탑승하고 있을 때, 탑승 차량이 존재할 경우, 자동으로 함선으로 귀환
        if (EnteringShuttle == true && ReadyForBattle == true)
        {
            if (EnteringOneTime == 0)
            {
                EnteringOneTime += Time.deltaTime;
                TakeOffClick();
            }
        }
    }

    //상승
    IEnumerator TheAir()
    {
        SoundManager.instance.SFXPlay9("Sound", LandingRokectLanding1);
        VehicleTake.raycastTarget = false;
        ReadyForBattle = false;
        this.gameObject.layer = 0;
        VehicleRecall.GetComponent<Animator>().SetBool("Recall, Vehicle recall", true);
        GetComponent<Animator>().SetBool("Landing, MBCA-79", false);
        GetComponent<Animator>().SetBool("Recall, MBCA-79", true);
        OutSpeedAccelerater = 3;
        StartOut = true;
        GameObject OutEffect = Instantiate(OutSmokePrefab, OutSmokePos.position, OutSmokePos.rotation);
        Destroy(OutEffect, 5);
        yield return new WaitForSeconds(1);
        SoundManager.instance.SFXPlay9("Sound", LandingRokectLanding2);
        OutSpeedAccelerater = 70f;
        yield return new WaitForSeconds(2);
        StartOut = false;
        OutSpeedAccelerater = 0;
        OutSpeed = 0;
        GetComponent<Animator>().SetBool("Recall, MBCA-79", false);
        yield return new WaitForSeconds(0.5f);
        transform.localPosition = new Vector3(0, 0, transform.position.z);
        transform.eulerAngles = new Vector3(0, 0, transform.position.z);
        RecallEffect.SetActive(true);
        yield return new WaitForSeconds(3);

        if (robotPlayer.hitPoints >= robotPlayer.startingHitPoints * 0.8f)
        {
            movement.VehicleItemCool = 5;
            robotPlayer.hitPoints = robotPlayer.startingHitPoints;
        }
        else if (robotPlayer.hitPoints > robotPlayer.startingHitPoints * 0.6f && robotPlayer.hitPoints <= robotPlayer.startingHitPoints * 0.8f)
        {
            movement.VehicleItemCool = 8;
            robotPlayer.hitPoints = robotPlayer.startingHitPoints;
        }
        else if (robotPlayer.hitPoints > robotPlayer.startingHitPoints * 0.4f && robotPlayer.hitPoints <= robotPlayer.startingHitPoints * 0.6f)
        {
            movement.VehicleItemCool = 13;
            robotPlayer.hitPoints = robotPlayer.startingHitPoints;
        }
        else if (robotPlayer.hitPoints > robotPlayer.startingHitPoints * 0.2f && robotPlayer.hitPoints <= robotPlayer.startingHitPoints * 0.4f)
        {
            movement.VehicleItemCool = 20;
            robotPlayer.hitPoints = robotPlayer.startingHitPoints;
        }
        else if (robotPlayer.hitPoints < robotPlayer.startingHitPoints * 0.2f)
        {
            movement.VehicleItemCool = 30;
            robotPlayer.hitPoints = robotPlayer.startingHitPoints;
        }

        RecallEffect.SetActive(false);
        Player.GetComponent<Movement>().CallBackComplete = false;
        VehicleRecall.GetComponent<Animator>().SetBool("Turn off, Vehicle recall", true);
        VehicleCall.GetComponent<Animator>().SetBool("Turn off, Vehicle call", false);
        VehicleCallActive.raycastTarget = true;
        GetComponent<Animator>().SetBool("Take wait, MBCA-79", false);

        VehicleRecall.GetComponent<Animator>().SetBool("Activated, Vehicle recall", false);
        VehicleRecall.GetComponent<Animator>().SetBool("Swap, Vehicle recall", false);
        VehicleRecall.GetComponent<Animator>().SetBool("Recall, Vehicle recall", false);

        if (HTACOnline == true)
            GetComponent<HTACController>().FillAmmo();
        if (APCOnline == true)
            GetComponent<APCController>().FillAmmo();
        if (FBWSOnline == true)
            FBWSAmmo.GetComponent<FBWSFire>().FillAmmo();
    }
}