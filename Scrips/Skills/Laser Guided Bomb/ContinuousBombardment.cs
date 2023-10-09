using System.Collections;
using UnityEngine;

public class ContinuousBombardment : MonoBehaviour
{
    private float random; // 미사일 위치 랜덤좌표 생성
    public int MissileAmountPlus; //미사일 갯수 강화용
    public float MissileDropTimePlus; //미사일 드랍 시간 강화용
    public int damage;

    public GameObject Missile;
    public Transform MissilePos;

    void OnEnable()
    {
        StartCoroutine(DropMissle()); //레이저포인트 주위로 미사일 생성 후 폭격
        StartCoroutine(AnimationTime());
        Destroy(gameObject, MissileDropTimePlus * MissileAmountPlus + 5);
    }

    private void Update()
    {
        random = Random.Range(-4, 4); //미사일 위치 랜덤좌표 생성
    }

    IEnumerator AnimationTime()
    {
        GetComponent<Animator>().SetFloat("Start, Air strike", 1);
        yield return new WaitForSeconds(MissileDropTimePlus * MissileAmountPlus + 1.5f);
        GetComponent<Animator>().SetFloat("Start, Air strike", 2);
    }

    //레이저포인트 주위로 미사일 생성 후 폭격
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