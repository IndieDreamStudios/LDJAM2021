using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : LivingEntity
{
    [SerializeField] private GameObject deadParticles;
    [SerializeField] private NavMeshAgent agent;
    private Animator anim;

    private EnemyAttack attack;
    private GameObject target;
    private Vector3 followPos;
    public bool isFollowing = false;

    bool isAttacking = false;
    float TimerToAttack = 1.5f;
    float timeToAttack = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        attack = GetComponent<EnemyAttack>();
        anim = GetComponent<Animator>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (Manager.Lose) return;
        if (isFollowing)
        {
            agent.SetDestination(followPos);


            if (attack.GetTarget() != null)
            {
                timeToAttack += Time.deltaTime;
                if (timeToAttack >= TimerToAttack)
                {
                    isAttacking = false;
                    timeToAttack = 0.0f;
                    Attack();
                }
            }
        }

        attack.SetColliderPosition(new Vector3(transform.position.x + (agent.velocity.x * 0.3f), transform.position.y + (agent.velocity.y * 0.3f), transform.position.z));
        anim.SetFloat("Horizontal", agent.velocity.x);
        anim.SetFloat("Vertical", agent.velocity.y);

    }

    void Attack()
    {
        if (isAttacking) return;
        isAttacking = true;
        target.GetComponent<Player>().Damage(damageValue);
        AudioManager.Instance.Play("Enemy_Attack");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isFollowing = true;
            target = collision.gameObject;
            followPos = collision.transform.position;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isFollowing = true;
            target = collision.gameObject;
            followPos = collision.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            target = null;
            isFollowing = false;
        }
    }

    public override void Damage(float amount)
    {
        base.Damage(amount);
        Destroy(GameObject.Instantiate(deadParticles, transform.position, Quaternion.identity), 1f);

        if (isDead)
        {
            // Play Dead Sound
            AudioManager.Instance.Play("Enemy_Die");

            StartCoroutine(DestroyMe());
        }

    }

    IEnumerator DestroyMe()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

}
