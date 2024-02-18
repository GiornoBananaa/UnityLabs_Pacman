using System.Collections.Generic;
using UnityEngine;

namespace PathSystem
{
    public class PathNode : MonoBehaviour
    {
        [field: SerializeField] public PathNode[] NearNodes { get; private set; }
        public Vector2 Point => transform.position;
        
        
        [ExecuteInEditMode]
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(Point,0.08f);
            for (int i = 0; i < NearNodes.Length; i++)
            {
                Gizmos.DrawLine(transform.position, NearNodes[i].gameObject.transform.position);
            }
        }
    }
}
