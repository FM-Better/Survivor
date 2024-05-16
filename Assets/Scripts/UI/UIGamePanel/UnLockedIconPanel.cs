/****************************************************************************
 * 2024.3 LAPTOP-FG35BCEI
 ****************************************************************************/

using System.Collections.Generic;
using QAssetBundle;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.U2D;

namespace Survivor
{
	public partial class UnLockedIconPanel : UIElement, IController
	{
		private Dictionary<string, System.Tuple<ExpUpgradeItem, Image>> mUnLockedKeys =
			new Dictionary<string, System.Tuple<ExpUpgradeItem, Image>>();

		private ResLoader mLoader = ResLoader.Allocate();
		private SpriteAtlas gameAtlas = null;
		
		private void Awake()
		{
			var expUpgradeItems = this.GetSystem<ExpUpgradeSystem>().Items;
			gameAtlas = mLoader.LoadSync<SpriteAtlas>("Game");
			
			foreach (var item in expUpgradeItems)
			{
				var itemCache = item;
				item.CurrentLevel.RegisterWithInitValue(level =>
				{
					if (level > 0)
					{
						if (!mUnLockedKeys.ContainsKey(itemCache.Key))
						{
							ImgIconTemplate.InstantiateWithParent(IconRoot)
								.Self(self =>
								{
									self.sprite = gameAtlas.GetSprite(itemCache.IconName);
									mUnLockedKeys.Add(itemCache.Key,
										new System.Tuple<ExpUpgradeItem, Image>(itemCache, self));
								})
								.Show();
						}
					}
				}).UnRegisterWhenGameObjectDestroyed(gameObject);
			}

			Global.SuperBomb.Register(unlocked =>
			{
				if (unlocked)
				{
					if (mUnLockedKeys.ContainsKey(AbilityConfig.BombKey))
					{
						var item = mUnLockedKeys[AbilityConfig.BombKey].Item1;
						mUnLockedKeys[AbilityConfig.BombKey].Item2.sprite =
							gameAtlas.GetSprite(Icon.PAIRED_BOMB_ICON);
					}
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			Global.SuperKnife.Register(unlocked =>
			{
				if (unlocked)
				{
					if (mUnLockedKeys.ContainsKey(AbilityConfig.SimpleKnifeKey))
					{
						var item = mUnLockedKeys[AbilityConfig.SimpleKnifeKey].Item1;
						mUnLockedKeys[AbilityConfig.SimpleKnifeKey].Item2.sprite =
							gameAtlas.GetSprite(Icon.PAIRED_SIMPLE_KNIFE_ICON);
					}
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			Global.SuperBasketBall.Register(unlocked =>
			{
				if (unlocked)
				{
					if (mUnLockedKeys.ContainsKey(AbilityConfig.BasketballKey))
					{
						var item = mUnLockedKeys[AbilityConfig.BasketballKey].Item1;
						mUnLockedKeys[AbilityConfig.BasketballKey].Item2.sprite =
							gameAtlas.GetSprite(Icon.PAIRED_BALL_ICON);
					}
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			Global.SuperRotateSword.Register(unlocked =>
			{
				if (unlocked)
				{
					if (mUnLockedKeys.ContainsKey(AbilityConfig.RotateSwordKey))
					{
						var item = mUnLockedKeys[AbilityConfig.RotateSwordKey].Item1;
						mUnLockedKeys[AbilityConfig.RotateSwordKey].Item2.sprite =
							gameAtlas.GetSprite(Icon.PAIRED_ROTATE_SWORD_ICON);
					}
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			Global.SuperSimpleSword.Register(unlocked =>
			{
				if (unlocked)
				{
					if (mUnLockedKeys.ContainsKey(AbilityConfig.SimpleSwordKey))
					{
						var item = mUnLockedKeys[AbilityConfig.SimpleSwordKey].Item1;
						mUnLockedKeys[AbilityConfig.SimpleSwordKey].Item2.sprite =
							gameAtlas.GetSprite(Icon.PAIRED_SIMPLE_SWORD_ICON);
					}
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		protected override void OnBeforeDestroy()
		{
			mLoader.Recycle2Cache();
			mLoader = null;
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}