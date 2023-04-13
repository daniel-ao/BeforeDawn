using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityRoyale
{
    [CreateAssetMenu(fileName = "Castle", menuName = "BeforeDown/Castle")]
    public class CastleDATA : ScriptableObject
    {
        public float Health = 15f;
        public float damage = 3f;
        public float AttackRange = 5f;
    }
}
