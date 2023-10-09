using System.Collections.Generic;
using UnityEngine;

public class AIShipManager : MonoBehaviour
{
    public static AIShipManager instance;

    public List<GameObject> NarihaFlagShipList = new List<GameObject>(); //������ �η����� ���� ���
    public List<GameObject> EnemiesFlagShipList = new List<GameObject>(); //�� ��� ���� ���
    public List<GameObject> EnemiesFormationShipList = new List<GameObject>(); //�� ��� �ܵ� ����� ���
    public List<GameObject> SloriusFlagShipList = new List<GameObject>(); //���θ�� ���� ���
    public List<GameObject> KantakriFlagShipList = new List<GameObject>(); //ĭŸũ�� ���� ���

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
}