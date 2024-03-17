using QFramework;
using UnityEngine;

namespace Survivor
{
        public class Dissolve : MonoBehaviour
        {
            public Material Material;
            public Color DissolveColor;
            private static readonly int Color = Shader.PropertyToID("_Color");
            private static readonly int Fade = Shader.PropertyToID("_Fade");

            private void Start()
            {
                var material = Instantiate(Material);
                GetComponent<SpriteRenderer>().material = material;
                
                material.SetColor(Color, DissolveColor);
                ActionKit.Lerp(1f, 0f, 1f, (fade) =>
                    {
                        material.SetFloat(Fade, fade);
                        this.LocalScale(1 + (1 - 0.5f) * fade);
                    })
                    .Start(this, () =>
                    {
                        this.DestroyGameObjGracefully();
                    });
            }
        }
}