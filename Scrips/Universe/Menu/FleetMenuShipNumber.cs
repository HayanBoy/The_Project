using System.Collections.Generic;
using UnityEngine;

public class FleetMenuShipNumber : MonoBehaviour
{
    [Header("�Լ� ����")]
    public bool isSelected; //���õǾ������� ���� ����
    public int FollowShipNumber; //�Ҽ� �Լ� ��ȣ
    public List<GameObject> FollowShipList = new List<GameObject>(); //�Ҽ� �Լ� ���
    public GameObject SelectedImage;
    public GameObject DeselectedImage;

    [Header("���� ������")]
    public GameObject Turret1;
    public GameObject Turret2;
    public GameObject Turret3;
    public GameObject Turret4;
    public GameObject Turret5;
    public GameObject Turret6;

    public void SwitchSelect()
    {
        isSelected = !isSelected;
    }
}