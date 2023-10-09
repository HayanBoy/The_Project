using UnityEngine;

public class DebrisAction2 : MonoBehaviour
{
    public float DestroyTime;
    public float DebrisSpeedMinus;
    public float DebrisSpeedPlus;
    public float DebrisRotationSpeedMinus;
    public float DebrisRotationSpeedPlus;

    float DebrisSpeed;
    float DebrisRotationSpeed;
    private float DebrisSpeed2;
    private float DebrisRotationSpeed2;
    float RandomMovement;
    Vector3 endposition;

    private float AddSpeed;
    private bool ShockWaveConflict;

    void Start()
    {
        int DebrisRotationInt = Random.Range(0, 2);
        bool DebrisRotation = false;
        if (DebrisRotationInt == 0)
            DebrisRotation = true;
        else
            DebrisRotation = false;

        DebrisSpeed = Random.Range(DebrisSpeedMinus, DebrisSpeedPlus);
        DebrisRotationSpeed = Random.Range(DebrisRotationSpeedMinus, DebrisRotationSpeedPlus);
        RandomMovement = Random.Range(100, 900);
        DebrisSpeed2 = DebrisSpeed;
        DebrisRotationSpeed2 = DebrisRotationSpeed;

        if (DebrisRotation == true)
            endposition = new Vector3(transform.position.x + RandomMovement, Random.Range(transform.position.y - RandomMovement, transform.position.y + RandomMovement), transform.position.z);
        else
            endposition = new Vector3(transform.position.x - RandomMovement, Random.Range(transform.position.y - RandomMovement, transform.position.y + RandomMovement), transform.position.z);

        Destroy(gameObject, DestroyTime);
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, DebrisRotationSpeed * Time.deltaTime));
        transform.position = Vector3.MoveTowards(transform.position, endposition, DebrisSpeed * Time.deltaTime);

        if (ShockWaveConflict == true)
        {
            if (DebrisSpeed <= DebrisSpeed2 + AddSpeed)
                DebrisSpeed += 0.1f;
            if (DebrisRotationSpeed <= DebrisRotationSpeed2 + AddSpeed)
                DebrisRotationSpeed += 0.1f;
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