using System.Collections;
using QFramework;
using UnityEngine;

namespace Survivor
{
    public abstract class PickUpObject: GamePlayObject
    {
        protected int retreatFrameCount = 20;
        protected float retreatSpeed = 4f;
        protected float flySpeed = 7.5f;
        
        protected abstract void Excute();

        protected void StartPickUpAnim()
        {
            StartCoroutine(PickUpAnim());
        }
        
        IEnumerator PickUpAnim()
        {
            if (Player.Default)
            {
                var retreatFrame = 0;
                GetComponent<SpriteRenderer>().sortingOrder = 5;
				
                while (Player.Default && retreatFrame < retreatFrameCount)
                {
                    var direction = transform.NormalizedDirection2DFrom(Player.Default);
                    transform.Translate(retreatSpeed * Time.deltaTime * direction);
                    retreatFrame++;
                    yield return null;
                }
				
                while (Player.Default && transform.Distance2D(Player.Default) > 0.1f)
                {
                    var direction = transform.NormalizedDirection2DTo(Player.Default);
                    transform.Translate(flySpeed * Time.deltaTime * direction);
                    yield return null;
                }

                if (Player.Default && transform.Distance2D(Player.Default) <= 0.1f)
                {
                    Excute();
                }
            }
        }
    }
}