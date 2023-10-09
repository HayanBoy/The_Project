using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagshipManagement : MonoBehaviour
{
    ////NOT USED////
    public GameObject Flagship;
    public int FlagshipNumber = 2; //기함 번호. 첫 시작은 기함이 1개부터 시작하므로 첫 번호가 1번부터 시작

    public void AddFlagship()
    {
        FlagshipNumber++;
        //Flagship.GetComponent<WarpFormationControl>().CreateWarpLocation(FlagshipNumber);
    }
}