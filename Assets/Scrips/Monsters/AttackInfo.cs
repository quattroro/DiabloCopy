using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    Melee,
    Projectile,
    AreaOfEffect,
    AttackTypeMax
}

public class AttackInfo
{
    public AttackType attackType;
    public GameObject targetObject;

    public float attackInterval;

    public float startDelay;
    public float recorverDelay;

    public int attackDamage;
    public GameObject attackObject;

    public GameObject attackEffect;
    public AnimationClip attackAni;


}
