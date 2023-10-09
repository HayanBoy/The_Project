using System.Collections;
using UnityEngine;

public class DisapearEffect2 : MonoBehaviour
{
    public Transform DT37AmmoPos;
    public Transform DS65AmmoPos;
    public Transform DP9007AmmoPos;
    public Transform CGD27AmmoPos;
    public Transform ChangeWeaponEnergyPos;
    public Transform M3078BodyPos;
    public Transform ASC365BodyPos;
    public int Type;
    public int EffectName;

    void Update()
    {
        if (Type == 1)
        {
            if (DT37AmmoPos == null)
            {
                if (EffectName == 1)
                    this.gameObject.GetComponent<Animator>().SetBool("Disapear1", true);
                else if (EffectName == 2)
                    this.gameObject.GetComponent<Animator>().SetBool("Disapear2", true);
                Destroy(gameObject, 5);
            }
        }
        else if (Type == 1000)
        {
            if (DS65AmmoPos == null)
            {
                if (EffectName == 1)
                    this.gameObject.GetComponent<Animator>().SetBool("Disapear1", true);
                else if (EffectName == 2)
                    this.gameObject.GetComponent<Animator>().SetBool("Disapear2", true);
                Destroy(gameObject, 5);
            }
        }
        else if (Type == 2000)
        {
            if (DP9007AmmoPos == null)
            {
                if (EffectName == 1)
                    this.gameObject.GetComponent<Animator>().SetBool("Disapear1", true);
                else if (EffectName == 2)
                    this.gameObject.GetComponent<Animator>().SetBool("Disapear2", true);
                Destroy(gameObject, 5);
            }
        }
        else if (Type == 3000)
        {
            if (CGD27AmmoPos == null)
            {
                if (EffectName == 1)
                    this.gameObject.GetComponent<Animator>().SetBool("Disapear1", true);
                else if (EffectName == 2)
                    this.gameObject.GetComponent<Animator>().SetBool("Disapear2", true);
                Destroy(gameObject, 5);
            }
        }
        else if (Type == 4999)
        {
            if (ChangeWeaponEnergyPos == null)
            {
                if (EffectName == 1)
                    this.gameObject.GetComponent<Animator>().SetBool("Disapear1", true);
                else if (EffectName == 2)
                    this.gameObject.GetComponent<Animator>().SetBool("Disapear2", true);
                Destroy(gameObject, 5);
            }
        }
        else if (Type == 5000)
        {
            if (M3078BodyPos == null)
            {
                if (EffectName == 1)
                    this.gameObject.GetComponent<Animator>().SetBool("Disapear1", true);
                else if (EffectName == 2)
                    this.gameObject.GetComponent<Animator>().SetBool("Disapear2", true);
                Destroy(gameObject, 5);
            }
        }
        else if (Type == 5001)
        {
            if (ASC365BodyPos == null)
            {
                if (EffectName == 1)
                    this.gameObject.GetComponent<Animator>().SetBool("Disapear1", true);
                else if (EffectName == 2)
                    this.gameObject.GetComponent<Animator>().SetBool("Disapear2", true);
                Destroy(gameObject, 5);
            }
        }
    }
}