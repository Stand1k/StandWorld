using StandWorld.Definitions;

namespace StandWorld.World
{
    public static class LayerUtils
    {
        public static float Height(Layer layer)
        {
            return (int) layer * -1f;
        }
    }
}