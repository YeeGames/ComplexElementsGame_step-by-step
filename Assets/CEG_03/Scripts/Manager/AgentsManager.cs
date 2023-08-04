using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CEG_03
{
    public class AgentsManager : MonoBehaviour
    {
        /// <summary>
        /// game settings
        /// </summary>
        public GameSettings gset;

        /// <summary>
        /// Yee个体
        /// </summary>
        public YeeAgent yeeAgent;

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

        // /// <summary>
        // /// Yee族枚举
        // /// </summary>
        // private Yee2EType yee2EType = new Yee2EType();

        // /// <summary>
        // /// Yee类型
        // /// </summary>
        // private YeeType yeeType;

        private void Awake()
        {
            /// 载入资源
            var YeeAgent_prefab = Resources.Load("CEG_03/Prefabs/Agent");
            YeeAgent_prefab.AddComponent<YeeAgent>();
            gset = Resources.Load<GameSettings>("CEG_03/Settings/Game Settings");
            yeeAgent = YeeAgent_prefab.GetComponent<YeeAgent>();

            /// 生成个体众
            for (var i = 0; i < gset.numAgent; i++) // 遍历以生成个体
            {
                var a = Instantiate(yeeAgent);

                /// 随机生成在一个圆圈范围内
                Vector2 pos = (Vector2) (this.transform.position) + Random.insideUnitCircle * radiusSize;

                a.aset.position = pos;
                a.aset.velocity = Random.insideUnitCircle;
                a.aset.id = i.ToString();
                // a.aset.YeeType = yee2EType.YeeTypes[t];
                // a.aset.color = yee2EType.Colors[t];

                /// 根据Agent Settings和Rule Settings初始化个体。
                a.Initialize(a.aset);
            }

            // /// 计算个体总数
            // _numAgentInWorld = gset.numAgent * yee2EType.NumElement;

            /// 计算维度总数
            _numDimentsionInWorld = gset.dimension;
        }
    }
}