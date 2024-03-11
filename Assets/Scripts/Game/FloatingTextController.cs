using UnityEngine;
using QFramework;
using UnityEngine.UI;

namespace Survivor
{
	public partial class FloatingTextController : ViewController
	{
		private static FloatingTextController mDefault;

		private void Awake()
		{
			mDefault = this;
		}

		private void OnDestroy()
		{
			mDefault = null;
		}

		public static void ShowFloatingText(Vector2 position, string message)
		{
			mDefault.TextRoot.InstantiateWithParent(mDefault.transform)
				.Position(position)
				.Self((self) =>
				{
					var initPostionY = position.y;
					var textTrans = self.Find("Text"); // 找到文本所在的物体缓存起来
					var textComp = textTrans.GetComponent<Text>(); // 找到文本组件
					textComp.text = message;

					ActionKit.Sequence()
						.Lerp(0, 0.5f, 0.5f, (t) =>
						{
							self.PositionY(initPostionY + t * 0.3f); // 文本往上移的动画
							// 文本从小变大的动画
							textComp.LocalScaleX(Mathf.Clamp01(t * 4));
							textComp.LocalScaleY(Mathf.Clamp01(t * 4));
						})
						.Delay(0.3f)
						.Lerp(1f, 0f, 0.3f, (t) =>
							{
								textComp.ColorAlpha(t);
							},
							() =>
							{
								textTrans.DestroyGameObjGracefully();
							}).Start(textComp);
				}).Show();
		}
	}
}
