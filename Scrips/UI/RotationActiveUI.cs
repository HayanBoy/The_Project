using System.Collections;
using UnityEngine;

public class RotationActiveUI : MonoBehaviour
{
    Coroutine rollStart;

    Animator animator;
    RectTransform selfAnchor;
    public bool ClockRotation; //UI 회전 방향 여부
    public bool isMove; //플레이어의 이동속도에 따른 UI 회전
    public bool HpDown; //플레이어의 체력 상태에 따른 UI 여부
    public bool FastStop;
    private bool SlowDownUI;

    public float Roll;
    private float Roll2; //Fast회전 전용
    public float RoationTime;
    public float IdleTimeForFastStop;
    public float StopSpeed;
    private float RollOneTime;
    private int Reverse;

    void Start()
    {
        animator = GetComponent<Animator>();
        selfAnchor = GetComponent<RectTransform>();
        Roll2 = Roll;
        Roll = 0;
    }

    private void OnDisable()
    {
        RollOneTime = 0;
        if (rollStart != null)
            StopCoroutine(rollStart);
        Roll = 0;
    }

    void Update()
    {
        if (isMove)
        {
            if (RollOneTime == 0)
            {
                RollOneTime += Time.deltaTime;
                rollStart = StartCoroutine(RollStart());
            }

            selfAnchor.Rotate(new Vector3(selfAnchor.rotation.x, selfAnchor.rotation.y, Roll) * Time.deltaTime);

            if (!SlowDownUI)
                Roll -= StopSpeed;
            else
                Roll += StopSpeed;
        }
        else
        {
            if (rollStart != null)
                StopCoroutine(rollStart);
            RollOneTime = 0;
            Roll = 0;
        }
    }

    IEnumerator RollStart()
    {
        if (!FastStop)
        {
            SlowDownUI = true;
            yield return new WaitForSeconds(RoationTime);

            while (true)
            {
                SlowDownUI = false;
                yield return new WaitForSeconds(RoationTime * 2);

                SlowDownUI = true;
                yield return new WaitForSeconds(RoationTime * 2);
            }
        }
        else
        {
            while (true)
            {
                IdleTimeForFastStop = Random.Range(1, 5);
                Reverse = Random.Range(0, 2);
                if (Reverse == 0)
                    Roll = Roll2;
                else
                    Roll = -Roll2;

                yield return new WaitForSeconds(RoationTime);
                Roll = 0;
                yield return new WaitForSeconds(IdleTimeForFastStop);
            }
        }
    }
}