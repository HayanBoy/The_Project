using UnityEngine;

public class SelecteShipRouteLine : MonoBehaviour
{
    public LineRenderer Router;
    public GameObject RouterPrefab;
    public GameObject EndPosition;

    private void OnEnable()
    {
        
    }

    public void OnDisable()
    {
        if (Router != null)
            Router.SetPosition(1, new Vector2(0, 0));
    }

    void Update()
    {
        if (EndPosition != null)
        {
            Vector3 dir = (EndPosition.transform.position - RouterPrefab.transform.position).normalized; // È¸Àü
            RouterPrefab.transform.up = Vector3.Lerp(RouterPrefab.transform.up, dir, 100 * Time.deltaTime);
            float AixsY  = Vector2.Distance(EndPosition.transform.position, transform.position);
            Router.SetPosition(1, new Vector2(0, AixsY));
        }
    }
}