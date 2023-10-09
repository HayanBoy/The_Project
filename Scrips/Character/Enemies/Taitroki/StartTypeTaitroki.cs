using UnityEngine;

public class StartTypeTaitroki : MonoBehaviour
{
    public int Type; //좀비 유형

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
        if (Type == 1) //블레이드 하이더 타이트로키
        {
            GetComponent<HealthInfector>().BladeTaitroki();
            GetComponent<TearInfector>().Type = true;
        }
    }
}