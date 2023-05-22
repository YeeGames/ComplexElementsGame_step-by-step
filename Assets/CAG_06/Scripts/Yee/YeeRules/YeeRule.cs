using UnityEngine;

namespace CAG_06
{
    /// <summary>
    /// Yee 规则 //NOTE 新增
    /// </summary>
    public class YeeRule : MonoBehaviour
    {
        // /// <summary>
        // /// 规则设置项
        // /// </summary>
        // [HideInInspector] public RuleSettings rset;

        /// <summary>
        /// 规则圆圈碰撞器半径
        /// </summary>
        [SerializeField] internal float ruleCircleColliderRadius;

        /// <summary>
        /// 力场强度
        /// </summary>
        [SerializeField] protected float fieldStrength;

        /// <summary>
        /// 力场强度指数衰减系数
        /// </summary>
        [SerializeField] protected float expCoefficient;

        /// <summary>
        /// 力场方向
        /// </summary>
        [SerializeField] protected int direction;

        /// <summary>
        /// 己方规则力
        /// </summary>
        protected Vector2 ThisRuleForce;

        /// <summary>
        /// 对方规则力
        /// </summary>
        protected Vector2 ThatRuleForce;

        /// <summary>
        /// 距离，从己方个体之位置到对方个体之位置
        /// </summary>
        protected float distance_from_a1_to_a2 = 0;

        /// <summary>
        /// 向量，从己方个体之位置到对方个体之位置
        /// </summary>
        protected Vector2 vector_from_a1_to_a2 = Vector2.zero;

        /// <summary>
        /// 方向，从己方个体之位置到对方个体之位置
        /// </summary>
        protected Vector2 direction_from_a1_to_a2 = Vector2.zero;

        /// <summary>
        /// 设置规则
        /// </summary>
        /// <param name="ruleSettings">rule settings</param>
        internal void SetRule(RuleSettings ruleSettings)
        {
            this.ruleCircleColliderRadius = ruleSettings.forceEffectiveRadius;
            this.fieldStrength = ruleSettings.fieldStrength;
            this.expCoefficient = ruleSettings.expCoefficient;
            this.direction = ruleSettings.direction;
        }

        /// <summary>
        /// 计算交互类型
        /// </summary>
        /// <param name="thisYeeType">己方Yee类型</param>
        /// <param name="thatYeeType">对方Yee类型</param>
        internal virtual string CalcInterType(string thisYeeType, string thatYeeType)
        {
            return default;
        }

        /// <summary>
        /// 计算规则力
        /// </summary>
        /// <param name="yeeInterType">Yee交互类型</param>
        /// <param name="pos1">己方个体位置</param>
        /// <param name="pos2">对方个体位置</param>
        internal virtual Vector2 CalcRuleForce(string yeeInterType, Vector2 pos1, Vector2 pos2)
        {
            return default;
        }
    }
}