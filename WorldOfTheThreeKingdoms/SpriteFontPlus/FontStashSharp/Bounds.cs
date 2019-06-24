namespace FontStashSharp
{
    /// <summary>
    /// 表示范围的结构体
    /// </summary>
    public struct Bounds
    {
        /// <summary>
        /// 表示范围矩形两角的4个坐标
        /// </summary>
        public float X, Y, X2, Y2;
        /// <summary>
        /// 矩形的宽度
        /// </summary>
        public float Width
        {
            get
            {
                return X2 - X;
            }
        }
        /// <summary>
        /// 矩形的高度
        /// </summary>
        public float Height
        {
            get
            {
                return Y2 - Y;
            }
        }
    }
}
