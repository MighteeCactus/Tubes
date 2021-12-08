using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using HB.InfiniteSequence;
using UnityEditor;
using UnityEngine;

public class NumSeqTest : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private List<Color> _colors = new List<Color>()
    {
        Color.cyan,
        Color.green,
        Color.red,
        Color.yellow
    };

    private IEnumerator Start()
    {
        var i = 0;
        var prevNum = 0;

        var dur = Random.Range(0f, 0.5f) + 0.7f;
        var scale = 3.5f;

        var trail = _target.GetComponent<TrailRenderer>();
        var c = _colors[Random.Range(0, _colors.Count)];
        trail.material.color = c;
        
        var length = 10000;

        var iterH = HemmingInt.GetIterator();
        var size = Vector3.one * 10.1f;
        var iterVec = AAUnitNoBackBoundedVector3.GetIterator(size * 0.5f, size);
        
        while (i < length)
        {
            iterH.MoveNext();
            var val = 1f; //iterH.Current;
            // Debug.Log(val);

            iterVec.MoveNext();
            var dir = iterVec.Current;
            var diff = dir * ((val - prevNum) * scale) + _target.position;
            // Debug.Log(diff);
            _target.DOMove(diff, dur - 0.001f).SetEase(Ease.Linear);

            i++;
            
            yield return new WaitForSeconds(dur);
        }
    }

    [MenuItem("Test/Benchmark")]
    public static void BenchmarkNoBack()
    {
        var limit = (int)10e4;
        var i = 0;
        var iter = AAUnitNoBackBoundedVector3.GetIterator(Vector3.zero, Vector3.one * 5.1f);
        var result = Vector3.zero;
        
        while (i < limit)
        {
            iter.MoveNext();

            result += iter.Current;
            
            i++;
        }
        
        Debug.Log($"<color=green>Res: {result}, iterations: {limit}</color>");
    }
}
