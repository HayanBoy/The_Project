using System.Collections;
using UnityEngine;

public class DebrisAction : MonoBehaviour
{
    public float DestroyTime;
    private float DebrisSpeed;
    private float DebrisSpeed2;
    private float DebrisRotationSpeed;
    private float DebrisRotationSpeed2;

    private bool DebrisRotation;
    private bool TakeDamage;
    private bool TakeExplosion;
    private Vector3 endposition;

    public GameObject SecondDivisionPrefab;
    public GameObject SecondPart;
    public Transform SecondPartPos;

    public GameObject LED;
    public GameObject LED2;
    private float RandomLED;

    private float AddSpeed;
    private bool ShockWaveConflict;
    GameObject Explosion;

    public void StartMoveDebris(float Speed, bool Rotation, bool Explosion)
    {
        DebrisSpeed *= Speed;
        DebrisRotation = Rotation;
        TakeExplosion = Explosion;
    }

    void Start()
    {
        DebrisSpeed2 = DebrisSpeed;
        DebrisRotationSpeed2 = DebrisRotationSpeed;

        DebrisSpeed = Random.Range(0.5f, 1f);
        DebrisRotationSpeed = Random.Range(-15f, 15f);
        RandomLED = Random.Range(0.02f, 0.06f);

        float RandomMovement = Random.Range(100, 900);
        if (DebrisRotation == true)
            endposition = new Vector3(transform.position.x + RandomMovement, Random.Range(transform.position.y - RandomMovement, transform.position.y + RandomMovement), transform.position.z);
        else
            endposition = new Vector3(transform.position.x - RandomMovement, Random.Range(transform.position.y - RandomMovement, transform.position.y + RandomMovement), transform.position.z);

        if (SecondPart != null)
            StartCoroutine(SecondDivision());
        StartCoroutine(LEDDown());
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
        int StartDivide = Random.Range(0, 0);
        yield return new WaitForSeconds(RandomDivision);

        if (StartDivide == 0)
        {
            SecondDivisionPrefab.SetActive(false);
            GameObject Main2Right2Destroy = Instantiate(SecondPart, SecondPartPos.position, SecondPartPos.rotation);
            Main2Right2Destroy.GetComponent<DebrisAction>().StartMoveDebris(DebrisSpeed, true, false);
            Main2Right2Destroy.GetComponent<DebrisAction>().LED.SetActive(false);
        }
    }

    //ºÒºûÀÌ ±ôºýÀÌ¸ç Á¤ÀüµÇ´Â È½¼ö
    IEnumerator LEDDown()
    {
        if (LED != null)
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
        if (LED2 != null)
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Disapear"))
        {
            AddSpeed = Random.Range(1.25f, 2.5f);
            ShockWaveConflict = true;
        }
    }
}