using System.Collections;
using UnityEngine;

public class ContinuousBombardment : MonoBehaviour
{
    private float random; // �̻��� ��ġ ������ǥ ����
    public int MissileAmountPlus; //�̻��� ���� ��ȭ��
    public float MissileDropTimePlus; //�̻��� ��� �ð� ��ȭ��
    public int damage;

    public GameObject Missile;
    public Transform MissilePos;

    void OnEnable()
    {
        StartCoroutine(DropMissle()); //����������Ʈ ������ �̻��� ���� �� ����
        StartCoroutine(AnimationTime());
        Destroy(gameObject, MissileDropTimePlus * MissileAmountPlus + 5);
    }

    private void Update()
    {
        random = Random.Range(-4, 4); //�̻��� ��ġ ������ǥ ����
    }

    IEnumerator AnimationTime()
    {
        GetComponent<Animator>().SetFloat("Start, Air strike", 1);
        yield return new WaitForSeconds(MissileDropTimePlus * MissileAmountPlus + 1.5f);
        GetComponent<Animator>().SetFloat("Start, Air strike", 2);
    }

    //����������Ʈ ������ �̻��� ���� �� ����
    IEnumerator DropMissle()
    {
        for(int i = 0; i < MissileAmountPlus + 1; i++)
        {
            int VectorRandom = Random.Range(0, 2);

            if (VectorRandom == 0)
            {
                GameObject PGM1036ScaletHawkDamage = Instantiate(Missile, MissilePos.position + Vector3.right * random + Vector3.up * random, MissilePos.rotation);
                PGM1036ScaletHawkDamage.transform.Find("Laser Missile Bomb").GetComponent<MissileEffect>().damage = this.damage;
                yield return new WaitForSeconds(MissileDropTimePlus);
            }
            else
            {
                GameObject PGM1036ScaletHawkDamage = Instantiate(Missile, MissilePos.position + Vector3.left * random + Vector3.up * random, MissilePos.rotation);
                PGM1036ScaletHawkDamage.transform.Find("Laser Missile Bomb").GetComponent<MissileEffect>().damage = this.damage;
                yield return new WaitForSeconds(MissileDropTimePlus);
            }
        }
    }
}