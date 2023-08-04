using System;
using Unity.Mathematics;
using UnityEngine;

namespace CEG_04
{
    /// <summary>
    /// Yee 2元素规则 //NOTE 新增
    /// </summary>
    public class Yee2ERule : MonoBehaviour
    {
        /// <summary>
        /// 规则设置项
        /// </summary>
        [HideInInspector] public RuleSettings rset;

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
        /// Yee 2元素规则
        /// </summary>
        private Yee2EType YeeType = new Yee2EType();

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
        /// 判断类型交互规则 
        /// </summary>
        /// <param name="thisYeeType">己方YeeType类型</param>
        /// <param name="thatYeeType">对方YeeType类型</param>
        internal string CalcInterType(string thisYeeType, string thatYeeType)
        {
            return YeeType.YeeRuleAdjecentMatrix[Array.IndexOf(YeeType.FromYeeTypeArray, thisYeeType), Array.IndexOf(YeeType.ToYeeTypeArray, thatYeeType)];
        }

        /// <summary>
        /// <para>计算规则力：</para>
        /// <para>假设方向`direction`为正向</para>
        /// <para>如果是交互状态己方为`Same`，则说明对方也为`Same`，则双方都受到拉力，形成效果力为互相吸引；</para>
        /// <para>如果是交互状态己方为`Opposite`，则说明对方也为`Opposite`，则双方都受到推力，形成效果力为互相排斥；</para>
        /// </summary>
        /// <param name="yeeInterType">Yee交互类型</param>
        /// <param name="pos1">己方个体位置</param>
        /// <param name="pos2">对方个体位置</param>
        /// <returns>己方所受的规则力</returns>
        internal Vector2 CalcRuleForce(string yeeInterType, Vector2 pos1, Vector2 pos2)
        {
            vector_from_a1_to_a2 = (Vector2) (pos2 - pos1);
            direction_from_a1_to_a2 = vector_from_a1_to_a2.normalized;
            distance_from_a1_to_a2 = direction_from_a1_to_a2.magnitude;
            if (yeeInterType == Yee2EInterTypeEnum.Same.ToString())
            {
                this.ThisRuleForce = fieldStrength * ((float) -direction * direction_from_a1_to_a2) /
                                     math.pow(distance_from_a1_to_a2, expCoefficient);
                ThatRuleForce = fieldStrength * ((float) -direction * direction_from_a1_to_a2) /
                                math.pow(distance_from_a1_to_a2, expCoefficient);
            }
            else if (yeeInterType == Yee2EInterTypeEnum.Opposite.ToString())
            {
                this.ThisRuleForce = fieldStrength * ((float) +direction * direction_from_a1_to_a2) /
                                     math.pow(distance_from_a1_to_a2, expCoefficient);
                ThatRuleForce = fieldStrength * ((float) +direction * direction_from_a1_to_a2) /
                                math.pow(distance_from_a1_to_a2, expCoefficient);
            }

            return this.ThisRuleForce;
        }
    }
}