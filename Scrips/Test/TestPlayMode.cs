using UnityEngine.UI;
using UnityEngine;

public class TestPlayMode : MonoBehaviour
{
    public Text GroundBattleCount;

    private void Update()
    {
        GroundBattleCount.text = string.Format("{0}", BattleSave.Save1.GroundBattleCount);
    }

    //슬로리어스 기함 소환
    public void SloriusFlagshipMake()
    {
        ShipManager.instance.SelectedFlagShip[0].GetComponent<EnemyGet>().FlagshipWarp = true;
        ShipManager.instance.SelectedFlagShip[0].GetComponent<EnemyGet>().WarpControsType = 1;
        ShipManager.instance.SelectedFlagShip[0].GetComponent<EnemyGet>().enabled = true;
        Invoke("TurnOffMakeShip", 0.1f);
    }

    //칸타크리 기함 소환
    public void KantakriFlagshipMake()
    {
        ShipManager.instance.SelectedFlagShip[0].GetComponent<EnemyGet>().FlagshipWarp = true;
        ShipManager.instance.SelectedFlagShip[0].GetComponent<EnemyGet>().WarpControsType = 2;
        ShipManager.instance.SelectedFlagShip[0].GetComponent<EnemyGet>().enabled = true;
        Invoke("TurnOffMakeShip", 0.1f);
    }

    //슬로리어스 편대함 소환
    public void SloriusFormationShipMake()
    {
        ShipManager.instance.SelectedFlagShip[0].GetComponent<EnemyGet>().FlagshipWarp = false;
        ShipManager.instance.SelectedFlagShip[0].GetComponent<EnemyGet>().WarpControsType = 1;
        ShipManager.instance.SelectedFlagShip[0].GetComponent<EnemyGet>().enabled = true;
        Invoke("TurnOffMakeShip", 0.1f);
    }

    //칸타크리 편대함 소환
    public void KantakriFormationShipMake()
    {
        ShipManager.instance.SelectedFlagShip[0].GetComponent<EnemyGet>().FlagshipWarp = false;
        ShipManager.instance.SelectedFlagShip[0].GetComponent<EnemyGet>().WarpControsType = 2;
        ShipManager.instance.SelectedFlagShip[0].GetComponent<EnemyGet>().enabled = true;
        Invoke("TurnOffMakeShip", 0.1f);
    }

    public void TurnOffMakeShip()
    {
        ShipManager.instance.SelectedFlagShip[0].GetComponent<EnemyGet>().enabled = false;
    }

    //글로파오로스 추가
    public void GlopaUp()
    {
        BattleSave.Save1.NarihaUnionGlopaoros = BattleSave.Save1.NarihaUnionGlopaoros + 5000;
    }

    //건설자원 추가
    public void CRUp()
    {
        BattleSave.Save1.ConstructionResource = BattleSave.Save1.ConstructionResource + 5000;
    }

    //타리트로닉 추가
    public void TraritronicUp()
    {
        BattleSave.Save1.Taritronic = BattleSave.Save1.Taritronic + 5000;
    }
}