using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : LivingEntity
{
    public Rigidbody2D rb;

    private Vector2 movement;
    private Vector2 lastMovement;

    public PlayerInventory inventory;
    public PlayerAttack attack;
    public Animator animator;

    public UnityEvent onPlayerTakeDamage;

    public bool IsGoingToNextLevel = false;

    public bool isMoving = false;
    private bool IsAttacking = false;
    [SerializeField] private bool HasSword = false;

    private float DefaultAttackTimer = 0.5f;
    private float attackTime = 0f;

    [SerializeField] private GameObject DamageParticle;

    // Start is called before the first frame update
    void Start()
    {
        Manager.Lose = false;
        animator = GetComponent<Animator>();

        inventory = GetComponent<PlayerInventory>();


        if (Manager.Potions != 0)
        {
            inventory.Inventory.Add(CollectableTypes.HealthPot, Manager.Potions);
        }

        Manager.Instance.Player = this;

        attack = GetComponent<PlayerAttack>();
        InputManager.Instance.onMouseLeftButtonPressed.AddListener(HandleMouse);
        Manager.Instance.Player = this;

        Manager.LevelEnd.AddListener(HandleLevelEnd);
        inventory.inventoryUpdate.AddListener(UpdateCharacter);
        Manager.Instance.UIManager.AddHealthListener();

    }

    void UpdateCharacter()
    {
        if (inventory.GetCollectable(CollectableTypes.Sword))
            HasSword = true;
    }

    void Move()
    {
        if (Input.GetAxisRaw("Horizontal") > 0.1f)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
        }
        else if (Input.GetAxisRaw("Horizontal") < -0.1f)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
        }
        else if (Input.GetAxisRaw("Vertical") > 0.1f)
        {
            movement.y = Input.GetAxisRaw("Vertical");
        }
        else if (Input.GetAxisRaw("Vertical") < -0.1f)
        {
            movement.y = Input.GetAxisRaw("Vertical");
        }

        if (movement != Vector2.zero)
        {
            isMoving = true;
            attack.SetColliderPosition(new Vector3(transform.position.x + (movement.x * 0.2f), transform.position.y + (movement.y * 0.2f), transform.position.z));
        }
        else
        {
            isMoving = false;
            attack.SetColliderPosition(new Vector3(transform.position.x + (lastMovement.x * 0.2f), transform.position.y + (lastMovement.y * 0.2f), transform.position.z));
        }

        HandleAnimations();

        movement = movement.normalized;
        
    }

    void HandleAnimations()
    {
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Horizontal_Last", lastMovement.x);
        animator.SetFloat("Vertical_Last", lastMovement.y);
        animator.SetBool("IsMoving", isMoving);
        animator.SetBool("Attack", IsAttacking);
        animator.SetBool("HasSword", HasSword);
    }

    void HandleMouse()
    {
        if (Manager.Instance.InventoryUIManager.slotActive != -1)
        {
            UseItem();

        }
        else
        {
            Attack();

        }
    }


    GameObject target = null;
    void Attack()
    {
        if (!HasSword) return;
        if (Manager.Lose) return;
        if (IsAttacking) return;

        IsAttacking = true;
        animator.SetBool("Attack", IsAttacking);
        target = attack.GetTarget();
        if (target  != null)
        {
            target.GetComponent<Boss>()?.Damage(damageValue);
            target.GetComponent<Enemy>()?.Damage(damageValue);
        }

        AudioManager.Instance.Play("Player_Attack");
    }

    // Update is called once per frame
    void Update()
    {
        if (Manager.Lose) return;

        if (isDead)
        {
            movement = Vector2.zero;
            return;
        }

        if ( isMoving )
            lastMovement = movement;

        if (IsAttacking)
        {
            attackTime += Time.deltaTime;
            if (attackTime > DefaultAttackTimer)
            {
                IsAttacking = false;
                attackTime = 0;
            }
        }
        

        movement = Vector2.zero;
        if (!IsGoingToNextLevel)
            Move();
        else
            movement = Vector2.zero;    
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }

    public void UseItem()
    {
        if(Manager.Instance.InventoryUIManager.slotActive == 0)
        {
            // Use Pot
            if ( health < 100f)
            {
                if ( inventory.Inventory[CollectableTypes.HealthPot] > 0)
                {
                    inventory.UseCollectable(CollectableTypes.HealthPot);
                    Heal(25f);
                }
                
            }
            
        }
        else if (Manager.Instance.InventoryUIManager.slotActive == 1)
        {
            // Detect if colliding to brother
            if (inventory.Inventory[CollectableTypes.Key] > 0)
            {
                inventory.UseCollectable(CollectableTypes.Key);
                Manager.Win = true;
                StartCoroutine(WinGame());
            }
                
        }
    }

    private IEnumerator WinGame()
    {
        yield return new WaitForSeconds(0.5f);
        Manager.OnWin?.Invoke();

    }

    private void HandleLevelEnd()
    {
        IsGoingToNextLevel = true;
    }

    public void Heal(float amount)
    {
        if (health + amount < 100f)
            health += amount;
        else
            health = 100f;
        onPlayerTakeDamage?.Invoke();
    }


    public override void Damage(float amount)
    {
        base.Damage(amount);
        onPlayerTakeDamage?.Invoke();
        Destroy(GameObject.Instantiate(DamageParticle, transform.position, Quaternion.identity), 1f);

        if ( isDead )
        {
            AudioManager.Instance.Play("Player_Die");
            Manager.OnLose?.Invoke();
        }
    }

}
