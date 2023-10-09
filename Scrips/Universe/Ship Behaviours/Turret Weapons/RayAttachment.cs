using UnityEngine;

public class RayAttachment : MonoBehaviour
{
    [Header("함포 발사 스위치")]
    public bool Fire;

    [Header("슬로리어스 함포 광선 유형")]
    public int WeaponType; //무기 타입. 1 = Ray1, Ray2가 포함된 광선, 2 = Ray3 광선

    [Header("타격 대상")]
    public LayerMask LayerMask; //타격 대상

    [Header("광선")]
    public LineRenderer Ray1; //슬로리어스 익쉬이우 슈루스 아광속 강습 두줄짜리 1번 광선
    public LineRenderer Ray2; //슬로리어스 익쉬이우 슈루스 아광속 강습 두줄짜리 2번 광선
    public LineRenderer Ray3; //슬로리어스 익쉬이우 슈루스 아광속 강습 한줄짜리 광선

    [Header("광선의 시작점과 도달점")]
    public Transform FirePosition;
    public Vector3 FireEndPos;

    [Header("광선 피격 이펙트")]
    public GameObject EndRayVFX; //피격 이펙트

    void EnableRay()
    {
        if (WeaponType == 1)
        {
            Ray1.enabled = true;
            Ray2.enabled = true;
            EndRayVFX.SetActive(true);
        }
        else if (WeaponType == 2)
        {
            Ray3.enabled = true;
            EndRayVFX.SetActive(true);
        }
    }

    void DisableRay()
    {
        if (WeaponType == 1)
        {
            Ray1.enabled = false;
            Ray2.enabled = false;
            EndRayVFX.SetActive(false);
        }
        else if (WeaponType == 2)
        {
            Ray3.enabled = false;
            EndRayVFX.SetActive(false);
        }
    }

    void UpdateRay()
    {
        float RotationZ = transform.rotation.eulerAngles.z;
        RotationZ *= Mathf.Deg2Rad;
        float Length;

        Vector2 Direction = new Vector2(Mathf.Cos(RotationZ), Mathf.Sin(RotationZ));
        Vector2 StartPos = FirePosition.position;

        RaycastHit2D HitRay2D = Physics2D.Raycast(transform.position, Direction.normalized, 1000, LayerMask);
        float EndRotation = 180;

        if (HitRay2D) //지정된 레이어 콜라이더에 빔이 충돌할 경우, 해당 충돌한 지점까지 빔을 출력
        {
            Length = (HitRay2D.point - StartPos).magnitude;
            Vector2 EndPos = StartPos + Length * Direction;
            EndRotation = Vector2.Angle(Direction, HitRay2D.normal);
            if (WeaponType == 1)
            {
                Ray1.SetPosition(1, new Vector2(Length, 0));
                Ray2.SetPosition(1, new Vector2(Length, 0));
            }
            else if (WeaponType == 2)
                Ray3.SetPosition(1, new Vector2(Length, 0));
            EndRayVFX.transform.position = EndPos;
            EndRayVFX.transform.rotation= Quaternion.Euler(0, 0, EndRotation);
        }
        else
        {
            Length = 1000;
            Vector2 EndPos = StartPos + Length * Direction;
            if (WeaponType == 1)
            {
                Ray1.SetPosition(1, new Vector2(Length, 0));
                Ray2.SetPosition(1, new Vector2(Length, 0));
            }
            else if (WeaponType == 2)
                Ray3.SetPosition(1, new Vector2(Length, 0));
            EndRayVFX.transform.position = EndPos;
            EndRayVFX.transform.rotation = Quaternion.Euler(0, 0, EndRotation);
        }
    }

    void Update()
    {
        if (Fire == true)
        {
            EnableRay();
            UpdateRay();
        }
        else
            DisableRay();
    }
}