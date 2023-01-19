// using CAG2D_05;

using System;
using System.Collections.Generic;
using UnityEngine;

namespace CAG2D_05
{
    /// <summary>
    /// Yee族选取机（简单工厂模式）
    /// </summary>
    internal static class YeeFamilyChooser
    {
        /// <summary>
        /// 指定一个YeeFamily，然后添加对应的YeeRule组件到指定对象。
        /// </summary>
        /// <param name="gameObject">游戏对象用于指定添加YeeRule组件</param>
        /// <param name="yeeFamilyEnum">指定一个YeeFamily</param>
        /// <returns>对应的YeeRule组件</returns>
        public static YeeRule ChooseYeeRule(GameObject gameObject, YeeFamilyEnum yeeFamilyEnum)
        {
            YeeRule yeeRule = null;
            switch (yeeFamilyEnum)
            {
                case YeeFamilyEnum.Yee2E:
                    // yeeRule = gameObject.AddComponent<Yee2ERule>(); //FIXME Yee3ERule to Yee2ERule
                    break;
                case YeeFamilyEnum.Yee3E:
                    yeeRule = gameObject.AddComponent<Yee3ERule>();
                    break;
                default:
                    break;
            }

            return yeeRule;
        }


        /// <summary>
        /// 选择指定的YeeType
        /// </summary>
        /// <param name="yeeFamilyEnum">指定YeeFamilyEnum</param>
        /// <returns>对应的YeeType组件</returns>
        public static YeeType ChooseYeeType(YeeFamilyEnum yeeFamilyEnum)
        {
            YeeType yeeType = null;
            switch (yeeFamilyEnum)
            {
                case YeeFamilyEnum.Yee2E:
                    yeeType = new Yee2EType();
                    break;
                case YeeFamilyEnum.Yee3E:
                    yeeType = new Yee3EType();
                    break;
                default:
                    break;
            }

            return yeeType;
        }
    }
}