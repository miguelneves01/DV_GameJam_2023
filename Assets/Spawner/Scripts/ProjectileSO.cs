using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile",menuName = "Spawner/Projectile")]
public class ProjectileSO : ScriptableObject
{
    [field: SerializeField] public int Damage { private set; get; }
    [field: SerializeField] public float Speed { private set; get; }
    [field: SerializeField] public Sprite Sprite { private set; get; }
    [field: SerializeField] public float Scale { private set; get; }
    [field: SerializeField] public float Rotation { private set; get; }
    [field: SerializeField] public float Time2Live { private set; get; }
    [field: SerializeField] public RuntimeAnimatorController AnimatorController { private set; get; }
    [field: SerializeField] public RuntimeAnimatorController HitAnimatorController { private set; get; }


}