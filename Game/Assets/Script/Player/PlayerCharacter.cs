using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacter : GameManager
{
    public PlayerCharacter Player;

    public GameManager GameManager;

    private Vector2 _moveInput;
    public float MoveSpeed;

    [HideInInspector] public Dictionary<EnumTypes.PlayerSkill, BaseSkill> Skills;
    [SerializeField] private GameObject[] _skillPrefabs;

    private bool binvincibility;
    private Coroutine invincibilityCoroutine;
    private const double invincibilityDurationInSeconds = 3;
    public bool bInvincibility
    {
        get { return binvincibility; }
        set { binvincibility = value; }
    }

    public int CurrentWeaponLevel = 0;
    public int MaxWeaponLevel = 3;

    public Transform[] AddOnTransform;
    public GameObject AddOnPrefab;
    [HideInInspector] public int MaxAddOnCount = 2;

    private void Awake()
    {

        for (int i = 0; i < GameInstance.instance.CurrentAddOnLevel; i++)
        {
            AddOnItem.SpawnAddOn(AddOnPrefab, AddOnTransform[i].position, AddOnTransform[i]);
        }

        InitializeSkills();
    }

    public void DeadProcess()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("MainMenu");
    }

    private void Update()
    {
        UpdateMovement();
        UpdateSkillInput();
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
        if(Camera.main != null)
        {
            Debug.Log(Camera.main.transform.position);
        }
        
        _moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        transform.Translate(new Vector3(_moveInput.x, _moveInput.y, 0f) * (MoveSpeed * Time.deltaTime));

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0f) pos.x = 0f;
        if (pos.x > 1f) pos.x = 1f;
        if (pos.y < 0f) pos.y = 0f;
        if (pos.y > 1f) pos.y = 1f;
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }
    private void UpdateSkillInput()
    {
        if (Input.GetKey(KeyCode.Z)) ActivateSkill(EnumTypes.PlayerSkill.Primary);
        if (Input.GetKeyUp(KeyCode.X)) ActivateSkill(EnumTypes.PlayerSkill.Repair);
        if (Input.GetKeyUp(KeyCode.C)) ActivateSkill(EnumTypes.PlayerSkill.Bomb);
        if (Input.GetKeyUp(KeyCode.V)) ActivateSkill(EnumTypes.PlayerSkill.Freeze);
        if (Input.GetKeyUp(KeyCode.B)) ActivateSkill(EnumTypes.PlayerSkill.Guard);
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
    private void ActivateSkill(EnumTypes.PlayerSkill skillType)
    {
        Debug.Log(skillType);
        if (Skills.ContainsKey(skillType))
        {
            
            if (Skills[skillType].IsAvailable())
            {
                
                Skills[skillType].Activate();
            }
        }
    }
    public void SetInvincibility(bool invin)
    {
        if (invin)
        {
            if (invincibilityCoroutine != null)
            {
                StopCoroutine(invincibilityCoroutine);
            }

            invincibilityCoroutine = StartCoroutine(InvincibilityCoroutine());
        }
    }

    private IEnumerator InvincibilityCoroutine()
    {
        binvincibility = true;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        float invincibilityDuration = 3f;
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);

        yield return new WaitForSeconds(invincibilityDuration);

        binvincibility = false;
        spriteRenderer.color = new Color(1, 1, 1, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            BaseItem baseIteml = collision.gameObject.GetComponent<BaseItem>();
            baseIteml.OnGetItem(GameManager);
        }
    }
}
