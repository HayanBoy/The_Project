using UnityEngine;

public class RandomSiteManager : MonoBehaviour
{
    [Header("랜덤 사이트")]
    public GameObject BattleSite;
    public GameObject BattleSite1Prefab;
    public GameObject BattleSite2Prefab;
    public GameObject BattleSite3Prefab;
    public GameObject BattleSite4Prefab;
    public GameObject BattleSite5Prefab;
    public bool isFlagship; //기함이 해당 항성계에 존재하는지에 대한 스위치

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6 && collision.CompareTag("Player Flag Ship") && isFlagship == false)
        {
            isFlagship = true;
            BattleSite.SetActive(true);
            if (BattleSite1Prefab != null)
            {
                ShipManager.instance.ActiveBattleSiteList.Add(BattleSite1Prefab);
                if (BattleSave.Save1.GroundBattleCount == 0)
                    BattleSite1Prefab.GetComponent<RandomSiteBattle>().RandomAreaSpawnStart();
            }
            if (BattleSite2Prefab != null)
            {
                ShipManager.instance.ActiveBattleSiteList.Add(BattleSite2Prefab);
                if (BattleSave.Save1.GroundBattleCount == 0)
                    BattleSite2Prefab.GetComponent<RandomSiteBattle>().RandomAreaSpawnStart();
            }
            if (BattleSite3Prefab != null)
            {
                ShipManager.instance.ActiveBattleSiteList.Add(BattleSite3Prefab);
                if (BattleSave.Save1.GroundBattleCount == 0)
                    BattleSite3Prefab.GetComponent<RandomSiteBattle>().RandomAreaSpawnStart();
            }
            if (BattleSite4Prefab != null)
            {
                ShipManager.instance.ActiveBattleSiteList.Add(BattleSite4Prefab);
                if (BattleSave.Save1.GroundBattleCount == 0)
                    BattleSite4Prefab.GetComponent<RandomSiteBattle>().RandomAreaSpawnStart();
            }
            if (BattleSite5Prefab != null)
            {
                ShipManager.instance.ActiveBattleSiteList.Add(BattleSite5Prefab);
                if (BattleSave.Save1.GroundBattleCount == 0)
                    BattleSite5Prefab.GetComponent<RandomSiteBattle>().RandomAreaSpawnStart();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6 && collision.CompareTag("Player Flag Ship") && isFlagship == true)
        {
            isFlagship = false;
            BattleSite.SetActive(false);
            if (BattleSite1Prefab != null)
            {
                ShipManager.instance.ActiveBattleSiteList.Remove(BattleSite1Prefab);
            }
            if (BattleSite2Prefab != null)
            {
                ShipManager.instance.ActiveBattleSiteList.Remove(BattleSite2Prefab);
            }
            if (BattleSite3Prefab != null)
            {
                ShipManager.instance.ActiveBattleSiteList.Remove(BattleSite3Prefab);
            }
            if (BattleSite4Prefab != null)
            {
                ShipManager.instance.ActiveBattleSiteList.Remove(BattleSite4Prefab);
            }
            if (BattleSite5Prefab != null)
            {
                ShipManager.instance.ActiveBattleSiteList.Remove(BattleSite5Prefab);
            }
        }
    }
}