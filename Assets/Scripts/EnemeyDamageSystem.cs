using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class EnemeyDamageSystem : MonoBehaviour
{
    [SerializeField] private EnemeyDeathParticle eDP;
    [Header("Settings")]
    [SerializeField] private float maxHealth = 20;
    //[SerializeField] private float FlashWaitSecond = 0.09f;

    [Header("Others")]
    private GameObject EnemySpawner;
    private PolygonCollider2D polygonCollider2D;
    private Animator anim;
    private SpriteRenderer EnemeyShadowSprite;
    private SpriteRenderer EnemeySprite;
    private EntityDamageVisual edv;
    private SpawnSystem spsy;
    private EnemeySoundEffect ea;
    private Enemy en;
    
    private float currentHealth;
    private bool isHit;
    public bool isDead = false;
    private bool deathProcessed = false;
    private bool hasHitParam;

    private void Awake()
    {
        EnemeySprite = GetComponent<SpriteRenderer>();
        en  = GetComponent<Enemy>();
        edv = GetComponent<EntityDamageVisual>();
        anim = GetComponent<Animator>();
        EnemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner");
        spsy  = EnemySpawner.GetComponent<SpawnSystem>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        EnemeyShadowSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        ea = GetComponent<EnemeySoundEffect>();

        currentHealth = maxHealth;
        EnemeyShadowSprite.enabled = true;

        foreach (AnimatorControllerParameter param in anim.parameters)
        {
            if (param.name == "isHit") { hasHitParam = true; break; }
        }
    }
    public void Damage(float d)
    {
        currentHealth -= d;
        isHit = true;
        edv.DamageFlashPlay();
        ea.HitSoundPlay();
    }

    private void Update() {
        //Hit Anim Set Old
        //anim.SetBool("isHit", isHit);
        //isHit = false;

        //New | I kept Old One For ReCheck
        if (hasHitParam)
        {
            anim.SetBool("isHit", isHit);
            isHit = false;
        }


        //Damage Check
        if (currentHealth < 0 && !isDead) {
            isDead = true;
            en.isMove = false;
            EnemeyShadowSprite.enabled = false;
            anim.SetTrigger("isDeath");
        }
    }
    public void OnDeath()
    {
        if (deathProcessed) return; 
        deathProcessed = true;
        spsy.OnEneDie();
        eDP.AfterDeath();
        EnemeySprite.enabled = false;
        polygonCollider2D.enabled = false;
    }

}
