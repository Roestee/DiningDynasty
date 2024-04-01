using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Fields.TomatoField
{
    public class Tomato : MonoBehaviour, IGrowable
    {
        [Header("Grow")]
        [SerializeField] private float growScale = 1;
        [Space]
        [SerializeField] private float minGrowTime = 2f;
        [SerializeField] private float maxGrowTime = 4f;
        [Space]
        [SerializeField] private float minGrowStartDelay = 0.6f;
        [SerializeField] private float maxGrowStartDelay = 1.2f;
        [Space] 
        [SerializeField] private Color growStartColor = Color.green;
        [SerializeField] private Color growEndColor = Color.red;

        private Transform _myTransform;
        private Renderer _renderer;

        public float MinGrowTime => minGrowTime;
        public float MaxGrowTime => maxGrowTime;
        public bool IsGrown { get; private set; }

        public void Init()
        {
            _renderer = GetComponent<Renderer>();
            
            _myTransform = transform;
            _myTransform.localScale = Vector3.zero;
            _myTransform.localRotation = Quaternion.Euler(0f, Random.Range(0f, 359f), 0f);
        }

        public IEnumerator Grow()
        {
            _myTransform.localScale = Vector3.zero;
            _renderer.material.color = growStartColor;
            IsGrown = false;
            yield return Random.Range(minGrowStartDelay, maxGrowStartDelay);

            var randomGrowTime = Random.Range(MinGrowTime, MaxGrowTime);
            var currentTime = 0f;
            var mat = _renderer.material;
            while (currentTime < randomGrowTime)
            {
                currentTime += Time.deltaTime;
                var per = currentTime / randomGrowTime;
                _myTransform.localScale = Mathf.Lerp(0, growScale, per) * Vector3.one;
                mat.color = Color.Lerp(growStartColor, growEndColor, per);
                yield return null;
            }

            IsGrown = true;
        }
    }
}