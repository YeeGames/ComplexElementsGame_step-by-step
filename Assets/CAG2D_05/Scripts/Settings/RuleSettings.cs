using UnityEngine;

namespace CAG2D_05
{
    [CreateAssetMenu]
    public class RuleSettings : ScriptableObject
    {
        /// <summary>
        /// Yee类型族
        /// </summary>
        public YeeType YeeType = new YeeType();

        /// <summary>
        /// 力场强度
        /// </summary>
        public float fieldStrength = 10f;
        
        /// <summary>
        /// 力场影响半径
        /// </summary>
        public float forceEffectiveRadius = 5f; 
        
        /// <summary>
        /// 力场衰减指数系数。默认为1，表示线性衰减。
        /// </summary>
        public float expCoefficient = 1f; 
        
        /// <summary>
        /// 力场适量反向。默认为1，表示不反向。
        /// </summary>
        public int direction = 1;
    }
}