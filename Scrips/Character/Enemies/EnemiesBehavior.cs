using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesBehavior : MonoBehaviour
{
    public float speed; //�⺻ �ӵ�, ���� �ӵ�
    public float runningSpeed; //�ٴ� �ӵ�, ȸ���ϰų� ������ �� ���δ�.
    private float currnetSpeed; //�⺻�� �ٴ� �ӵ��� ��ȯ���ִ� ����
    public float Damage; //���� �߻�� ���ط�
    public float lineOfSite; //�ֿܰ���, �÷��̾� ������ ��ȸ�ϱ� ���� �뵵
    public float traceSite; //������, �ֿܰ����� ������ ���̿��� �÷��̾ ������Ű�� �뵵
    public float avoidSite; //���� ���ʿ� �ִ� ��, �÷��̾ ������ ���� ���, ���ϱ� ���� �뵵
    public float directionChangeTime; //��ȸ�� ���� ��ȯ���� �ɸ��� �ð�

    private Transform player;
    private Vector2 target;

    public int shootingDelayOnEasyLevel; //���̵��� ���� ���� ����
    private int enemyShoot; //����Ȯ���� ���� ����
    private int moveDown; //����Ȯ���� �����ϱ� ���� �����Լ�

    bool avoid = false;

    public GameObject enemyAmmoPrefab; //�߻� �Ѿ� ������
    public Transform enemyAmmoPos; //�Ѿ� ���� ��ǥ

    Transform targetTransform = null;

    Rigidbody2D rb2d;
    Coroutine moveCoroutine;
    Vector3 endposition;
    Animator animator;

    float currentAngle = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);

        StartCoroutine(yMove());
    }

    //�����ð��� ����
    IEnumerator yMove()
    {
        while (true)
        {
            moveDown = Random.Range(0, 5);
            yield return new WaitForSeconds(1f);
        }
    }

    //yMove ���߱�
    IEnumerator StopyMove()
    {
        StopCoroutine(yMove());
        yield return new WaitForSeconds(1f);
    }

    void Update()
    {
        //float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        ////Debug.Log(avoid);
        
        ////���� �ݰ� �� ���ٽ� ��ȸ ����
        //if(distanceFromPlayer < lineOfSite)
        //{
        //    //���� ������ �ֿܰ� ������ ���� ���, �÷��̾ ���� ���������� ����
        //    if (distanceFromPlayer > traceSite)
        //    {
        //        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        //    }
        //    else if (distanceFromPlayer < traceSite && distanceFromPlayer > avoidSite && avoid == false) //��ȸ �� Y�� �̵�
        //    {
        //        currnetSpeed = speed;
        //        Movement();

        //        if(avoid == true)
        //        {
        //            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        //        }
        //    }

        //    //������ �������� ���, ȸ��
        //    if (distanceFromPlayer < avoidSite)
        //    {
        //        avoid = true;
        //        StartCoroutine(StopyMove());
        //        StartCoroutine(Avoid());
        //    }
        //    else if(distanceFromPlayer > avoidSite && avoid == true)
        //    {
        //        StartCoroutine(AvoidDuringWander()); //�����ϴ� ���� ȸ�Ƕ��ο��� ȸ�Ǹ� ���� ���� ������ �ٽõ��� ���
        //    }
        //}

        ////���� �ݰ��� ��Ż��, ��ȸ ����
        //if (distanceFromPlayer > lineOfSite)
        //{
        //    if (moveCoroutine != null)
        //    {
        //        StopCoroutine(ShootingMovement());
        //        StopCoroutine(Move(rb2d, currnetSpeed));
        //        currnetSpeed = 0;
        //    }

        //    targetTransform = null;
        //}
    }

    //ȸ��
    IEnumerator Avoid()
    {
        yield return new WaitForSeconds(0.3f);
        transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
    }

    //�����ϴ� ���� ȸ�Ƕ��ο��� ȸ�Ǹ� ���� ���� ������ �ٽõ��� ���
    IEnumerator AvoidDuringWander()
    {
        yield return new WaitForSeconds(2f);
        avoid = false;
    }

    //��ȸ �� Y�� �̵�
    void Movement()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        if (moveDown == 1 && distanceFromPlayer > avoidSite && avoid == false) //�÷��̾� ��� ����
        {
            StopCoroutine(ShootingMovement());
            transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * speed);
            //this.gameObject.AddComponent<EnemiesAI>().yChase(); //Y�� ������ by EnemiesAI
        }
        else
        {
            StartCoroutine(ShootingMovement());
        }
    }

    //�Ϲ� ��ȸ
    public IEnumerator ShootingMovement()
    {
        while(true)
        {
            ChooseNewEndPoint();

            if(moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }

            moveCoroutine = StartCoroutine(Move(rb2d, currnetSpeed));
            yield return new WaitForSeconds(directionChangeTime);
        }
    }

    //��ǥ ���� ����
    public void ChooseNewEndPoint()
    {
        currentAngle += Random.Range(0, 360);
        currentAngle = Mathf.Repeat(currentAngle, 360);
        endposition += Vector3FromAngle(currentAngle);
    }

    //���� ����
    Vector3 Vector3FromAngle(float inputAngleDegrees)
    {
        float inputAngleRadians = inputAngleDegrees * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(inputAngleRadians), Mathf.Sin(inputAngleRadians), 0);
    }

    //������ ���� ������ ����
    public IEnumerator Move(Rigidbody2D rigidbodyToMove, float speed)
    {
        float remainingDistance = (transform.position - endposition).sqrMagnitude;

        while(remainingDistance > float.Epsilon)
        {
            if(targetTransform != null)
            {
                endposition = targetTransform.position;
            }

            //�̵�
            if(rigidbodyToMove != null)
            {
                //animater.SetBool("", true);
                Vector3 newPosition = Vector3.MoveTowards(rigidbodyToMove.position, endposition, speed * Time.deltaTime);
                rb2d.MovePosition(newPosition);
                remainingDistance = (transform.position - endposition).sqrMagnitude;

                yield return new WaitForSeconds(directionChangeTime);
            }
        }
        //animater.SetBool("", false);
    }

    //�� �߻�
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision is BoxCollider2D)
        {
            enemyShoot = Random.Range(1, shootingDelayOnEasyLevel);
            if(enemyShoot == 1)
            {
                Player player = collision.gameObject.GetComponent<Player>();
                GameObject e_ammo = Instantiate(enemyAmmoPrefab, enemyAmmoPos.position, transform.rotation); //�߻� �Ѿ� ����
                //e_ammo.GetComponent<AmmoMovement>().SetDamage(Damage); //�Ѿ˿��� ������ ����
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, avoidSite);
        Gizmos.DrawWireSphere(transform.position, traceSite);
    }
}