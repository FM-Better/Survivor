using QFramework;
using UnityEngine;

namespace Survivor
{
    public abstract class GamePlayObject : ViewController
    {
        protected abstract new Collider2D  collider { get; }
        
        private void OnBecameVisible()
        {
            collider.enabled = true;
        }
        
        private void OnBecameInvisible()
        {
            collider.enabled = false;
        }
    }
}