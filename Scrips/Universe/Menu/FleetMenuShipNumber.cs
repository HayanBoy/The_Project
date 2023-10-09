using System.Collections.Generic;
using UnityEngine;

public class FleetMenuShipNumber : MonoBehaviour
{
    [Header("함선 선택")]
    public bool isSelected; //선택되었는지에 대한 여부
    public int FollowShipNumber; //소속 함선 번호
    public List<GameObject> FollowShipList = new List<GameObject>(); //소속 함선 목록
    public GameObject SelectedImage;
    public GameObject DeselectedImage;

    [Header("함포 프리팹")]
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