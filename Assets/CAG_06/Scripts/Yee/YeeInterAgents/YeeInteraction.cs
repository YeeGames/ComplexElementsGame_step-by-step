using System.Collections.Generic;
using UnityEngine;

namespace CAG_06
{
    /// <summary>
    /// Yee交互组件
    /// </summary>
    public class YeeInteraction : MonoBehaviour
    {
        /// <summary>
        /// YeeInterAgent众列表
        /// </summary>
        [SerializeField] private List<YeeInterAgent> YeeInterAgents = new List<YeeInterAgent>();
        private YeeInterAgent _yeeInterAgent;

        [HideInInspector] public YeeRule yeeRule;  //NOTE 变动


        /// <summary>
        /// 添加单个YeeInterAgent
        /// </summary>
        /// <param name="thisYeeAgent">己方YeeAgent</param>
        /// <param name="thatYeeAgent">对方YeeAgent</param>
        internal void AddYeeInterAgent(YeeAgent thisYeeAgent, YeeAgent thatYeeAgent)
        {
            this._yeeInterAgent.agentName = thatYeeAgent.name;
            /// 计算Yee交互规则
            this._yeeInterAgent.yeeInterType = this.yeeRule.CalcInterType(thisYeeAgent.yeeType, thatYeeAgent.yeeType);
            this._yeeInterAgent.yeeRuleForce = Vector2.zero;
            this.YeeInterAgents.Add(this._yeeInterAgent);
        }

        /// <summary>
        /// 移除单个YeeInterAgent
        /// </summary>
        /// <param name="thatYeeAgent">对方YeeAgent</param>
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
        /// <param name="thisPos">己方Agent位置</param>
        /// <param name="thatPos">对方Agent位置</param>
        internal void UpdateYeeInteractionStatus(Vector2 thisPos, Vector2 thatPos)
        {
            for (int i = 0; i < this.YeeInterAgents.Count; i++)
            {
                _yeeInterAgent.agentName = this.YeeInterAgents[i].agentName;
                _yeeInterAgent.agentPosition = thatPos;
                _yeeInterAgent.yeeInterType = this.YeeInterAgents[i].yeeInterType;

                /// 计算Yee规则圆圈碰撞器范围内的邻居个体之Yee规则力
                _yeeInterAgent.yeeRuleForce = this.yeeRule.CalcRuleForce(_yeeInterAgent.yeeInterType, thisPos, thatPos);

                YeeInterAgents[i] = _yeeInterAgent;
            }
        }


        /// <summary>
        /// 施加规则力
        /// </summary>
        /// <returns>规则力净力</returns>
        internal Vector2 ApplyRuleForce()
        {
            Vector2 netRuleForce = Vector2.zero;
            /// 计算所有对方规则力之合力
            if (this.YeeInterAgents.Count != 0)
            {
                foreach (var yeeInterAgent in this.YeeInterAgents)
                {
                    netRuleForce += yeeInterAgent.yeeRuleForce;
                }
            }

            return netRuleForce;
        }
    }
}