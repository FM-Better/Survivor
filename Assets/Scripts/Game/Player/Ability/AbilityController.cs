using System.Linq;
using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class AbilityController : ViewController, IController
	{
		void Start()
		{
			Global.SimpleSwordUnlocked.Register(unlocked =>
			{
				if (unlocked)
				{
					SimpleSword.Show();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			Global.SimpleKnifeUnlocked.Register(unlocked =>
			{
				if (unlocked)
				{
					SimpleKnife.Show();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			Global.RotateSwordUnlocked.Register(unlocked =>
			{
				if (unlocked)
				{
					RotateSword.Show();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			Global.BasketBallUnlocked.Register(unlocked =>
			{
				if (unlocked)
				{
					Basketball.Show();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			// 游戏开始随机解锁一个武器能力
			// this.GetSystem<ExpUpgradeSystem>().Items.Where(item => item.IsWeapon).ToList().GetRandomItem().Upgrade();
			this.GetSystem<ExpUpgradeSystem>().Items.Where(item => item.Key == "RotateSword").First().Upgrade();
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
