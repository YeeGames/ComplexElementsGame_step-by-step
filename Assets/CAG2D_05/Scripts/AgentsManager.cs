using UnityEngine;
using Random = UnityEngine.Random;

namespace CAG2D_05
{
    public class AgentsManager : MonoBehaviour
    {
        /// <summary>
        /// game settings
        /// </summary>
        [HideInInspector] public GameSettings gset;

        /// <summary>
        /// Yee个体
        /// </summary>
        [HideInInspector] public YeeAgent yeeAgent;

        /// <summary>
        /// 世界维度数
        /// </summary>
        private int _numDimentsionInWorld;

        /// <summary>
        /// 世界个体总数
        /// </summary>
        private int _numAgentInWorld;

        /// <summary>
        /// 随机生成范围之半径 
        /// </summary>
        public float radiusSize = 30f;

        /// <summary>
        /// Yee族枚举
        /// </summary>
        private YeeFamilyEnum yeeFamilyEnum;

        /// <summary>
        /// Yee类型
        /// </summary>
        private YeeType yeeType;

        private void Awake()
        {
            /// 选择YeeType类型
            yeeType = YeeFamilyChooser.ChooseYeeType(gset.yeeFamilyEnum);

            /// 生成个体众
            for (var t = 0; t < yeeType.NumElement; t++) // 遍历每一类YeeType，以生成个体
            {
                for (var i = 0; i < gset.numAgent; i++) // 遍历单类YeeType之所有预定数量，以生成个体
                {
                    YeeAgent a = Instantiate(yeeAgent);

                    /// 随机生成在一个圆圈范围内
                    Vector2 pos = (Vector2) (this.transform.position) + Random.insideUnitCircle * radiusSize;

                    a.aset.position = pos;
                    a.aset.velocity = Random.insideUnitCircle;
                    a.aset.id = i.ToString();
                    a.aset.YeeType = yeeType.YeeTypes[t];
                    a.aset.color = yeeType.Colors[t];

                    /// 根据Agent Settings和Rule Settings初始化个体。
                    a.Initialize(a.aset, a.rset);
                }
            }

            /// 计算个体总数
            _numAgentInWorld = gset.numAgent * yeeType.NumElement;

            /// 计算维度总数
            _numDimentsionInWorld = gset.dimension;
        }


        // Start is called before the first frame update
        void Start()
        {
            // /// TODO 用YeeInteractionMatrix版本时会用到
            // int[] agentsID = new int[_numAgentInWorld];
            //
            // /// 查找所有具有`agent`标记的游戏对象，这里是`AgentRuleEffector`  NOTE 后续可能会改变对象名称
            // yeeAgentObjects = GameObject.FindGameObjectsWithTag(yeeAgent.aset.tag);
            //
            // Debug.Log("yeeAgentObjects.Length = " + yeeAgentObjects.Length);
            //
            // /// 获取各个体之ID值
            // for (var i = 0; i < _numAgentInWorld; i++)
            // {
            //     agentsID[i] = yeeAgentObjects[i].GetInstanceID();
            // }
            //
            // /// 初始化YeeInteraction
            // YeeInteractionMatrix.Initialize(_numAgentInWorld, _numDimentsionInWorld, agentsID);
        }


        // Update is called once per frame
        void FixedUpdate()
        {
            // /// TODO 用YeeInteractionMatrix版本时会用到
            // ///TODO 获取所有的粒子对象相关的属性值，用一个数组向量
            // for (int i = 0; i < _numAgentInWorld; i++)
            // {
            //     YeeInteractionMatrix.AgentsPosition[i] = yeeAgentObjects[i].transform.position;
            //     // agentsPosition[i].X = yeeAgentObjects[i].transform.position.x;
            //     // agentsPosition[i].Y = yeeAgentObjects[i].transform.position.y;
            //     // agentsPosition[i].Z = yeeAgentObjects[i].transform.position.z;
            // }
            //
            // /// TODO 计算交互情况
            // // float[,] DistanceMatrix = YeeInteractionMatrix.CalculateYeeInteraction(agentsPosition);
            // YeeInteractionMatrix.CalculateYeeInteraction(YeeInteractionMatrix.AgentsPosition);
            // // Console.Write(YeeInteractionMatrix.DistanceMatrix);
            // Debug.Log("Distance Matrix: " + YeeInteractionMatrix.DistanceMatrix.ToString());
        }
    }
}