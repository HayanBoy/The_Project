using System.Collections;
using UnityEngine;

public class HeavyWeaponSupport : MonoBehaviour
{
    ArthesL775Controller arthesL775Controller;
    Hydra56Controller hydra56Controller;
    MEAGController meagController;
    UGG98Controller ugg98Controller;
    VM5GrenadeController vm5GrenadeController;
    Movement movement;
    GunController gunController;

    public GameObject AnimationUIWeaponDrop;

    public GameObject ShipBoxPrefab;
    public Transform ShipBoxPos;
    private BoxCollider2D area;
    public GameObject SitePrefab; //���޹ڽ� ���� ����Ʈ ������
    GameObject Site;

    public int AmmoAmount;
    public int WeaponDropItemCnt;
    public float WeaponDropItemCool; //�������� ��Ÿ�� 
    public int TimeStart; //��ȭ�⸦ �� ����ϱ� ������ ��Ÿ���� ���� �ʵ��� ����
    private float WeaponDropItemTime;

    public int HeavyWeaponType; //��ȭ�� Ÿ��
    public bool isWeaponItem;
    public bool Reload;
    public bool VehicleActive; //������ ž������ �� �̵� �� �ൿ ������ ����ġ
    private bool Click;

    public AudioClip Beep1;
    public AudioClip Beep2;

    public void ReloadTime(bool Reloading)
    {
        if (Reloading == true)
        {
            Reload = true;
        }
        else
        {
            Reload = false;
        }
    }

    public void WeaponDropItemClick()
    {
        if (WeaponDropItemCnt > 0)
            isWeaponItem = true;
    }

    public void WeaponDropItemUp()
    {
        if (Click == true)
            AnimationUIWeaponDrop.GetComponent<Animator>().SetBool("Click, Weapon drop", false);
        Click = false;
    }

    public void WeaponDropItemDown()
    {
        Click = true;
        SoundManager.instance.SFXPlay2("Sound", Beep1);
        SoundManager.instance.SFXPlay2("Sound", Beep2);
        AnimationUIWeaponDrop.GetComponent<Animator>().SetBool("Click, Weapon drop", true);
    }

    public void WeaponDropItemEnter()
    {
        if (Click == true)
        {
            SoundManager.instance.SFXPlay2("Sound", Beep1);
            SoundManager.instance.SFXPlay2("Sound", Beep2);
            AnimationUIWeaponDrop.GetComponent<Animator>().SetBool("Click, Weapon drop", true);
        }
    }

    public void WeaponDropItemExit()
    {
        if (Click == true)
            AnimationUIWeaponDrop.GetComponent<Animator>().SetBool("Click, Weapon drop", false);
    }

    void Start()
    {
        arthesL775Controller = FindObjectOfType<ArthesL775Controller>();
        hydra56Controller = FindObjectOfType<Hydra56Controller>();
        meagController = FindObjectOfType<MEAGController>();
        ugg98Controller = FindObjectOfType<UGG98Controller>();
        vm5GrenadeController = FindObjectOfType<VM5GrenadeController>();
        movement = FindObjectOfType<Movement>();
        gunController = FindObjectOfType<GunController>();
        area = GetComponent<BoxCollider2D>();

        TimeStart = 1;
    }

    void Update()
    {
        if (TimeStart == 1)
            if (VehicleActive == false)
                WeaponDropItem_Cool(); //�������� ������ ��

        if (isWeaponItem || Input.GetKeyDown(KeyCode.M))
        {
            if (WeaponDropItemCnt > 0)
            {
                WeaponDropItemCnt--;
                isWeaponItem = false;
                AnimationUIWeaponDrop.GetComponent<Animator>().SetBool("Using, Weapon drop", true);
                TimeStart = 0;
                Instantiate(ShipBoxPrefab, ShipBoxPos.position, ShipBoxPos.rotation); //�Լ� ���� ���� ����
                SpawnSite();
            }
        }
    }

    //���� ��ǥ
    void SpawnSite()
    {
        Vector3 basePosition = transform.position;
        Vector3 size = area.size;

        float posX = basePosition.x + Random.Range(-size.x / 2f, size.x / 2f);
        float posY = basePosition.y + Random.Range(-size.y / 2f, size.y / 2f);
        float posZ = basePosition.z + Random.Range(-size.z / 2f, size.z / 2f);

        Vector3 spawnPos = new Vector3(posX, posY, posZ);

        Site = Instantiate(SitePrefab, spawnPos, Quaternion.Euler(0f, 0f, 0f));
        if (DeltaHrricaneData.instance.SelectedHeavyWeaponNumber == 5000)
            Site.GetComponent<RandomSitePlayer>().DropType = 5000; //M3078 �̴ϰ� ��û
        else if (DeltaHrricaneData.instance.SelectedHeavyWeaponNumber == 5001)
            Site.GetComponent<RandomSitePlayer>().DropType = 5001; //ASC 365 ȭ������ ��û
    }

    void WeaponDropItem_Cool() //�������� ��Ÿ�� �Լ� 
    {
        if (WeaponDropItemCnt == 0)
        {
            AnimationUIWeaponDrop.GetComponent<Animator>().SetBool("Cool time start, Weapon drop", true);
            AnimationUIWeaponDrop.GetComponent<Animator>().SetBool("Cool time running, Weapon drop", true);
            AnimationUIWeaponDrop.GetComponent<Animator>().SetFloat("Cool time, Weapon drop", 1 / WeaponDropItemCool);
            WeaponDropItemTime += Time.deltaTime;
        }

        if (WeaponDropItemTime > WeaponDropItemCool)
        {
            WeaponDropItemCnt++;
            WeaponDropItemTime = 0;
            AnimationUIWeaponDrop.GetComponent<Animator>().SetBool("Cool time end, Weapon drop", true);
            AnimationUIWeaponDrop.GetComponent<Animator>().SetBool("Cool time cycle count, Weapon drop", true);
            Invoke("AfterEndCycleWeapon", 0.5f);
            Invoke("ViewCountCompleteWeapon", 0.5f);
            AnimationUIWeaponDrop.GetComponent<Animator>().SetBool("Using, Weapon drop", false);
            AnimationUIWeaponDrop.GetComponent<Animator>().SetBool("Cool time start, Weapon drop", false);
            AnimationUIWeaponDrop.GetComponent<Animator>().SetBool("Cool time running, Weapon drop", false);
            AnimationUIWeaponDrop.GetComponent<Animator>().SetFloat("Cool time, Weapon drop", 0);
        }
    }

    void AfterEndCycleWeapon()
    {
        AnimationUIWeaponDrop.GetComponent<Animator>().SetBool("Cool time end, Weapon drop", false);
    }

    void ViewCountCompleteWeapon()
    {
        AnimationUIWeaponDrop.GetComponent<Animator>().SetBool("Cool time cycle count, Weapon drop", false);
    }

    //���� ������ �Ա�
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (Reload == false)
        {
            if (collision.gameObject.layer == 29)
            {
                if (collision.gameObject.tag == "M3078 item")
                {
                    if (arthesL775Controller.UsingTask == false && hydra56Controller.UsingTask == false && meagController.UsingTask == false && ugg98Controller.UsingTask == false 
                        && movement.UsingTask == false && gunController.UsingTask == false && vm5GrenadeController.UsingTask == false)
                    {
                        collision.GetComponent<DestroyItem>().Destroy();
                        this.GetComponent<M3078Controller>().HeavyWeaponType = 1;
                        this.GetComponent<M3078Controller>().GetHeavyWeapon = 1;
                        this.GetComponent<M3078Controller>().RealAmmoAmount = DeltaHrricaneData.instance.M3078AmmoAmount; //ó���� ������ �̴ϰ� źâ�� ����������, �̴ϰ� �� ���¿��� ������ �ִ� źâ���� �߰� 
                        this.GetComponent<M3078Controller>().ViewSW06_Magazine = DeltaHrricaneData.instance.M3078AmmoAmount;
                        this.GetComponent<M3078Controller>().UsingTime = 1;
                    }
                }
                else if (collision.gameObject.tag == "ASC 365 item")
                {
                    if (arthesL775Controller.UsingTask == false && hydra56Controller.UsingTask == false && meagController.UsingTask == false && ugg98Controller.UsingTask == false
                        && movement.UsingTask == false && gunController.UsingTask == false && vm5GrenadeController.UsingTask == false)
                    {
                        collision.GetComponent<DestroyItem>().Destroy();
                        this.GetComponent<M3078Controller>().HeavyWeaponType = 2;
                        this.GetComponent<M3078Controller>().GetHeavyWeapon = 2;
                        this.GetComponent<M3078Controller>().RealAmmoAmount = DeltaHrricaneData.instance.ASC365AmmoAmount;
                        this.GetComponent<M3078Controller>().ViewSW06_Magazine = DeltaHrricaneData.instance.ASC365AmmoAmount;
                        this.GetComponent<M3078Controller>().UsingTime = 1;
                    }
                }
            }
        }
    }
}
