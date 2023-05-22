﻿using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileSO _projectileInfo;

    private float _currentTime = 0f;

    private void Start()
    {
        GetComponent<Animator>().runtimeAnimatorController = _projectileInfo.AnimatorController;
        GetComponent<SpriteRenderer>().sprite = _projectileInfo.Sprite;
        GetComponent<CircleCollider2D>().radius /= _projectileInfo.Scale;
        this.transform.localScale *= _projectileInfo.Scale;
        transform.Rotate(Vector3.forward,_projectileInfo.Rotation);
        
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
            transform.Translate(_projectileInfo.Speed * Time.deltaTime * Vector2.right);
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