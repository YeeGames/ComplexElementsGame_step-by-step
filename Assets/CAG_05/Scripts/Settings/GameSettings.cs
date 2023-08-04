using UnityEngine;

namespace CEG_05
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
        [Range(1, 200)] public int numAgent = 10;

        /// <summary>
        /// 启动暂停时间
        /// </summary>
        public int pauseTime = 0;

        /// <summary>
        /// 舞台半径
        /// </summary>
        [Range(50, 200)] public float stageRadius = 100;

        /// <summary>
        /// 围墙之摩擦性
        /// </summary>
        public float physicsMaterialsFriction = 0.0f;

        /// <summary>
        /// 围墙之弹性
        /// </summary>
        public float physicsMaterialsBounciness = 1.0f;
    }
}