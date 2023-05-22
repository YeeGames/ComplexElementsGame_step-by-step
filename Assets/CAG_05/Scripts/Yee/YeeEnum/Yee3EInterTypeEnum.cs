namespace CAG_05
{
    /// <summary>
    /// Yee 3元素类型关系枚举  //NOTE 新增
    /// </summary>
    public enum Yee3EInterTypeEnum
    {
        /// <summary>
        /// 相同。The meaning is similar to this element and another element are the same type.
        /// </summary>
        Same,

        /// <summary>
        /// 克。The meaning is similar to this element suppresses another element.
        /// </summary>
        Ke,

        /// <summary>
        /// 被克。The meaning is similar to this element is suppressed by another element.
        /// </summary>
        BeKe,
    }
}