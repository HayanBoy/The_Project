using UnityEngine;

public class HA767DropItem : MonoBehaviour
{
    Animator animator;

    public int EatItem;
    public int MissionOption;

    GameObject item;
    public GameObject itemPrefab;
    public Transform ItemPos;

    GameObject M3078;
    public GameObject M3078Prefab;

    public AudioClip AirDrop;
    public AudioClip BoostOn;

    public void MissionType(int num)
    {
        MissionOption = num;
    }

    public void ItemEat(int num)
    {
        EatItem = num;
    }

    void Start()
    {
        animator = GetComponent<Animator>();

        if(MissionOption == 1)
        {
            SoundManager.instance.SFXPlay29("Sound", AirDrop);
            animator.SetBool("Drop item, HA-767", true);
            Invoke("DroppingAmmo", 2f);
            Invoke("Boost", 3.5f);
        }
        else if (MissionOption == 100)
        {
            SoundManager.instance.SFXPlay29("Sound", AirDrop);
            animator.SetBool("Drop item, HA-767", true);
            Invoke("DroppingM3078", 2f);
            Invoke("Boost", 3.5f);
        }
    }

    void DroppingAmmo()
    {
        item = Instantiate(itemPrefab, ItemPos.position, ItemPos.rotation);
    }

    void DroppingM3078()
    {
        M3078 = Instantiate(M3078Prefab, ItemPos.position, ItemPos.rotation);
    }

    void Boost()
    {
        SoundManager.instance.SFXPlay24("Sound", BoostOn);
    }

    void Update()
    {
        if (EatItem == 1)
            Destroy(item);
        else if (EatItem == 100)
        {
            Destroy(M3078);
        }
    }
}
