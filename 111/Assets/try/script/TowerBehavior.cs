using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.Playables;

    public class TowerBehavior : MonoBehaviour
    {
        public CastleDATA castle;
        public PlayableDirector CastleCollapse;

        public float Health;
        private float Damage;
        private float AttackRange;
        private bool NoHealth = false;

        private void Awake()
        {
            Health = castle.Health;
            Damage = castle.damage;
            AttackRange = castle.AttackRange;
        }

        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (NoHealth)
            {
                CastleCollapse.Play();
                StartCoroutine(WaitDestruction());
            }
        }

        public void TakeDamage(float amout)
        {
            Health -= amout;
            if (Health <= 0)
            {
                NoHealth = true;
            }
        }
        public IEnumerator WaitDestruction()
        {
            Debug.Log("waiting");
            yield return new WaitForSeconds(5);
            
            Destroy(gameObject);
        }

    }

