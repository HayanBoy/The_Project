using UnityEngine;

public class StartTypeTaitroki : MonoBehaviour
{
    public int Type; //���� ����

    private void OnEnable()
    {
        Invoke("UpdateType", 0.5f);
    }

    private void OnDisable()
    {
        Type = 0;
    }

    void UpdateType()
    {
        if (Type == 1) //���̵� ���̴� Ÿ��Ʈ��Ű
        {
            GetComponent<HealthInfector>().BladeTaitroki();
            GetComponent<TearInfector>().Type = true;
        }
    }
}