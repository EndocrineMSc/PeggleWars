using EnumCollection;
using Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        #region Fields

        protected Animator _animator;

        #endregion

        #region Properties

        [SerializeField] protected EnemyAttackType _enemyAttackType;

        public EnemyAttackType AttackType
        {
            get { return _enemyAttackType; }
            private set { _enemyAttackType = value; }
        }


        [SerializeField] protected int _damage;

        public int Damage
        {
            get { return _damage; }
            set { _damage = value; }
        }

        [SerializeField] protected int _health = 20;

        public int Health
        {
            get { return _health; }
            private set { _health = value; }
        }

        #endregion

        #region Protected Functions

        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();
            _animator.SetTrigger("Spawn");
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.name.Contains("Attack"))
            {
                _animator.SetTrigger("Hurt");
            }
        }

        #endregion

        #region Public Functions

        public virtual void LoseHealth(int damage)
        {
            _health -= damage;

            if (_health <= 0)
            {
                _animator.SetTrigger("Death");
                EnemyManager.Instance.KillEnemy();
            }
        }

        #endregion
    }
}
