using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PeggleAttacks.AttackManager;

namespace PeggleMana.RottedMana
{
    public class RottedMana : Mana
    {
        #region Fields

        private PlayerAttackManager _playerAttackManager;
        [SerializeField] private float _attackModifier = 0.8f;

        #endregion

        #region Private Functions

        private void Start()
        {
            _playerAttackManager = PlayerAttackManager.Instance;
        }

        private void OnDestroy()
        {
            _playerAttackManager.ModifiyDamage(_attackModifier);
        }

        #endregion
    }
}
