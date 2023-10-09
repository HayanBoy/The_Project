using System.Collections.Generic;
using UnityEngine;

public class AIShipManager : MonoBehaviour
{
    public static AIShipManager instance;

    public List<GameObject> NarihaFlagShipList = new List<GameObject>(); //나리하 인류연합 기함 목록
    public List<GameObject> EnemiesFlagShipList = new List<GameObject>(); //적 모든 기함 목록
    public List<GameObject> EnemiesFormationShipList = new List<GameObject>(); //적 모든 단독 편대함 목록
    public List<GameObject> SloriusFlagShipList = new List<GameObject>(); //슬로리어스 기함 목록
    public List<GameObject> KantakriFlagShipList = new List<GameObject>(); //칸타크리 기함 목록

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
}