using System;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

namespace CAG2D_05
{
    // public class Yee3ERule : MonoBehaviour,IYeeRule
    public class Yee3ERule : YeeRule
    {
        public Yee3EType YeeType = new Yee3EType();


        // public Yee3ERule()
        // {
        //     // this.ruleCircleCollider2D = this.transform.Find("AgentRuleEffector").GetComponent<CircleCollider2D>();
        //     // this.ruleCircleCollider2D = this.GetComponent<CircleCollider2D>();
        //     // this.Yee3E = this.transform.Find("AgentRuleEffector").GetComponent<Yee3E>();
        //     // Initialize(rset);
        // }

        // protected override void Initialize(RuleSettings ruleSettings)
        // public override void Initialize(RuleSettings ruleSettings)
        // {
        //     // SetRule(rset);
        // }


        /// <summary>
        /// 判断类型交互规则 
        /// </summary>
        /// <param name="thisYeeType">己方YeeType3E类型</param>
        /// <param name="thatYeeType">对方YeeType3E类型</param>
        public override string CalcInterType(string thisYeeType, string thatYeeType)
        {
            return YeeType.YeeRuleAdjecentMatrix[Array.IndexOf(YeeType.FromYeeTypeArray, thisYeeType), Array.IndexOf(YeeType.ToYeeTypeArray, thatYeeType)]; //BUG
        }


        /// <summary>
        /// <para>计算规则力：</para>
        /// <para>如果是交互状态我方为Ke，则说明对方为BeKe，则己方受到拉力，对方受到推力，形成效果力之己方追逐对方逃避；</para>
        /// <para>如果是交互状态我方为BeKe，则说明对方为Ke，则己方受到推力，对方受到拉力，形成效果力之对方追逐我方逃避；</para>
        /// <para>如果是交互状态我方为Same，则说明对方也为Same，则双方互相无作用力；</para>
        /// </summary>
        /// <param name="yeeInterType">YeeTypeInter3E类型</param>
        /// <param name="pos1">我方个体位置</param>
        /// <param name="pos2">对方个体位置</param>
        public override Vector2 CalcRuleForce(string yeeInterType, Vector2 pos1, Vector2 pos2)
        {
            vector_from_a1_to_a2 = (Vector2) (pos2 - pos1);
            direction_from_a1_to_a2 = vector_from_a1_to_a2.normalized;
            distance_from_a1_to_a2 = direction_from_a1_to_a2.magnitude;
            if (yeeInterType == Yee3EInterTypeEnum.Ke.ToString())
            {
                ThisRuleForce = fieldStrength * ((float) direction * direction_from_a1_to_a2) /
                                math.pow(distance_from_a1_to_a2, expCoefficient);
                ThatRuleForce = fieldStrength * ((float) -direction * direction_from_a1_to_a2) /
                                math.pow(distance_from_a1_to_a2, expCoefficient);
            }
            else if (yeeInterType == Yee3EInterTypeEnum.BeKe.ToString())
            {
                ThisRuleForce = fieldStrength * ((float) -direction * direction_from_a1_to_a2) /
                                math.pow(distance_from_a1_to_a2, expCoefficient);
                ThatRuleForce = fieldStrength * ((float) direction * direction_from_a1_to_a2) /
                                math.pow(distance_from_a1_to_a2, expCoefficient);
            }

            return ThisRuleForce;
        }

        // private void OnTriggerEnter2D(Collider2D otherCollider2D)
        // {
        //     /// 添加入Yee规则圆圈碰撞器范围内的邻居YeeAgent
        //     if (otherCollider2D.GetComponentInParent<YeeAgent>()) // 不与墙发生YeeRule交互
        //     {
        //         this.thatYeeAgent = otherCollider2D.GetComponentInParent<YeeAgent>();
        //         this.YeeAgentsInNeighbors.Add(this.thatYeeAgent);
        //     }
        // }
        //
        // private void OnTriggerExit2D(Collider2D otherCollider2D)
        // {
        //     /// 移除出Yee规则圆圈碰撞器范围外的邻居YeeAgent
        //     if (otherCollider2D.GetComponentInParent<YeeAgent>()) // 不与墙发生YeeRule交互
        //     {
        //         this.thatYeeAgent = otherCollider2D.GetComponentInParent<YeeAgent>();
        //         this.YeeAgentsInNeighbors.Remove(this.thatYeeAgent);
        //     }
        // }
        //
        // // public override void OnTriggerStay2D2D(Collider2D2D otherCollider2D)
        // private void OnTriggerStay2D(Collider2D otherCollider2D)
        // {
        //     if (otherCollider2D.GetComponentInParent<YeeAgent>()) // 不与墙发生YeeRule交互
        //     {
        //         this.thisRigidbody2D = this.GetComponentInParent<Rigidbody2D>();
        //         this.thisPosition2D = this.GetComponentInParent<Transform>().position;
        //         this.thisYeeType = this.GetComponentInParent<YeeAgent>().yeeType;
        //         // string thisYeeInterType = this.GetComponentInParent<YeeAgent>().aset.YeeInterTypesInNeighbors;
        //         this.thatRigidbody2D = otherCollider2D.GetComponentInParent<Rigidbody2D>();
        //         this.thatPosition2D = otherCollider2D.GetComponentInParent<Transform>().position;
        //         // Yee3ETypeEnum thatYeeType = otherCollider2D.GetComponentInParent<YeeAgent>().yee3ETypeEnum;
        //         // yeeFamilyEnum thatYeeFamily = otherCollider2D.GetComponent<yee>().yeeFamilyEnum;
        //         this.thatYeeType = otherCollider2D.GetComponentInParent<YeeAgent>().yeeType;
        //         // List<string> thatYeeType = YeeFamily.YeeTypes;
        //         // this._yee3EInterTypeEnum = CalcInterType(thisYeeFamily.YeeTypes[1], thatYeeType);
        //         this.yeeInterType = CalcInterType(this.thisYeeType, this.thatYeeType); //BUG
        //         CalcRuleForce(yeeInterType, thisPosition2D, thatPosition2D);
        //     }
        // }
    }
}