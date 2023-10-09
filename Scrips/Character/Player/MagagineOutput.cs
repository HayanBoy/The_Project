using UnityEngine;

public class MagagineOutput : MonoBehaviour
{
    public int GunType;

    public GameObject DT37MagazinePrefab; //DT-37 źâ ������
    public Transform DT37MagazinePos; //DT-37 źâ ���� ��ǥ
    public GameObject DP9007MagazinePrefab; //DP-9007 źâ ������
    public Transform DP9007MagazinePos; //DP-9007 źâ ���� ��ǥ
    public GameObject CGD27MagazinePrefab; //CGD-27 źâ ������
    public Transform CGD27MagazinePos; //CGD-27 źâ ���� ��ǥ1
    public Transform CGD27MagazinePos2; //CGD-27 źâ ���� ��ǥ2

    private void OnEnable()
    {
        if (GunType == 1)
        {
            GameObject ejectedMagazine = Instantiate(DT37MagazinePrefab, DT37MagazinePos.position, Quaternion.identity);
            float xVnot = 0;
            float yVnot = -0.2f;

            ejectedMagazine.GetComponent<SW06MagazineFall>().xVnot = xVnot;
            ejectedMagazine.GetComponent<SW06MagazineFall>().yVnot = yVnot;

            Destroy(ejectedMagazine, 15.0f);  //�����ð���, ������ źâ ����, ź���� 1�����̶� ������Ʈ Ǯ�� x 
        }
        else if (GunType == 2000)
        {
            GameObject ejectedMagazine = Instantiate(DP9007MagazinePrefab, DP9007MagazinePos.position, Quaternion.identity);
            float xVnot = 0;
            float yVnot = -0.2f;

            ejectedMagazine.GetComponent<SW06MagazineFall>().xVnot = xVnot;
            ejectedMagazine.GetComponent<SW06MagazineFall>().yVnot = yVnot;

            Destroy(ejectedMagazine, 15.0f);
        }
        else if (GunType == 3000)
        {
            GameObject ejectedMagazine = Instantiate(CGD27MagazinePrefab, CGD27MagazinePos.position, Quaternion.identity);
            float xVnot = 0;
            float yVnot = -0.2f;

            ejectedMagazine.GetComponent<SW06MagazineFall>().xVnot = xVnot;
            ejectedMagazine.GetComponent<SW06MagazineFall>().yVnot = yVnot;

            Destroy(ejectedMagazine, 15.0f);
        }
        else if (GunType == 3001)
        {
            GameObject ejectedMagazine = Instantiate(CGD27MagazinePrefab, CGD27MagazinePos2.position, Quaternion.identity);
            float xVnot = 0;
            float yVnot = -0.2f;

            ejectedMagazine.GetComponent<SW06MagazineFall>().xVnot = xVnot;
            ejectedMagazine.GetComponent<SW06MagazineFall>().yVnot = yVnot;

            Destroy(ejectedMagazine, 15.0f);
        }
    }
}