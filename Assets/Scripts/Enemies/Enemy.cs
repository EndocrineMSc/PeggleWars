using EnumCollection;
using Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PeggleAttacks.AttackVisuals.PopUps;

namespace Enemies
{
    [RequireComponent(typeof(PopUpSpawner))]
    public class Enemy : MonoBehaviour
    {
        #region Fields

        protected Animator _animator;
        protected PopUpSpawner _popUpSpawner;

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
            PlaySpawnSound();
            _popUpSpawner = GetComponent<PopUpSpawner>();
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.name.Contains("Attack"))
            {
                _animator.SetTrigger("Hurt");
                PlayHurtSound();
            }
        }

        protected virtual void HandleDeath()
        {
            _animator.SetTrigger("Death");
            Collider2D collider = GetComponent<Collider2D>();
            collider.enabled = false;
            EnemyManager.Instance.Enemies.Remove(this);
        }

        protected virtual void PlaySpawnSound()
        {
            //overrides will implement spawn sounds here
        }

        protected virtual void PlayHurtSound()
        {
            //overrides will implement hurt sounds here
        }

        #endregion

        #region Public Functions

        public virtual void LoseHealth(int damage)
        {
            _health -= damage;

            _popUpSpawner.SpawnPopUp(damage);

            if (_health <= 0)
            {
                HandleDeath();
                StartCoroutine(DestroyEnemy());
            }
        }

        #endregion

        #region IEnumerators

        protected virtual IEnumerator DestroyEnemy()
        {
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }

        #endregion
    }
}
