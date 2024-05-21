using System;
using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class MarkController : ViewController
	{
		private Camera _main; // 缓存主相机

		private void Start()
		{
			_main = Camera.main;
			
			Mark.OnBecameVisibleEvent(() => // 进入相机范围打开动画
			{
				if (Global.IsMarked.Value)
				{
					Mark.enabled = true;
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			Mark.OnBecameInvisibleEvent(() => // 离开相机范围关闭动画
			{
				if (Global.IsMarked.Value)
				{
					Mark.enabled = false;
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void Update()
		{
			if (Global.CanMark.Value && Input.GetMouseButtonDown(0)) // 鼠标左键点击
			{
				var pos = _main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
				transform.position = pos + Vector3.up; // 移动父物体 否则Mark的坐标会被动画重置回去
				Mark.gameObject.SetActive(true);
				Global.IsMarked.Value = true;
				Global.MarkPos = pos;
			}

			if (Global.IsMarked.Value && Input.GetMouseButtonDown(1)) // 已标记 且 鼠标右键点击
			{
				Mark.gameObject.SetActive(false);
				Global.IsMarked.Value = false;
			}
		}
	}
}
