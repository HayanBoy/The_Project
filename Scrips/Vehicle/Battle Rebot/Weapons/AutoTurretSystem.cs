using System.Collections;
using UnityEngine;

public class AutoTurretSystem : MonoBehaviour
{
    [SerializeField]
    public GameObject target = null;
    public GameObject targetUIPrefabKantakri;
    public GameObject targetUIPrefabSlorius;
    public GameObject targetUIPrefabInfector;
    public GameObject targetUIPrefabTaitroki;
    public GameObject AutoUI;
    public float range;
    public Vector2 direction;
    public Transform OnlineState;
    public Transform OfflineState;

    Animator animator;
    Rigidbody2D rb2D;

    Coroutine onlineFBWS;

    [SerializeField] LayerMask layerMask = 0; //어떤 레이어를 특정할 것인가

    public bool isAutoTurretOnline = false;
    private bool TurretOn = false;
    public bool NoAmmo = false;
    private bool TargetOnline = false;
    private bool StartBack = true;
    private bool Click;

    public float lookSpeed = 50;
    public float DelayTime;
    private float TargetMarkTime;
    public int AutoWeaponType; //이 시스템이 적을 향해서 사격을 전달하기 위한 무기별 변수

    ObjectManager objectManager;

    public AudioClip TurretActive;
    public AudioClip TurretInactive;
    public AudioClip Beep1;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        animator.SetBool("Offline state FBWS, MBCA-79", true);
    }

    private void Update()
    {
        if (transform.eulerAngles.y == 180)
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);

        if (target != null && target.activeSelf == true && NoAmmo == false) //타겟이 잡혔을 때, 탄약이 존재할 경우
        {
            direction = target.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * lookSpeed);
        }
        else if (target != null && target.activeSelf == true && NoAmmo == true) //타겟이 잡혔을 때, 탄약이 없을 경우
        {
            targetUIPrefabKantakri.transform.position = new Vector3(-500, -500, 0);
            targetUIPrefabSlorius.transform.position = new Vector3(-500, -500, 0);
            targetUIPrefabInfector.transform.position = new Vector3(-500, -500, 0);
            targetUIPrefabTaitroki.transform.position = new Vector3(-500, -500, 0);
        }

        if (TurretOn && target != null && target.gameObject.activeSelf == true && DelayTime >= 1 && NoAmmo == false)
        {
            if (AutoWeaponType == 1)
                GetComponent<FBWSFire>().FireStart = true;
        }
        else if (!TurretOn || target == null || target.gameObject.activeSelf == false)
        {
            if (AutoWeaponType == 1)
            {
                GetComponent<FBWSFire>().FireStart = false;
            }
        }
        UpdateTarget();

        if (TargetOnline == true && NoAmmo == false)
        {
            if (target.gameObject.CompareTag("Kantakri"))
                targetUIPrefabKantakri.transform.position = target.transform.position;
            else if (target.gameObject.CompareTag("Slorius"))
                targetUIPrefabSlorius.transform.position = target.transform.position;
            else if (target.gameObject.CompareTag("Infector"))
            {
                targetUIPrefabTaitroki.transform.position = new Vector3(-500, -500, 0);
                targetUIPrefabInfector.transform.position = target.transform.position;
            }
            else if (target.gameObject.CompareTag("Taitroki"))
            {
                targetUIPrefabInfector.transform.position = new Vector3(-500, -500, 0);
                targetUIPrefabTaitroki.transform.position = target.transform.position;
            }
        }
        else if (TargetOnline == false && NoAmmo == false)
        {
            targetUIPrefabKantakri.transform.position = new Vector3(-500, -500, 0);
            targetUIPrefabSlorius.transform.position = new Vector3(-500, -500, 0);
            targetUIPrefabInfector.transform.position = new Vector3(-500, -500, 0);
            targetUIPrefabTaitroki.transform.position = new Vector3(-500, -500, 0);
        }

        if (TurretOn == false && StartBack == true)
        {
            if (AutoWeaponType == 1)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, OfflineState.rotation, Time.deltaTime * lookSpeed);
                if (target == null && transform.rotation.z == OfflineState.rotation.z)
                    StartCoroutine(OfflineFBWS());
            }
        }

        if (NoAmmo == true)
        {
            if (AutoWeaponType == 1)
                target = null;
        }

        if (TurretOn == true && target == null)
        {
            if (AutoWeaponType == 1)
                transform.rotation = Quaternion.RotateTowards(transform.rotation, OnlineState.rotation, Time.deltaTime * lookSpeed);
        }
    }

    void DestoryAimKantakri()
    {
        targetUIPrefabKantakri.GetComponent<Animator>().SetFloat("Start, Auto aim", 0);
    }
    void DestoryAimSlorius()
    {
        targetUIPrefabSlorius.GetComponent<Animator>().SetFloat("Start, Auto aim", 0);
    }
    void DestoryAimInfector()
    {
        targetUIPrefabInfector.GetComponent<Animator>().SetFloat("Start, Auto aim", 0);
    }
    void DestoryAimTaitroki()
    {
        targetUIPrefabTaitroki.GetComponent<Animator>().SetFloat("Start, Auto aim", 0);
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            TargetOnline = false;
            return;
        }
    }

    public void AutoSystemUp()
    {
        if (Click == true)
        {
            if (isAutoTurretOnline == true)
                AutoUI.GetComponent<Animator>().SetBool("Click, FBWS", false);
            else
                AutoUI.GetComponent<Animator>().SetBool("ClickOnline, FBWS", false);
        }
        Click = false;
    }

    public void AutoSystemDown()
    {
        Click = true;
        if (isAutoTurretOnline == true)
            AutoUI.GetComponent<Animator>().SetBool("Click, FBWS", true);
        else
            AutoUI.GetComponent<Animator>().SetBool("ClickOnline, FBWS", true);
    }

    public void AutoSystemEnter()
    {
        if (Click == true)
        {
            if (isAutoTurretOnline == true)
                AutoUI.GetComponent<Animator>().SetBool("Click, FBWS", true);
            else
                AutoUI.GetComponent<Animator>().SetBool("ClickOnline, FBWS", true);
        }
    }

    public void AutoSystemExit()
    {
        if (Click == true)
        {
            if (isAutoTurretOnline == true)
                AutoUI.GetComponent<Animator>().SetBool("Click, FBWS", false);
            else
                AutoUI.GetComponent<Animator>().SetBool("ClickOnline, FBWS", false);
        }
    }

    public void AutoSystem()
    {
        if (isAutoTurretOnline)
        {
            //Debug.Log("Auto System On");
            if (AutoWeaponType == 1)
                onlineFBWS = StartCoroutine(OnlineFBWS());
            SoundManager.instance.SFXPlay(" Sound", TurretActive);
            AutoUI.GetComponent<Animator>().SetBool("Active, FBWS", true);
            StartBack = false;
        }

        if (!isAutoTurretOnline)
        {
            //Debug.Log("Auto System Off");
            SoundManager.instance.SFXPlay(" Sound", TurretInactive);
            AutoUI.GetComponent<Animator>().SetBool("Active, FBWS", false);
            StopCoroutine(onlineFBWS);
            TurretOn = false;
            StartBack = true;
            target = null;
        }

        isAutoTurretOnline = !isAutoTurretOnline;
    }

    void UpdateTarget()
    {
        if (NoAmmo == false)
        {
            if (TurretOn && target == null || TurretOn && target.gameObject.activeSelf == false) //터렛이 켜진 상태에서 타겟이 존재하지 않을 경우
            {
                Collider2D[] Monsters = Physics2D.OverlapCircleAll(transform.position, 100F, layerMask);
                float shortestDistance = Mathf.Infinity;
                Collider2D nearestMonster = null;

                DelayTime = 0;
                foreach (Collider2D Monster in Monsters)
                {
                    float DistanceToMonsters = Vector3.Distance(transform.position, Monster.transform.position);

                    if (DistanceToMonsters < shortestDistance) //가장 가까운 타겟으로 변경
                    {
                        targetUIPrefabKantakri.transform.position = new Vector3(-500, -500, 0);
                        targetUIPrefabSlorius.transform.position = new Vector3(-500, -500, 0);
                        targetUIPrefabInfector.transform.position = new Vector3(-500, -500, 0);
                        targetUIPrefabTaitroki.transform.position = new Vector3(-500, -500, 0);
                        shortestDistance = DistanceToMonsters;
                        nearestMonster = Monster;
                        TargetMarkTime = 0;
                    }
                }

                if (nearestMonster != null && shortestDistance <= range) //다음 가장 가까운 타겟으로 적을 변경했을 때, 해당 적의 목록을 리스트에 올리기
                {
                    target = nearestMonster.gameObject;
                    TargetOnline = true;

                    if (TargetMarkTime == 0)
                    {
                        TargetMarkTime += Time.deltaTime;
                        SoundManager.instance.SFXPlay11("Sound", Beep1);
                        if (target.gameObject.CompareTag("Kantakri"))
                        {
                            Invoke("DestoryAimKantakri", 0.5f);
                            targetUIPrefabKantakri.GetComponent<Animator>().SetFloat("Start, Auto aim", 1);
                        }
                        else if (target.gameObject.CompareTag("Slorius"))
                        {
                            Invoke("DestoryAimSlorius", 0.5f);
                            targetUIPrefabSlorius.GetComponent<Animator>().SetFloat("Start, Auto aim", 1);
                        }
                        else if (target.gameObject.CompareTag("Infector"))
                        {
                            Invoke("DestoryAimInfector", 0.5f);
                            targetUIPrefabInfector.GetComponent<Animator>().SetFloat("Start, Auto aim", 1);
                        }
                        else if (target.gameObject.CompareTag("Taitroki"))
                        {
                            Invoke("DestoryAimTaitroki", 0.5f);
                            targetUIPrefabTaitroki.GetComponent<Animator>().SetFloat("Start, Auto aim", 1);
                        }
                    }
                }

                else
                {
                    target = null;
                    TargetOnline = false;
                }
            }

            else
            {
                DelayTime += Time.deltaTime;
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
    }

    IEnumerator OnlineFBWS()
    {
        animator.SetBool("Offline2 FBWS, MBCA-79", false);
        animator.SetBool("Offline FBWS, MBCA-79", true);
        yield return new WaitForSeconds(0.16f);
        TurretOn = true;
    }

    IEnumerator OfflineFBWS()
    {
        animator.SetBool("Offline FBWS, MBCA-79", false);
        animator.SetBool("Offline2 FBWS, MBCA-79", true);
        yield return new WaitForSeconds(0.16f);
    }
}