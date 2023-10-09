using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class LiveCommunicationSystem : MonoBehaviour
{
    [Header("스크립트")]
    public TutorialSystem TutorialSystem;
    public WordPrintSystem WordPrintSystem;

    [Header("대화 텍스트")]
    public GameObject MainTextPrefab;
    public GameObject SubTextPrefab;
    public Text MainText;
    public Text MainShadowText;
    public Text SubText;
    public Text SubShadowText;
    public string PlanetName;

    [Header("대화 출력 조절")]
    private bool MainTextPrintting = false;
    private bool SubTextPrintting = false;

    //메인 대화 출력
    public IEnumerator MainCommunication(float number)
    {
        if (MainTextPrintting == false)
        {
            MainTextPrintting = true;
            MainTextPrefab.SetActive(true);

            if (number == 1.00f) //첫 시작(튜토리얼)
            {
                if (WordPrintSystem.LanguageType == 1)
                {
                    yield return new WaitForSeconds(3);
                    ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber = 1;
                    ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().SystemNowNumber = 1;
                    MainText.text = string.Format("<color=#47BEFF>Benedict Archi</color> : Alright, I've connect the Command Center console now. I see the flagship and ships standing by through this screen.");
                    MainShadowText.text = string.Format("<color=#494949>Benedict Archi : Alright, I've connect the Command Center console now. I see the flagship and ships standing by through this screen.</color>");
                    yield return new WaitForSeconds(7);
                    MainText.text = string.Format("<color=#47BEFF>Benedict Archi</color> : Let me check again the fleet command before starting the operation.");
                    MainShadowText.text = string.Format("<color=#494949>Benedict Archi : Let me check again the fleet command before starting the operation.</color>");
                    yield return new WaitForSeconds(7);
                    MainText.text = string.Format("<color=#47BEFF>Benedict Archi</color> : All ship move by screen touch. A ship will move to the touched screen area.");
                    MainShadowText.text = string.Format("<color=#494949>Benedict Archi : All ship move by screen touch. A ship will move to the touched screen area.</color>");
                    yield return new WaitForSeconds(7);
                    MainText.text = string.Format("<color=#47BEFF>Benedict Archi</color> : The ships move with their flagship cause most fleet of our Nariha operate with their flagship.");
                    MainShadowText.text = string.Format("<color=#494949>Benedict Archi : The ships move with their flagship cause most fleet of our Nariha operate with their flagship.</color>");
                    yield return new WaitForSeconds(7);
                    MainText.text = string.Format("<color=#47BEFF>Benedict Archi</color> : And if enemy fleet is in our fleet attack range, our ship will engage.");
                    MainShadowText.text = string.Format("<color=#494949>Benedict Archi : And if enemy fleet is in our fleet attack range, our ship will engage.</color>");
                }
                else if (WordPrintSystem.LanguageType == 2)
                {
                    yield return new WaitForSeconds(3);
                    ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber = 1;
                    ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().SystemNowNumber = 1;
                    MainText.text = string.Format("<color=#47BEFF>베네딕트 아르키</color> : 좋아, 나는 지금 사령부 콘솔에 접속했다. 화면을 통해 기함과 함선들이 대기하고 있는 모습이 잘 보이는 군.");
                    MainShadowText.text = string.Format("<color=#494949>베네딕트 아르키 : 좋아, 나는 지금 사령부 콘솔에 접속했다. 화면을 통해 기함과 함선들이 대기하고 있는 모습이 잘 보이는 군.</color>");
                    yield return new WaitForSeconds(7);
                    MainText.text = string.Format("<color=#47BEFF>베네딕트 아르키</color> : 해방 작전을 시작하기 전에 함대 명령을 다시 확인해보겠다.");
                    MainShadowText.text = string.Format("<color=#494949>베네딕트 아르키 : 해방 작전을 시작하기 전에 함대 명령을 다시 확인해보겠다.</color>");
                    yield return new WaitForSeconds(7);
                    MainText.text = string.Format("<color=#47BEFF>베네딕트 아르키</color> : 모든 함선은 직접 화면 터치를 통해 이동명령을 실시한다. 화면 터치한 곳을 향해 함대가 움직일테지.");
                    MainShadowText.text = string.Format("<color=#494949>베네딕트 아르키 : 모든 함선은 직접 화면 터치를 통해 이동명령을 실시한다. 화면 터치한 곳을 향해 함대가 움직일테지.</color>");
                    yield return new WaitForSeconds(7);
                    MainText.text = string.Format("<color=#47BEFF>베네딕트 아르키</color> : 우리 나리하의 대부분의 함대는 기함 중심 작전을 수행하기 때문에, 기함을 중심으로 함선들이 함께 움직일 것이다.");
                    MainShadowText.text = string.Format("<color=#494949>베네딕트 아르키 : 우리 나리하의 대부분의 함대는 기함 중심 작전을 수행하기 때문에, 기함을 중심으로 함선들이 함께 움직일 것이다.</color>");
                    yield return new WaitForSeconds(7);
                    MainText.text = string.Format("<color=#47BEFF>베네딕트 아르키</color> : 그리고 적 함대가 우리 함대의 공격 범위에 있다면, 함선들은 교전을 즉시 실시할 것이다.");
                    MainShadowText.text = string.Format("<color=#494949>베네딕트 아르키 : 그리고 적 함대가 우리 함대의 공격 범위에 있다면, 함선들은 교전을 즉시 실시할 것이다.</color>");
                }
                StartCoroutine(SubCommunication(1.00f));
                yield return new WaitForSeconds(7);
            }
            else if (number == 1.01f) //첫 시작(튜토리얼)
            {
                if (WordPrintSystem.LanguageType == 1)
                {
                    MainText.text = string.Format("<color=#47BEFF>Benedict Archi</color> : Copy. Then all we need is to warp fleet into the battle area and support the our force ships.");
                    MainShadowText.text = string.Format("<color=#494949>Benedict Archi : Copy. Then all we need is to warp fleet into the battle area and support the our force ships.</color>");
                }
                else if (WordPrintSystem.LanguageType == 2)
                {
                    MainText.text = string.Format("<color=#47BEFF>베네딕트 아르키</color> : 확인했다. 그럼 먼저 해당 전투 지역으로 함대를 워프하여 아군 함선을 지원하라.");
                    MainShadowText.text = string.Format("<color=#494949>베네딕트 아르키 : 확인했다. 그럼 먼저 해당 전투 지역으로 함대를 워프하여 아군 함선을 지원하라.</color>");
                }
                yield return new WaitForSeconds(7);
                TutorialSystem.PopUpUniverseMapButton();
            }
            else if (number == 1.02f) //카메라 활성화 및 사타리우스 글래시아 활성화
            {
                if (WordPrintSystem.LanguageType == 1)
                {
                    yield return new WaitForSeconds(7);
                    MainText.text = string.Format("<color=#47BEFF>Benedict Archi</color> : Well done, It looks like complete to ready for planet liberation.");
                    MainShadowText.text = string.Format("<color=#494949>Benedict Archi : Well done, It looks like complete to ready for planet liberation.</color>");
                    yield return new WaitForSeconds(7);
                    MainText.text = string.Format("<color=#47BEFF>Benedict Archi</color> : First planet we have to liberate is the Satarius Glessia. This planet has great economy.");
                    MainShadowText.text = string.Format("<color=#494949>Benedict Archi : First planet we have to liberate is the Satarius Glessia. This planet has great economy.</color>");
                    yield return new WaitForSeconds(7);
                    MainText.text = string.Format("<color=#47BEFF>Benedict Archi</color> : The UCCIS can approve using assets right if we liberate this planet.");
                    MainShadowText.text = string.Format("<color=#494949>Benedict Archi : The UCCIS can approve using assets right if we liberate this planet.</color>");
                }
                else if (WordPrintSystem.LanguageType == 2)
                {
                    yield return new WaitForSeconds(7);
                    MainText.text = string.Format("<color=#47BEFF>베네딕트 아르키</color> : 잘했다. 이제 본격적으로 행성 해방 준비가 완료된 것 같군.");
                    MainShadowText.text = string.Format("<color=#494949>베네딕트 아르키 : 잘했다. 이제 본격적으로 행성 해방 준비가 완료된 것 같군.</color>");
                    yield return new WaitForSeconds(7);
                    MainText.text = string.Format("<color=#47BEFF>베네딕트 아르키</color> : 우리가 가장 먼저 해방해야 할 행성은 사타리우스 글래시아다. 이 행성은 제법 준수한 경제를 보유하고 있는 행성이지.");
                    MainShadowText.text = string.Format("<color=#494949>베네딕트 아르키 : 우리가 가장 먼저 해방해야 할 행성은 사타리우스 글래시아다. 이 행성은 제법 준수한 경제를 보유하고 있는 행성이지.</color>");
                    yield return new WaitForSeconds(7);
                    MainText.text = string.Format("<color=#47BEFF>베네딕트 아르키</color> : 이 행성을 해방해야 성간우주사령연합으로부터 자금 사용 권한을 허가받을 수 있을 것이다.");
                    MainShadowText.text = string.Format("<color=#494949>베네딕트 아르키 : 이 행성을 해방해야 성간우주사령연합으로부터 자금 사용 권한을 허가받을 수 있을 것이다.</color>");
                }

                yield return new WaitForSeconds(7);
                TutorialSystem.UniverseMapViewGuidePrefab.SetActive(true);
                TutorialSystem.UniverseMapSystem.TutorialMapStep = 2;
                TutorialSystem.PlanetOurForceShipsManager.Tutorial = true;
            }
            else if (number == 1.03f) //사타리우스 글래시아 교전
            {
                if (WordPrintSystem.LanguageType == 1)
                {
                    yield return new WaitForSeconds(3);
                    MainText.text = string.Format("<color=#47BEFF>Benedict Archi</color> : All fleets, Take fire to enemy.");
                    MainShadowText.text = string.Format("<color=#494949>Benedict Archi : All fleets, Take fire to enemy.</color>");
                    yield return new WaitForSeconds(7);
                    MainText.text = string.Format("<color=#FF7948>Delta Hurricane</color> : This is operations commander, Delta Hurricane of the Delta Strike Group. We are ready to engage ground warfare of the Satarius Glessia.");
                    MainShadowText.text = string.Format("<color=#494949>Delta Hurricane : This is operations commander, Delta Hurricane of the Delta Strike Group. We are ready to engage ground warfare of the Satarius Glessia.</color>");
                    yield return new WaitForSeconds(7);
                    MainText.text = string.Format("<color=#47BEFF>Benedict Archi</color> : Hurricane, We are engage but you are able to proceed right now.");
                    MainShadowText.text = string.Format("<color=#494949>Benedict Archi : Hurricane, We are engage but you are able to proceed right now.</color>");
                    yield return new WaitForSeconds(7);
                    MainText.text = string.Format("<color=#FF7948>Delta Hurricane</color> : No problem. I'm standing by for planetary operation, proceed when you ready.");
                    MainShadowText.text = string.Format("<color=#494949>Delta Hurricane : No problem. I'm standing by for planetary operation, proceed when you ready.</color>");
                }
                else if (WordPrintSystem.LanguageType == 2)
                {
                    yield return new WaitForSeconds(3);
                    MainText.text = string.Format("<color=#47BEFF>베네딕트 아르키</color> : 함대는 즉시 적과 교전을 실시하라.");
                    MainShadowText.text = string.Format("<color=#494949>베네딕트 아르키 : 함대는 즉시 적과 교전을 실시하라.</color>");
                    yield return new WaitForSeconds(7);
                    MainText.text = string.Format("<color=#FF7948>델타 허리케인</color> : 여기는 델타 전단의 작전 수행 사령관 델타 허리케인이다. 이곳은 사타리우스 글래시아 지상전을 수행할 준비가 되었다.");
                    MainShadowText.text = string.Format("<color=#494949>델타 허리케인 : 여기는 델타 전단의 작전 수행 사령관 델타 허리케인이다. 이곳은 사타리우스 글래시아 지상전을 수행할 준비가 되었다.</color>");
                    yield return new WaitForSeconds(7);
                    MainText.text = string.Format("<color=#47BEFF>베네딕트 아르키</color> : 허리케인, 현재 함대가 교전을 시작했지만, 자네라면 지금이라도 진행을 할 수 있어 보이는군.");
                    MainShadowText.text = string.Format("<color=#494949>베네딕트 아르키 : 허리케인, 현재 함대가 교전을 시작했지만, 자네라면 지금이라도 진행을 할 수 있어 보이는군.</color>");
                    yield return new WaitForSeconds(7);
                    MainText.text = string.Format("<color=#FF7948>델타 허리케인</color> : 문제 없다. 언제든 행성 작전을 기다리고 있으니, 준비가 되었으면 진행하도록.");
                    MainShadowText.text = string.Format("<color=#494949>델타 허리케인 : 문제 없다. 언제든 행성 작전을 기다리고 있으니, 준비가 되었으면 진행하도록.</color>");
                }

                yield return new WaitForSeconds(7);
                TutorialSystem.HurricaneOperationViewGuidePrefab.SetActive(true);
                TutorialSystem.HurricaneButton1.SetActive(true);
                TutorialSystem.HurricaneButton2.SetActive(true);
                TutorialSystem.HurricaneOperationMenu.HurricaneMainWeaponActive();
            }

            if (number == 2.01f) //행성 해방
            {
                int RandomText = Random.Range(0, 4);
                if (WordPrintSystem.LanguageType == 1)
                {
                    if (RandomText == 0)
                    {
                        SubText.text = string.Format("<color=#47BEFF>Benedict Archi</color> : Good. Our force's fleet is going to deploy at the " + PlanetName + " planet.");
                        SubShadowText.text = string.Format("<color=#494949>Benedict Archi : Good. Our force's fleet is going to deploy at the " + PlanetName + " planet.</color>");
                    }
                    else if (RandomText == 1)
                    {
                        SubText.text = string.Format("<color=#47BEFF>Benedict Archi</color> : Now the " + PlanetName + " are free, our force's fleet will stationed on there.");
                        SubShadowText.text = string.Format("<color=#494949>Benedict Archi : Now the " + PlanetName + " are free, our force's fleet will stationed on there.</color>");
                    }
                    else if (RandomText == 2)
                    {
                        MainText.text = string.Format("<color=#FF7948>Delta Hurricane</color> : Now the " + PlanetName + " Planet can get back peace.");
                        MainShadowText.text = string.Format("<color=#494949>Delta Hurricane : Now the " + PlanetName + " Planet can get back peace.</color>");
                    }
                    else if (RandomText == 3)
                    {
                        MainText.text = string.Format("<color=#FF7948>Delta Hurricane</color> : Nice to liberate Another planet. The " + PlanetName + " planet will safe.");
                        MainShadowText.text = string.Format("<color=#494949>Delta Hurricane : Nice to liberate Another planet. The " + PlanetName + " planet will safe.</color>");
                    }
                }
                else if (WordPrintSystem.LanguageType == 2)
                {
                    if (RandomText == 0)
                    {
                        MainText.text = string.Format("<color=#47BEFF>베네딕트 아르키</color> : 좋아. 우리 아군 함대가 지금 " + PlanetName + " 행성에 배치되고 있다네.");
                        MainShadowText.text = string.Format("<color=#494949>베네딕트 아르키 : 좋아. 우리 아군 함대가 지금 " + PlanetName + " 행성에 배치되고 있다네.</color>");
                    }
                    else if (RandomText == 1)
                    {
                        MainText.text = string.Format("<color=#47BEFF>베네딕트 아르키</color> : " + PlanetName + " 행성이 해방되었으니, 아군 함대가 그곳에 주둔할 걸세.");
                        MainShadowText.text = string.Format("<color=#494949>베네딕트 아르키 : " + PlanetName + " 행성이 해방되었으니, 아군 함대가 그곳에 주둔할 걸세.</color>");
                    }
                    else if (RandomText == 2)
                    {
                        MainText.text = string.Format("<color=#FF7948>델타 허리케인</color> : 이제 " + PlanetName + " 행성이 평온을 되찾을 수 있겠군.");
                        MainShadowText.text = string.Format("<color=#494949>델타 허리케인 : 이제 " + PlanetName + " 행성이 평온을 되찾을 수 있겠군.</color>");
                    }
                    else if (RandomText == 3)
                    {
                        MainText.text = string.Format("<color=#FF7948>델타 허리케인</color> : 또 하나의 행성을 수복해서 다행이군. " + PlanetName + " 행성은 이제 안전할 걸세.");
                        MainShadowText.text = string.Format("<color=#494949>델타 허리케인 : 또 하나의 행성을 수복해서 다행이군. " + PlanetName + " 행성은 이제 안전할 걸세.</color>");
                    }
                }
                yield return new WaitForSeconds(7);
            }

            else if (number == 3.01f) //항성 해방
            {
                int RandomText = Random.Range(0, 4);
                if (WordPrintSystem.LanguageType == 1)
                {
                    if (RandomText == 0)
                    {
                        SubText.text = string.Format("<color=#47BEFF>Benedict Archi</color> : Good. We took back the " + PlanetName + " star, and we will fight more easy against the Contros.");
                        SubShadowText.text = string.Format("<color=#494949>Benedict Archi : Good. We took back the " + PlanetName + " star, and we will fight more easy against the Contros.</color>");
                    }
                    else if (RandomText == 1)
                    {
                        SubText.text = string.Format("<color=#47BEFF>Benedict Archi</color> : Thanks to " + PlanetName + "'s liberity, the Contros fleet got a weak again.");
                        SubShadowText.text = string.Format("<color=#494949>Benedict Archi : Thanks to " + PlanetName + "'s liberity, the Contros fleet got a weak again.</color>");
                    }
                    else if (RandomText == 2)
                    {
                        MainText.text = string.Format("<color=#FF7948>Delta Hurricane</color> : Great. You win the battle at the " + PlanetName + " star. May be Contros won't be happy in this battle.");
                        MainShadowText.text = string.Format("<color=#494949>Delta Hurricane : Great. You win the battle at the " + PlanetName + " star. May be Contros won't be happy in this battle.</color>");
                    }
                    else if (RandomText == 3)
                    {
                        MainText.text = string.Format("<color=#FF7948>Delta Hurricane</color> : I guess the Contros will lose some power. Because we took back the " + PlanetName + " star.");
                        MainShadowText.text = string.Format("<color=#494949>Delta Hurricane : I guess the Contros will lose some power. Because we took back the " + PlanetName + " star.</color>");
                    }
                }
                else if (WordPrintSystem.LanguageType == 2)
                {
                    if (RandomText == 0)
                    {
                        MainText.text = string.Format("<color=#47BEFF>베네딕트 아르키</color> : 잘했다. 이제 " + PlanetName + " 항성을 되찾았으니, 이제 컨트로스를 상대하기 수월할 것이다.");
                        MainShadowText.text = string.Format("<color=#494949>베네딕트 아르키 : 잘했다. 이제 " + PlanetName + " 항성을 되찾았으니, 이제 컨트로스를 상대하기 수월할 것이다.</color>");
                    }
                    else if (RandomText == 1)
                    {
                        MainText.text = string.Format("<color=#47BEFF>베네딕트 아르키</color> : " + PlanetName + " 항성 해방 덕분에 컨트로스 함대가 또 한번 약해졌군.");
                        MainShadowText.text = string.Format("<color=#494949>베네딕트 아르키 : " + PlanetName + " 항성 해방 덕분에 컨트로스 함대가 또 한번 약해졌군.</color>");
                    }
                    else if (RandomText == 2)
                    {
                        MainText.text = string.Format("<color=#FF7948>델타 허리케인</color> : 훌륭하군. " + PlanetName + " 항성에서의 전투를 이겨내다니 말이야. 이번 전투로 컨트로스가 기분이 좋진 않겠군.");
                        MainShadowText.text = string.Format("<color=#494949>델타 허리케인 : 훌륭하군. " + PlanetName + " 항성에서의 전투를 이겨내다니 말이야. 이번 전투로 컨트로스가 기분이 좋진 않겠군.</color>");
                    }
                    else if (RandomText == 3)
                    {
                        MainText.text = string.Format("<color=#FF7948>델타 허리케인</color> : 컨트로스가 힘 좀 빠지겠군. " + PlanetName + " 항성을 우리가 되찾아서 말이지.");
                        MainShadowText.text = string.Format("<color=#494949>델타 허리케인 : 컨트로스가 힘 좀 빠지겠군. " + PlanetName + " 항성을 우리가 되찾아서 말이지.</color>");
                    }
                }
                yield return new WaitForSeconds(7);
            }

            else if (number == 4.01f) //행성이 모두 해방된 이후의, 컨트로스 항성 철수
            {
                int RandomText = Random.Range(0, 4);
                if (WordPrintSystem.LanguageType == 1)
                {
                    if (RandomText == 0)
                    {
                        SubText.text = string.Format("<color=#47BEFF>Benedict Archi</color> : This " + PlanetName + " star system's planets took back the freedom and It seems like the Contros knows how to retreat from this star.");
                        SubShadowText.text = string.Format("<color=#494949>Benedict Archi : This " + PlanetName + " star system's planets took back the freedom and It seems like the Contros knows how to retreat from this star.</color>");
                    }
                    else if (RandomText == 1)
                    {
                        SubText.text = string.Format("<color=#47BEFF>Benedict Archi</color> : Thanks to " + PlanetName + " star system's planets got liberity, I see the Contros retreat from this star.");
                        SubShadowText.text = string.Format("<color=#494949>Benedict Archi : Thanks to " + PlanetName + " star system's planets got liberity, I see the Contros retreat from this star.</color>");
                    }
                    else if (RandomText == 2)
                    {
                        MainText.text = string.Format("<color=#FF7948>Delta Hurricane</color> : This " + PlanetName + " star system's planets took back free, Let me talk to Contros. like, Get out this star.");
                        MainShadowText.text = string.Format("<color=#494949>Delta Hurricane : This " + PlanetName + " star system's planets took back free, Let me talk to Contros. like, Get out this star.</color>");
                    }
                    else if (RandomText == 3)
                    {
                        MainText.text = string.Format("<color=#FF7948>Delta Hurricane</color> : The " + PlanetName + " star system has been in peace. And the Contros is retreating from this star. They know they lost here.");
                        MainShadowText.text = string.Format("<color=#494949>Delta Hurricane : The " + PlanetName + " star system has been in peace. And the Contros is retreating from this star. They know they lost here.</color>");
                    }
                }
                else if (WordPrintSystem.LanguageType == 2)
                {
                    if (RandomText == 0)
                    {
                        MainText.text = string.Format("<color=#47BEFF>베네딕트 아르키</color> : 이 " + PlanetName + " 항성계의 행성들이 자유를 얻으니 컨트로스도 알아서 항성에서 물러갈 줄은 아는 모양이군.");
                        MainShadowText.text = string.Format("<color=#494949>베네딕트 아르키 : 이 " + PlanetName + " 항성계의 행성들이 자유를 얻으니 컨트로스도 알아서 항성에서 물러갈 줄은 아는 모양이군.</color>");
                    }
                    else if (RandomText == 1)
                    {
                        MainText.text = string.Format("<color=#47BEFF>베네딕트 아르키</color> : " + PlanetName + " 항성계의 전 행성들이 해방된 덕분에 컨트로스가 항성에서 철수하고 있는 것이 보인다네.");
                        MainShadowText.text = string.Format("<color=#494949>베네딕트 아르키 : " + PlanetName + " 항성계의 전 행성들이 해방된 덕분에 컨트로스가 항성에서 철수하고 있는 것이 보인다네.</color>");
                    }
                    else if (RandomText == 2)
                    {
                        MainText.text = string.Format("<color=#FF7948>델타 허리케인</color> : 이번 " + PlanetName + " 항성계 행성들이 해방되었으니 컨트로스에게 한 마디 해야겠군. 여기 항성에서 나가라고 말이야.");
                        MainShadowText.text = string.Format("<color=#494949>델타 허리케인 : 이번 " + PlanetName + " 항성계 행성들이 해방되었으니 컨트로스에게 한 마디 해야겠군. 여기 항성에서 나가라고 말이야.</color>");
                    }
                    else if (RandomText == 3)
                    {
                        MainText.text = string.Format("<color=#FF7948>델타 허리케인</color> : " + PlanetName + " 항성계가 안정을 되찾았어. 그리고 컨트로스가 항성에서 철수하고 있지. 자신들이 이곳에서 패배했다는 걸 알고 있는 거야.");
                        MainShadowText.text = string.Format("<color=#494949>델타 허리케인 : " + PlanetName + " 항성계가 안정을 되찾았어. 그리고 컨트로스가 항성에서 철수하고 있지. 자신들이 이곳에서 패배했다는 걸 알고 있는 거야.</color>");
                    }
                }
                yield return new WaitForSeconds(7);
            }

            else if (number == 5.01f) //컨트로스 증원 함대
            {
                int RandomText = Random.Range(0, 4);
                if (WordPrintSystem.LanguageType == 1)
                {
                    if (RandomText == 0)
                    {
                        SubText.text = string.Format("<color=#47BEFF>Benedict Archi</color> : Contros reinforcement fleet! Be careful!");
                        SubShadowText.text = string.Format("<color=#494949>Benedict Archi : Contros reinforcement fleet! Be careful!</color>");
                    }
                    else if (RandomText == 1)
                    {
                        SubText.text = string.Format("<color=#47BEFF>Benedict Archi</color> : All fleets, prepare for the Contros reinforcement fleet!");
                        SubShadowText.text = string.Format("<color=#494949>Benedict Archi : All fleets, prepare for the Contros reinforcement fleet!</color>");
                    }
                    else if (RandomText == 2)
                    {
                        MainText.text = string.Format("<color=#FF7948>Delta Hurricane</color> : Conros is getting their reinforcement fleet. Be careful!");
                        MainShadowText.text = string.Format("<color=#494949>Delta Hurricane : Conros is getting their reinforcement fleet. Be careful!</color>");
                    }
                    else if (RandomText == 3)
                    {
                        MainText.text = string.Format("<color=#FF7948>Delta Hurricane</color> : It looks like we are getting hard battle cause that reinforcement fleet.");
                        MainShadowText.text = string.Format("<color=#494949>Delta Hurricane : It looks like we are getting hard battle cause that reinforcement fleet.</color>");
                    }
                }
                else if (WordPrintSystem.LanguageType == 2)
                {
                    if (RandomText == 0)
                    {
                        MainText.text = string.Format("<color=#47BEFF>베네딕트 아르키</color> : 컨트로스 증원 함대다! 조심하라!");
                        MainShadowText.text = string.Format("<color=#494949>베네딕트 아르키 : 컨트로스 증원 함대다! 조심하라!</color>");
                    }
                    else if (RandomText == 1)
                    {
                        MainText.text = string.Format("<color=#47BEFF>베네딕트 아르키</color> : 전 함대는 컨트로스 증원 함대에 대비하라!");
                        MainShadowText.text = string.Format("<color=#494949>베네딕트 아르키 : 전 함대는 컨트로스 증원 함대에 대비하라!</color>");
                    }
                    else if (RandomText == 2)
                    {
                        MainText.text = string.Format("<color=#FF7948>델타 허리케인</color> : 컨트로스가 증원 함대를 몰고 왔군. 조심해!");
                        MainShadowText.text = string.Format("<color=#494949>델타 허리케인 : 컨트로스가 증원 함대를 몰고 왔군. 조심해!</color>");
                    }
                    else if (RandomText == 3)
                    {
                        MainText.text = string.Format("<color=#FF7948>델타 허리케인</color> : 저 증원 함대 때문에 이번 전투가 좀 더 힘들어지는 군.");
                        MainShadowText.text = string.Format("<color=#494949>델타 허리케인 : 저 증원 함대 때문에 이번 전투가 좀 더 힘들어지는 군.</color>");
                    }
                }
                yield return new WaitForSeconds(7);
            }

            else if (number == 6.01f) //행성 해방 전투
            {
                if (BattleSave.Save1.FirstStart == false)
                {
                    int RandomText = Random.Range(0, 8);
                    if (WordPrintSystem.LanguageType == 1)
                    {
                        if (RandomText == 0)
                        {
                            SubText.text = string.Format("<color=#47BEFF>Benedict Archi</color> : Alright, We wil be in planet battle soon. Get ready.");
                            SubShadowText.text = string.Format("<color=#494949>Benedict Archi : Alright, We wil be in planet battle soon. Get ready.</color>");
                        }
                        else if (RandomText == 1)
                        {
                            SubText.text = string.Format("<color=#47BEFF>Benedict Archi</color> : Our fleet will start the planet battle soon.");
                            SubShadowText.text = string.Format("<color=#494949>Benedict Archi : Our fleet will start the planet battle soon.</color>");
                        }
                        else if (RandomText == 2)
                        {
                            MainText.text = string.Format("<color=#FF7948>Delta Hurricane</color> : If planet ground warfare is ready, Call me anytime.");
                            MainShadowText.text = string.Format("<color=#494949>Delta Hurricane : If planet ground warfare is ready, Call me anytime.</color>");
                        }
                        else if (RandomText == 3)
                        {
                            MainText.text = string.Format("<color=#FF7948>Delta Hurricane</color> : Is it a planet battle soon? yes, then I will get ready to engage.");
                            MainShadowText.text = string.Format("<color=#494949>Delta Hurricane : Is it a planet battle soon? yes, then I will get ready to engage</color>.");
                        }
                    }
                    else if (WordPrintSystem.LanguageType == 2)
                    {
                        if (RandomText == 0)
                        {
                            MainText.text = string.Format("<color=#47BEFF>베네딕트 아르키</color> : 좋아, 곧 행성 전투에 돌입하겠군. 준비하라.");
                            MainShadowText.text = string.Format("<color=#494949>베네딕트 아르키 : 좋아, 곧 행성 전투에 돌입하겠군. 준비하라.</color>");
                        }
                        else if (RandomText == 1)
                        {
                            MainText.text = string.Format("<color=#47BEFF>베네딕트 아르키</color> : 우리 함대가 곧 행성 전투를 시작할 것이다.");
                            MainShadowText.text = string.Format("<color=#494949>베네딕트 아르키 : 우리 함대가 곧 행성 전투를 시작할 것이다.</color>");
                        }
                        else if (RandomText == 2)
                        {
                            MainText.text = string.Format("<color=#FF7948>델타 허리케인</color> : 행성 지상전이 준비된다면 언제든 나를 부르도록.");
                            MainShadowText.text = string.Format("<color=#494949>델타 허리케인 : 행성 지상전이 준비된다면 언제든 나를 부르도록.</color>");
                        }
                        else if (RandomText == 3)
                        {
                            MainText.text = string.Format("<color=#FF7948>델타 허리케인</color> : 이제 곧 행성 전투인가? 그래, 그러면 나 또한 작전을 준비해야 겠군.");
                            MainShadowText.text = string.Format("<color=#494949>델타 허리케인 : 이제 곧 행성 전투인가? 그래, 그러면 나 또한 작전을 준비해야 겠군.</color>");
                        }
                    }
                    yield return new WaitForSeconds(7);
                }
            }
        }
        MainText.text = string.Format("");
        MainShadowText.text = string.Format("");
        MainTextPrintting = false;
        MainTextPrefab.SetActive(false);
    }

    //서브 대화 출력
    public IEnumerator SubCommunication(float number)
    {
        if (SubTextPrintting == false)
        {
            SubTextPrintting = true;
            SubTextPrefab.SetActive(true);

            if (number == 1.00f) //첫 시작(튜토리얼)
            {
                MainTextPrintting = false;
                if (WordPrintSystem.LanguageType == 1)
                {
                    yield return new WaitForSeconds(7);
                    SubText.text = string.Format("Flagship : Commander, Our force's battle signal has been detected near our ship.");
                    SubShadowText.text = string.Format("<color=#494949>Flagship : Commander, Our force's battle signal has been detected near our ship.</color>");
                    yield return new WaitForSeconds(3);
                    StartCoroutine(MainCommunication(1.01f));
                }
                else if (WordPrintSystem.LanguageType == 2)
                {
                    yield return new WaitForSeconds(7);
                    SubText.text = string.Format("아군 기함 : 사령관님, 우리 함대 근처에서 아군 교전 신호가 포착되고 있습니다.");
                    SubShadowText.text = string.Format("<color=#494949>아군 기함 : 사령관님, 우리 함대 근처에서 아군 교전 신호가 포착되고 있습니다.</color>");
                    yield return new WaitForSeconds(3);
                    StartCoroutine(MainCommunication(1.01f));
                }
                yield return new WaitForSeconds(4);
            }
            else if (number == 2.00f) //교전 지역에서 이겼을 때
            {
                int RandomText = Random.Range(0, 4);
                if (WordPrintSystem.LanguageType == 1)
                {
                    if (RandomText == 0)
                    {
                        SubText.text = string.Format("Our force ship : Thanck you, UCCIS. We will go to other mission area.");
                        SubShadowText.text = string.Format("<color=#494949>Our force ship : Thanck you, UCCIS. We will go to other mission area.</color>");
                    }
                    else if (RandomText == 1)
                    {
                        SubText.text = string.Format("Our force ship : Thanks to your support, we win this battle.");
                        SubShadowText.text = string.Format("<color=#494949>Our force ship : Thanks to your support, we win this battle.</color>");
                    }
                    else if (RandomText == 2)
                    {
                        SubText.text = string.Format("Our force ship : Now we will support other ships.");
                        SubShadowText.text = string.Format("<color=#494949>Our force ship : Now we will support other ships.</color>");
                    }
                    else if (RandomText == 3)
                    {
                        SubText.text = string.Format("Our force ship : Thank you for your support. We will go to other battle area.");
                        SubShadowText.text = string.Format("<color=#494949>Our force ship : Thank you for your support. We will go to other battle area.</color>");
                    }
                }
                else if (WordPrintSystem.LanguageType == 2)
                {
                    if (RandomText == 0)
                    {
                        SubText.text = string.Format("아군 함선 : 감사합니다, UCCIS. 저희는 이제 다른 임무 지역으로 가겠습니다.");
                        SubShadowText.text = string.Format("<color=#494949>아군 함선 : 감사합니다, UCCIS. 저희는 이제 다른 임무 지역으로 가겠습니다.</color>");
                    }
                    else if (RandomText == 1)
                    {
                        SubText.text = string.Format("아군 함선 : 덕분에 전투에서 이길 수 있었습니다.");
                        SubShadowText.text = string.Format("<color=#494949>아군 함선 : 덕분에 전투에서 이길 수 있었습니다.</color>");
                    }
                    else if (RandomText == 2)
                    {
                        SubText.text = string.Format("아군 함선 : 이제 우리는 다른 함대를 지원하겠습니다.");
                        SubShadowText.text = string.Format("<color=#494949>아군 함선 : 이제 우리는 다른 함대를 지원하겠습니다.</color>");
                    }
                    else if (RandomText == 3)
                    {
                        SubText.text = string.Format("아군 함선 : 사령관님의 지원에 감사드립니다. 이제 다른 교전 지역으로 이동하겠습니다.");
                        SubShadowText.text = string.Format("<color=#494949>아군 함선 : 사령관님의 지원에 감사드립니다. 이제 다른 교전 지역으로 이동하겠습니다.</color>");
                    }
                }
                if (RandomText <= 3)
                    yield return new WaitForSeconds(7);
            }

            else if (number == 3.00f) //기함 선체가 파괴되었을 때
            {
                int RandomText = Random.Range(0, 6);
                if (WordPrintSystem.LanguageType == 1)
                {
                    if (RandomText == 0)
                    {
                        SubText.text = string.Format("Flagship : Hull is destroying!");
                        SubShadowText.text = string.Format("<color=#494949>Flagship : Hull is destroying!</color>");
                    }
                    else if (RandomText == 1)
                    {
                        SubText.text = string.Format("Flagship : Ship damage detected!");
                        SubShadowText.text = string.Format("<color=#494949>Flagship : Ship damage detected!</color>");
                    }
                    else if (RandomText == 2)
                    {
                        SubText.text = string.Format("Flagship : Our ship is under damage!");
                        SubShadowText.text = string.Format("<color=#494949>Flagship : Our ship is under damage!</color>");
                    }
                    else if (RandomText == 3)
                    {
                        SubText.text = string.Format("Flagship : We've get hull damage!");
                        SubShadowText.text = string.Format("<color=#494949>Flagship : We've get hull damage!</color>");
                    }
                }
                else if (WordPrintSystem.LanguageType == 2)
                {
                    if (RandomText == 0)
                    {
                        SubText.text = string.Format("기함 : 선체가 파괴되고 있습니다!");
                        SubShadowText.text = string.Format("<color=#494949>기함 : 선체가 파괴되고 있습니다!</color>");
                    }
                    else if (RandomText == 1)
                    {
                        SubText.text = string.Format("기함 : 함선 타격 감지!");
                        SubShadowText.text = string.Format("<color=#494949>기함 : 함선 타격 감지!</color>");
                    }
                    else if (RandomText == 2)
                    {
                        SubText.text = string.Format("기함 : 우리 함선이 피해를 입고 있습니다!");
                        SubShadowText.text = string.Format("<color=#494949>기함 : 우리 함선이 피해를 입고 있습니다!</color>");
                    }
                    else if (RandomText == 3)
                    {
                        SubText.text = string.Format("기함 : 선체 피해를 입었습니다!");
                        SubShadowText.text = string.Format("<color=#494949>기함 : 선체 피해를 입었습니다!</color>");
                    }
                }
                if (RandomText <= 3)
                    yield return new WaitForSeconds(7);
            }

            else if (number == 4.00f) //기함 함포가 무력화 되었을 때
            {
                int RandomText = Random.Range(0, 6);
                if (WordPrintSystem.LanguageType == 1)
                {
                    if (RandomText == 0)
                    {
                        SubText.text = string.Format("Flagship : Cannon is down!");
                        SubShadowText.text = string.Format("<color=#494949>Flagship : Cannon is down!</color>");
                    }
                    else if (RandomText == 1)
                    {
                        SubText.text = string.Format("Flagship : Fire detected!");
                        SubShadowText.text = string.Format("<color=#494949>Flagship : Fire detected!</color>");
                    }
                    else if (RandomText == 2)
                    {
                        SubText.text = string.Format("Flagship : Our cannon has been destroyed!");
                        SubShadowText.text = string.Format("<color=#494949>Flagship : Our cannon has been destroyed!</color>");
                    }
                    else if (RandomText == 3)
                    {
                        SubText.text = string.Format("Flagship crew : Shit, cannon broken!");
                        SubShadowText.text = string.Format("<color=#494949>Flagship crew : Shit, cannon broken!</color>");
                    }
                    else if (RandomText == 4)
                    {
                        SubText.text = string.Format("Flagship crew : They destroy our cannon!");
                        SubShadowText.text = string.Format("<color=#494949>Flagship crew : They destroy our cannon!</color>");
                    }
                }
                else if (WordPrintSystem.LanguageType == 2)
                {
                    if (RandomText == 0)
                    {
                        SubText.text = string.Format("기함 : 함포 무력화!");
                        SubShadowText.text = string.Format("<color=#494949>기함 : 함포 무력화!</color>");
                    }
                    else if (RandomText == 1)
                    {
                        SubText.text = string.Format("기함 : 화재 발생!");
                        SubShadowText.text = string.Format("<color=#494949>기함 : 화재 발생!</color>");
                    }
                    else if (RandomText == 2)
                    {
                        SubText.text = string.Format("기함 : 함포가 무력화되었습니다!");
                        SubShadowText.text = string.Format("<color=#494949>기함 : 함포가 무력화되었습니다!</color>");
                    }
                    else if (RandomText == 3)
                    {
                        SubText.text = string.Format("기함 선원 : 젠장, 함포가 날아갔어!");
                        SubShadowText.text = string.Format("<color=#494949>기함 선원 : 젠장, 함포가 날아갔어!</color>");
                    }
                    else if (RandomText == 4)
                    {
                        SubText.text = string.Format("기함 선원 : 놈들이 우리 함포를 날려버렸어!");
                        SubShadowText.text = string.Format("<color=#494949>기함 선원 : 놈들이 우리 함포를 날려버렸어!</color>");
                    }
                }
                if (RandomText <= 4)
                    yield return new WaitForSeconds(7);
            }

            else if (number == 5.00f) //함선 선체가 파괴되었을 때
            {
                int RandomText = Random.Range(0, 20);
                if (WordPrintSystem.LanguageType == 1)
                {
                    if (RandomText == 0)
                    {
                        SubText.text = string.Format("Our force ship : Hull is destroying!");
                        SubShadowText.text = string.Format("<color=#494949>Our force ship : Hull is destroying!</color>");
                    }
                    else if (RandomText == 1)
                    {
                        SubText.text = string.Format("Our force ship : Ship damage detected!");
                        SubShadowText.text = string.Format("<color=#494949>Our force ship : Ship damage detected!</color>");
                    }
                    else if (RandomText == 2)
                    {
                        SubText.text = string.Format("Our force ship : Our ship is under damage!");
                        SubShadowText.text = string.Format("<color=#494949>Our force ship : Our ship is under damage!</color>");
                    }
                    else if (RandomText == 3)
                    {
                        SubText.text = string.Format("Our force ship : We've get hull damage!");
                        SubShadowText.text = string.Format("<color=#494949>Our force ship : We've get hull damage!</color>");
                    }
                    else if (RandomText == 4)
                    {
                        SubText.text = string.Format("Our force ship crew : Outside wall is going down.. Aaaaah!");
                        SubShadowText.text = string.Format("<color=#494949>Our force ship crew : Outside wall is going down.. Aaaaah!</color>");
                    }
                    else if (RandomText == 5)
                    {
                        SubText.text = string.Format("Our force ship crew : Hull is going down!");
                        SubShadowText.text = string.Format("<color=#494949>Our force ship crew : Hull is going down!</color>");
                    }
                    else if (RandomText == 6)
                    {
                        SubText.text = string.Format("Our force ship AI : Warnning. Hull damage detected.");
                        SubShadowText.text = string.Format("<color=#494949>Our force ship AI : Warnning. Hull damage detected.</color>");
                    }
                }
                else if (WordPrintSystem.LanguageType == 2)
                {
                    if (RandomText == 0)
                    {
                        SubText.text = string.Format("아군 함선 : 선체가 파괴되고 있습니다!");
                        SubShadowText.text = string.Format("<color=#494949>아군 함선 : 선체가 파괴되고 있습니다!</color>");
                    }
                    else if (RandomText == 1)
                    {
                        SubText.text = string.Format("아군 함선 : 함선 타격 감지!");
                        SubShadowText.text = string.Format("<color=#494949>아군 함선 : 함선 타격 감지!</color>");
                    }
                    else if (RandomText == 2)
                    {
                        SubText.text = string.Format("아군 함선 : 우리 함선이 피해를 입고 있습니다!");
                        SubShadowText.text = string.Format("<color=#494949>아군 함선 : 우리 함선이 피해를 입고 있습니다!</color>");
                    }
                    else if (RandomText == 3)
                    {
                        SubText.text = string.Format("아군 함선 : 선체 피해를 입었습니다!");
                        SubShadowText.text = string.Format("<color=#494949>아군 함선 : 선체 피해를 입었습니다!</color>");
                    }
                    else if (RandomText == 4)
                    {
                        SubText.text = string.Format("아군 함선 선원 : 외벽이 무너졌... 으아아악!");
                        SubShadowText.text = string.Format("<color=#494949>아군 함선 선원 : 외벽이 무너졌... 으아아악!</color>");
                    }
                    else if (RandomText == 5)
                    {
                        SubText.text = string.Format("아군 함선 선원 : 선체가 파괴되고 있어!");
                        SubShadowText.text = string.Format("<color=#494949>아군 함선 선원 : 선체가 파괴되고 있어!</color>");
                    }
                    else if (RandomText == 6)
                    {
                        SubText.text = string.Format("아군 함선 AI : 경고. 선체 데미지 감지.");
                        SubShadowText.text = string.Format("<color=#494949>아군 함선 AI : 경고. 선체 데미지 감지.</color>");
                    }
                }
                if (RandomText <= 6)
                    yield return new WaitForSeconds(7);
            }

            else if (number == 6.00f) //함선 함포가 무력화 되었을 때
            {
                int RandomText = Random.Range(0, 20);
                if (WordPrintSystem.LanguageType == 1)
                {
                    if (RandomText == 0)
                    {
                        SubText.text = string.Format("Our force ship : Cannon is down!");
                        SubShadowText.text = string.Format("<color=#494949>Our force ship : Cannon is down!</color>");
                    }
                    else if (RandomText == 1)
                    {
                        SubText.text = string.Format("Our force ship : Fire detected!");
                        SubShadowText.text = string.Format("<color=#494949>Our force ship : Fire detected!</color>");
                    }
                    else if (RandomText == 2)
                    {
                        SubText.text = string.Format("Our force ship : Our cannon has been destroyed!");
                        SubShadowText.text = string.Format("<color=#494949>Our force ship : Our cannon has been destroyed!</color>");
                    }
                    else if (RandomText == 3)
                    {
                        SubText.text = string.Format("Our force ship crew : Shit, the cannon has been broken!");
                        SubShadowText.text = string.Format("<color=#494949>Our force ship crew : Shit, the cannon has been broken!</color>");
                    }
                    else if (RandomText == 4)
                    {
                        SubText.text = string.Format("Our force ship crew : They destroy our cannon!");
                        SubShadowText.text = string.Format("<color=#494949>Our force ship crew : They destroy our cannon!</color>");
                    }
                }
                else if (WordPrintSystem.LanguageType == 2)
                {
                    if (RandomText == 0)
                    {
                        SubText.text = string.Format("아군 함선 : 함포 무력화!");
                        SubShadowText.text = string.Format("<color=#494949>아군 함선 : 함포 무력화!</color>");
                    }
                    else if (RandomText == 1)
                    {
                        SubText.text = string.Format("아군 함선 : 화재 발생!");
                        SubShadowText.text = string.Format("<color=#494949>아군 함선 : 화재 발생!</color>");
                    }
                    else if (RandomText == 2)
                    {
                        SubText.text = string.Format("아군 함선 : 함포가 무력화되었습니다!");
                        SubShadowText.text = string.Format("<color=#494949>아군 함선 : 함포가 무력화되었습니다!</color>");
                    }
                    else if (RandomText == 3)
                    {
                        SubText.text = string.Format("아군 함선 선원 : 젠장, 함포가 날아갔어!");
                        SubShadowText.text = string.Format("<color=#494949>아군 함선 : 젠장, 함포가 날아갔어!</color>");
                    }
                    else if (RandomText == 4)
                    {
                        SubText.text = string.Format("아군 함선 선원 : 놈들이 우리 함포를 날려버렸어!");
                        SubShadowText.text = string.Format("<color=#494949>아군 함선 : 놈들이 우리 함포를 날려버렸어!</color>");
                    }
                }
                if (RandomText <= 4)
                    yield return new WaitForSeconds(7);
            }

            else if (number == 7.01f) //함대가 회복을 시작할 때
            {
                int RandomText = Random.Range(0, 4);
                if (WordPrintSystem.LanguageType == 1)
                {
                    if (RandomText == 0)
                    {
                        SubText.text = string.Format("Fleet : Start to hull repair.");
                        SubShadowText.text = string.Format("<color=#494949>Fleet : Start to hull repair.</color>");
                    }
                    else if (RandomText == 1)
                    {
                        SubText.text = string.Format("Fleet : We'll start to repair.");
                        SubShadowText.text = string.Format("<color=#494949>Fleet : We'll start to repair.</color>");
                    }
                    else if (RandomText == 2)
                    {
                        SubText.text = string.Format("Fleet : Activate hull repair matrix.");
                        SubShadowText.text = string.Format("<color=#494949>Fleet : Activate hull repair matrix.</color>");
                    }
                    else if (RandomText == 3)
                    {
                        SubText.text = string.Format("Fleet : Now we can take a break.");
                        SubShadowText.text = string.Format("<color=#494949>Fleet : Now we can take a break.</color>");
                    }
                }
                else if (WordPrintSystem.LanguageType == 2)
                {
                    if (RandomText == 0)
                    {
                        SubText.text = string.Format("함대 : 선체 수리를 시작합니다.");
                        SubShadowText.text = string.Format("<color=#494949>함대 : 선체 수리를 시작합니다.</color>");
                    }
                    else if (RandomText == 1)
                    {
                        SubText.text = string.Format("함대 : 손상된 선체를 수리하겠습니다.");
                        SubShadowText.text = string.Format("<color=#494949>함대 : 손상된 선체를 수리하겠습니다.</color>");
                    }
                    else if (RandomText == 2)
                    {
                        SubText.text = string.Format("함대 : 선체 복구 매트릭스 가동.");
                        SubShadowText.text = string.Format("<color=#494949>함대 : 선체 복구 매트릭스 가동.</color>");
                    }
                    else if (RandomText == 3)
                    {
                        SubText.text = string.Format("함대 : 이제야 한숨 돌릴 수 있겠군요.");
                        SubShadowText.text = string.Format("<color=#494949>함대 : 이제야 한숨 돌릴 수 있겠군요.</color>");
                    }
                }
                if (RandomText <= 3)
                    yield return new WaitForSeconds(7);
            }

            else if (number == 7.02f) //함대가 회복을 완료했을 때
            {
                int RandomText = Random.Range(0, 4);
                if (WordPrintSystem.LanguageType == 1)
                {
                    if (RandomText == 0)
                    {
                        SubText.text = string.Format("Ship : Repair complete.");
                        SubShadowText.text = string.Format("<color=#494949>Ship : Repair complete.</color>");
                    }
                    else if (RandomText == 1)
                    {
                        SubText.text = string.Format("Ship : Restore complete.");
                        SubShadowText.text = string.Format("<color=#494949>Ship : Restore complete.</color>");
                    }
                    else if (RandomText == 2)
                    {
                        SubText.text = string.Format("Ship : Repair is completed and we are ready to fight.");
                        SubShadowText.text = string.Format("<color=#494949>Ship : Repair is completed and we are ready to fight.</color>");
                    }
                    else if (RandomText == 3)
                    {
                        SubText.text = string.Format("Ship : Ready to fight again.");
                        SubShadowText.text = string.Format("<color=#494949>Ship : Ready to fight again.</color>");
                    }
                }
                else if (WordPrintSystem.LanguageType == 2)
                {
                    if (RandomText == 0)
                    {
                        SubText.text = string.Format("함선 : 수리 완료.");
                        SubShadowText.text = string.Format("<color=#494949>함선 : 수리 완료.</color>");
                    }
                    else if (RandomText == 1)
                    {
                        SubText.text = string.Format("함선 : 복구 완료.");
                        SubShadowText.text = string.Format("<color=#494949>함선 : 복구 완료.</color>");
                    }
                    else if (RandomText == 2)
                    {
                        SubText.text = string.Format("함선 : 수리가 완료되어 교전 준비가 끝났습니다.");
                        SubShadowText.text = string.Format("<color=#494949>함선 : 수리가 완료되어 교전 준비가 끝났습니다.</color>");
                    }
                    else if (RandomText == 3)
                    {
                        SubText.text = string.Format("함선 : 다시 싸울 준비가 되었습니다.");
                        SubShadowText.text = string.Format("<color=#494949>함선 : 다시 싸울 준비가 되었습니다.</color>");
                    }
                }
                if (RandomText <= 3)
                    yield return new WaitForSeconds(7);
            }

            else if (number == 8.00f) //함선이 격침되었을 때
            {
                int RandomText = Random.Range(0, 6);
                if (WordPrintSystem.LanguageType == 1)
                {
                    if (RandomText == 0)
                    {
                        SubText.text = string.Format("Ship crew : Abandon ship! Abandon ship!");
                        SubShadowText.text = string.Format("<color=#494949>Ship crew : Abandon ship! Abandon ship!</color>");
                    }
                    else if (RandomText == 1)
                    {
                        SubText.text = string.Format("Ship crew : Where is escape pod... Aaaaah!!");
                        SubShadowText.text = string.Format("<color=#494949>Ship crew : Where is escape pod... Aaaaah!!</color>");
                    }
                    else if (RandomText == 2)
                    {
                        SubText.text = string.Format("Ship crew : Escape! Get out of here!");
                        SubShadowText.text = string.Format("<color=#494949>Ship crew : Escape! Get out of here!</color>");
                    }
                    else if (RandomText == 3)
                    {
                        SubText.text = string.Format("Ship crew : We need to get out of here now!");
                        SubShadowText.text = string.Format("<color=#494949>Ship crew : We need to get out of here now!</color>");
                    }
                }
                else if (WordPrintSystem.LanguageType == 2)
                {
                    if (RandomText == 0)
                    {
                        SubText.text = string.Format("함선 선원 : 배를 버려라! 배를 버리라고!");
                        SubShadowText.text = string.Format("<color=#494949>함선 선원 : 배를 버려라! 배를 버려라!</color>");
                    }
                    else if (RandomText == 1)
                    {
                        SubText.text = string.Format("함선 선원 : 탈출포드는 어디있는... 으아아아악!!");
                        SubShadowText.text = string.Format("<color=#494949>함선 선원 : 탈출포드는 어디있는... 으아아아악!!</color>");
                    }
                    else if (RandomText == 2)
                    {
                        SubText.text = string.Format("함선 선원 : 탈출한다! 이곳에서 빨리 나가!");
                        SubShadowText.text = string.Format("<color=#494949>함선 선원 : 탈출한다! 이곳에서 빨리 나가!</color>");
                    }
                    else if (RandomText == 3)
                    {
                        SubText.text = string.Format("함선 선원 : 이곳에서 빨리 탈출해야해!");
                        SubShadowText.text = string.Format("<color=#494949>함선 선원 : 이곳에서 빨리 탈출해야해!</color>");
                    }
                }
                if (RandomText <= 3)
                    yield return new WaitForSeconds(7);
            }

            else if (number == 9.00f) //컨트로스와의 교전이 시작되었을 때
            {
                int RandomText = Random.Range(0, 20);
                if (WordPrintSystem.LanguageType == 1)
                {
                    if (RandomText == 0)
                    {
                        SubText.text = string.Format("Fleet : Engage!");
                        SubShadowText.text = string.Format("<color=#494949>Fleet : Engage!</color>");
                    }
                    else if (RandomText == 1)
                    {
                        SubText.text = string.Format("Fleet : Contros fleet detected! Engage!");
                        SubShadowText.text = string.Format("<color=#494949>Fleet : Contros fleet detected! Engage!</color>");
                    }
                    else if (RandomText == 2)
                    {
                        SubText.text = string.Format("Fleet : Contros fleet! Fire!");
                        SubShadowText.text = string.Format("<color=#494949>Fleet : Contros fleet! Fire!</color>");
                    }
                    else if (RandomText == 3)
                    {
                        SubText.text = string.Format("Fleet : For the Nariha!");
                        SubShadowText.text = string.Format("<color=#494949>Fleet : For the Nariha!</color>");
                    }
                    else if (RandomText == 4)
                    {
                        SubText.text = string.Format("Fleet : Contros! Let's drive them out!");
                        SubShadowText.text = string.Format("<color=#494949>Fleet : Contros! Let's drive them out!</color>");
                    }
                    else if (RandomText == 5)
                    {
                        SubText.text = string.Format("Fleet : May the Constellation time pass by. Engage the target!");
                        SubShadowText.text = string.Format("<color=#494949>Fleet : May the Constellation time pass by. Engage the target!</color>");
                    }
                    else if (RandomText == 6)
                    {
                        SubText.text = string.Format("Fleet : For the Ilux!");
                        SubShadowText.text = string.Format("<color=#494949>Fleet : For the Ilux!</color>");
                    }
                    else if (RandomText == 7)
                    {
                        SubText.text = string.Format("Fleet : Death to you Contros!");
                        SubShadowText.text = string.Format("<color=#494949>Fleet : Death to you Contros!</color>");
                    }
                    else if (RandomText == 8)
                    {
                        SubText.text = string.Format("Fleet : Iceu Ershrow go to hell!");
                        SubShadowText.text = string.Format("<color=#494949>Fleet : Iceu Ershrow go to hell!</color>");
                    }
                }
                else if (WordPrintSystem.LanguageType == 2)
                {
                    if (RandomText == 0)
                    {
                        SubText.text = string.Format("함대 : 교전을 시작합니다!");
                        SubShadowText.text = string.Format("<color=#494949>함대 : 교전을 시작합니다!</color>");
                    }
                    else if (RandomText == 1)
                    {
                        SubText.text = string.Format("함대 : 컨트로스 함대 감지! 교전 개시!");
                        SubShadowText.text = string.Format("<color=#494949>함대 : 컨트로스 함대 감지! 교전 개시!</color>");
                    }
                    else if (RandomText == 2)
                    {
                        SubText.text = string.Format("함대 : 컨트로스 함대다! 사격하라!");
                        SubShadowText.text = string.Format("<color=#494949>함대 : 컨트로스 함대다! 사격하라!</color>");
                    }
                    else if (RandomText == 3)
                    {
                        SubText.text = string.Format("함대 : 나리하를 위하여!");
                        SubShadowText.text = string.Format("<color=#494949>함대 : 나리하를 위하여!</color>");
                    }
                    else if (RandomText == 4)
                    {
                        SubText.text = string.Format("함대 : 컨트로스다! 놈들을 몰아내자!");
                        SubShadowText.text = string.Format("<color=#494949>함대 : 컨트로스다! 놈들을 몰아내자!</color>");
                    }
                    else if (RandomText == 5)
                    {
                        SubText.text = string.Format("함대 : 별자리 시간이 흐르길. 전투 개시!");
                        SubShadowText.text = string.Format("<color=#494949>함대 : 별자리 시간이 흐르길. 전투 개시!</color>");
                    }
                    else if (RandomText == 6)
                    {
                        SubText.text = string.Format("함대 : 아이룩스를 위하여!");
                        SubShadowText.text = string.Format("<color=#494949>함대 : 아이룩스를 위하여!</color>");
                    }
                    else if (RandomText == 7)
                    {
                        SubText.text = string.Format("함대 : 너희 컨트로스 놈들에게 죽음을!");
                        SubShadowText.text = string.Format("<color=#494949>함대 : 너희 컨트로스 놈들에게 죽음을!</color>");
                    }
                    else if (RandomText == 8)
                    {
                        SubText.text = string.Format("함대 : 아이스 에르쇼는 지옥에나 가라!");
                        SubShadowText.text = string.Format("<color=#494949>함대 : 아이스 에르쇼는 지옥에나 가라!</color>");
                    }
                }
                if (RandomText <= 8)
                    yield return new WaitForSeconds(7);
            }

            else if (number == 10.01f) //나리하의 교전이 시작되었을 때(슬로리어스)
            {
                int RandomText = Random.Range(0, 20);
                if (WordPrintSystem.LanguageType == 1)
                {
                    if (RandomText == 0)
                    {
                        SubText.text = string.Format("<color=#C900FF>Slorius fleet</color> : Receive Iceu Ershrow's mercy.");
                        SubShadowText.text = string.Format("<color=#494949>Slorius fleet : Receive Iceu Ershrow's mercy.</color>");
                    }
                    else if (RandomText == 1)
                    {
                        SubText.text = string.Format("<color=#C900FF>Slorius fleet</color> : To reject our mercy is to reject your existence.");
                        SubShadowText.text = string.Format("<color=#494949>Slorius fleet : To reject our mercy is to reject your existence.</color>");
                    }
                    else if (RandomText == 2)
                    {
                        SubText.text = string.Format("<color=#C900FF>Slorius fleet</color> : We'll see how you are strong, human.");
                        SubShadowText.text = string.Format("<color=#494949>Slorius fleet : We'll see how you are strong, human.</color>");
                    }
                    else if (RandomText == 3)
                    {
                        SubText.text = string.Format("<color=#C900FF>Slorius fleet</color> : Your resistance is meaningless.");
                        SubShadowText.text = string.Format("<color=#494949>Slorius fleet : Your resistance is meaningless.</color>");
                    }
                    else if (RandomText == 4)
                    {
                        SubText.text = string.Format("<color=#C900FF>Slorius fleet</color> : Our behavior for you is meanning of the our Aaileshi Parliament.");
                        SubShadowText.text = string.Format("<color=#494949>Slorius fleet : Our behavior for you is meanning of the our Aaileshi Parliament.</color>");
                    }
                    else if (RandomText == 5)
                    {
                        SubText.text = string.Format("<color=#C900FF>Slorius fleet</color> : There is no hope for you.");
                        SubShadowText.text = string.Format("<color=#494949>Slorius fleet : There is no hope for you.</color>");
                    }
                    else if (RandomText == 6)
                    {
                        SubText.text = string.Format("<color=#C900FF>Slorius fleet</color> : Did you think we forgot the battle of Ilux?");
                        SubShadowText.text = string.Format("<color=#494949>Slorius fleet : Did you think we forgot the battle of Ilux?</color>");
                    }
                    else if (RandomText == 7)
                    {
                        SubText.text = string.Format("<color=#C900FF>Slorius fleet</color> : The place you stand here will be new our home.");
                        SubShadowText.text = string.Format("<color=#494949>Slorius fleet : The place you stand here will be new our home.</color>");
                    }
                    else if (RandomText == 8)
                    {
                        SubText.text = string.Format("<color=#C900FF>Slorius fleet</color> : You will pay as you take our Iiock Ashi's life.");
                        SubShadowText.text = string.Format("<color=#494949>Slorius fleet : You will pay as you take our Iiock Ashi's life.</color>");
                    }
                }
                else if (WordPrintSystem.LanguageType == 2)
                {
                    if (RandomText == 0)
                    {
                        SubText.text = string.Format("<color=#C900FF>슬로리어스 함대</color> : 아이스 에르쇼 슈라이의 자비를 받으라.");
                        SubShadowText.text = string.Format("<color=#494949>슬로리어스 함대 : 아이스 에르쇼 슈라이의 자비를 받으라.</color>");
                    }
                    else if (RandomText == 1)
                    {
                        SubText.text = string.Format("<color=#C900FF>슬로리어스 함대</color> : 우리의 자비를 거부하는 것은 너희 존재를 거부하는 것이다.");
                        SubShadowText.text = string.Format("<color=#494949>슬로리어스 함대 : 우리의 자비를 거부하는 것은 너희 존재를 거부하는 것이다.</color>");
                    }
                    else if (RandomText == 2)
                    {
                        SubText.text = string.Format("<color=#C900FF>슬로리어스 함대</color> : 너희 인간이 얼마나 강력한지 지켜보겠다.");
                        SubShadowText.text = string.Format("<color=#494949>슬로리어스 함대 : 너희 인간이 얼마나 강력한지 지켜보겠다.</color>");
                    }
                    else if (RandomText == 3)
                    {
                        SubText.text = string.Format("<color=#C900FF>슬로리어스 함대</color> : 저항은 무의미하다.");
                        SubShadowText.text = string.Format("<color=#494949>슬로리어스 함대 : 저항은 무의미하다.</color>");
                    }
                    else if (RandomText == 4)
                    {
                        SubText.text = string.Format("<color=#C900FF>슬로리어스 함대</color> : 너희에 대한 우리의 행동은 우리 아아이레쉬 의희의 뜻이로다.");
                        SubShadowText.text = string.Format("<color=#494949>슬로리어스 함대 : 너희에 대한 우리의 행동은 우리 아아이레쉬 의희의 뜻이로다.</color>");
                    }
                    else if (RandomText == 5)
                    {
                        SubText.text = string.Format("<color=#C900FF>슬로리어스 함대</color> : 몸부림 쳐봐야 아무 소용 없다.");
                        SubShadowText.text = string.Format("<color=#494949>슬로리어스 함대 : 몸부림 쳐봐야 아무 소용 없다.</color>");
                    }
                    else if (RandomText == 6)
                    {
                        SubText.text = string.Format("<color=#C900FF>슬로리어스 함대</color> : 우리가 아이룩스 전투를 망각하리라 생각했는가?");
                        SubShadowText.text = string.Format("<color=#494949>슬로리어스 함대 : 우리가 아이룩스 전투를 망각하리라 생각했는가?</color>");
                    }
                    else if (RandomText == 7)
                    {
                        SubText.text = string.Format("<color=#C900FF>슬로리어스 함대</color> : 너희들이 있는 곳이 곧 우리의 새로운 보금자리가 될 것이다.");
                        SubShadowText.text = string.Format("<color=#494949>슬로리어스 함대 : 너희들이 있는 곳이 곧 우리의 새로운 보금자리가 될 것이다.</color>");
                    }
                    else if (RandomText == 8)
                    {
                        SubText.text = string.Format("<color=#C900FF>슬로리어스 함대</color> : 우리 이이옥아쉬의 목숨을 앗아간 너희들은 대가를 치를 것이다.");
                        SubShadowText.text = string.Format("<color=#494949>슬로리어스 함대 : 우리 이이옥아쉬의 목숨을 앗아간 너희들은 대가를 치를 것이다.</color>");
                    }
                }
                if (RandomText <= 8)
                    yield return new WaitForSeconds(7);
            }

            else if (number == 10.02f) //나리하의 교전이 시작되었을 때(칸타크리)
            {
                int RandomText = Random.Range(0, 20);
                if (WordPrintSystem.LanguageType == 1)
                {
                    if (RandomText == 0)
                    {
                        SubText.text = string.Format("<color=#FF5B51>Kantakri fleet</color> : We start to fight human.");
                        SubShadowText.text = string.Format("<color=#494949>Kantakri fleet : We start to fight human.</color>");
                    }
                    else if (RandomText == 1)
                    {
                        SubText.text = string.Format("<color=#FF5B51>Kantakri fleet</color> : As we see you, Our Prokrasist value increase.");
                        SubShadowText.text = string.Format("<color=#494949>Kantakri fleet : As we see you, Our Prokrasist value increase.</color>");
                    }
                    else if (RandomText == 2)
                    {
                        SubText.text = string.Format("<color=#FF5B51>Kantakri fleet</color> : We hope this battle will be stronger than our independence battle.");
                        SubShadowText.text = string.Format("<color=#494949>Kantakri fleet : We hope this battle will be stronger than our independence battle.</color>");
                    }
                    else if (RandomText == 3)
                    {
                        SubText.text = string.Format("<color=#FF5B51>Kantakri fleet</color> : It's good opportunity to be a Hunter by you.");
                        SubShadowText.text = string.Format("<color=#494949>Kantakri fleet : It's good opportunity to be a Hunter by you.</color>");
                    }
                    else if (RandomText == 4)
                    {
                        SubText.text = string.Format("<color=#FF5B51>Kantakri fleet</color> : Soon, Our behaivor will print a value of your destroy.");
                        SubShadowText.text = string.Format("<color=#494949>Kantakri fleet : Soon, Our behaivor will print a value of your destroy.</color>");
                    }
                    else if (RandomText == 5)
                    {
                        SubText.text = string.Format("<color=#FF5B51>Kantakri fleet</color> : We do not know about death number.");
                        SubShadowText.text = string.Format("<color=#494949>Kantakri fleet : We do not know about death number.</color>");
                    }
                    else if (RandomText == 6)
                    {
                        SubText.text = string.Format("<color=#FF5B51>Kantakri fleet</color> : Make me a glorious Hunter right now.");
                        SubShadowText.text = string.Format("<color=#494949>Kantakri fleet : Make me a glorious Hunter right now.</color>");
                    }
                    else if (RandomText == 7)
                    {
                        SubText.text = string.Format("<color=#FF5B51>Kantakri fleet</color> : I'm looking forward to the battle. The Prokrasist increase.");
                        SubShadowText.text = string.Format("<color=#494949>Kantakri fleet : I'm looking forward to the battle. The Prokrasist increase.</color>");
                    }
                    else if (RandomText == 8)
                    {
                        SubText.text = string.Format("<color=#FF5B51>Kantakri fleet</color> : Kakros-Taijaelos 1389 will remember you.");
                        SubShadowText.text = string.Format("<color=#494949>Kantakri fleet : Kakros-Taijaelos 1389 will remember you.</color>");
                    }
                }
                else if (WordPrintSystem.LanguageType == 2)
                {
                    if (RandomText == 0)
                    {
                        SubText.text = string.Format("<color=#FF5B51>칸타크리 함대</color> : 인간과의 교전을 시작한다.");
                        SubShadowText.text = string.Format("<color=#494949>칸타크리 함대 : 인간과의 교전을 시작한다.</color>");
                    }
                    else if (RandomText == 1)
                    {
                        SubText.text = string.Format("<color=#FF5B51>칸타크리 함대</color> : 너희를 바라보니, 우리 프로크래시스트 값이 치솟고 있다.");
                        SubShadowText.text = string.Format("<color=#494949>칸타크리 함대 : 너희를 바라보니, 우리 프로크래시스트 값이 치솟고 있다.</color>");
                    }
                    else if (RandomText == 2)
                    {
                        SubText.text = string.Format("<color=#FF5B51>칸타크리 함대</color> : 우리의 독립전쟁보다 더 강력한 전쟁이 되길 기대하겠다.");
                        SubShadowText.text = string.Format("<color=#494949>칸타크리 함대 : 우리의 독립전쟁보다 더 강력한 전쟁이 되길 기대하겠다.</color>");
                    }
                    else if (RandomText == 3)
                    {
                        SubText.text = string.Format("<color=#FF5B51>칸타크리 함대</color> : 너희들에 의해 헌터가 되기 아주 좋은 기회다.");
                        SubShadowText.text = string.Format("<color=#494949>칸타크리 함대 : 너희들에 의해 헌터가 되기 아주 좋은 기회다.</color>");
                    }
                    else if (RandomText == 4)
                    {
                        SubText.text = string.Format("<color=#FF5B51>칸타크리 함대</color> : 곧 우리의 행동이 너희들의 파괴 값을 산출할 것이다.");
                        SubShadowText.text = string.Format("<color=#494949>칸타크리 함대 : 곧 우리의 행동이 너희들의 파괴 값을 산출할 것이다.</color>");
                    }
                    else if (RandomText == 5)
                    {
                        SubText.text = string.Format("<color=#FF5B51>칸타크리 함대</color> : 우리는 죽음에 대한 값을 모른다.");
                        SubShadowText.text = string.Format("<color=#494949>칸타크리 함대 : 우리는 죽음에 대한 값을 모른다.</color>");
                    }
                    else if (RandomText == 6)
                    {
                        SubText.text = string.Format("<color=#FF5B51>칸타크리 함대</color> : 어서 우리를 영광스러운 헌터로 만들어라.");
                        SubShadowText.text = string.Format("<color=#494949>칸타크리 함대 : 어서 우리를 영광스러운 헌터로 만들어라.</color>");
                    }
                    else if (RandomText == 7)
                    {
                        SubText.text = string.Format("<color=#FF5B51>칸타크리 함대</color> : 전투가 기대된다. 프로크래시스트가 치솟는다.");
                        SubShadowText.text = string.Format("<color=#494949>칸타크리 함대 : 전투가 기대된다. 프로크래시스트가 치솟는다.</color>");
                    }
                    else if (RandomText == 8)
                    {
                        SubText.text = string.Format("<color=#FF5B51>칸타크리 함대</color> : 카크로스-타이제로스 1389께서 너희를 기억할 것이다.");
                        SubShadowText.text = string.Format("<color=#494949>칸타크리 함대 : 카크로스-타이제로스 1389께서 너희를 기억할 것이다.</color>");
                    }
                }
                if (RandomText <= 8)
                    yield return new WaitForSeconds(7);
            }
        }
        SubText.text = string.Format("");
        SubShadowText.text = string.Format("");
        SubTextPrintting = false;
        SubTextPrefab.SetActive(false);
    }
}