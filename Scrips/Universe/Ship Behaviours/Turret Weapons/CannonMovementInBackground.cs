using UnityEngine;

public class CannonMovementInBackground : MonoBehaviour
{
    public int TargetType;
    public float AmmoSpeed; //함포 탄알 속도
    private string ExplosionName;
    GameObject Explosion; //탄알이 폭발할 때의 이펙트. 이 이펙트가 생성한 지점의 함선과 충돌하면 데미지를 전달한다
    private int AmmoDamage;
    public GameObject TargetLocation; //목표 대상

    private float Temp;

    private void OnEnable()
    {
        Invoke("TurnOff", 1);
    }

    private void OnDisable()
    {
        Temp = 0;
        AmmoDamage = 0;
        ExplosionName = null;
        TargetLocation = null;
    }

    void TurnOff()
    {
        this.gameObject.SetActive(false);
    }

    public void SetDamage(int Damage, GameObject Target, string Name)
    {
        AmmoDamage = Damage;
        TargetLocation = Target;
        ExplosionName = Name;
    }

    void Update()
    {
        if (TargetLocation != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetLocation.transform.position, AmmoSpeed * Time.deltaTime);
            Vector3 MoveDir = (TargetLocation.transform.position - transform.position).normalized;
            transform.right = Vector3.Lerp(transform.right, MoveDir, 100 * Time.deltaTime);
            if (Vector3.Distance(TargetLocation.transform.position, transform.position) <= 0.01f) //목적지에 도달할 경우, 데미지 전달 오브젝트 생성
            {
                if (Temp == 0)
                {
                    Temp += Time.deltaTime;
                    Explosion = ShipAmmoObjectPoolInBackground.instance.Loader(ExplosionName);
                    Explosion.transform.position = transform.position;
                    Explosion.transform.rotation = transform.rotation;
                    Explosion.GetComponent<CannonExplosionInBackground>().Damage = AmmoDamage;
                    this.gameObject.SetActive(false);
                }
            }
        }
        else
            this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TargetType == 6 && collision.gameObject.layer == 19) //컨트로스 포탄
        {
            Explosion = ShipAmmoObjectPoolInBackground.instance.Loader(ExplosionName);
            Explosion.transform.position = transform.position;
            Explosion.transform.rotation = transform.rotation;
            Explosion.GetComponent<CannonExplosionInBackground>().Damage = AmmoDamage;
            this.gameObject.SetActive(false);
        }
        if (TargetType == 7 && collision.gameObject.layer == 20) //나리하 포탄
        {
            Explosion = ShipAmmoObjectPoolInBackground.instance.Loader(ExplosionName);
            Explosion.transform.position = transform.position;
            Explosion.transform.rotation = transform.rotation;
            Explosion.GetComponent<CannonExplosionInBackground>().Damage = AmmoDamage;
            this.gameObject.SetActive(false);
        }
    }
}