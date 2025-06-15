using Cinemachine;
using System.Collections;
using System.Data.Common;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public PlayerData playerData;

    public float IdleSpeed = 5f; //튀어오르는 기본 속도
    // public float gravity = -9.8f; IdleSpeed로 통함
    public float attackPower = 10f; //임시 공격력
    public float IdleAttackPower => playerData.atk * 0.1f; //Idle 공격력 (클릭 안했을때)
    private float velocity;
    private float currentHeight;
    private float maxHeight = 0.5f;
    private float minHeight = -0.5f;
    public bool isJump = true;
    public bool isAttack = true;
    public bool OnAttack;
    public float AttackDelay = 0.5f; //어택딜레이 임시
    public float AttackTimer = 0;


    //자동공격
    public float autoAttackDuration = 5f; //자동공격 시간
    public float autoAttackSpeed = 50f; //공격속도
    public bool OnAuto;

    //0.5 -0.5


    public CinemachineImpulseSource idleimpulseSource;
    public CinemachineImpulseSource attackimpulseSource;
    public CinemachineImpulseSource autoattackimpulseSource;
    public ParticleSystem attackParticle;
    public TrailRenderer trailRenderer;

    public void IdleTriggerImpulse()
    {
        idleimpulseSource.GenerateImpulse();
    }
    public void AttackTriggerImpulse()
    {

        attackimpulseSource.GenerateImpulse();
    }
    public void AutoAttackTriggerImpulse()
    {

        autoattackimpulseSource.GenerateImpulse();
    }
    void Start()
    {
        currentHeight = transform.position.y;
        trailRenderer.emitting = false;
    }

    void Update()
    {
        PlayerAttack();
        AttackTimer += Time.deltaTime;
        if (OnAttack||OnAuto)
        {

            trailRenderer.emitting = true;
        }
        else
        {
            trailRenderer.emitting = false;
        }
    }
    public void PlayerAttack()
    {
        // 마우스 클릭 시 빠르게 낙하하도록 처리
        if (Input.GetMouseButtonDown(0) && AttackTimer >= AttackDelay && OnAuto == false)
        {
            Debug.Log("클릭 인식확인");
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


        if (Input.GetKeyDown(KeyCode.Space) && OnAuto == false)
        {
            OnAuto = true;
            Debug.Log("자동공격 실행");
            StartCoroutine(AutoAttack());
        }
    }

    private IEnumerator AutoAttack()
    {
        float timer = 0f;

        while (timer < autoAttackDuration)
        {
            if (isJump)
            {
                velocity += 30 * IdleSpeed * Time.deltaTime;
                velocity = Mathf.Min(velocity, 10f);
            }
            else
            {
                velocity += -30 * IdleSpeed * Time.deltaTime;
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

            yield return null;
            timer += Time.deltaTime;

        }
        OnAuto = false;
        Debug.Log("자동공격 종료");


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy dmg = other.gameObject.GetComponent<Enemy>();
        //DamageTile dmg = other.gameObject.GetComponent<DamageTile>();
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            //Debug.Log("충돌함");
            attackParticle.Play();


            if (OnAttack) // 클릭했을때 공격
            {
                GetComponent<Attack>().AttackTriggerImpulse();
                dmg.TakeDamage(attackPower);
                OnAttack = false;
            }
            else if(!OnAttack && !OnAuto) //가만히 있을때
            {
                GetComponent<Attack>().IdleTriggerImpulse();
                dmg.TakeDamage(IdleAttackPower);
            }


            if (OnAuto) //자동공격
            {
                GetComponent<Attack>().AutoAttackTriggerImpulse();
                dmg.TakeDamage(attackPower*1.2f);
            }

            
        }
    }


}

