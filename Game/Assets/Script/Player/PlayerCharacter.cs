using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

public class PlayerCharacter : GameManager
{
    public PlayerCharacter Player;

    public GameManager GameManager;

    public PlayerUI PlayerUI;

    public SpriteRenderer SpriteRenderer;

    public Animator Animator;

    private Vector2 _moveInput;
    public float MoveSpeed;

    [HideInInspector] public Dictionary<EnumTypes.PlayerSkill, BaseSkill> Skills;
    [SerializeField] private GameObject[] _skillPrefabs;

    private bool bdead = false;
    private bool binvincibility;
    private Coroutine invincibilityCoroutine;
    private const double invincibilityDurationInSeconds = 3;
    public bool bInvincibility
    {
        get { return binvincibility; }
        set { binvincibility = value; }
    }

    public int CurrentWeaponLevel = 0;
    public int MaxWeaponLevel = 5;

    public AddOnItem AddOnItem;
    public Transform[] AddOnTransform;
    public GameObject AddOnPrefab;
    [HideInInspector] public int MaxAddOnCount = 2;

    public bool bInvincibilityCheat = false;

    private void Start()
    {
        Animator.SetBool("Dead", false);
        for (int i = 0; i < GameInstance.instance.CurrentAddOnLevel; i++)
        {
            AddOnItem.SpawnAddOn(AddOnPrefab, AddOnTransform[i].position, AddOnTransform[i]);
        }

        InitializeSkills();
    }

    public IEnumerator DeadProcess()
    {
        bdead = true;
        SoundManager.PlaySFX(8);
        gameObject.layer = 14;
        Animator.SetBool("Dead", true);
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
        SceneManager.LoadScene("Main");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject obj in enemies)
            {
                Enemy enemy = obj?.GetComponent<Enemy>();
                enemy?.Dead();
            }
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            PlayerCharacter.CurrentWeaponLevel = 5;
            GameInstance.instance.CurrentPlayerWeaponLevel = PlayerCharacter.CurrentWeaponLevel;
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            PlayerCharacter.InitskillCoolDown();
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {
            PlayerCharacter.GetComponent<PlayerHPSystem>().InitHealth();
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            PlayerCharacter.GetComponent<PlayerFuelSystem>().InitFuel();
        }

        if (Input.GetKeyDown(KeyCode.F6))
        {
            StageClear();
        }

        if (Input.GetKeyDown(KeyCode.F7))
        {
            if (bInvincibilityCheat)
            {
                bInvincibilityCheat = false;
                return;
            }
            if (!bInvincibilityCheat)
            {
                bInvincibilityCheat = true;
                return;
            }
        }
        if (!bdead)
        {
            UpdateMovement();
            UpdateSkillInput();
        }        
    }
    
    public void InitskillCoolDown()
    {
        foreach (var skill in Skills.Values)
        {
            skill.InitCoolDown();
        }
    }
    private void UpdateMovement()
    {
        _moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        transform.Translate(new Vector3(_moveInput.x, _moveInput.y, 0f) * (MoveSpeed * Time.deltaTime));

        if(_moveInput.x > 0)
        {
            Animator.SetBool("Right", true);
        }
        else
        {
            Animator.SetBool("Right", false);
        }
        if (_moveInput.x < 0)
        {
            Animator.SetBool("Left", true);
        }
        else
        {
            Animator.SetBool("Left", false);
        }
        if (_moveInput.x == 0)    
        {
            Animator.SetBool("Idle", true);
        }
        else
        {
            Animator.SetBool("Idle", false);
        }

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0f) pos.x = 0f;
        if (pos.x > 1f) pos.x = 1f;
        if (pos.y < 0f) pos.y = 0f;
        if (pos.y > 1f) pos.y = 1f;
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }
    private void UpdateSkillInput()
    {
        if (Input.GetKey(KeyCode.Z)) ActivateSkill(EnumTypes.PlayerSkill.Primary, true);
        if (Input.GetKeyUp(KeyCode.X)) ActivateSkill(EnumTypes.PlayerSkill.Repair, false);
        if (Input.GetKeyUp(KeyCode.C)) ActivateSkill(EnumTypes.PlayerSkill.Bomb, false);
        if (Input.GetKeyUp(KeyCode.V)) ActivateSkill(EnumTypes.PlayerSkill.Freeze, false);
        if (Input.GetKeyUp(KeyCode.B)) ActivateSkill(EnumTypes.PlayerSkill.Guard, false);
    }

    private void InitializeSkills()
    {
        Skills = new Dictionary<EnumTypes.PlayerSkill, BaseSkill>();

        for (int i = 0; i < _skillPrefabs.Length; i++)
        {
            AddSkill((EnumTypes.PlayerSkill)i, _skillPrefabs[i]);
        }

        CurrentWeaponLevel = GameInstance.instance.CurrentPlayerWeaponLevel;
    }

    private void AddSkill(EnumTypes.PlayerSkill skillType, GameObject prefab)
    {
        GameObject skillObject = Instantiate(prefab, transform.position, Quaternion.identity);
        skillObject.transform.parent = this.transform;

        if (skillObject != null)
        {
            BaseSkill skillComponent = skillObject.GetComponent<BaseSkill>();
            skillComponent.Init(PlayerCharacter);
            Skills.Add(skillType, skillComponent);
        }
    }
    private void ActivateSkill(EnumTypes.PlayerSkill skillType, bool bprimary)
    {
        if (Skills.ContainsKey(skillType))
        {
            if (Skills[skillType].IsAvailable())
            {
                Skills[skillType].Activate();
            }
            else
            {
                if(bprimary)
                {
                    return;
                }
                else
                {
                    PlayerUI.NoticeSkillCooldown(skillType);
                }               
            }
        }
    }
    public void SetInvincibility(bool invin,float inviTime)
    {
        if (invin)
        {
            if (invincibilityCoroutine != null)
            {
                StopCoroutine(invincibilityCoroutine);
            }

            invincibilityCoroutine = StartCoroutine(InvincibilityCoroutine(inviTime));
        }
    }

    private IEnumerator InvincibilityCoroutine(float inviTime)
    {
        binvincibility = true;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        float invincibilityDuration = inviTime;
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);

        yield return new WaitForSeconds(invincibilityDuration);

        binvincibility = false;
        spriteRenderer.color = new Color(1, 1, 1, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            BaseItem baseIteml = collision.gameObject.GetComponent<BaseItem>();
            baseIteml.OnGetItem(GameManager);
            SoundManager.PlaySFX(6);
        }
    }
}
