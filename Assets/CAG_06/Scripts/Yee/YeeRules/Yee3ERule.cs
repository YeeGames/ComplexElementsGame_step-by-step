using System;
using Unity.Mathematics;
using UnityEngine;

namespace CAG_06
{
    /// <summary>
    /// Yee 3元素规则
    /// </summary>
    public class Yee3ERule : YeeRule
    {
        /// <summary>
        /// Yee 3元素类型
        /// </summary>
        private Yee3EType YeeType = new Yee3EType();

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
        /// <para>如果是交互状态己方为`Ke`，则说明对方为`BeKe`，则己方受到拉力，对方受到推力，形成效果力为己方追逐对方逃避；</para>
        /// <para>如果是交互状态己方为`BeKe`，则说明对方为`Ke`，则己方受到推力，对方受到拉力，形成效果力为对方追逐己方逃避；</para>
        /// <para>如果是交互状态己方为`Same`，则说明对方也为`Same`，则双方互相无作用力；</para>
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
            if (yeeInterType == Yee3EInterTypeEnum.Ke.ToString())
            {
                this.ThisRuleForce = fieldStrength * ((float) +direction * direction_from_a1_to_a2) /
                                     math.pow(distance_from_a1_to_a2, expCoefficient);
                ThatRuleForce = fieldStrength * ((float) -direction * direction_from_a1_to_a2) /
                                math.pow(distance_from_a1_to_a2, expCoefficient);
            }
            else if (yeeInterType == Yee3EInterTypeEnum.BeKe.ToString())
            {
                this.ThisRuleForce = fieldStrength * ((float) -direction * direction_from_a1_to_a2) /
                                     math.pow(distance_from_a1_to_a2, expCoefficient);
                ThatRuleForce = fieldStrength * ((float) +direction * direction_from_a1_to_a2) /
                                math.pow(distance_from_a1_to_a2, expCoefficient);
            }

            return this.ThisRuleForce;
        }
    }
}