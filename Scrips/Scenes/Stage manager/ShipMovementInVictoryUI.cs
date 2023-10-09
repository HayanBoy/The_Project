using System.Collections;
using UnityEngine;

public class ShipMovementInVictoryUI : MonoBehaviour
{
    public RectTransform rectTransform;
    private float Speed;
    private bool isWarpStart;
    private bool isWarpEnd;
    private float NowSize;
    private float ShipSize;
    private float OneTime;

    void OnEnable()
    {
        Speed = 100;
        ShipSize = Random.Range(0.3f, 1);
        rectTransform.localScale = new Vector3(ShipSize, 0.05f, rectTransform.localScale.z);
        StartCoroutine(WarpStop());

        float XAixs = Random.Range(-2000, -1500);
        float YAixs = Random.Range(-500, 500);
        rectTransform.anchoredPosition = new Vector2(XAixs, YAixs);
    }

    IEnumerator WarpStop()
    {
        float time = Random.Range(0.15f, 0.25f);
        yield return new WaitForSeconds(time);
        isWarpEnd = true;
    }

    IEnumerator WarpStart()
    {
        float time = Random.Range(5, 10);
        yield return new WaitForSeconds(time);
        int WarpGo = Random.Range(0, 2);
        if (WarpGo == 0)
            isWarpStart = true;
    }

    void Update()
    {
        transform.Translate(transform.right * Speed * Time.deltaTime);

        if (isWarpStart == true)
        {
            if (OneTime == 0)
            {
                OneTime += Time.deltaTime;
                Speed = 100;
                NowSize = ShipSize;
                GetComponent<Animator>().SetFloat("Warp size, Formation ship Victory UI", 1);
            }
            if (NowSize >= 0.05f)
            {
                NowSize -= 3.5f * Time.deltaTime;
                rectTransform.localScale = new Vector3(rectTransform.localScale.x, NowSize, rectTransform.localScale.z);
            }
            else
                NowSize = 0.05f;
        }
        if (isWarpEnd == true)
        {
            if (OneTime == 0)
            {
                OneTime += Time.deltaTime;
                Speed = Random.Range(0.5f, 3);
                ShipSize = Random.Range(0.3f, 1);
                NowSize = 0.05f;
                GetComponent<Animator>().SetFloat("Warp size, Formation ship Victory UI", 2);
            }
            if (NowSize <= ShipSize)
            {
                NowSize += 3.5f * Time.deltaTime;
                rectTransform.localScale = new Vector3(ShipSize, NowSize, rectTransform.localScale.z);
            }
        }

        if (rectTransform.anchoredPosition.x >= 1500)
        {
            isWarpStart = false;
            isWarpEnd = false;
            OneTime = 0;
            NowSize = 0;
            GetComponent<Animator>().SetFloat("Warp size, Formation ship Victory UI", 0);

            int Warp = Random.Range(0, 2);
            if (Warp == 0)
            {
                float XAixs = Random.Range(-2000, -1500);
                float YAixs = Random.Range(-500, 500);
                rectTransform.anchoredPosition = new Vector2(XAixs, YAixs);

                Speed = 100;
                ShipSize = Random.Range(0.3f, 1);
                rectTransform.localScale = new Vector3(ShipSize, 0.05f, rectTransform.localScale.z);
                StartCoroutine(WarpStop());
            }
            else
            {
                float YAixs = Random.Range(-500, 500);
                rectTransform.anchoredPosition = new Vector2(-1500, YAixs);

                Speed = Random.Range(0.5f, 3);
                ShipSize = Random.Range(0.3f, 1);
                rectTransform.localScale = new Vector3(ShipSize, ShipSize, rectTransform.localScale.z);
                StartCoroutine(WarpStart());
            }
        }
    }
}