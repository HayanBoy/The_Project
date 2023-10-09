using System.Collections;
using UnityEngine;

public class DebrisActionFlagship : MonoBehaviour
{
    public float DestroyTime;
    private float DebrisSpeed;
    private float DebrisSpeed2;
    private float DebrisRotationSpeed;
    private float DebrisRotationSpeed2;

    public bool Flagship;
    public bool isNariha;
    public int ControsNationNumber; //1 = 슬로리어스, 2 = 칸타크리
    public int Type; //기함 번호
    public int HowManyPart; //최대 몇 조각으로 분해되는가
    private bool DebrisRotation;
    private bool TakeDamage;
    private bool TakeExplosion;
    private Vector3 endposition;

    //하나만 분리될 때 사용되는 영역
    public GameObject SecondDivisionPrefab;
    public GameObject SecondPart;
    public Transform SecondPartPos;

    //3개 이상 분리될 수 있을 경우에 사용되는 영역
    public GameObject Part1;
    public GameObject Part1_1;
    public GameObject Part2;
    public GameObject Part2_1;
    public GameObject Part3;
    public GameObject Part3_1;

    public GameObject LED;
    public GameObject LED2;
    public GameObject LED3;
    public GameObject LED4;
    public GameObject LED5;
    public GameObject LED6;
    public GameObject LED7;
    public GameObject LED8;
    public GameObject LED9;
    public GameObject LED10;
    private float RandomLED;

    GameObject Explosion;

    //함선 부위 무력화 여부
    public bool Main1Left1Down = false;
    public bool Main1Left2Down = false;
    public bool Main1Right1Down = false;
    public bool Main1Right2Down = false;
    public bool Main2Left1Down = false;
    public bool Main2Left2Down = false;
    public bool Main2Right1Down = false;
    public bool Main2Right2Down = false;
    public bool Main3Left1Down = false;
    public bool Main3Right1Down = false;

    //함선별 파괴시 스프라이트 끄기용
    public GameObject Main1Left1prefab;
    public GameObject Main1Left2prefab;
    public GameObject Main1Right1prefab;
    public GameObject Main1Right2prefab;
    public GameObject Main2Left1prefab;
    public GameObject Main2Left2prefab;
    public GameObject Main2Right1prefab;
    public GameObject Main2Right2prefab;
    public GameObject Main3Left1prefab;
    public GameObject Main3Right1prefab;

    public bool Debris1;
    public bool Debris2;
    public bool Debris3;
    public bool Debris4;
    public bool Debris5;

    public bool FirstExplosion;
    public GameObject ShockWave;
    public Transform WaveTransform;
    public float ShockWaveStartTime;
    private float AddSpeed;
    private bool ShockWaveConflict;

    public void StartMoveDebris(float Speed, bool Rotation, bool Explosion)
    {
        DebrisSpeed *= Speed;
        DebrisRotation = Rotation;
        TakeExplosion = Explosion;
    }

    public void TurnOffPart()
    {
        if (Main1Left1Down == true && Main1Left1prefab != null)
            Main1Left1prefab.SetActive(false);
        if (Main1Left2Down == true && Main1Left2prefab != null)
            Main1Left2prefab.SetActive(false);
        if (Main1Right1Down == true && Main1Right1prefab != null)
            Main1Right1prefab.SetActive(false);
        if (Main1Right2Down == true && Main1Right2prefab != null)
            Main1Right2prefab.SetActive(false);
        if (Main2Left1Down == true && Main2Left1prefab != null)
            Main2Left1prefab.SetActive(false);
        if (Main2Left2Down == true && Main2Left2prefab != null)
            Main2Left2prefab.SetActive(false);
        if (Main2Right1Down == true && Main2Right1prefab != null)
            Main2Right1prefab.SetActive(false);
        if (Main2Right2Down == true && Main2Right2prefab != null)
            Main2Right2prefab.SetActive(false);
        if (Main3Left1Down == true && Main3Left1prefab != null)
            Main3Left1prefab.SetActive(false);
        if (Main3Right1Down == true && Main3Right1prefab != null)
            Main3Right1prefab.SetActive(false);
    }

    void Start()
    {
        DebrisSpeed2 = DebrisSpeed;
        DebrisRotationSpeed2 = DebrisRotationSpeed;

        if (FirstExplosion == true)
            Invoke("FlyAway", ShockWaveStartTime);

        DebrisSpeed = Random.Range(0.15f, 0.35f);
        DebrisRotationSpeed = Random.Range(-10f, 10f);
        RandomLED = Random.Range(0.02f, 0.06f);

        float RandomMovement = Random.Range(100, 900);
        if (DebrisRotation == true)
            endposition = new Vector3(transform.position.x + RandomMovement, Random.Range(transform.position.y - RandomMovement, transform.position.y + RandomMovement), transform.position.z);
        else
            endposition = new Vector3(transform.position.x - RandomMovement, Random.Range(transform.position.y - RandomMovement, transform.position.y + RandomMovement), transform.position.z);

        if (SecondPart != null)
            StartCoroutine(SecondDivision());

        if (LED != null)
            StartCoroutine(LEDDown1());
        if (LED2 != null)
            StartCoroutine(LEDDown2());
        if (LED3 != null)
            StartCoroutine(LEDDown3());
        if (LED4 != null)
            StartCoroutine(LEDDown4());
        if (LED5 != null)
            StartCoroutine(LEDDown5());
        if (LED6 != null)
            StartCoroutine(LEDDown6());
        if (LED7 != null)
            StartCoroutine(LEDDown7());
        if (LED8 != null)
            StartCoroutine(LEDDown8());
        if (LED9 != null)
            StartCoroutine(LEDDown9());
        if (LED10 != null)
            StartCoroutine(LEDDown10());

        Destroy(gameObject, DestroyTime);
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, DebrisRotationSpeed * Time.deltaTime));

        if (TakeDamage == false)
            transform.position = Vector3.MoveTowards(transform.position, endposition, DebrisSpeed * Time.deltaTime);

        if (ShockWaveConflict == true)
        {
            if (DebrisSpeed <= DebrisSpeed2 + AddSpeed)
                DebrisSpeed += 0.1f;
            if (DebrisRotationSpeed <= DebrisRotationSpeed2 + AddSpeed)
                DebrisRotationSpeed += 0.1f;
        }
    }

    IEnumerator SecondDivision()
    {
        int RandomDivision = Random.Range(1, 4);
        int StartDivide = Random.Range(0, 2);
        yield return new WaitForSeconds(RandomDivision);

        if (StartDivide == 0)
        {
            if (isNariha == false) //컨트로스 기함
            {
                SecondDivisionPrefab.SetActive(false);
                GameObject Main2Right2Destroy = Instantiate(SecondPart, SecondPartPos.position, SecondPartPos.rotation);
                Main2Right2Destroy.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                Main2Right2Destroy.GetComponent<DebrisActionFlagship>().LED.SetActive(false);
                if (Flagship == true)
                {
                    if (Debris1 == true)
                    {
                        Main2Right2Destroy.GetComponent<DebrisActionFlagship>().Main1Left1Down = Main1Left1Down;
                        Main2Right2Destroy.GetComponent<DebrisActionFlagship>().Main1Left2Down = Main1Left2Down;
                        Main2Right2Destroy.GetComponent<DebrisActionFlagship>().Main1Right1Down = Main1Right1Down;
                        Main2Right2Destroy.GetComponent<DebrisActionFlagship>().Main1Right2Down = Main1Right2Down;
                    }
                    if (Debris2 == true)
                    {
                        Main2Right2Destroy.GetComponent<DebrisActionFlagship>().Main2Left1Down = Main2Left1Down;
                        Main2Right2Destroy.GetComponent<DebrisActionFlagship>().Main2Left2Down = Main2Left2Down;
                        Main2Right2Destroy.GetComponent<DebrisActionFlagship>().Main2Right1Down = Main2Right1Down;
                        Main2Right2Destroy.GetComponent<DebrisActionFlagship>().Main2Right2Down = Main2Right2Down;
                    }
                    if (Debris3 == true)
                    {
                        Main2Right2Destroy.GetComponent<DebrisActionFlagship>().Main3Left1Down = Main3Left1Down;
                        Main2Right2Destroy.GetComponent<DebrisActionFlagship>().Main3Right1Down = Main3Right1Down;
                    }
                    Main2Right2Destroy.GetComponent<DebrisActionFlagship>().TurnOffPart();
                }
            }
            else //나리하 기함
            {
                if (Flagship == true)
                {
                    if (Type == 1)
                    {
                        if (HowManyPart == 1)
                        {
                            SecondDivisionPrefab.SetActive(false);
                            GameObject Main2Right2Destroy = Instantiate(SecondPart, SecondPartPos.position, SecondPartPos.rotation);
                            Main2Right2Destroy.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                            Main2Right2Destroy.GetComponent<DebrisActionFlagship>().LED.SetActive(false);
                            ObjectCopy(Main2Right2Destroy);
                        }
                        else if (HowManyPart > 1)
                        {
                            int RandomPart = Random.Range(0, HowManyPart + 1);

                            if (RandomPart == 0)
                            {
                                GameObject Part1Add = Instantiate(Part1, SecondPartPos.position, SecondPartPos.rotation);
                                Part1Add.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                                Part1Add.GetComponent<DebrisActionFlagship>().LED.SetActive(false);
                                ObjectCopy(Part1Add);

                                GameObject Part2Add = Instantiate(Part1_1, SecondPartPos.position, SecondPartPos.rotation);
                                Part2Add.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                                Part2Add.GetComponent<DebrisActionFlagship>().LED.SetActive(false);
                                ObjectCopy(Part2Add);
                                this.gameObject.SetActive(false);
                            }
                            else if (RandomPart == 1)
                            {
                                GameObject Part1Add = Instantiate(Part2, SecondPartPos.position, SecondPartPos.rotation);
                                Part1Add.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                                Part1Add.GetComponent<DebrisActionFlagship>().LED.SetActive(false);
                                ObjectCopy(Part1Add);

                                GameObject Part2Add = Instantiate(Part2_1, SecondPartPos.position, SecondPartPos.rotation);
                                Part2Add.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                                Part2Add.GetComponent<DebrisActionFlagship>().LED.SetActive(false);
                                ObjectCopy(Part2Add);
                                this.gameObject.SetActive(false);
                            }
                            else if (RandomPart == 2)
                            {
                                GameObject Part1Add = Instantiate(Part3, SecondPartPos.position, SecondPartPos.rotation);
                                Part1Add.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                                Part1Add.GetComponent<DebrisActionFlagship>().LED.SetActive(false);
                                ObjectCopy(Part1Add);

                                GameObject Part2Add = Instantiate(Part3_1, SecondPartPos.position, SecondPartPos.rotation);
                                Part2Add.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                                Part2Add.GetComponent<DebrisActionFlagship>().LED.SetActive(false);
                                ObjectCopy(Part2Add);
                                this.gameObject.SetActive(false);
                            }
                        }
                    }
                }
                else
                {
                    SecondDivisionPrefab.SetActive(false);
                    GameObject Part2Add = Instantiate(SecondPart, SecondPartPos.position, SecondPartPos.rotation);
                    Part2Add.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                    Part2Add.GetComponent<DebrisActionFlagship>().LED.SetActive(false);
                    ObjectCopy(Part2Add);
                }
            }
        }
    }

    void ObjectCopy(GameObject Part)
    {
        if (Debris1 == true)
        {
            Part.GetComponent<DebrisActionFlagship>().Main1Left1Down = Main1Left1Down;
            Part.GetComponent<DebrisActionFlagship>().Main1Left2Down = Main1Left2Down;
            Part.GetComponent<DebrisActionFlagship>().Main1Right1Down = Main1Right1Down;
            Part.GetComponent<DebrisActionFlagship>().Main1Right2Down = Main1Right2Down;
        }
        if (Debris2 == true)
        {
            Part.GetComponent<DebrisActionFlagship>().Main2Left1Down = Main2Left1Down;
            Part.GetComponent<DebrisActionFlagship>().Main2Left2Down = Main2Left2Down;
        }
        if (Debris3 == true)
        {
            Part.GetComponent<DebrisActionFlagship>().Main2Right1Down = Main2Right1Down;
            Part.GetComponent<DebrisActionFlagship>().Main2Right2Down = Main2Right2Down;
        }
        if (Debris5 == true)
        {
            Part.GetComponent<DebrisActionFlagship>().Main3Left1Down = Main3Left1Down;
            Part.GetComponent<DebrisActionFlagship>().Main3Right1Down = Main3Right1Down;
        }

        Part.GetComponent<DebrisActionFlagship>().TurnOffPart();
    }

    //불빛이 깜빡이며 정전되는 횟수
    IEnumerator LEDDown1()
    {
        int RandomLEDCount = Random.Range(2, 8);
        float RandomLEDDownStart = Random.Range(0, 3);

        yield return new WaitForSeconds(RandomLEDDownStart);

        for (int i = 0; i < RandomLEDCount; i++)
        {
            yield return new WaitForSeconds(RandomLED);
            LED.SetActive(false);
            yield return new WaitForSeconds(RandomLED);
            LED.SetActive(true);
            yield return new WaitForSeconds(RandomLED);
            LED.SetActive(false);
        }

        yield return new WaitForSeconds(0.1f);
        LED.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        LED.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        LED.SetActive(false);
    }
    IEnumerator LEDDown2()
    {
        int RandomLEDCount = Random.Range(2, 8);
        float RandomLEDDownStart = Random.Range(0, 3);

        yield return new WaitForSeconds(RandomLEDDownStart);

        for (int i = 0; i < RandomLEDCount; i++)
        {
            yield return new WaitForSeconds(RandomLED);
            LED2.SetActive(false);
            yield return new WaitForSeconds(RandomLED);
            LED2.SetActive(true);
            yield return new WaitForSeconds(RandomLED);
            LED2.SetActive(false);
        }

        yield return new WaitForSeconds(0.1f);
        LED2.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        LED2.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        LED2.SetActive(false);
    }
    IEnumerator LEDDown3()
    {
        int RandomLEDCount = Random.Range(2, 8);
        float RandomLEDDownStart = Random.Range(0, 3);

        yield return new WaitForSeconds(RandomLEDDownStart);

        for (int i = 0; i < RandomLEDCount; i++)
        {
            yield return new WaitForSeconds(RandomLED);
            LED3.SetActive(false);
            yield return new WaitForSeconds(RandomLED);
            LED3.SetActive(true);
            yield return new WaitForSeconds(RandomLED);
            LED3.SetActive(false);
        }

        yield return new WaitForSeconds(0.1f);
        LED3.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        LED3.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        LED3.SetActive(false);
    }
    IEnumerator LEDDown4()
    {
        int RandomLEDCount = Random.Range(2, 8);
        float RandomLEDDownStart = Random.Range(0, 3);

        yield return new WaitForSeconds(RandomLEDDownStart);

        for (int i = 0; i < RandomLEDCount; i++)
        {
            yield return new WaitForSeconds(RandomLED);
            LED4.SetActive(false);
            yield return new WaitForSeconds(RandomLED);
            LED4.SetActive(true);
            yield return new WaitForSeconds(RandomLED);
            LED4.SetActive(false);
        }

        yield return new WaitForSeconds(0.1f);
        LED4.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        LED4.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        LED4.SetActive(false);
    }
    IEnumerator LEDDown5()
    {
        int RandomLEDCount = Random.Range(2, 8);
        float RandomLEDDownStart = Random.Range(0, 3);

        yield return new WaitForSeconds(RandomLEDDownStart);

        for (int i = 0; i < RandomLEDCount; i++)
        {
            yield return new WaitForSeconds(RandomLED);
            LED5.SetActive(false);
            yield return new WaitForSeconds(RandomLED);
            LED5.SetActive(true);
            yield return new WaitForSeconds(RandomLED);
            LED5.SetActive(false);
        }

        yield return new WaitForSeconds(0.1f);
        LED5.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        LED5.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        LED5.SetActive(false);
    }
    IEnumerator LEDDown6()
    {
        int RandomLEDCount = Random.Range(2, 8);
        float RandomLEDDownStart = Random.Range(0, 3);

        yield return new WaitForSeconds(RandomLEDDownStart);

        for (int i = 0; i < RandomLEDCount; i++)
        {
            yield return new WaitForSeconds(RandomLED);
            LED6.SetActive(false);
            yield return new WaitForSeconds(RandomLED);
            LED6.SetActive(true);
            yield return new WaitForSeconds(RandomLED);
            LED6.SetActive(false);
        }

        yield return new WaitForSeconds(0.1f);
        LED6.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        LED6.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        LED6.SetActive(false);
    }
    IEnumerator LEDDown7()
    {
        int RandomLEDCount = Random.Range(2, 8);
        float RandomLEDDownStart = Random.Range(0, 3);

        yield return new WaitForSeconds(RandomLEDDownStart);

        for (int i = 0; i < RandomLEDCount; i++)
        {
            yield return new WaitForSeconds(RandomLED);
            LED7.SetActive(false);
            yield return new WaitForSeconds(RandomLED);
            LED7.SetActive(true);
            yield return new WaitForSeconds(RandomLED);
            LED7.SetActive(false);
        }

        yield return new WaitForSeconds(0.1f);
        LED7.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        LED7.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        LED7.SetActive(false);
    }
    IEnumerator LEDDown8()
    {
        int RandomLEDCount = Random.Range(2, 8);
        float RandomLEDDownStart = Random.Range(0, 3);

        yield return new WaitForSeconds(RandomLEDDownStart);

        for (int i = 0; i < RandomLEDCount; i++)
        {
            yield return new WaitForSeconds(RandomLED);
            LED8.SetActive(false);
            yield return new WaitForSeconds(RandomLED);
            LED8.SetActive(true);
            yield return new WaitForSeconds(RandomLED);
            LED8.SetActive(false);
        }

        yield return new WaitForSeconds(0.1f);
        LED8.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        LED8.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        LED8.SetActive(false);
    }
    IEnumerator LEDDown9()
    {
        int RandomLEDCount = Random.Range(2, 8);
        float RandomLEDDownStart = Random.Range(0, 3);

        yield return new WaitForSeconds(RandomLEDDownStart);

        for (int i = 0; i < RandomLEDCount; i++)
        {
            yield return new WaitForSeconds(RandomLED);
            LED9.SetActive(false);
            yield return new WaitForSeconds(RandomLED);
            LED9.SetActive(true);
            yield return new WaitForSeconds(RandomLED);
            LED9.SetActive(false);
        }

        yield return new WaitForSeconds(0.1f);
        LED9.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        LED9.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        LED9.SetActive(false);
    }
    IEnumerator LEDDown10()
    {
        int RandomLEDCount = Random.Range(2, 8);
        float RandomLEDDownStart = Random.Range(0, 3);

        yield return new WaitForSeconds(RandomLEDDownStart);

        for (int i = 0; i < RandomLEDCount; i++)
        {
            yield return new WaitForSeconds(RandomLED);
            LED10.SetActive(false);
            yield return new WaitForSeconds(RandomLED);
            LED10.SetActive(true);
            yield return new WaitForSeconds(RandomLED);
            LED10.SetActive(false);
        }

        yield return new WaitForSeconds(0.1f);
        LED10.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        LED10.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        LED10.SetActive(false);
    }

    void FlyAway()
    {
        Instantiate(ShockWave, WaveTransform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Disapear"))
        {
            AddSpeed = Random.Range(0.75f, 1.5f);
            ShockWaveConflict = true;
        }
    }
}