namespace VFXText
{
    public enum Pivot
    {
        BottomLeft = 0,
        Bottom,
        BottomRight,
        Left,
        Center,
        Right,
        TopLeft,
        Top,
        TopRight
    }

    public static class PivotExtensions
    {
        private static readonly float[] HorizontalOffsetMultiplier =
        {
            -0f,
            -0.5f,
            -1f,
            -0f,
            -0.5f,
            -1f,
            -0f,
            -0.5f,
            -1f
        };

        private static readonly float[] VerticalOffsetMultiplier =
        {
            -0f,
            -0f,
            -0f,
            -0.5f,
            -0.5f,
            -0.5f,
            -1.0f,
            -1.0f,
            -1.0f
        };

        public static (float offsetX, float offsetY) GetOffset(this Pivot pivot, float width, float height)
        {
            return (width * HorizontalOffsetMultiplier[(int)pivot], height * VerticalOffsetMultiplier[(int)pivot]);
        }
    }
}