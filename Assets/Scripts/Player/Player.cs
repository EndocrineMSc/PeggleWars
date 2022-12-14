using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PeggleWars;
using EnumCollection;
using PeggleWars.TurnManagement;

namespace PeggleWars.Player
{
    public class Player : MonoBehaviour
    {

        #region Fields

        private SpriteRenderer _spriteRenderer;
        private Color _color;

        public static Player Instance { get; private set; }
        private Animator _animator;
        private CardTurnManager _cardTurnManager;

        #endregion

        #region Properties

        [SerializeField] private int _health;

        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }

        private int _shield;

        public int Shield
        {
            get { return _shield; }
            set { _shield = value; }
        }

        private int _maxHealth;

        public int MaxHealth
        {
            get { return _maxHealth; }
            set { _maxHealth = value; }
        }

        #endregion

        #region Public Functions

        public void TakeDamage(int damage)
        {
            int calcDamage = damage;
            if (_shield > calcDamage)
            {
                _shield -= damage;
            }
            else if (_shield > 0)
            {
                calcDamage = damage - _shield;
                _shield = 0;
                _health -= calcDamage;
                StartCoroutine(nameof(ColorShiftDamage));
                //_animator.SetTrigger("Hurt");
            }
            else
            {
                _health -= damage;
                StartCoroutine(nameof(ColorShiftDamage));
                //_animator.SetTrigger("Hurt");
            }
        }

        #endregion

        #region Private Functions

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
            _maxHealth = _health;
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _color = _spriteRenderer.color;
            _cardTurnManager = CardTurnManager.Instance;

            _cardTurnManager.StartCardTurn += OnCardTurnStart;
        }

        private void OnDisable()
        {
            _cardTurnManager.StartCardTurn -= OnCardTurnStart;
        }

        private void OnCardTurnStart()
        {
            Instance.Shield = 0;
        }
        #endregion

        #region IEnumerators

        private IEnumerator ColorShiftDamage()
        {
            _spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.2f);
            _spriteRenderer.color = _color;
        }

        #endregion
    }
}
