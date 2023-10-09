using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class LiveCommunicationSystem : MonoBehaviour
{
    [Header("��ũ��Ʈ")]
    public TutorialSystem TutorialSystem;
    public WordPrintSystem WordPrintSystem;

    [Header("��ȭ �ؽ�Ʈ")]
    public GameObject MainTextPrefab;
    public GameObject SubTextPrefab;
    public Text MainText;
    public Text MainShadowText;
    public Text SubText;
    public Text SubShadowText;
    public string PlanetName;

    [Header("��ȭ ��� ����")]
    private bool MainTextPrintting = false;
    private bool SubTextPrintting = false;

    //���� ��ȭ ���
    public IEnumerator MainCommunication(float number)
    {
        if (MainTextPrintting == false)
        {
            MainTextPrintting = true;
            MainTextPrefab.SetActive(true);

            if (number == 1.00f) //ù ����(Ʃ�丮��)
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
                    MainText.text = string.Format("<color=#47BEFF>���׵�Ʈ �Ƹ�Ű</color> : ����, ���� ���� ��ɺ� �ֿܼ� �����ߴ�. ȭ���� ���� ���԰� �Լ����� ����ϰ� �ִ� ����� �� ���̴� ��.");
                    MainShadowText.text = string.Format("<color=#494949>���׵�Ʈ �Ƹ�Ű : ����, ���� ���� ��ɺ� �ֿܼ� �����ߴ�. ȭ���� ���� ���԰� �Լ����� ����ϰ� �ִ� ����� �� ���̴� ��.</color>");
                    yield return new WaitForSeconds(7);
                    MainText.text = string.Format("<color=#47BEFF>���׵�Ʈ �Ƹ�Ű</color> : �ع� ������ �����ϱ� ���� �Դ� ����� �ٽ� Ȯ���غ��ڴ�.");
                    MainShadowText.text = string.Format("<color=#494949>���׵�Ʈ �Ƹ�Ű : �ع� ������ �����ϱ� ���� �Դ� ����� �ٽ� Ȯ���غ��ڴ�.</color>");
                    yield return new WaitForSeconds(7);
                    MainText.text = string.Format("<color=#47BEFF>���׵�Ʈ �Ƹ�Ű</color> : ��� �Լ��� ���� ȭ�� ��ġ�� ���� �̵������ �ǽ��Ѵ�. ȭ�� ��ġ�� ���� ���� �Դ밡 ����������.");
                    MainShadowText.text = string.Format("<color=#494949>���׵�Ʈ �Ƹ�Ű : ��� �Լ��� ���� ȭ�� ��ġ�� ���� �̵������ �ǽ��Ѵ�. ȭ�� ��ġ�� ���� ���� �Դ밡 ����������.</color>");
                    yield return new WaitForSeconds(7);
                    MainText.text = string.Format("<color=#47BEFF>���׵�Ʈ �Ƹ�Ű</color> : �츮 �������� ��κ��� �Դ�� ���� �߽� ������ �����ϱ� ������, ������ �߽����� �Լ����� �Բ� ������ ���̴�.");
                    MainShadowText.text = string.Format("<color=#494949>���׵�Ʈ �Ƹ�Ű : �츮 �������� ��κ��� �Դ�� ���� �߽� ������ �����ϱ� ������, ������ �߽����� �Լ����� �Բ� ������ ���̴�.</color>");
                    yield return new WaitForSeconds(7);
                    MainText.text = string.Format("<color=#47BEFF>���׵�Ʈ �Ƹ�Ű</color> : �׸��� �� �Դ밡 �츮 �Դ��� ���� ������ �ִٸ�, �Լ����� ������ ��� �ǽ��� ���̴�.");
                    MainShadowText.text = string.Format("<color=#494949>���׵�Ʈ �Ƹ�Ű : �׸��� �� �Դ밡 �츮 �Դ��� ���� ������ �ִٸ�, �Լ����� ������ ��� �ǽ��� ���̴�.</color>");
                }
                StartCoroutine(SubCommunication(1.00f));
                yield return new WaitForSeconds(7);
            }
            else if (number == 1.01f) //ù ����(Ʃ�丮��)
            {
                if (WordPrintSystem.LanguageType == 1)
                {
                    MainText.text = string.Format("<color=#47BEFF>Benedict Archi</color> : Copy. Then all we need is to warp fleet into the battle area and support the our force ships.");
                    MainShadowText.text = string.Format("<color=#494949>Benedict Archi : Copy. Then all we need is to warp fleet into the battle area and support the our force ships.</color>");
                }
                else if (WordPrintSystem.LanguageType == 2)
                {
                    MainText.text = string.Format("<color=#47BEFF>���׵�Ʈ �Ƹ�Ű</color> : Ȯ���ߴ�. �׷� ���� �ش� ���� �������� �Դ븦 �����Ͽ� �Ʊ� �Լ��� �����϶�.");
                    MainShadowText.text = string.Format("<color=#494949>���׵�Ʈ �Ƹ�Ű : Ȯ���ߴ�. �׷� ���� �ش� ���� �������� �Դ븦 �����Ͽ� �Ʊ� �Լ��� �����϶�.</color>");
                }
                yield return new WaitForSeconds(7);
                TutorialSystem.PopUpUniverseMapButton();
            }
            else if (number == 1.02f) //ī�޶� Ȱ��ȭ �� ��Ÿ���콺 �۷��þ� Ȱ��ȭ
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
                    MainText.text = string.Format("<color=#47BEFF>���׵�Ʈ �Ƹ�Ű</color> : ���ߴ�. ���� ���������� �༺ �ع� �غ� �Ϸ�� �� ����.");
                    MainShadowText.text = string.Format("<color=#494949>���׵�Ʈ �Ƹ�Ű : ���ߴ�. ���� ���������� �༺ �ع� �غ� �Ϸ�� �� ����.</color>");
                    yield return new WaitForSeconds(7);
                    MainText.text = string.Format("<color=#47BEFF>���׵�Ʈ �Ƹ�Ű</color> : �츮�� ���� ���� �ع��ؾ� �� �༺�� ��Ÿ���콺 �۷��þƴ�. �� �༺�� ���� �ؼ��� ������ �����ϰ� �ִ� �༺����.");
                    MainShadowText.text = string.Format("<color=#494949>���׵�Ʈ �Ƹ�Ű : �츮�� ���� ���� �ع��ؾ� �� �༺�� ��Ÿ���콺 �۷��þƴ�. �� �༺�� ���� �ؼ��� ������ �����ϰ� �ִ� �༺����.</color>");
                    yield return new WaitForSeconds(7);
                    MainText.text = string.Format("<color=#47BEFF>���׵�Ʈ �Ƹ�Ű</color> : �� �༺�� �ع��ؾ� �������ֻ�ɿ������κ��� �ڱ� ��� ������ �㰡���� �� ���� ���̴�.");
                    MainShadowText.text = string.Format("<color=#494949>���׵�Ʈ �Ƹ�Ű : �� �༺�� �ع��ؾ� �������ֻ�ɿ������κ��� �ڱ� ��� ������ �㰡���� �� ���� ���̴�.</color>");
                }

                yield return new WaitForSeconds(7);
                TutorialSystem.UniverseMapViewGuidePrefab.SetActive(true);
                TutorialSystem.UniverseMapSystem.TutorialMapStep = 2;
                TutorialSystem.PlanetOurForceShipsManager.Tutorial = true;
            }
            else if (number == 1.03f) //��Ÿ���콺 �۷��þ� ����
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
                    MainText.text = string.Format("<color=#47BEFF>���׵�Ʈ �Ƹ�Ű</color> : �Դ�� ��� ���� ������ �ǽ��϶�.");
                    MainShadowText.text = string.Format("<color=#494949>���׵�Ʈ �Ƹ�Ű : �Դ�� ��� ���� ������ �ǽ��϶�.</color>");
                    yield return new WaitForSeconds(7);
                    MainText.text = string.Format("<color=#FF7948>��Ÿ �㸮����</color> : ����� ��Ÿ ������ ���� ���� ��ɰ� ��Ÿ �㸮�����̴�. �̰��� ��Ÿ���콺 �۷��þ� �������� ������ �غ� �Ǿ���.");
                    MainShadowText.text = string.Format("<color=#494949>��Ÿ �㸮���� : ����� ��Ÿ ������ ���� ���� ��ɰ� ��Ÿ �㸮�����̴�. �̰��� ��Ÿ���콺 �۷��þ� �������� ������ �غ� �Ǿ���.</color>");
                    yield return new WaitForSeconds(7);
                    MainText.text = string.Format("<color=#47BEFF>���׵�Ʈ �Ƹ�Ű</color> : �㸮����, ���� �Դ밡 ������ ����������, �ڳ׶�� �����̶� ������ �� �� �־� ���̴±�.");
                    MainShadowText.text = string.Format("<color=#494949>���׵�Ʈ �Ƹ�Ű : �㸮����, ���� �Դ밡 ������ ����������, �ڳ׶�� �����̶� ������ �� �� �־� ���̴±�.</color>");
                    yield return new WaitForSeconds(7);
                    MainText.text = string.Format("<color=#FF7948>��Ÿ �㸮����</color> : ���� ����. ������ �༺ ������ ��ٸ��� ������, �غ� �Ǿ����� �����ϵ���.");
                    MainShadowText.text = string.Format("<color=#494949>��Ÿ �㸮���� : ���� ����. ������ �༺ ������ ��ٸ��� ������, �غ� �Ǿ����� �����ϵ���.</color>");
                }

                yield return new WaitForSeconds(7);
                TutorialSystem.HurricaneOperationViewGuidePrefab.SetActive(true);
                TutorialSystem.HurricaneButton1.SetActive(true);
                TutorialSystem.HurricaneButton2.SetActive(true);
                TutorialSystem.HurricaneOperationMenu.HurricaneMainWeaponActive();
            }

            if (number == 2.01f) //�༺ �ع�
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
                        MainText.text = string.Format("<color=#47BEFF>���׵�Ʈ �Ƹ�Ű</color> : ����. �츮 �Ʊ� �Դ밡 ���� " + PlanetName + " �༺�� ��ġ�ǰ� �ִٳ�.");
                        MainShadowText.text = string.Format("<color=#494949>���׵�Ʈ �Ƹ�Ű : ����. �츮 �Ʊ� �Դ밡 ���� " + PlanetName + " �༺�� ��ġ�ǰ� �ִٳ�.</color>");
                    }
                    else if (RandomText == 1)
                    {
                        MainText.text = string.Format("<color=#47BEFF>���׵�Ʈ �Ƹ�Ű</color> : " + PlanetName + " �༺�� �ع�Ǿ�����, �Ʊ� �Դ밡 �װ��� �ֵ��� �ɼ�.");
                        MainShadowText.text = string.Format("<color=#494949>���׵�Ʈ �Ƹ�Ű : " + PlanetName + " �༺�� �ع�Ǿ�����, �Ʊ� �Դ밡 �װ��� �ֵ��� �ɼ�.</color>");
                    }
                    else if (RandomText == 2)
                    {
                        MainText.text = string.Format("<color=#FF7948>��Ÿ �㸮����</color> : ���� " + PlanetName + " �༺�� ����� ��ã�� �� �ְڱ�.");
                        MainShadowText.text = string.Format("<color=#494949>��Ÿ �㸮���� : ���� " + PlanetName + " �༺�� ����� ��ã�� �� �ְڱ�.</color>");
                    }
                    else if (RandomText == 3)
                    {
                        MainText.text = string.Format("<color=#FF7948>��Ÿ �㸮����</color> : �� �ϳ��� �༺�� �����ؼ� �����̱�. " + PlanetName + " �༺�� ���� ������ �ɼ�.");
                        MainShadowText.text = string.Format("<color=#494949>��Ÿ �㸮���� : �� �ϳ��� �༺�� �����ؼ� �����̱�. " + PlanetName + " �༺�� ���� ������ �ɼ�.</color>");
                    }
                }
                yield return new WaitForSeconds(7);
            }

            else if (number == 3.01f) //�׼� �ع�
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
                        MainText.text = string.Format("<color=#47BEFF>���׵�Ʈ �Ƹ�Ű</color> : ���ߴ�. ���� " + PlanetName + " �׼��� ��ã������, ���� ��Ʈ�ν��� ����ϱ� ������ ���̴�.");
                        MainShadowText.text = string.Format("<color=#494949>���׵�Ʈ �Ƹ�Ű : ���ߴ�. ���� " + PlanetName + " �׼��� ��ã������, ���� ��Ʈ�ν��� ����ϱ� ������ ���̴�.</color>");
                    }
                    else if (RandomText == 1)
                    {
                        MainText.text = string.Format("<color=#47BEFF>���׵�Ʈ �Ƹ�Ű</color> : " + PlanetName + " �׼� �ع� ���п� ��Ʈ�ν� �Դ밡 �� �ѹ� ��������.");
                        MainShadowText.text = string.Format("<color=#494949>���׵�Ʈ �Ƹ�Ű : " + PlanetName + " �׼� �ع� ���п� ��Ʈ�ν� �Դ밡 �� �ѹ� ��������.</color>");
                    }
                    else if (RandomText == 2)
                    {
                        MainText.text = string.Format("<color=#FF7948>��Ÿ �㸮����</color> : �Ǹ��ϱ�. " + PlanetName + " �׼������� ������ �̰ܳ��ٴ� ���̾�. �̹� ������ ��Ʈ�ν��� ����� ���� �ʰڱ�.");
                        MainShadowText.text = string.Format("<color=#494949>��Ÿ �㸮���� : �Ǹ��ϱ�. " + PlanetName + " �׼������� ������ �̰ܳ��ٴ� ���̾�. �̹� ������ ��Ʈ�ν��� ����� ���� �ʰڱ�.</color>");
                    }
                    else if (RandomText == 3)
                    {
                        MainText.text = string.Format("<color=#FF7948>��Ÿ �㸮����</color> : ��Ʈ�ν��� �� �� �����ڱ�. " + PlanetName + " �׼��� �츮�� ��ã�Ƽ� ������.");
                        MainShadowText.text = string.Format("<color=#494949>��Ÿ �㸮���� : ��Ʈ�ν��� �� �� �����ڱ�. " + PlanetName + " �׼��� �츮�� ��ã�Ƽ� ������.</color>");
                    }
                }
                yield return new WaitForSeconds(7);
            }

            else if (number == 4.01f) //�༺�� ��� �ع�� ������, ��Ʈ�ν� �׼� ö��
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
                        MainText.text = string.Format("<color=#47BEFF>���׵�Ʈ �Ƹ�Ű</color> : �� " + PlanetName + " �׼����� �༺���� ������ ������ ��Ʈ�ν��� �˾Ƽ� �׼����� ������ ���� �ƴ� ����̱�.");
                        MainShadowText.text = string.Format("<color=#494949>���׵�Ʈ �Ƹ�Ű : �� " + PlanetName + " �׼����� �༺���� ������ ������ ��Ʈ�ν��� �˾Ƽ� �׼����� ������ ���� �ƴ� ����̱�.</color>");
                    }
                    else if (RandomText == 1)
                    {
                        MainText.text = string.Format("<color=#47BEFF>���׵�Ʈ �Ƹ�Ű</color> : " + PlanetName + " �׼����� �� �༺���� �ع�� ���п� ��Ʈ�ν��� �׼����� ö���ϰ� �ִ� ���� ���δٳ�.");
                        MainShadowText.text = string.Format("<color=#494949>���׵�Ʈ �Ƹ�Ű : " + PlanetName + " �׼����� �� �༺���� �ع�� ���п� ��Ʈ�ν��� �׼����� ö���ϰ� �ִ� ���� ���δٳ�.</color>");
                    }
                    else if (RandomText == 2)
                    {
                        MainText.text = string.Format("<color=#FF7948>��Ÿ �㸮����</color> : �̹� " + PlanetName + " �׼��� �༺���� �ع�Ǿ����� ��Ʈ�ν����� �� ���� �ؾ߰ڱ�. ���� �׼����� ������� ���̾�.");
                        MainShadowText.text = string.Format("<color=#494949>��Ÿ �㸮���� : �̹� " + PlanetName + " �׼��� �༺���� �ع�Ǿ����� ��Ʈ�ν����� �� ���� �ؾ߰ڱ�. ���� �׼����� ������� ���̾�.</color>");
                    }
                    else if (RandomText == 3)
                    {
                        MainText.text = string.Format("<color=#FF7948>��Ÿ �㸮����</color> : " + PlanetName + " �׼��谡 ������ ��ã�Ҿ�. �׸��� ��Ʈ�ν��� �׼����� ö���ϰ� ����. �ڽŵ��� �̰����� �й��ߴٴ� �� �˰� �ִ� �ž�.");
                        MainShadowText.text = string.Format("<color=#494949>��Ÿ �㸮���� : " + PlanetName + " �׼��谡 ������ ��ã�Ҿ�. �׸��� ��Ʈ�ν��� �׼����� ö���ϰ� ����. �ڽŵ��� �̰����� �й��ߴٴ� �� �˰� �ִ� �ž�.</color>");
                    }
                }
                yield return new WaitForSeconds(7);
            }

            else if (number == 5.01f) //��Ʈ�ν� ���� �Դ�
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
                        MainText.text = string.Format("<color=#47BEFF>���׵�Ʈ �Ƹ�Ű</color> : ��Ʈ�ν� ���� �Դ��! �����϶�!");
                        MainShadowText.text = string.Format("<color=#494949>���׵�Ʈ �Ƹ�Ű : ��Ʈ�ν� ���� �Դ��! �����϶�!</color>");
                    }
                    else if (RandomText == 1)
                    {
                        MainText.text = string.Format("<color=#47BEFF>���׵�Ʈ �Ƹ�Ű</color> : �� �Դ�� ��Ʈ�ν� ���� �Դ뿡 ����϶�!");
                        MainShadowText.text = string.Format("<color=#494949>���׵�Ʈ �Ƹ�Ű : �� �Դ�� ��Ʈ�ν� ���� �Դ뿡 ����϶�!</color>");
                    }
                    else if (RandomText == 2)
                    {
                        MainText.text = string.Format("<color=#FF7948>��Ÿ �㸮����</color> : ��Ʈ�ν��� ���� �Դ븦 ���� �Ա�. ������!");
                        MainShadowText.text = string.Format("<color=#494949>��Ÿ �㸮���� : ��Ʈ�ν��� ���� �Դ븦 ���� �Ա�. ������!</color>");
                    }
                    else if (RandomText == 3)
                    {
                        MainText.text = string.Format("<color=#FF7948>��Ÿ �㸮����</color> : �� ���� �Դ� ������ �̹� ������ �� �� ��������� ��.");
                        MainShadowText.text = string.Format("<color=#494949>��Ÿ �㸮���� : �� ���� �Դ� ������ �̹� ������ �� �� ��������� ��.</color>");
                    }
                }
                yield return new WaitForSeconds(7);
            }

            else if (number == 6.01f) //�༺ �ع� ����
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
                            MainText.text = string.Format("<color=#47BEFF>���׵�Ʈ �Ƹ�Ű</color> : ����, �� �༺ ������ �����ϰڱ�. �غ��϶�.");
                            MainShadowText.text = string.Format("<color=#494949>���׵�Ʈ �Ƹ�Ű : ����, �� �༺ ������ �����ϰڱ�. �غ��϶�.</color>");
                        }
                        else if (RandomText == 1)
                        {
                            MainText.text = string.Format("<color=#47BEFF>���׵�Ʈ �Ƹ�Ű</color> : �츮 �Դ밡 �� �༺ ������ ������ ���̴�.");
                            MainShadowText.text = string.Format("<color=#494949>���׵�Ʈ �Ƹ�Ű : �츮 �Դ밡 �� �༺ ������ ������ ���̴�.</color>");
                        }
                        else if (RandomText == 2)
                        {
                            MainText.text = string.Format("<color=#FF7948>��Ÿ �㸮����</color> : �༺ �������� �غ�ȴٸ� ������ ���� �θ�����.");
                            MainShadowText.text = string.Format("<color=#494949>��Ÿ �㸮���� : �༺ �������� �غ�ȴٸ� ������ ���� �θ�����.</color>");
                        }
                        else if (RandomText == 3)
                        {
                            MainText.text = string.Format("<color=#FF7948>��Ÿ �㸮����</color> : ���� �� �༺ �����ΰ�? �׷�, �׷��� �� ���� ������ �غ��ؾ� �ڱ�.");
                            MainShadowText.text = string.Format("<color=#494949>��Ÿ �㸮���� : ���� �� �༺ �����ΰ�? �׷�, �׷��� �� ���� ������ �غ��ؾ� �ڱ�.</color>");
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

    //���� ��ȭ ���
    public IEnumerator SubCommunication(float number)
    {
        if (SubTextPrintting == false)
        {
            SubTextPrintting = true;
            SubTextPrefab.SetActive(true);

            if (number == 1.00f) //ù ����(Ʃ�丮��)
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
                    SubText.text = string.Format("�Ʊ� ���� : ��ɰ���, �츮 �Դ� ��ó���� �Ʊ� ���� ��ȣ�� �����ǰ� �ֽ��ϴ�.");
                    SubShadowText.text = string.Format("<color=#494949>�Ʊ� ���� : ��ɰ���, �츮 �Դ� ��ó���� �Ʊ� ���� ��ȣ�� �����ǰ� �ֽ��ϴ�.</color>");
                    yield return new WaitForSeconds(3);
                    StartCoroutine(MainCommunication(1.01f));
                }
                yield return new WaitForSeconds(4);
            }
            else if (number == 2.00f) //���� �������� �̰��� ��
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
                        SubText.text = string.Format("�Ʊ� �Լ� : �����մϴ�, UCCIS. ����� ���� �ٸ� �ӹ� �������� ���ڽ��ϴ�.");
                        SubShadowText.text = string.Format("<color=#494949>�Ʊ� �Լ� : �����մϴ�, UCCIS. ����� ���� �ٸ� �ӹ� �������� ���ڽ��ϴ�.</color>");
                    }
                    else if (RandomText == 1)
                    {
                        SubText.text = string.Format("�Ʊ� �Լ� : ���п� �������� �̱� �� �־����ϴ�.");
                        SubShadowText.text = string.Format("<color=#494949>�Ʊ� �Լ� : ���п� �������� �̱� �� �־����ϴ�.</color>");
                    }
                    else if (RandomText == 2)
                    {
                        SubText.text = string.Format("�Ʊ� �Լ� : ���� �츮�� �ٸ� �Դ븦 �����ϰڽ��ϴ�.");
                        SubShadowText.text = string.Format("<color=#494949>�Ʊ� �Լ� : ���� �츮�� �ٸ� �Դ븦 �����ϰڽ��ϴ�.</color>");
                    }
                    else if (RandomText == 3)
                    {
                        SubText.text = string.Format("�Ʊ� �Լ� : ��ɰ����� ������ ����帳�ϴ�. ���� �ٸ� ���� �������� �̵��ϰڽ��ϴ�.");
                        SubShadowText.text = string.Format("<color=#494949>�Ʊ� �Լ� : ��ɰ����� ������ ����帳�ϴ�. ���� �ٸ� ���� �������� �̵��ϰڽ��ϴ�.</color>");
                    }
                }
                if (RandomText <= 3)
                    yield return new WaitForSeconds(7);
            }

            else if (number == 3.00f) //���� ��ü�� �ı��Ǿ��� ��
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
                        SubText.text = string.Format("���� : ��ü�� �ı��ǰ� �ֽ��ϴ�!");
                        SubShadowText.text = string.Format("<color=#494949>���� : ��ü�� �ı��ǰ� �ֽ��ϴ�!</color>");
                    }
                    else if (RandomText == 1)
                    {
                        SubText.text = string.Format("���� : �Լ� Ÿ�� ����!");
                        SubShadowText.text = string.Format("<color=#494949>���� : �Լ� Ÿ�� ����!</color>");
                    }
                    else if (RandomText == 2)
                    {
                        SubText.text = string.Format("���� : �츮 �Լ��� ���ظ� �԰� �ֽ��ϴ�!");
                        SubShadowText.text = string.Format("<color=#494949>���� : �츮 �Լ��� ���ظ� �԰� �ֽ��ϴ�!</color>");
                    }
                    else if (RandomText == 3)
                    {
                        SubText.text = string.Format("���� : ��ü ���ظ� �Ծ����ϴ�!");
                        SubShadowText.text = string.Format("<color=#494949>���� : ��ü ���ظ� �Ծ����ϴ�!</color>");
                    }
                }
                if (RandomText <= 3)
                    yield return new WaitForSeconds(7);
            }

            else if (number == 4.00f) //���� ������ ����ȭ �Ǿ��� ��
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
                        SubText.text = string.Format("���� : ���� ����ȭ!");
                        SubShadowText.text = string.Format("<color=#494949>���� : ���� ����ȭ!</color>");
                    }
                    else if (RandomText == 1)
                    {
                        SubText.text = string.Format("���� : ȭ�� �߻�!");
                        SubShadowText.text = string.Format("<color=#494949>���� : ȭ�� �߻�!</color>");
                    }
                    else if (RandomText == 2)
                    {
                        SubText.text = string.Format("���� : ������ ����ȭ�Ǿ����ϴ�!");
                        SubShadowText.text = string.Format("<color=#494949>���� : ������ ����ȭ�Ǿ����ϴ�!</color>");
                    }
                    else if (RandomText == 3)
                    {
                        SubText.text = string.Format("���� ���� : ����, ������ ���ư���!");
                        SubShadowText.text = string.Format("<color=#494949>���� ���� : ����, ������ ���ư���!</color>");
                    }
                    else if (RandomText == 4)
                    {
                        SubText.text = string.Format("���� ���� : ����� �츮 ������ �������Ⱦ�!");
                        SubShadowText.text = string.Format("<color=#494949>���� ���� : ����� �츮 ������ �������Ⱦ�!</color>");
                    }
                }
                if (RandomText <= 4)
                    yield return new WaitForSeconds(7);
            }

            else if (number == 5.00f) //�Լ� ��ü�� �ı��Ǿ��� ��
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
                        SubText.text = string.Format("�Ʊ� �Լ� : ��ü�� �ı��ǰ� �ֽ��ϴ�!");
                        SubShadowText.text = string.Format("<color=#494949>�Ʊ� �Լ� : ��ü�� �ı��ǰ� �ֽ��ϴ�!</color>");
                    }
                    else if (RandomText == 1)
                    {
                        SubText.text = string.Format("�Ʊ� �Լ� : �Լ� Ÿ�� ����!");
                        SubShadowText.text = string.Format("<color=#494949>�Ʊ� �Լ� : �Լ� Ÿ�� ����!</color>");
                    }
                    else if (RandomText == 2)
                    {
                        SubText.text = string.Format("�Ʊ� �Լ� : �츮 �Լ��� ���ظ� �԰� �ֽ��ϴ�!");
                        SubShadowText.text = string.Format("<color=#494949>�Ʊ� �Լ� : �츮 �Լ��� ���ظ� �԰� �ֽ��ϴ�!</color>");
                    }
                    else if (RandomText == 3)
                    {
                        SubText.text = string.Format("�Ʊ� �Լ� : ��ü ���ظ� �Ծ����ϴ�!");
                        SubShadowText.text = string.Format("<color=#494949>�Ʊ� �Լ� : ��ü ���ظ� �Ծ����ϴ�!</color>");
                    }
                    else if (RandomText == 4)
                    {
                        SubText.text = string.Format("�Ʊ� �Լ� ���� : �ܺ��� ������... ���ƾƾ�!");
                        SubShadowText.text = string.Format("<color=#494949>�Ʊ� �Լ� ���� : �ܺ��� ������... ���ƾƾ�!</color>");
                    }
                    else if (RandomText == 5)
                    {
                        SubText.text = string.Format("�Ʊ� �Լ� ���� : ��ü�� �ı��ǰ� �־�!");
                        SubShadowText.text = string.Format("<color=#494949>�Ʊ� �Լ� ���� : ��ü�� �ı��ǰ� �־�!</color>");
                    }
                    else if (RandomText == 6)
                    {
                        SubText.text = string.Format("�Ʊ� �Լ� AI : ���. ��ü ������ ����.");
                        SubShadowText.text = string.Format("<color=#494949>�Ʊ� �Լ� AI : ���. ��ü ������ ����.</color>");
                    }
                }
                if (RandomText <= 6)
                    yield return new WaitForSeconds(7);
            }

            else if (number == 6.00f) //�Լ� ������ ����ȭ �Ǿ��� ��
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
                        SubText.text = string.Format("�Ʊ� �Լ� : ���� ����ȭ!");
                        SubShadowText.text = string.Format("<color=#494949>�Ʊ� �Լ� : ���� ����ȭ!</color>");
                    }
                    else if (RandomText == 1)
                    {
                        SubText.text = string.Format("�Ʊ� �Լ� : ȭ�� �߻�!");
                        SubShadowText.text = string.Format("<color=#494949>�Ʊ� �Լ� : ȭ�� �߻�!</color>");
                    }
                    else if (RandomText == 2)
                    {
                        SubText.text = string.Format("�Ʊ� �Լ� : ������ ����ȭ�Ǿ����ϴ�!");
                        SubShadowText.text = string.Format("<color=#494949>�Ʊ� �Լ� : ������ ����ȭ�Ǿ����ϴ�!</color>");
                    }
                    else if (RandomText == 3)
                    {
                        SubText.text = string.Format("�Ʊ� �Լ� ���� : ����, ������ ���ư���!");
                        SubShadowText.text = string.Format("<color=#494949>�Ʊ� �Լ� : ����, ������ ���ư���!</color>");
                    }
                    else if (RandomText == 4)
                    {
                        SubText.text = string.Format("�Ʊ� �Լ� ���� : ����� �츮 ������ �������Ⱦ�!");
                        SubShadowText.text = string.Format("<color=#494949>�Ʊ� �Լ� : ����� �츮 ������ �������Ⱦ�!</color>");
                    }
                }
                if (RandomText <= 4)
                    yield return new WaitForSeconds(7);
            }

            else if (number == 7.01f) //�Դ밡 ȸ���� ������ ��
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
                        SubText.text = string.Format("�Դ� : ��ü ������ �����մϴ�.");
                        SubShadowText.text = string.Format("<color=#494949>�Դ� : ��ü ������ �����մϴ�.</color>");
                    }
                    else if (RandomText == 1)
                    {
                        SubText.text = string.Format("�Դ� : �ջ�� ��ü�� �����ϰڽ��ϴ�.");
                        SubShadowText.text = string.Format("<color=#494949>�Դ� : �ջ�� ��ü�� �����ϰڽ��ϴ�.</color>");
                    }
                    else if (RandomText == 2)
                    {
                        SubText.text = string.Format("�Դ� : ��ü ���� ��Ʈ���� ����.");
                        SubShadowText.text = string.Format("<color=#494949>�Դ� : ��ü ���� ��Ʈ���� ����.</color>");
                    }
                    else if (RandomText == 3)
                    {
                        SubText.text = string.Format("�Դ� : ������ �Ѽ� ���� �� �ְڱ���.");
                        SubShadowText.text = string.Format("<color=#494949>�Դ� : ������ �Ѽ� ���� �� �ְڱ���.</color>");
                    }
                }
                if (RandomText <= 3)
                    yield return new WaitForSeconds(7);
            }

            else if (number == 7.02f) //�Դ밡 ȸ���� �Ϸ����� ��
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
                        SubText.text = string.Format("�Լ� : ���� �Ϸ�.");
                        SubShadowText.text = string.Format("<color=#494949>�Լ� : ���� �Ϸ�.</color>");
                    }
                    else if (RandomText == 1)
                    {
                        SubText.text = string.Format("�Լ� : ���� �Ϸ�.");
                        SubShadowText.text = string.Format("<color=#494949>�Լ� : ���� �Ϸ�.</color>");
                    }
                    else if (RandomText == 2)
                    {
                        SubText.text = string.Format("�Լ� : ������ �Ϸ�Ǿ� ���� �غ� �������ϴ�.");
                        SubShadowText.text = string.Format("<color=#494949>�Լ� : ������ �Ϸ�Ǿ� ���� �غ� �������ϴ�.</color>");
                    }
                    else if (RandomText == 3)
                    {
                        SubText.text = string.Format("�Լ� : �ٽ� �ο� �غ� �Ǿ����ϴ�.");
                        SubShadowText.text = string.Format("<color=#494949>�Լ� : �ٽ� �ο� �غ� �Ǿ����ϴ�.</color>");
                    }
                }
                if (RandomText <= 3)
                    yield return new WaitForSeconds(7);
            }

            else if (number == 8.00f) //�Լ��� ��ħ�Ǿ��� ��
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
                        SubText.text = string.Format("�Լ� ���� : �踦 ������! �踦 �������!");
                        SubShadowText.text = string.Format("<color=#494949>�Լ� ���� : �踦 ������! �踦 ������!</color>");
                    }
                    else if (RandomText == 1)
                    {
                        SubText.text = string.Format("�Լ� ���� : Ż������� ����ִ�... ���ƾƾƾ�!!");
                        SubShadowText.text = string.Format("<color=#494949>�Լ� ���� : Ż������� ����ִ�... ���ƾƾƾ�!!</color>");
                    }
                    else if (RandomText == 2)
                    {
                        SubText.text = string.Format("�Լ� ���� : Ż���Ѵ�! �̰����� ���� ����!");
                        SubShadowText.text = string.Format("<color=#494949>�Լ� ���� : Ż���Ѵ�! �̰����� ���� ����!</color>");
                    }
                    else if (RandomText == 3)
                    {
                        SubText.text = string.Format("�Լ� ���� : �̰����� ���� Ż���ؾ���!");
                        SubShadowText.text = string.Format("<color=#494949>�Լ� ���� : �̰����� ���� Ż���ؾ���!</color>");
                    }
                }
                if (RandomText <= 3)
                    yield return new WaitForSeconds(7);
            }

            else if (number == 9.00f) //��Ʈ�ν����� ������ ���۵Ǿ��� ��
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
                        SubText.text = string.Format("�Դ� : ������ �����մϴ�!");
                        SubShadowText.text = string.Format("<color=#494949>�Դ� : ������ �����մϴ�!</color>");
                    }
                    else if (RandomText == 1)
                    {
                        SubText.text = string.Format("�Դ� : ��Ʈ�ν� �Դ� ����! ���� ����!");
                        SubShadowText.text = string.Format("<color=#494949>�Դ� : ��Ʈ�ν� �Դ� ����! ���� ����!</color>");
                    }
                    else if (RandomText == 2)
                    {
                        SubText.text = string.Format("�Դ� : ��Ʈ�ν� �Դ��! ����϶�!");
                        SubShadowText.text = string.Format("<color=#494949>�Դ� : ��Ʈ�ν� �Դ��! ����϶�!</color>");
                    }
                    else if (RandomText == 3)
                    {
                        SubText.text = string.Format("�Դ� : �����ϸ� ���Ͽ�!");
                        SubShadowText.text = string.Format("<color=#494949>�Դ� : �����ϸ� ���Ͽ�!</color>");
                    }
                    else if (RandomText == 4)
                    {
                        SubText.text = string.Format("�Դ� : ��Ʈ�ν���! ����� ���Ƴ���!");
                        SubShadowText.text = string.Format("<color=#494949>�Դ� : ��Ʈ�ν���! ����� ���Ƴ���!</color>");
                    }
                    else if (RandomText == 5)
                    {
                        SubText.text = string.Format("�Դ� : ���ڸ� �ð��� �帣��. ���� ����!");
                        SubShadowText.text = string.Format("<color=#494949>�Դ� : ���ڸ� �ð��� �帣��. ���� ����!</color>");
                    }
                    else if (RandomText == 6)
                    {
                        SubText.text = string.Format("�Դ� : ���̷轺�� ���Ͽ�!");
                        SubShadowText.text = string.Format("<color=#494949>�Դ� : ���̷轺�� ���Ͽ�!</color>");
                    }
                    else if (RandomText == 7)
                    {
                        SubText.text = string.Format("�Դ� : ���� ��Ʈ�ν� ��鿡�� ������!");
                        SubShadowText.text = string.Format("<color=#494949>�Դ� : ���� ��Ʈ�ν� ��鿡�� ������!</color>");
                    }
                    else if (RandomText == 8)
                    {
                        SubText.text = string.Format("�Դ� : ���̽� ������� �������� ����!");
                        SubShadowText.text = string.Format("<color=#494949>�Դ� : ���̽� ������� �������� ����!</color>");
                    }
                }
                if (RandomText <= 8)
                    yield return new WaitForSeconds(7);
            }

            else if (number == 10.01f) //�������� ������ ���۵Ǿ��� ��(���θ��)
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
                        SubText.text = string.Format("<color=#C900FF>���θ�� �Դ�</color> : ���̽� ������ �������� �ں� ������.");
                        SubShadowText.text = string.Format("<color=#494949>���θ�� �Դ� : ���̽� ������ �������� �ں� ������.</color>");
                    }
                    else if (RandomText == 1)
                    {
                        SubText.text = string.Format("<color=#C900FF>���θ�� �Դ�</color> : �츮�� �ں� �ź��ϴ� ���� ���� ���縦 �ź��ϴ� ���̴�.");
                        SubShadowText.text = string.Format("<color=#494949>���θ�� �Դ� : �츮�� �ں� �ź��ϴ� ���� ���� ���縦 �ź��ϴ� ���̴�.</color>");
                    }
                    else if (RandomText == 2)
                    {
                        SubText.text = string.Format("<color=#C900FF>���θ�� �Դ�</color> : ���� �ΰ��� �󸶳� �������� ���Ѻ��ڴ�.");
                        SubShadowText.text = string.Format("<color=#494949>���θ�� �Դ� : ���� �ΰ��� �󸶳� �������� ���Ѻ��ڴ�.</color>");
                    }
                    else if (RandomText == 3)
                    {
                        SubText.text = string.Format("<color=#C900FF>���θ�� �Դ�</color> : ������ ���ǹ��ϴ�.");
                        SubShadowText.text = string.Format("<color=#494949>���θ�� �Դ� : ������ ���ǹ��ϴ�.</color>");
                    }
                    else if (RandomText == 4)
                    {
                        SubText.text = string.Format("<color=#C900FF>���θ�� �Դ�</color> : ���� ���� �츮�� �ൿ�� �츮 �ƾ��̷��� ������ ���̷δ�.");
                        SubShadowText.text = string.Format("<color=#494949>���θ�� �Դ� : ���� ���� �츮�� �ൿ�� �츮 �ƾ��̷��� ������ ���̷δ�.</color>");
                    }
                    else if (RandomText == 5)
                    {
                        SubText.text = string.Format("<color=#C900FF>���θ�� �Դ�</color> : ���θ� �ĺ��� �ƹ� �ҿ� ����.");
                        SubShadowText.text = string.Format("<color=#494949>���θ�� �Դ� : ���θ� �ĺ��� �ƹ� �ҿ� ����.</color>");
                    }
                    else if (RandomText == 6)
                    {
                        SubText.text = string.Format("<color=#C900FF>���θ�� �Դ�</color> : �츮�� ���̷轺 ������ �����ϸ��� �����ߴ°�?");
                        SubShadowText.text = string.Format("<color=#494949>���θ�� �Դ� : �츮�� ���̷轺 ������ �����ϸ��� �����ߴ°�?</color>");
                    }
                    else if (RandomText == 7)
                    {
                        SubText.text = string.Format("<color=#C900FF>���θ�� �Դ�</color> : ������� �ִ� ���� �� �츮�� ���ο� �����ڸ��� �� ���̴�.");
                        SubShadowText.text = string.Format("<color=#494949>���θ�� �Դ� : ������� �ִ� ���� �� �츮�� ���ο� �����ڸ��� �� ���̴�.</color>");
                    }
                    else if (RandomText == 8)
                    {
                        SubText.text = string.Format("<color=#C900FF>���θ�� �Դ�</color> : �츮 ���̿��ƽ��� ����� �Ѿư� ������� �밡�� ġ�� ���̴�.");
                        SubShadowText.text = string.Format("<color=#494949>���θ�� �Դ� : �츮 ���̿��ƽ��� ����� �Ѿư� ������� �밡�� ġ�� ���̴�.</color>");
                    }
                }
                if (RandomText <= 8)
                    yield return new WaitForSeconds(7);
            }

            else if (number == 10.02f) //�������� ������ ���۵Ǿ��� ��(ĭŸũ��)
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
                        SubText.text = string.Format("<color=#FF5B51>ĭŸũ�� �Դ�</color> : �ΰ����� ������ �����Ѵ�.");
                        SubShadowText.text = string.Format("<color=#494949>ĭŸũ�� �Դ� : �ΰ����� ������ �����Ѵ�.</color>");
                    }
                    else if (RandomText == 1)
                    {
                        SubText.text = string.Format("<color=#FF5B51>ĭŸũ�� �Դ�</color> : ���� �ٶ󺸴�, �츮 ����ũ���ý�Ʈ ���� ġ�ڰ� �ִ�.");
                        SubShadowText.text = string.Format("<color=#494949>ĭŸũ�� �Դ� : ���� �ٶ󺸴�, �츮 ����ũ���ý�Ʈ ���� ġ�ڰ� �ִ�.</color>");
                    }
                    else if (RandomText == 2)
                    {
                        SubText.text = string.Format("<color=#FF5B51>ĭŸũ�� �Դ�</color> : �츮�� �������ﺸ�� �� ������ ������ �Ǳ� ����ϰڴ�.");
                        SubShadowText.text = string.Format("<color=#494949>ĭŸũ�� �Դ� : �츮�� �������ﺸ�� �� ������ ������ �Ǳ� ����ϰڴ�.</color>");
                    }
                    else if (RandomText == 3)
                    {
                        SubText.text = string.Format("<color=#FF5B51>ĭŸũ�� �Դ�</color> : ����鿡 ���� ���Ͱ� �Ǳ� ���� ���� ��ȸ��.");
                        SubShadowText.text = string.Format("<color=#494949>ĭŸũ�� �Դ� : ����鿡 ���� ���Ͱ� �Ǳ� ���� ���� ��ȸ��.</color>");
                    }
                    else if (RandomText == 4)
                    {
                        SubText.text = string.Format("<color=#FF5B51>ĭŸũ�� �Դ�</color> : �� �츮�� �ൿ�� ������� �ı� ���� ������ ���̴�.");
                        SubShadowText.text = string.Format("<color=#494949>ĭŸũ�� �Դ� : �� �츮�� �ൿ�� ������� �ı� ���� ������ ���̴�.</color>");
                    }
                    else if (RandomText == 5)
                    {
                        SubText.text = string.Format("<color=#FF5B51>ĭŸũ�� �Դ�</color> : �츮�� ������ ���� ���� �𸥴�.");
                        SubShadowText.text = string.Format("<color=#494949>ĭŸũ�� �Դ� : �츮�� ������ ���� ���� �𸥴�.</color>");
                    }
                    else if (RandomText == 6)
                    {
                        SubText.text = string.Format("<color=#FF5B51>ĭŸũ�� �Դ�</color> : � �츮�� ���������� ���ͷ� ������.");
                        SubShadowText.text = string.Format("<color=#494949>ĭŸũ�� �Դ� : � �츮�� ���������� ���ͷ� ������.</color>");
                    }
                    else if (RandomText == 7)
                    {
                        SubText.text = string.Format("<color=#FF5B51>ĭŸũ�� �Դ�</color> : ������ ���ȴ�. ����ũ���ý�Ʈ�� ġ�ڴ´�.");
                        SubShadowText.text = string.Format("<color=#494949>ĭŸũ�� �Դ� : ������ ���ȴ�. ����ũ���ý�Ʈ�� ġ�ڴ´�.</color>");
                    }
                    else if (RandomText == 8)
                    {
                        SubText.text = string.Format("<color=#FF5B51>ĭŸũ�� �Դ�</color> : īũ�ν�-Ÿ�����ν� 1389���� ���� ����� ���̴�.");
                        SubShadowText.text = string.Format("<color=#494949>ĭŸũ�� �Դ� : īũ�ν�-Ÿ�����ν� 1389���� ���� ����� ���̴�.</color>");
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