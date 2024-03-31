using System.Collections;

namespace Project.Fields.TomatoField
{
    public interface IGrowable
    {
        public float MinGrowTime { get;}
        public float MaxGrowTime { get;}
        public bool IsGrown { get; }
        IEnumerator Grow();
    }
}