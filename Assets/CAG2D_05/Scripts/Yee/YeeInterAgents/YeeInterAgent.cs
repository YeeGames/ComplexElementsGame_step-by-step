using UnityEngine;

namespace CAG2D_05
{
    [System.Serializable]
    public struct YeeInterAgent
    {
        /// <summary>
        /// 个体名称
        /// </summary>
        public string agentName;

        /// <summary>
        /// Yee交互类型
        /// </summary>
        public string yeeInterType;

        /// <summary>
        /// 个体位置
        /// </summary>
        public Vector2 agentPosition;

        /// <summary>
        /// Yee规则力
        /// </summary>
        public Vector2 yeeRuleForce;
    }
}