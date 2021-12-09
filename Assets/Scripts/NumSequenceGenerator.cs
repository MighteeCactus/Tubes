using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HB.InfiniteSequence
{
    #region int
    
    public class NoneInt
    {
        public static IEnumerator<int> GetIterator()
        {
            while (true)
            {
                yield return 0;
            }
        }
    }
    
    public class HemmingInt
    {
        public static IEnumerator<int> GetIterator()
        {
            int cur = 0;
            
            while (true)
            {
                var num = cur;

                while (true)
                {
                    num++;
                    if (num % 2 == 0 || num % 3 == 0 || num % 5 == 0) { break; }
                }

                cur = num;
                yield return num;
            }
        }
    }
    
    #endregion int
    
    #region Vector3
    
    public class NoneVector3
    {
        public static IEnumerator<Vector3> GetIterator()
        {
            while (true)
            {
                yield return Vector3.zero;
            }
        }
    }
    
    public static class AAUnitVector3
    {
        private static List<Vector3> _dirs = new List<Vector3>()
        {
            Vector3.forward,
            Vector3.back,
            Vector3.left,
            Vector3.right,
            Vector3.up,
            Vector3.down
        };
        
        public static IEnumerator<Vector3> GetIterator()
        {
            int _curIdx = 0;
            while (true)
            {
                _curIdx = Random.Range(0, _dirs.Count);
                yield return _dirs[_curIdx];
            }
        }
    }
    
    public static class AAUnitUniqueVector3
    {
        private static List<Vector3> _dirs = new List<Vector3>()
        {
            Vector3.forward,
            Vector3.back,
            Vector3.left,
            Vector3.right,
            Vector3.up,
            Vector3.down
        };

        public static IEnumerator<Vector3> GetIterator()
        {
            int _curIdx = 0;
            while (true)
            {
                var idx = _curIdx;
                while (true)
                {
                    idx = Random.Range(0, _dirs.Count);
                    if (idx == _curIdx) { continue; }

                    break;
                }

                _curIdx = idx;
                yield return _dirs[_curIdx];
            }
        }
    }
    
    public static class AAUnitNoBackVector3
    {
        private static List<Vector3> _dirs = new List<Vector3>()
        {
            Vector3.forward,
            Vector3.back,
            Vector3.left,
            Vector3.right,
            Vector3.up,
            Vector3.down
        };
        
        public static IEnumerator<Vector3> GetIterator()
        {
            var cur  = Vector3.zero;
            var prev = Vector3.zero;

            while (true)
            {
                var i = 0;
                while (true)
                {
                    cur = _dirs[Random.Range(0, _dirs.Count)];
                    if ((cur - prev).sqrMagnitude < float.Epsilon || Mathf.Abs((cur - prev).sqrMagnitude - 4f) < float.Epsilon) { continue; }

                    i++;
                    if (i > 20)
                    {
                        throw new Exception("Something is wrong");
                    }
                    
                    break;
                }

                prev = cur;
                yield return cur;
            }
        }
    }
    
    public static class AAUnitNoBackBoundedVector3
    {
        private static List<Vector3> _dirs = new List<Vector3>()
        {
            Vector3.forward,
            Vector3.back,
            Vector3.left,
            Vector3.right,
            Vector3.up,
            Vector3.down
        };
        
        public static IEnumerator<Vector3> GetIterator(Vector3 start, Vector3 size)
        {
            var cur  = Vector3.zero;
            var prev1 = Vector3.zero;
            var prev2 = Vector3.zero;
            var sum  = start;
            var tSum = sum;

            while (true)
            {
                while (true)
                {
                    cur = _dirs[Random.Range(0, _dirs.Count)];
                    // no back movement
                    if ((cur - prev1).sqrMagnitude < float.Epsilon ||
                        (cur - prev2).sqrMagnitude < float.Epsilon ||
                        Mathf.Abs((cur - prev1).sqrMagnitude - 4f) < float.Epsilon) { continue; }
                    tSum = sum + cur;
                    // stay inside
                    if (tSum.x < 0 || tSum.y < 0 || tSum.z < 0 ||
                        tSum.x > size.x || tSum.y > size.y || tSum.z > size.z) { continue; }

                    break;
                }

                prev2 = -prev1; // this minus is the thing
                prev1 = cur;
                sum  = tSum;
                yield return cur;
            }
        }
    }
    
    #endregion Vector3
}