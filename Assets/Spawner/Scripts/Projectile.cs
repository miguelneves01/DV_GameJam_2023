using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileSO _projectileInfo;

    private float _currentTime = 0f;

    private void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = _projectileInfo.Sprite;
        this.transform.localScale *= _projectileInfo.Scale;
    }

    public GameObject Clone(Vector2 location)
    {
        return Instantiate(this.gameObject, location, Quaternion.identity);
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime < _projectileInfo.Time2Live)
        {
            transform.Translate(_projectileInfo.Speed * Time.deltaTime * Vector2.down);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public float GetDamage()
    {
        return _projectileInfo.Damage;
    }
}