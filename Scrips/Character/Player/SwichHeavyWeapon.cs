using UnityEngine;

public class SwichHeavyWeapon : MonoBehaviour
{
    Animator animator;
    private bool UsingChangeWeapon = true;
    private float SwichTime = 0.5f;

    public bool isSwapM3078 = false; //M3078 교체 버튼
    public GameObject WeaponSwapBtn;

    public void M3078Swap()
    {
        if(SwichTime < 0)
            isSwapM3078 = !isSwapM3078;
    }

    //X축 반전 원격 제어
    public void XMoveStop(bool ChangeWeaponUsing)
    {
        if (ChangeWeaponUsing == true)
        {
            UsingChangeWeapon = true;
        }
        else
        {
            UsingChangeWeapon = false;
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (UsingChangeWeapon == false)
            SwichTime -= Time.deltaTime;
    }
}
