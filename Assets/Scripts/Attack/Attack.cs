using System.Data.Common;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float IdleSpeed = 5f;
   // public float gravity = -9.8f; IdleSpeed로 통함
    public float attackPower = 10f;
    public float IdleAttackPower => attackPower * 0.1f;
    private float velocity;
    private float currentHeight;
    private float maxHeight = 0.5f;
    private float minHeight = -0.5f;
    public bool isJump = true;
    public bool isAttack = true;
    public bool OnAttack;
    public float AttackDelay = 0.5f; //어택딜레이 임시
    public float AttackTimer = 0;

    //0.5 -0.5
    void Start()
    {
        currentHeight = transform.position.y;
        
    }

    void Update()
    {
        PlayerAttack();
        AttackTimer += Time.deltaTime;
    }
    public void PlayerAttack()
    {
        // 마우스 클릭 시 빠르게 낙하하도록 처리
        if (Input.GetMouseButtonDown(0)&& AttackTimer >=  AttackDelay)
        {
            AttackTimer = 0f;
            if (isAttack) 
            {
                OnAttack = true;
                isJump = false;
                velocity = -IdleSpeed * 2f; // 빠르게 낙하
            }

        }

        if (transform.position.y <= 0.5 && transform.position.y >= -0.15)
        {
            isAttack = true;
        }
        else
        {

            isAttack = false;
        }
        // 점프/하강 처리
        if (isJump)
        {
            velocity += IdleSpeed * Time.deltaTime;
            velocity = Mathf.Min(velocity, 10f);
        }
        else
        {
            velocity += -IdleSpeed * Time.deltaTime;
        }

        transform.position += new Vector3(0, velocity * Time.deltaTime, 0);
        currentHeight = transform.position.y;

        // maxHeight 도달 시 낙하
        if (transform.position.y >= maxHeight)
        {
            isJump = false;
            velocity = 0f;
            transform.position = new Vector3(transform.position.x, maxHeight, transform.position.z);
        }

        // minHeight 도달 시 다시 점프
        if (transform.position.y <= minHeight)
        {
            isJump = true;
            velocity = 0f;
            transform.position = new Vector3(transform.position.x, minHeight, transform.position.z);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageTile dmg = other.gameObject.GetComponent<DamageTile>();
        if (other.gameObject.layer == LayerMask.NameToLayer("enemy"))
        {
            if (OnAttack)
            {
                
                dmg.TakeDamage(attackPower);
                OnAttack = false;
            }
            else
            {

                dmg.TakeDamage(IdleAttackPower);
            }

                Debug.Log("땅과 충돌");
        }
    }


}

