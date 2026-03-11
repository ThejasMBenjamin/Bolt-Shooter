using UnityEngine;

public class EnemeyDeathParticle : MonoBehaviour
{
    [SerializeField] private GameObject currentParent;
    [SerializeField] private SpriteRenderer currentParentSprite;
    private SpriteRenderer deathParticleSprite;

    private void Start()
    {
        deathParticleSprite = GetComponent<SpriteRenderer>();
        deathParticleSprite.enabled = false;
    }
    public void AfterDeath()
    {
        deathParticleSprite.enabled = true;
        Destroy(currentParent.gameObject, 4);
    }

}
