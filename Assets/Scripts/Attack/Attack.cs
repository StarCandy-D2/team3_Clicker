using Cinemachine;
using System.Collections;
using System.Data.Common;
using PlayerUpgrade;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Attack Instance;
    public PlayerData playerData;
    public WeaponData weaponData;

    public float IdleSpeed = 5f; //튀어오르는 기본 속도
    // public float gravity = -9.8f; IdleSpeed로 통함
    public float attackPower =>  playerData.GetStat(StatType.atk) + weaponData.Attack; //임시 공격력
    public float IdleAttackPower => attackPower * 0.1f; //Idle 공격력 (클릭 안했을때)
    private float velocity;
    private float currentHeight;
    private float maxHeight = 0.5f;
    private float minHeight = -0.5f;
    public bool isJump = true;
    public bool isAttack = true;
    public bool OnAttack;
    public float AttackDelay = 0.3f; //어택딜레이
    public float AttackTimer = 0;

    public float Maxdurability => weaponData.MaxDurability; // 내구도 테스트 임시 변수
    public float CurrentDurability
    {
        get => weaponData.CurrentDurability;
        set => weaponData.CurrentDurability = value;
    }
    public float durabilityTimer = 0; //내구도 테스트 임시 타이머
    public float recoveryDurabilityTime = 5f;
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
        weaponData.CurrentDurability = Maxdurability;
        Debug.Log(attackPower);
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
        if (CurrentDurability == 0)
        {
            durabilityTimer += Time.deltaTime;
            if (durabilityTimer >= recoveryDurabilityTime)
            {
                CurrentDurability = Maxdurability;
                durabilityTimer = 0;
            }
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
                CurrentDurability -= 2f; //내구도 2감소
                if (CurrentDurability <= 0f)
                {
                    CurrentDurability = 0f;
                }
            }

        }

        //정해진 높이에서 클릭으로 공격 가능
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

        // 자동 공격 테스트 코드
        if (Input.GetKeyDown(KeyCode.Space) && OnAuto == false)
        {
            OnAuto = true;
            Debug.Log("자동공격 실행");
            StartCoroutine(AutoAttack());
        }
    }

    //자동 공격 코루틴
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

    //타일과 충돌 했을때 공격 로직
    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy dmg = other.gameObject.GetComponent<Enemy>();
        //DamageTile dmg = other.gameObject.GetComponent<DamageTile>();
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log("충돌함");
            attackParticle.Play();


            if (OnAttack) // 클릭했을때 공격
            {
                GetComponent<Attack>().AttackTriggerImpulse();
                dmg.TakeDamage(attackPower); //클릭 공격 데미지
                OnAttack = false;
            }
            else if(!OnAttack && !OnAuto) //가만히 있을때
            {
                GetComponent<Attack>().IdleTriggerImpulse();
                dmg.TakeDamage(IdleAttackPower); //기본 공격 데미지
            }
            

            if (OnAuto) //자동공격
            {
                GetComponent<Attack>().AutoAttackTriggerImpulse();
                dmg.TakeDamage(attackPower*1.2f); // 자동 공격 데미지 클릭 공격 데미지 1.2배율
            }

            
        }
    }


}

