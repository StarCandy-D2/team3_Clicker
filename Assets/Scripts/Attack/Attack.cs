using Cinemachine;
using PlayerUpgrade;
using System.Collections;
using System.Data.Common;
using PlayerUpgrade;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.UI;

public class Attack : MonoBehaviour
{

    public PlayerData playerData;
    public WeaponData weaponData;
    public SettingUI settingUI;
    public float IdleSpeed = 5f; //튀어오르는 기본 속도
    // public float gravity = -9.8f; IdleSpeed로 통함
    public float attackPower => playerData.GetStat(StatType.atk); //임시 공격력
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
    private bool isCri = false;
    public float Maxdurability => weaponData.MaxDurability; // 내구도 테스트 임시 변수
    public float CurrentDurability
    {
        get => weaponData.CurrentDurability;
        set => weaponData.CurrentDurability = value;
    }
    public float durabilityTimer = 0; //내구도 테스트 임시 타이머
    public float recoveryDurabilityTime = 5f;
    //자동공격
    public float autoAttackDuration => weaponData.AutoAttackDuration; //자동공격 시간
    public float autoAttackSpeed = 50f; //공격속도
    public bool OnAuto;

    private float touchDuration = 0f; //터치 시간
    private float requiredHoldTime = 1f;//꾹 누르기를 위한 변수

    //0.5 -0.5


    public CinemachineImpulseSource idleimpulseSource; //idle 카메라 shake
    public CinemachineImpulseSource attackimpulseSource; // attack 카메라 shake
    public CinemachineImpulseSource autoattackimpulseSource; //autoattack 카메라 shake

    //파티클 넣으면 됩니다
    public ParticleSystem Crust_Particle;
    public ParticleSystem InnerCore_Particle;
    public ParticleSystem LowerMantle_Particle;
    public ParticleSystem OuterCore_Particle;
    public ParticleSystem UpperMantle_Particle;
    public TrailRenderer trailRenderer;

    //데미지 텍스트 박스
    public GameObject damageTextPrefab;
    public Transform spawnPosition; //데미지 위치

    public Image Autotime;
    public Image Durabilitytime;
    private Dictionary<string, ParticleSystem> tagToParticle;



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
    public void impulse()
    {

        if (!settingUI.shakeonoff)
        {

            if (OnAttack)
            {

                GetComponent<Attack>().AttackTriggerImpulse();
            }

            else if (OnAuto)
            {

                GetComponent<Attack>().AutoAttackTriggerImpulse();
            }

            else if (!OnAttack && !OnAuto)
            {

                GetComponent<Attack>().IdleTriggerImpulse();
            }

        }
    }
    void Start()
    {
        currentHeight = transform.position.y;
        trailRenderer.emitting = false;
        weaponData.CurrentDurability = Maxdurability;
        Debug.Log(attackPower);

        tagToParticle = new Dictionary<string, ParticleSystem>()
    {
        { "Crust", Crust_Particle },
        { "InnerCore", InnerCore_Particle },
        { "OuterCore", OuterCore_Particle },
        { "UpperMantle", UpperMantle_Particle },
        { "LowerMantle", LowerMantle_Particle }
    };



    }

    void Update()
    {
        PlayerAttack();
        AttackTimer += Time.deltaTime;
        if (OnAttack || OnAuto)
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
            Durabilitytime.fillAmount = 1f;
            Durabilitytime.fillAmount = 1f - (durabilityTimer / recoveryDurabilityTime);
            if (durabilityTimer >= recoveryDurabilityTime)
            {
                CurrentDurability = Maxdurability;


                durabilityTimer = 0;


            }
        }
    }

    private void PlayHitParticle(string tag)
    {
        if (tagToParticle.ContainsKey(tag))
        {
            tagToParticle[tag].Play();
            Debug.Log(tag);
        }
    }
    public void PlayerAttack()
    {
        // 마우스 클릭 시 빠르게 낙하하도록 처리
        if (Input.GetMouseButtonDown(0) && AttackTimer >= AttackDelay && OnAuto == false && CurrentDurability != 0)
        {
            Debug.Log("클릭 인식확인");
            AttackTimer = 0f;
            if (isAttack)
            {
                OnAttack = true;
                isJump = false;
                velocity = -IdleSpeed * 2f; // 빠르게 낙하

                playerData.SetStat(StatType.CurEnergy, playerData.GetStat(StatType.CurEnergy) - 2f);
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
        // 자동 공격 조건 (모바일 탭 1초 이상 유지 시 발동)
#if UNITY_EDITOR
        // 에디터 테스트용 마우스 클릭 유지
        if (!OnAuto)
        {
            if (Input.GetMouseButton(0))
            {
                touchDuration += Time.deltaTime;
                if (touchDuration >= requiredHoldTime)
                {
                    OnAuto = true;
                    Debug.Log("자동공격 실행 (에디터)");
                    StartCoroutine(AutoAttack());
                }
            }
            else
            {
                touchDuration = 0f;
            }
        }
#else
// 모바일 터치 유지 감지
if (!OnAuto && Input.touchCount > 0)
{
    Touch touch = Input.GetTouch(0);

    if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
    {
        touchDuration += Time.deltaTime;
        if (touchDuration >= requiredHoldTime)
        {
            OnAuto = true;
            Debug.Log("자동공격 실행 (모바일)");
            StartCoroutine(AutoAttack());
        }
    }
    else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
    {
        touchDuration = 0f;
    }
}
else if (Input.touchCount == 0)
{
    touchDuration = 0f;
}
#endif
    }
    //자동 공격 코루틴
    private IEnumerator AutoAttack()
    {
        float timer = 0f;
        Autotime.fillAmount = 1f;

        while (timer < autoAttackDuration)
        {
            Autotime.fillAmount = 1f - (timer / autoAttackDuration);
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
            Autotime.fillAmount = 0f;
        }
        OnAuto = false;
        Debug.Log("자동공격 종료");
    }
    //타일과 충돌 했을때 공격 로직
    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy dmg = other.gameObject.GetComponent<Enemy>();
        // DamageTile dmg = other.gameObject.GetComponent<DamageTile>();

        float randomValue = Random.value;
        float iscritical;
        if (playerData.GetStat(StatType.critRate) / 100 >= randomValue) //크리 떴을때 데미지 배율
        {
            iscritical = 2f;
            isCri = true;
        }
        else
        {

            iscritical = 1f;
            isCri = false;
        }
        Debug.Log($"{iscritical}");

        //레이어가 Enemy이고 파티클on일때만 파티클 재생
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log(settingUI.particleonoff);

            if (!settingUI.particleonoff)
            {

            PlayHitParticle(other.gameObject.tag);

            }

            //switch (other.gameObject.tag)
            //{
            //    case "Crust":
            //        Crust_Particle.Play();

            //        Debug.Log("크러스트");
            //        break;

            //    case "InnerCore":
            //        InnerCore_Particle.Play();
            //        Debug.Log("내핵");
            //        break;

            //    case "OuterCore":
            //        OuterCore_Particle.Play();
            //        Debug.Log("외핵");
            //        break;

            //    case "UpperMantle":
            //        UpperMantle_Particle.Play();
            //        Debug.Log("상부맨틀");
            //        break;
            //    case "LowerMantle":
            //        LowerMantle_Particle.Play();
            //        Debug.Log("하부맨틀");
            //        break;

            //}
            //딕셔너리로 수정
            if (OnAttack) // 클릭했을때 공격
            {
                impulse();
                dmg.TakeDamage(attackPower * iscritical); //클릭 공격 데미지
                Vector3 spawnPos = transform.position + new Vector3(0, -2f, 0);
                ShowDamage(attackPower * iscritical, spawnPos);
                OnAttack = false;
            }
            else if (!OnAttack && !OnAuto) //가만히 있을때
            {
                impulse();
                dmg.TakeDamage(IdleAttackPower * iscritical); //기본 공격 데미지
                Vector3 spawnPos = transform.position + new Vector3(0, -2f, 0);
                ShowDamage(attackPower * iscritical, spawnPos);
            }


            if (OnAuto) //자동공격
            {
                impulse();
                dmg.TakeDamage(attackPower * 1.2f * iscritical); // 자동 공격 데미지 클릭 공격 데미지 1.2배율
                Vector3 spawnPos = transform.position + new Vector3(0, -2f, 0);
                ShowDamage(attackPower * iscritical, spawnPos);
            }
        }
    }



    public void ShowDamage(float damage, Vector3 position)
    {

        GameObject obj = Instantiate(damageTextPrefab, spawnPosition);

        RectTransform rectTransform = obj.GetComponent<RectTransform>();
        Vector2 basePosition = Vector2.down * 195f;


        Vector2 randomOffset = new Vector2(
         Random.Range(-30f, 30f),
         Random.Range(-15f, 15f)
        );
        rectTransform.anchoredPosition = basePosition + randomOffset;
        TMP_Text text = obj.GetComponent<TMP_Text>();
        text.text = damage.ToString("F1");


        rectTransform.DOAnchorPos(rectTransform.anchoredPosition + Vector2.up * 100f, 1f).SetEase(Ease.OutCubic);
        text.DOFade(0f, 1f).OnComplete(() => Destroy(obj));

        if (isCri)
        {

            text.color = Color.red;
            text.fontSize += 10;

        }

    }


}

