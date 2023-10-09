using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RobotPlayer : RobotCharacter
{
    Player Player;

    public GameObject VehicleCall;
    public GameObject VehicleRecall;
    public GameObject AnimationUIMove;
    public GameObject AnimationUIAttack;
    public GameObject AnimationUIVehicleHUD;
    public GameObject player;
    public Image VehicleCallActive;
    public Image VehicleRecallActive;
    public GameObject FBWSAmmo;

    //타격 빔 생성
    public GameObject ShockWaveTaken;
    public Transform BeamTakenPos;
    public int BeamDamageAction; //빔 효과 받기
    float TimeStemp;

    private float HitAction;
    private float DamageTime;
    public float hitPoints; //체력
    public float hitPoints2;
    public float Robotarmor; //방어력 
    float Ricochet;
    float DeathTime;

    SpriteRenderer sprite;

    ObjectManager objectManager;

    public GameObject Body;
    public Transform BodyPos;

    public GameObject RightLeg;
    public Transform RightLegPos;

    public GameObject LeftLeg;
    public Transform LeftLegPos;

    public GameObject RightArm;
    public Transform RightArmPos;

    public GameObject LeftArm;
    public Transform LeftArmPos;

    public GameObject Bump;
    public Transform BumpPos;

    public GameObject ModuleWeapon1;
    public Transform ModuleWeapon1Pos;

    public GameObject ModuleWeapon2;
    public Transform ModuleWeapon2Pos;

    public GameObject ModuleWeapon3;
    public Transform ModuleWeapon3Pos;

    public GameObject ModuleWeapon4;
    public Transform ModuleWeapon4Pos;

    public GameObject ModuleWeapon4_1;
    public Transform ModuleWeapon4_1Pos;

    //도탄 수치 받기
    public void RicochetNum(int num)
    {
        Ricochet = num;
    }

    //빔 데미지 받기
    public void SetBeam(int num)
    {
        BeamDamageAction = num;
    }

    public void Start()
    {
        startingHitPoints = UpgradeDataSystem.instance.MBCA79IronHurricaneHitPoint;
        startingArmor = UpgradeDataSystem.instance.MBCA79IronHurricaneArmor;

        objectManager = FindObjectOfType<ObjectManager>();
        Player = FindObjectOfType<Player>();
        sprite = GetComponent<SpriteRenderer>();
        hitPoints = startingHitPoints;
        Robotarmor = startingArmor;
        hitPoints2 = startingHitPoints;
    }

    public void Update()
    {
        if (HitAction >= 0)
            HitAction -= Time.deltaTime;

        if (TimeStemp > 0)
            TimeStemp -= Time.deltaTime;

        if (TimeStemp < 0)
        {
            TimeStemp = 0;
            BeamDamageAction = 0; //레이져 무기에 타격받은 이후, 다른 무기 타격을 받았을 때 레이져 맞은 효과가 나타나지 않도록 하기 위한 조취
        }

        if (hitPoints < hitPoints2 * 0.2f)
        {
            AnimationUIMove.GetComponent<Animator>().SetBool("Rad heath vehicle, Move joystick", true);
            AnimationUIAttack.GetComponent<Animator>().SetBool("Rad heath vehicle, Attack joystick", true);
            AnimationUIVehicleHUD.GetComponent<Animator>().SetBool("Red, Vehicle HUD", true);
        }
        else
        {
            AnimationUIMove.GetComponent<Animator>().SetBool("Rad heath vehicle, Move joystick", false);
            AnimationUIAttack.GetComponent<Animator>().SetBool("Rad heath vehicle, Attack joystick", false);
            AnimationUIVehicleHUD.GetComponent<Animator>().SetBool("Red, Vehicle HUD", false);
        }
    }

    //플레이어가 타격을 받았을 때의 데미지 적용
    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        while (true)
        {
            if (Ricochet != 0)
            {
                hitPoints = hitPoints - (damage/ Robotarmor);

                StartCoroutine(BeamAction());
                player.GetComponent<Player>().VehicleHealthDamage();
                TimeStemp += 0.055f;

                if (hitPoints <= float.Epsilon)
                {
                    if(DeathTime == 0)
                    {
                        DeathTime += Time.deltaTime;
                        DestroyRobot();
                    }
                    objectManager.SupplyList.Remove(gameObject);
                    break;
                }

                if (interval > float.Epsilon)
                {
                    yield return new WaitForSeconds(interval);
                }

                else
                {
                    break;
                }
            }
            else if (Ricochet == 0)
            {
                //Debug.Log("팅!");
                break;
            }
        }
    }

    public IEnumerator NearDamageCharacter(int damage, float interval)
    {
        while (true)
        {
            if (HitAction <= 0)
            {
                HitAction = interval;
                DamageTime = interval;
                hitPoints = hitPoints - (damage / Robotarmor);

                player.GetComponent<Player>().VehicleHealthDamage();
            }

            if (hitPoints <= float.Epsilon)
            {
                if (DeathTime == 0)
                {
                    DeathTime += Time.deltaTime;
                    DestroyRobot();
                }
                objectManager.SupplyList.Remove(gameObject);
                break;
            }

            if (interval > float.Epsilon)
            {
                yield return new WaitForSeconds(interval);
            }

            else
            {
                break;
            }
        }
    }

    //빔 공격 받았을 때의 시각효과 발생
    public IEnumerator BeamAction()
    {
        //Debug.Log(TimeStemp);
        if (BeamDamageAction == 1) //슬로리어스 쇼크 웨이브 타격 이펙트
        {
            while (TimeStemp > 0)
            {
                GameObject DamageBeam1 = Instantiate(ShockWaveTaken, BeamTakenPos.transform.position, BeamTakenPos.transform.rotation);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    void DestroyRobot()
    {
        if (Player.hitPoints != 0) //플레이어가 타지 않은 상태로 간주
        {
            this.gameObject.layer = 0;
            Player.GetComponent<Movement>().CallBackComplete = false;
            VehicleRecall.GetComponent<Animator>().SetBool("Turn off, Vehicle recall", true);
            VehicleCall.GetComponent<Animator>().SetBool("Turn off, Vehicle call", false);
            VehicleRecallActive.raycastTarget = false;
            VehicleCallActive.raycastTarget = true;
            if (GetComponent<Animator>().GetBool("Dash, MBCA-79") == true)
                GetComponent<Animator>().SetBool("Dash, MBCA-79", false);
            if (GetComponent<Animator>().GetBool("Move, MBCA-79") == true)
                GetComponent<Animator>().SetBool("Move, MBCA-79", false);
            GetComponent<Animator>().SetBool("Take wait, MBCA-79", false);
        }
        GameObject Body1 = Instantiate(Body, BodyPos.transform.position, BodyPos.transform.rotation);
        GameObject RightLeg1 = Instantiate(RightLeg, RightLegPos.transform.position, RightLegPos.transform.rotation);
        GameObject LeftLeg1 = Instantiate(LeftLeg, LeftLegPos.transform.position, LeftLegPos.transform.rotation);
        GameObject RightArm1 = Instantiate(RightArm, RightArmPos.transform.position, RightArmPos.transform.rotation);
        GameObject LeftArm1 = Instantiate(LeftArm, LeftArmPos.transform.position, LeftArmPos.transform.rotation);
        GameObject Bump1 = Instantiate(Bump, BumpPos.transform.position, BumpPos.transform.rotation);
        GameObject moduleWeapon1 = Instantiate(ModuleWeapon1, ModuleWeapon1Pos.transform.position, ModuleWeapon1Pos.transform.rotation);
        GameObject moduleWeapon2 = Instantiate(ModuleWeapon2, ModuleWeapon2Pos.transform.position, ModuleWeapon2Pos.transform.rotation);
        GameObject moduleWeapon3 = Instantiate(ModuleWeapon3, ModuleWeapon3Pos.transform.position, ModuleWeapon3Pos.transform.rotation);
        GameObject moduleWeapon4 = Instantiate(ModuleWeapon4, ModuleWeapon4Pos.transform.position, ModuleWeapon4Pos.transform.rotation);
        GameObject moduleWeapon4_1 = Instantiate(ModuleWeapon4_1, ModuleWeapon4_1Pos.transform.position, ModuleWeapon4_1Pos.transform.rotation);
        DeathTime = 0;
        gameObject.SetActive(false);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "item")
    //    {
    //        Debug.Log("아이템을 먹었습니다.");
    //        Destroy(gameObject);         
    //    }
    //}
}