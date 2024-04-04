/****************************************************************************
 * 2024.4 LAPTOP-FG35BCEI
 ****************************************************************************/

using System;
using System.Collections.Generic;
using QAssetBundle;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Unity.VisualScripting;

namespace Survivor
{
	public partial class AchievementController : UIElement
	{
		ResLoader mLoader = ResLoader.Allocate();
        
		private void Awake()
		{
			AchievementSystem.OnAchievementUnlocked.Register(item =>
			{
				var originLoaclPosY = AchievementItem.LocalPosition().y;
				
				TxtTitle.text = $"<b>成就 {item.Name} 达成!</b>";
				TxtDescription.text = item.Description;
				ImgIcon.sprite = mLoader.LoadSync<Sprite>(item.IconName);
				AchievementItem.Show();
				AudioKit.PlaySound(Sound.ACHIEVEMENT);
				
				AchievementItem.LocalPositionY(-200);
				
				ActionKit.Sequence()
					.Lerp(-200, originLoaclPosY, 0.3f, (y) => AchievementItem.LocalPositionY(y))
					.Delay(2)
					.Lerp(originLoaclPosY, -200, 0.3f, (y) => AchievementItem.LocalPositionY(y), () =>
					{
						AchievementItem.Hide();
					})
				.Start(this);
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		protected override void OnBeforeDestroy()
		{
			mLoader.Recycle2Cache();
			mLoader = null;
		}
	}
}