using UnityEngine;

public class CannonMovementInBackground : MonoBehaviour
{
    public int TargetType;
    public float AmmoSpeed; //���� ź�� �ӵ�
    private string ExplosionName;
    GameObject Explosion; //ź���� ������ ���� ����Ʈ. �� ����Ʈ�� ������ ������ �Լ��� �浹�ϸ� �������� �����Ѵ�
    private int AmmoDamage;
    public GameObject TargetLocation; //��ǥ ���

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
            if (Vector3.Distance(TargetLocation.transform.position, transform.position) <= 0.01f) //�������� ������ ���, ������ ���� ������Ʈ ����
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
        if (TargetType == 6 && collision.gameObject.layer == 19) //��Ʈ�ν� ��ź
        {
            Explosion = ShipAmmoObjectPoolInBackground.instance.Loader(ExplosionName);
            Explosion.transform.position = transform.position;
            Explosion.transform.rotation = transform.rotation;
            Explosion.GetComponent<CannonExplosionInBackground>().Damage = AmmoDamage;
            this.gameObject.SetActive(false);
        }
        if (TargetType == 7 && collision.gameObject.layer == 20) //������ ��ź
        {
            Explosion = ShipAmmoObjectPoolInBackground.instance.Loader(ExplosionName);
            Explosion.transform.position = transform.position;
            Explosion.transform.rotation = transform.rotation;
            Explosion.GetComponent<CannonExplosionInBackground>().Damage = AmmoDamage;
            this.gameObject.SetActive(false);
        }
    }
}