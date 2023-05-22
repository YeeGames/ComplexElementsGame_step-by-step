using Unity.Collections;
using UnityEngine;

namespace CAG_06
{
    /// <summary>
    /// Yee 类型 //NOTE 新增
    /// </summary>
    public class YeeType
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
    }
}