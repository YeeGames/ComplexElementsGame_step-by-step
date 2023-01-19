using System.Collections.Generic;
using UnityEngine;

namespace CAG2D_05
{
    
    /// <summary>
    /// Yee交互组件
    /// </summary>
    public class YeeInteraction : MonoBehaviour
    {
        // [SerializeField] internal List<YeeInterAgent> NeighborsOfYeeAgent;
        [SerializeField] private List<YeeInterAgent> YeeInterAgents = new List<YeeInterAgent>();
        [HideInInspector] private YeeInterAgent _yeeInterAgent;

        // [SerializeField] internal YeeAgent thisYeeAgent;
        // [SerializeField] internal YeeAgent thatYeeAgent;
        [SerializeField] public YeeRule yeeRule;


        internal void AddYeeInterAgent(YeeAgent thisYeeAgent, YeeAgent thatYeeAgent)
        {
            this._yeeInterAgent.agentName = thatYeeAgent.name;
            /// 计算Yee交互规则
            this._yeeInterAgent.yeeInterType = this.yeeRule.CalcInterType(thisYeeAgent.yeeType, thatYeeAgent.yeeType);
            this._yeeInterAgent.yeeRuleForce = Vector2.zero;
            this.YeeInterAgents.Add(this._yeeInterAgent);
        }

        internal void RemoveYeeInterAgent(YeeAgent thatYeeAgent)
        {
            var idx_thatYeeAgent = this.YeeInterAgents.FindIndex(v => v.agentName == thatYeeAgent.name);
            if (idx_thatYeeAgent != -1)
            {
                this.YeeInterAgents.RemoveAt(idx_thatYeeAgent);
                // this.YeeInterTypesInNeighbors.RemoveAt(idx_thatYeeAgent);
                // this.YeeRuleForcesInNeighbors.RemoveAt(idx_thatYeeAgent);
            }
            else
            {
                Debug.LogError("没有找到YeeInterAgent！");
            }
        }

        /// <summary>
        /// 更新各YeeInterAgent之情况
        /// </summary>
        /// <param name="pos1"></param>
        /// <param name="pos2"></param>
        internal void UpdateYeeInteractionStatus(Vector2 pos1, Vector2 pos2)
        {
            for (int i = 0; i < this.YeeInterAgents.Count; i++)
            {
                _yeeInterAgent.agentName = this.YeeInterAgents[i].agentName;
                _yeeInterAgent.agentPosition = pos2;
                _yeeInterAgent.yeeInterType = this.YeeInterAgents[i].yeeInterType;

                /// 计算Yee规则圆圈碰撞器范围内的邻居个体之Yee规则力
                _yeeInterAgent.yeeRuleForce = this.yeeRule.CalcRuleForce(_yeeInterAgent.yeeInterType, pos1, pos2);

                YeeInterAgents[i] = _yeeInterAgent;
            }
        }
        // /// HACK 方案二
        // internal void UpdateYeeInteractionStatus()
        // {
        //     foreach (var yeeAgent in this.YeeInterAgents)
        //     {
        //         /// 计算Yee规则力
        //         yeeAgent.netRuleForce = yeeAgent.yeeRule.CalcRuleForce(this., this.Position, this.Interaction.thatYeeAgent.Position);
        //     }
        // }


        /// <summary>
        /// 施加规则力 TODO
        /// </summary>
        /// <param name="ruleNetForce">规则力之合力</param>
        /// <param name="yeeAgentNeighbors">YeeAgent之邻居Agents</param>
        public Vector2 ApplyRuleForce()
        {
            Vector2 ruleNetForce = Vector2.zero;
            /// 计算所有对方规则力之合力
            if (this.YeeInterAgents.Count != 0)
            {
                foreach (var yeeInterAgent in this.YeeInterAgents)
                {
                    ruleNetForce += yeeInterAgent.yeeRuleForce;
                }
            }

            return ruleNetForce;
        }


        // protected string thisYeeAgentName;
        // protected string thisYeeIntertype;
        // protected Vector2 thisYeeRuleForce;
    }
}