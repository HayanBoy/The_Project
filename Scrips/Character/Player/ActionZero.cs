using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionZero : MonoBehaviour
{
    //플레이어가 비콘에 충돌 시, 비콘으로 자동으로 움직이면서 모든 행동을 중지하기
    public void StopUI(Collider2D collision)
    {
        GameControlSystem GameControlSystem = GameObject.Find("Game Control").GetComponent<GameControlSystem>();

        //플레이어 UI를 모두 비활성화
        StartCoroutine(GameControlSystem.DisappearPlayerUI());
        StartCoroutine(GameControlSystem.DisappearPlayerController());
        StartCoroutine(GameControlSystem.DisappearShipUI());
        StartCoroutine(GameControlSystem.HealthBarDeactive());
        StartCoroutine(GameControlSystem.AmmoHUDDeactive());

        //중화기 착용 중일 경우, 해제
        if (GameControlSystem.UsingChangeWeapon == true) //중화기를 발포하고 있는 경우, 즉시 발포를 중지하고 해제
        {
            GameControlSystem.UsingChangeWeapon = false;
            ArthesL775Controller ArthesL775Controller = collision.transform.parent.GetComponent<ArthesL775Controller>();
            StartCoroutine(ArthesL775Controller.StopFire());
        }
        else if (GameControlSystem.ChangeWeaponOnline > 1)
            GameControlSystem.ChangeWeaponSystemClick();
        else if (GameControlSystem.inWeapon == true)
            collision.transform.parent.GetComponent<M3078Controller>().M3078SwapClick();
    }

    //UI 재시작
    public void RestartUI()
    {
        GameControlSystem GameControlSystem = GameObject.Find("Play Control/Game Control").GetComponent<GameControlSystem>();
        StartCoroutine(GameControlSystem.StartButten());
        StartCoroutine(GameControlSystem.ActivePlayerController());
        StartCoroutine(GameControlSystem.StartShipUIActive());
        StartCoroutine(GameControlSystem.HealthBarActive());
    }
}