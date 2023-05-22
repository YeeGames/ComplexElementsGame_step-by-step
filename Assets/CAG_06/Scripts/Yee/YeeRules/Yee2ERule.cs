using System;
using Unity.Mathematics;
using UnityEngine;

namespace CAG_06
{
    /// <summary>
    /// Yee 2元素规则
    /// </summary>
    public class Yee2ERule : YeeRule
    {
        /// <summary>
        /// Yee 2元素类型
        /// </summary>
        private Yee2EType YeeType = new Yee2EType();

        /// <summary>
        /// 判断类型交互规则 
        /// </summary>
        /// <param name="thisYeeType">己方YeeType类型</param>
        /// <param name="thatYeeType">对方YeeType类型</param>
        internal override string CalcInterType(string thisYeeType, string thatYeeType)
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
        internal override Vector2 CalcRuleForce(string yeeInterType, Vector2 pos1, Vector2 pos2)
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