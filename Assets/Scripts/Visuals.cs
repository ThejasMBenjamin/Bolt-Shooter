using UnityEngine;
using System.Collections;

public class Visuals : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private WeaponHold wph;
    [SerializeField] private Weapon wp;

    [Header("Settings")]
    [SerializeField] private float flasDuration;

    private void Start()
    {
        if (wph.transform.childCount == 0) return;
        wph.muzzleSprite.enabled = false;

    }
    public void MuzzleFlashPlay()
    {
        StopAllCoroutines();
        StartCoroutine(MuzzleFlash());
    }

    //Gun Muzzle Flash Anim
    private IEnumerator MuzzleFlash()
    {
        wph.muzzleSprite.enabled = true;
        yield return new WaitForSeconds(flasDuration);
        wph.muzzleSprite.enabled = false;

    }


}
