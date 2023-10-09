using UnityEngine;

public class BeltScrollGunUIStart : MonoBehaviour
{
    [Header("�� ���� UI")]
    public GameObject DT47Prefab;
    public GameObject DS65Prefab;
    public GameObject DP9007Prefab;
    public GameObject CGD27Prefab;

    [Header("��ȭ�� UI")]
    public GameObject M3078Prefab;
    public GameObject ASC365Prefab;

    void Start()
    {
        //�� ����
        if (DeltaHrricaneData.instance.SelectedMainWeaponNumber == 1)
        {
            DT47Prefab.SetActive(true);
        }
        else if (DeltaHrricaneData.instance.SelectedMainWeaponNumber == 1000)
        {
            DS65Prefab.SetActive(true);
        }
        else if (DeltaHrricaneData.instance.SelectedMainWeaponNumber == 2000)
        {
            DP9007Prefab.SetActive(true);
        }
        else if (DeltaHrricaneData.instance.SelectedMainWeaponNumber == 0)
        {
            CGD27Prefab.SetActive(true);
        }

        //��ȭ��
        if (DeltaHrricaneData.instance.SelectedHeavyWeaponNumber == 5000)
        {
            M3078Prefab.SetActive(true);
        }
        else if (DeltaHrricaneData.instance.SelectedHeavyWeaponNumber == 5001)
        {
            ASC365Prefab.SetActive(true);
        }
    }
}