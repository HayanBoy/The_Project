using UnityEngine.UI;
using UnityEngine;

public class TestPlayMode : MonoBehaviour
{
    public Text GroundBattleCount;

    private void Update()
    {
        GroundBattleCount.text = string.Format("{0}", BattleSave.Save1.GroundBattleCount);
    }

    //���θ�� ���� ��ȯ
    public void SloriusFlagshipMake()
    {
        ShipManager.instance.SelectedFlagShip[0].GetComponent<EnemyGet>().FlagshipWarp = true;
        ShipManager.instance.SelectedFlagShip[0].GetComponent<EnemyGet>().WarpControsType = 1;
        ShipManager.instance.SelectedFlagShip[0].GetComponent<EnemyGet>().enabled = true;
        Invoke("TurnOffMakeShip", 0.1f);
    }

    //ĭŸũ�� ���� ��ȯ
    public void KantakriFlagshipMake()
    {
        ShipManager.instance.SelectedFlagShip[0].GetComponent<EnemyGet>().FlagshipWarp = true;
        ShipManager.instance.SelectedFlagShip[0].GetComponent<EnemyGet>().WarpControsType = 2;
        ShipManager.instance.SelectedFlagShip[0].GetComponent<EnemyGet>().enabled = true;
        Invoke("TurnOffMakeShip", 0.1f);
    }

    //���θ�� ����� ��ȯ
    public void SloriusFormationShipMake()
    {
        ShipManager.instance.SelectedFlagShip[0].GetComponent<EnemyGet>().FlagshipWarp = false;
        ShipManager.instance.SelectedFlagShip[0].GetComponent<EnemyGet>().WarpControsType = 1;
        ShipManager.instance.SelectedFlagShip[0].GetComponent<EnemyGet>().enabled = true;
        Invoke("TurnOffMakeShip", 0.1f);
    }

    //ĭŸũ�� ����� ��ȯ
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

    //�۷��Ŀ��ν� �߰�
    public void GlopaUp()
    {
        BattleSave.Save1.NarihaUnionGlopaoros = BattleSave.Save1.NarihaUnionGlopaoros + 5000;
    }

    //�Ǽ��ڿ� �߰�
    public void CRUp()
    {
        BattleSave.Save1.ConstructionResource = BattleSave.Save1.ConstructionResource + 5000;
    }

    //Ÿ��Ʈ�δ� �߰�
    public void TraritronicUp()
    {
        BattleSave.Save1.Taritronic = BattleSave.Save1.Taritronic + 5000;
    }
}