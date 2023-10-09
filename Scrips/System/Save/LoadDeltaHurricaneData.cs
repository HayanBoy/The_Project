using UnityEngine;

public class LoadDeltaHurricaneData : MonoBehaviour
{
    DeltaHrricaneData DeltaHrricaneData;
    GunController GunController;

    private void Awake()
    {
        DeltaHrricaneData = FindObjectOfType<DeltaHrricaneData>();
        GunController = FindObjectOfType<GunController>();

        GunController.GunType = DeltaHrricaneData.SelectedMainWeaponNumber;

        if (DeltaHrricaneData.SelectedMainWeaponNumber != 0)
        {
            GunController.SubGunTypeFront = 0;
            GunController.SubGunTypeBack = 0;
        }
        else
        {
            GunController.SubGunTypeFront = 1;
            GunController.SubGunTypeBack = 1;
        }
    }
}