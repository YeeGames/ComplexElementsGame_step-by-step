using UnityEngine;

namespace CAG2D_05
{
    [CreateAssetMenu]
    public class GameSettings : ScriptableObject
    {
        /// <summary>
        /// 游戏维度
        /// </summary>
        public short dimension = 2;

        /// <summary>
        /// 个体初始数量
        /// </summary>
        public int numAgent = 10;

        /// <summary>
        /// 启动暂停时间
        /// </summary>
        public int pauseTime = 0;

        /// <summary>
        /// 舞台半径
        /// </summary>
        public float stageRadiu = 100;

        /// <summary>
        /// 围墙之摩擦性
        /// </summary>
        public float physicsMaterialsFriction = 0.0f;

        /// <summary>
        /// 围墙之弹性
        /// </summary>
        public float physicsMaterialsBounciness = 1.0f;

        /// <summary>
        /// Yee类型
        /// </summary>
        public YeeFamilyEnum yeeFamilyEnum = YeeFamilyEnum.Yee3E;

        /// <summary>
        /// 是否启用运行时设置模式 //TODO 
        /// </summary>
        public bool isSettingAtPlayMode = false;
    }
}