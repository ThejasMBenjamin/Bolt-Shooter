using UnityEngine;
using System.Collections;

public class EntityDamageVisual : MonoBehaviour
{
    private Material ml;
    [SerializeField] private float FlashWaitSecond = 0.09f;


    private void Start()
    {
        ml = GetComponent<SpriteRenderer>().material;
    }
    private IEnumerator DamageFlash(float ws)
    {
        if (PlayerDamageSystem.isDeath) yield break;
        ml.SetFloat("_FlashRange", 1);
        yield return new WaitForSeconds(ws);
        if (PlayerDamageSystem.isDeath) yield break;
        ml.SetFloat("_FlashRange", 0);

    }

    public void DamageFlashPlay()
    {
        StopAllCoroutines();
        StartCoroutine(DamageFlash(FlashWaitSecond));
    }

    public void StopDamageFlash()
    {
        StopAllCoroutines();
        ml.SetFloat("_FlashRange", 0);
    }
}
