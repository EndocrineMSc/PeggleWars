using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumCollection;
using Enemies;
using PeggleOrbs;

namespace PeggleAttacks.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        #region Fields

        protected PlayerAttack _attack;
        [SerializeField] protected PlayerAttackTarget _target;
        [SerializeField] protected float _attackFlySpeed = 10;


        #endregion

        #region Properties

        protected int _damage;

        public int Damage
        {
            get { return _damage; }
            private set { _damage = value; }
        }

        #endregion

        #region Public Functions

        public virtual void ShootAttack(Vector3 startPosition, PlayerAttack playerAttack)
        {
            Enemy enemy = null;
            Vector3 targetPosition = new();

            if (EnemyManager.Instance.Enemies.Count > 0 ) 
            {
                switch (_target)
                {
                    case PlayerAttackTarget.FirstEnemy:
                        enemy = EnemyManager.Instance.Enemies[0];
                        targetPosition = enemy.transform.position;
                        break;
                }

                Vector3 direction = targetPosition - startPosition;

                Debug.Log("StartPosition: " + startPosition + " to " + direction + " to " + targetPosition);

                float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                PlayerAttack tempAttack = Instantiate(playerAttack, startPosition, Quaternion.Euler(0, 0, rotation * 2.5f));
                Rigidbody2D rigidbody = tempAttack.GetComponent<Rigidbody2D>();

                rigidbody.velocity = new Vector2(direction.x, direction.y).normalized * _attackFlySpeed;
            }           
        }

        #endregion

        #region Protected Functions

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            Enemy enemy = null;
            switch (_target)
            {
                case PlayerAttackTarget.FirstEnemy:
                enemy = EnemyManager.Instance.Enemies[0];
                break;                       
            }

            if (enemy != null)
            {
                enemy.LoseHealth(_damage);
            }

            //ToDo: Polish with particle effect or animation here
            Destroy(gameObject);       
        }

        protected virtual void Awake()
        {
            _attack = this;
        }

        #endregion
    }
}
