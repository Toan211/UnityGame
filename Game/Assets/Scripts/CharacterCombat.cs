﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    public float attackSpeed = 1f;
    private float attackCooldown = 0f;
    const float combatCooldown = 5;
    float lastAttackTime;   

    public float attackDelay = .6f;

    public bool InCombat { get; private set; }

    public event System.Action OnAttack;

    CharacterStats opponentStats;

    CharacterStats myStats;
    void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }


    void Update()
    {
        attackCooldown -= Time.deltaTime;
        if(Time.time - lastAttackTime > combatCooldown)
        {
            InCombat = false;
        }

    }

    public void Attack(CharacterStats targetStats)
    {
        if(attackCooldown <= 0f)
        {

            opponentStats = targetStats;
            if (OnAttack != null)
            {
                OnAttack();
            }

            attackCooldown = 1f / attackSpeed;
            InCombat = true;
            lastAttackTime = Time.time;
        }
        
    }



    public void AttackHit_AnimationEvent()
    {
        opponentStats.TakeDamage(myStats.damage.GetValue());

        if (opponentStats.currentHealth <= 0)
        {
            InCombat = false;
        }
    }
} 
