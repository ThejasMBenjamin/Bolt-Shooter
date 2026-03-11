using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public bool isMove;
    private EnemeyDamageSystem eneDS;
    SpriteRenderer sp;
    Animator anim;
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 direction;
    Transform plyTm;
    private float attackCoolDown = 2.5f;
    private  float lastAttackTime;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        if(PlayerDamageSystem.isDeath) return;
        plyTm = GameObject.FindGameObjectWithTag("Player").transform;
        isMove = true;
        eneDS = GetComponent<EnemeyDamageSystem>();
    }
    private void Update()
    {
        if (plyTm == null) return;
        if (plyTm.position.x > transform.position.x) { sp.flipX = false; }
        else { sp.flipX = true; }
    }

    private void FixedUpdate()
    {
        if (plyTm == null) return;
        direction = (plyTm.position - transform.position).normalized;
        if (!isMove) return;
        transform.position  += (Vector3)(direction * moveSpeed ) * Time.fixedDeltaTime;
    }


    //Attack Player

    private void OnTriggerEnter2D(Collider2D collision){HitPlayer(collision);}
    private void OnTriggerStay2D(Collider2D collision){ HitPlayer(collision);}

    private void HitPlayer(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !PlayerDamageSystem.isDeath && !eneDS.isDead)
        {
            if (Time.time > lastAttackTime + attackCoolDown)
            {
                collision.gameObject.GetComponent<PlayerDamageSystem>().OnHit(3);
                lastAttackTime = Time.time;
            } }} 
     }
