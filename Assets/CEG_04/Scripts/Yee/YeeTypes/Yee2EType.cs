using Unity.Collections;
using UnityEngine;

namespace CEG_04
{
    /// <summary>
    /// Yee 2元素类型  //NOTE 新增 
    /// </summary>
    public class Yee2EType
    {
        /// <summary>
        /// 元素个数
        /// </summary>
        [ReadOnly] [SerializeField] internal int NumElement;

        /// <summary>
        /// 元素颜色
        /// </summary>
        [ReadOnly] [SerializeField] internal Color[] Colors;

        /// <summary>
        /// 元素类型
        /// </summary>
        [ReadOnly] [SerializeField] internal string[] YeeTypes;

        /// <summary>
        /// 元素交互类型
        /// </summary>
        [ReadOnly] [SerializeField] internal string[] YeeInterTypes;

        /// <summary>
        /// 起始Yee类型数组
        /// </summary>
        internal string[] FromYeeTypeArray = new string[]
        {
        };


        /// <summary>
        /// 目标Yee类型数组
        /// </summary>
        internal string[] ToYeeTypeArray = new string[]
        {
        };

        /// <summary>
        /// Yee规则邻接矩阵
        /// </summary>
        internal string[,] YeeRuleAdjecentMatrix = new string[,]
        {
        };

        /// <summary>
        /// Yee 2元素类型枚举
        /// </summary>
        public Yee2ETypeEnum Yee2ETypeEnum;

        public Yee2EType()
        {
            this.NumElement = 2;
            this.Colors = new Color[] {Color.red, Color.blue};
            this.YeeTypes = new string[] {"Yang", "Yin"};
            this.YeeInterTypes = new string[] {"Same", "Opposite"};
            this.FromYeeTypeArray = new string[]
            {
                this.YeeTypes[0], this.YeeTypes[1]
            };
            this.ToYeeTypeArray = new string[]
            {
                this.YeeTypes[0], this.YeeTypes[1]
            };
            this.YeeRuleAdjecentMatrix = new string[,]
            {
                {this.YeeInterTypes[0], this.YeeInterTypes[1]},
                {this.YeeInterTypes[1], this.YeeInterTypes[0]},
            };
        }
    }
}