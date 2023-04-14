using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hero", menuName = "BeforeDown/Hero")]
    public class HeroData : ScriptableObject
    {
        public float Health = 30f;
        public float Damage = 8f;
        public float Speed = 2f;
        public float AttackRange = 3f;
        public float SightRange = 8f;
        public float TimeAttack = 1f;
    }
