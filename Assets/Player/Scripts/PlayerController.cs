using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO _initialPlayerStatsSO;
    [SerializeField] private PlayerStatsSO _playerStatsSO;

    private PlayerMovement _playerMovement;

    private bool _dash;
    
    void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
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
    }

    private Vector2 GetInputs()
    {
        float moveHz = Input.GetAxis("Horizontal");
        float moveVt = Input.GetAxis("Vertical");

        _dash = Input.GetKeyDown(KeyCode.LeftShift) && _playerMovement.CanDash;

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
        _playerStatsSO.Health -= damage;
        _playerStatsSO.Health = Mathf.Clamp(_playerStatsSO.Health, 0, _initialPlayerStatsSO.Health);
        Debug.Log(_playerStatsSO.Health);
        StartCoroutine(DamageFlashRed());

        if (_playerStatsSO.Health <= 0)
        {
            ScenesManager.LoadSceneByName("WinScene");
        }
    }

    private IEnumerator DamageFlashRed()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(0.1f);
    }
}
