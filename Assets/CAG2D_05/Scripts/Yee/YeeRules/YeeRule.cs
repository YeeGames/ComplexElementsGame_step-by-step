// using CAG2D_05;

using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace CAG2D_05
{
    public class YeeRule : MonoBehaviour
    {
        [HideInInspector] public RuleSettings rset;
        [SerializeField] internal float ruleCircleCollider2DRadius;
        [SerializeField] protected float fieldStrength;
        [SerializeField] protected float expCoefficient;
        [SerializeField] protected int direction;

        [SerializeField] protected Rigidbody2D thisRigidbody2D;
        [HideInInspector] protected Vector2 thisPosition2D;
        // [SerializeField] protected string thisYeeType;
        [SerializeField] protected Rigidbody2D thatRigidbody2D;
        [HideInInspector] protected Vector2 thatPosition2D;
        protected Vector2 ThisRuleForce;
        protected Vector2 ThatRuleForce;


        // [ReadOnly] [SerializeField] protected List<YeeAgent> YeeAgentsInNeighbors;
        // [ReadOnly] [SerializeField] protected List<string> YeeInterTypesInNeighbors;
        // [ReadOnly] [SerializeField] protected List<Vector2> YeeRuleForcesInNeighbors;

        [SerializeField] protected string thatYeeType;
        // private YeeAgent thisYeeAgent;


        protected float distance_from_a1_to_a2 = 0;

        protected Vector2 vector_from_a1_to_a2 = Vector2.zero;

        protected Vector2 direction_from_a1_to_a2 = Vector2.zero;

        // protected YeeAgent thisYeeAgent;

        protected YeeAgent thatYeeAgent;
        // protected string thisYeeAgentName;
        // protected string thisYeeIntertype;
        protected Vector2 thisYeeRuleForce;

        // /// <summary>
        // /// YeeTypeInter之规则之邻接矩阵
        // /// </summary>
        // public string[,] YeeRuleAdjecentMatrix = 
        // {
        //     {Yee3E.YeeInterTypesInNeighbors[0], Yee3E.YeeInterTypesInNeighbors[1], Yee3E.YeeInterTypesInNeighbors[2]},
        //     {Yee3E.YeeInterTypesInNeighbors[2], Yee3E.YeeInterTypesInNeighbors[0], Yee3E.YeeInterTypesInNeighbors[1]},
        //     {Yee3E.YeeInterTypesInNeighbors[1], Yee3E.YeeInterTypesInNeighbors[0], Yee3E.YeeInterTypesInNeighbors[2]},
        // };
        // // private static readonly Yee3EInterTypeEnum[,] YeeRuleAdjecentMatrix = new Yee3EInterTypeEnum[RowSize, ColSize]
        // // {
        // //     {Yee3EInterTypeEnum.Same, Yee3EInterTypeEnum.Ke, Yee3EInterTypeEnum.BeKe},
        // //     {Yee3EInterTypeEnum.BeKe, Yee3EInterTypeEnum.Same, Yee3EInterTypeEnum.Ke},
        // //     {Yee3EInterTypeEnum.Ke, Yee3EInterTypeEnum.BeKe, Yee3EInterTypeEnum.Same}
        // // };


        public virtual void SetRule(RuleSettings ruleSettings)
        {
            // float ruleCircleCollider2DRadius = ruleSettings.forceEffectiveRadius;
            // RuleCircleCollider2DRadius
            this.ruleCircleCollider2DRadius = ruleSettings.forceEffectiveRadius;
            this.fieldStrength = ruleSettings.fieldStrength;
            this.expCoefficient = ruleSettings.expCoefficient;
            this.direction = ruleSettings.direction;
        }

        private void Awake()
        {
            // this.thisYeeAgent = this.GetComponentInParent<YeeAgent>();
            // this.thisYeeAgentName = this.GetComponentInParent<YeeAgent>().name;
            // this.thisYeeType = this.GetComponentInParent<YeeAgent>().yeeType;
        }

        public virtual void Initialize(RuleSettings ruleSettings)
        {
        }

        public virtual void ApplyRuleForce(string yeeInterType)
        {
        }

        // public abstract string CalcInterType(string thisYeeType, string thatYeeType);
        public virtual string CalcInterType(string thisYeeType, string thatYeeType)
        {
            return default;
        }

        // public virtual void CalcRuleForce(string yeeInterType, Vector2 pos1, Vector2 pos2)
        // {
        // }

        public virtual Vector2 CalcRuleForce(string yeeInterType, Vector2 pos1, Vector2 pos2)
        {
            return default;
        }

        // /// <summary>
        // /// 施加规则力
        // /// </summary>
        // /// <param name="netRuleForce">规则力之合力</param>
        // /// <param name="yeeAgentNeighbors">YeeAgent之邻居Agents</param>
        // public void ApplyRuleForce(Vector2 netRuleForce, List<YeeAgent> yeeAgentNeighbors)
        // {
        //     netRuleForce = Vector2.zero;
        //     /// 计算所有对方规则力之合力
        //     foreach (var eachYeeAgent in yeeAgentNeighbors)
        //     {
        //         netRuleForce += eachYeeAgent.yeeRule.thisRuleForce;
        //     }
        //
        //     this.thisYeeAgent.agentRigidbody2D.AddForce(netRuleForce, ForceMode2D.Force);
        // }

        // private void Awake()
        // {
        //     // this.ruleCircleCollider2D = GameObject.Find("AgentRuleEffector").GetComponent<CircleCollider2D>();
        //     // Initialize(rset);
        // }


        // private void OnTriggerEnter2D(Collider2D otherCollider2D)
        // {
        //     /// 添加入Yee规则圆圈碰撞器范围内的邻居个体之YeeAgent、以及计算相关的、YeeInterType、添加Yee规则力。
        //     if (otherCollider2D.GetComponentInParent<YeeAgent>()) // 不与墙发生YeeRule交互
        //     {
        //         this.thatYeeAgent = otherCollider2D.GetComponentInParent<YeeAgent>();
        //         YeeInterAgent neighborYeeAgent;
        //         neighborYeeAgent.agentName = this.thatYeeAgent.name;
        //         neighborYeeAgent.yeeInterType =  this.CalcInterType(this.thisYeeAgent.yeeType, this.thatYeeAgent.yeeType);
        //         neighborYeeAgent.yeeRuleForce = thatYeeAgent.yeeRule.thisRuleForce;
        //         this.NeighborsOfYeeAgent.Add(neighborYeeAgent);
        //
        //         /// 计算Yee交互规则
        //         this.YeeInterTypesInNeighbors.Add(this.CalcInterType(this.thisYeeAgent.yeeType, this.thatYeeAgent.yeeType));
        //
        //         this.YeeRuleForcesInNeighbors.Add(Vector2.zero);
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
        //         var idx_thatYeeAgent = YeeAgentsInNeighbors.FindIndex(v => v.thatYeeAgent);
        //         this.YeeInterTypesInNeighbors.RemoveAt(idx_thatYeeAgent);
        //         this.YeeRuleForcesInNeighbors.RemoveAt(idx_thatYeeAgent);
        //     }
        // }
        //
        // public void OnTriggerStay2D(Collider2D otherCollider2D)
        // {
        //     if (otherCollider2D.GetComponentInParent<YeeAgent>()) // 不与墙发生YeeRule交互
        //     {
        //         // this.YeeAgentsInNeighbors.FindIndex(otherCollider2D.GetComponentInParent<YeeAgent>().name);
        //         // otherCollider2D.GetComponentInParent<YeeAgent>();
        //     }
        // }
    }
}