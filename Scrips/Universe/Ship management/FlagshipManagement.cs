using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagshipManagement : MonoBehaviour
{
    ////NOT USED////
    public GameObject Flagship;
    public int FlagshipNumber = 2; //���� ��ȣ. ù ������ ������ 1������ �����ϹǷ� ù ��ȣ�� 1������ ����

    public void AddFlagship()
    {
        FlagshipNumber++;
        //Flagship.GetComponent<WarpFormationControl>().CreateWarpLocation(FlagshipNumber);
    }
}