using UnityEngine;

public class FighterObjectPool : MonoBehaviour
{
    ////NOT USED////
    public bool isNariha;
    public int ControsType;

    [Header("나리하 인류연합 함재기")]
    public GameObject NarihaFighter1Prefab;
    public GameObject NarihaBomer1Prefab;
    public GameObject NarihaBomer1Artillery1Prefab;
    GameObject[] NarihaFighter1;
    GameObject[] NarihaBomer1;
    GameObject[] NarihaBomer1Artillery1;

    GameObject[] PoolMaker;

    //우주모함이 생성될 때 해당 함재기 프리팹 생성
    public void RemoteGenerateNarihaFighters(bool Fighter, bool Bomer, int Fighters, int Bomers)
    {
        if (Fighter == true)
        {
            NarihaFighter1 = new GameObject[Fighters];
        }
        if (Bomer == true)
        {
            NarihaBomer1 = new GameObject[Bomers];
            NarihaBomer1Artillery1 = new GameObject[Bomers * 2];
        }

        NarihaFighterGenerate(Fighter, Bomer);
    }

    void NarihaFighterGenerate(bool Fighter, bool Bomer)
    {
        if (Fighter == true)
        {
            for (int index = 0; index < NarihaFighter1.Length; index++)
            {
                NarihaFighter1[index] = Instantiate(NarihaFighter1Prefab);
                NarihaFighter1[index].SetActive(false);
            }
        }
        if (Bomer == true)
        {
            for (int index = 0; index < NarihaBomer1.Length; index++)
            {
                NarihaBomer1[index] = Instantiate(NarihaBomer1Prefab);
                NarihaBomer1[index].SetActive(false);
            }
            for (int index = 0; index < NarihaBomer1Artillery1.Length; index++)
            {
                NarihaBomer1Artillery1[index] = Instantiate(NarihaBomer1Artillery1Prefab);
                NarihaBomer1Artillery1[index].SetActive(false);
            }
        }
    }

    public GameObject Loader(string type)
    {
        switch (type)
        {            
            case "NarihaFighter1":
                PoolMaker = NarihaFighter1;
                break;
            case "NarihaBomer1":
                PoolMaker = NarihaBomer1;
                break;
            case "NarihaBomer1Artillery1":
                PoolMaker = NarihaBomer1Artillery1;
                break;
        }

        for (int index = 0; index < PoolMaker.Length; index++)
        {
            if (!PoolMaker[index].activeSelf)
            {
                PoolMaker[index].SetActive(true);
                return PoolMaker[index];
            }
        }

        return null;
    }
}