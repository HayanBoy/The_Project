using UnityEngine;

public class RandomsiteFlagshipDetect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        WordPrintSystem WordPrintSystem;

        if (collision.gameObject.layer == 6 && collision.CompareTag("Player Flag Ship"))
        {
            this.gameObject.transform.parent.GetComponent<RandomSiteBattle>().isInFight = true;
            collision.GetComponent<FlagshipSystemNumber>().PlayerNumber = transform.parent.GetComponent<RandomSiteBattle>().BattleSiteNumber;
            collision.GetComponent<FlagshipSystemNumber>().SystemNowNumber = transform.parent.GetComponent<RandomSiteBattle>().MySystem;

            WordPrintSystem = FindObjectOfType<WordPrintSystem>();
            if (WordPrintSystem.LanguageType == 1)
            {
                if (collision.GetComponent<FlagshipSystemNumber>().SystemNowNumber == 1)
                    collision.GetComponent<FlagshipSystemNumber>().playerAreaName = "Toropio";
                else if (collision.GetComponent<FlagshipSystemNumber>().SystemNowNumber == 2)
                    collision.GetComponent<FlagshipSystemNumber>().playerAreaName = "Roro";
                else if (collision.GetComponent<FlagshipSystemNumber>().SystemNowNumber == 3)
                    collision.GetComponent<FlagshipSystemNumber>().playerAreaName = "Sarisi";
                else if (collision.GetComponent<FlagshipSystemNumber>().SystemNowNumber == 4)
                    collision.GetComponent<FlagshipSystemNumber>().playerAreaName = "Garix";
                else if (collision.GetComponent<FlagshipSystemNumber>().SystemNowNumber == 5)
                    collision.GetComponent<FlagshipSystemNumber>().playerAreaName = "OctoKrasis Patoro";
                else if (collision.GetComponent<FlagshipSystemNumber>().SystemNowNumber == 6)
                    collision.GetComponent<FlagshipSystemNumber>().playerAreaName = "Delta D31-402054";
                else if (collision.GetComponent<FlagshipSystemNumber>().SystemNowNumber == 7)
                    collision.GetComponent<FlagshipSystemNumber>().playerAreaName = "Jerato O95-99024";
            }
            if (WordPrintSystem.LanguageType == 2)
            {
                if (collision.GetComponent<FlagshipSystemNumber>().SystemNowNumber == 1)
                    collision.GetComponent<FlagshipSystemNumber>().playerAreaName = "����ǿ�";
                else if (collision.GetComponent<FlagshipSystemNumber>().SystemNowNumber == 2)
                    collision.GetComponent<FlagshipSystemNumber>().playerAreaName = "�η�";
                else if (collision.GetComponent<FlagshipSystemNumber>().SystemNowNumber == 3)
                    collision.GetComponent<FlagshipSystemNumber>().playerAreaName = "�縮��";
                else if (collision.GetComponent<FlagshipSystemNumber>().SystemNowNumber == 4)
                    collision.GetComponent<FlagshipSystemNumber>().playerAreaName = "������";
                else if (collision.GetComponent<FlagshipSystemNumber>().SystemNowNumber == 5)
                    collision.GetComponent<FlagshipSystemNumber>().playerAreaName = "����ũ��ý� �����";
                else if (collision.GetComponent<FlagshipSystemNumber>().SystemNowNumber == 6)
                    collision.GetComponent<FlagshipSystemNumber>().playerAreaName = "��Ÿ D31-402054";
                else if (collision.GetComponent<FlagshipSystemNumber>().SystemNowNumber == 7)
                    collision.GetComponent<FlagshipSystemNumber>().playerAreaName = "������ O95-99024";
            }
        }
    }
}