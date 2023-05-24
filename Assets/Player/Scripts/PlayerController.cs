using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO _initialPlayerStatsSO;
    [SerializeField] private PlayerStatsSO _playerStatsSO;

    [SerializeField] private Image healthBarFill;
    [SerializeField] private Image healthBarFollow;

    [SerializeField] private AudioClip healSound;
    [SerializeField] private AudioClip dmgSound;


    private PlayerMovement _playerMovement;

    private RectTransform _healthBarRectTransform;
    [SerializeField] private Vector3 healthBarOffset = new Vector3(0, 1, 0);


    private bool _dash;
    
    void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _healthBarRectTransform = healthBarFollow.GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadStats();
    }

    void Update()
    {
        if (_playerMovement.IsDashing)
        {
            return;
        }

        var movement = GetInputs();
        _playerMovement.Move(_playerStatsSO.Speed, movement);

        if (_dash && !movement.Equals(Vector2.zero))
        {
            StartCoroutine(_playerMovement.Dash(_playerStatsSO.Speed, movement, _playerStatsSO.DashingPower, _playerStatsSO.DashingTime, _playerStatsSO.DashingCooldown));
        }
        UpdateHealthBarPosition();
    }

    private Vector2 GetInputs()
    {
        float moveHz = Input.GetAxis("Horizontal");
        float moveVt = Input.GetAxis("Vertical");

        _dash = Input.GetKeyDown(KeyCode.LeftShift) && _playerMovement.CanDash;
        UpdateHealthBarPosition();
        return new Vector2(moveHz, moveVt);
    }

    private void LoadStats()
    {
        _playerStatsSO.Health = _initialPlayerStatsSO.Health;
        _playerStatsSO.Speed = _initialPlayerStatsSO.Speed;
        _playerStatsSO.DashingPower = _initialPlayerStatsSO.DashingPower;
        _playerStatsSO.DashingTime = _initialPlayerStatsSO.DashingTime;
        _playerStatsSO.DashingCooldown = _initialPlayerStatsSO.DashingCooldown;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            other.TryGetComponent<Projectile>(out var projectile);
            TakeDamage(projectile.GetDamage());
            Destroy(other.gameObject);
        }
    }

    private void TakeDamage(float damage)
    {
        if (damage > 0){
            AudioSource.PlayClipAtPoint(dmgSound, transform.position, 1f);
        }else{
            AudioSource.PlayClipAtPoint(healSound, transform.position, 1f);
        }

        _playerStatsSO.Health -= damage;
        _playerStatsSO.Health = Mathf.Clamp(_playerStatsSO.Health, 0, _initialPlayerStatsSO.Health);
        Debug.Log(_playerStatsSO.Health);
        StartCoroutine(DamageFlashRed());

        if (_playerStatsSO.Health <= 0)
        {
            ScenesManager.LoadSceneByName("WinScene");
        }

        healthBarFill.fillAmount = _playerStatsSO.Health / _initialPlayerStatsSO.Health;
    }

    private IEnumerator DamageFlashRed()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(0.1f);
    }

    private void UpdateHealthBarPosition()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + healthBarOffset);
        _healthBarRectTransform.position = screenPos;
    }
}
