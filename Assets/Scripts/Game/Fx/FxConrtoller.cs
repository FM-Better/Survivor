using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class FxConrtoller : ViewController
	{
		private static FxConrtoller mDefault;

		private void Awake()
		{
			mDefault = this;
		}

		private void OnDestroy()
		{
			mDefault = null;
		}

		public static void Play(SpriteRenderer sprite, Color dissolveColor)
		{
			mDefault.EnemyDieFx.Instantiate()
				.Position(sprite.Position())
				.LocalScale(sprite.Scale())
				.Self(sp =>
				{
					sp.GetComponent<Dissolve>().DissolveColor = dissolveColor;
					sp.sprite = sprite.sprite;
				})
				.Show();
		}
	}
}
