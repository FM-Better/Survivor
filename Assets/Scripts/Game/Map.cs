using System;
using UnityEngine;
using QFramework;
using UnityEngine.Tilemaps;

namespace Survivor
{
	public partial class Map : ViewController
	{
		private Tilemap leftTop;
		private Tilemap leftCenter;
		private Tilemap leftBottom;
		private Tilemap middleTop;
		private Tilemap middleCenter;
		private Tilemap middleBottom;
		private Tilemap rightTop;
		private Tilemap rightCenter;
		private Tilemap rightBottom;

		private int areaX = 0;
		private int areaY = 0;
		
		private int cellX;
		private int cellY;
		
		void Start()
		{
			Tilemap.GetComponent<Tilemap>().CompressBounds();
			CreateNineGridMaps();
			cellX = Tilemap.size.x;
			cellY = Tilemap.size.y;
		}

		private void Update()
		{
			if (Player.Default && Time.frameCount % 60 == 0)
			{
				var cellPos = Tilemap.layoutGrid.WorldToCell(Player.Default.Position());
				areaX = cellPos.x / cellX;
				areaY = cellPos.y / cellY;
				UpdateMapPosition();
			}
		}

		void CreateNineGridMaps()
		{
			leftTop = Tilemap.InstantiateWithParent(transform)
				.Name("LeftTop");
			leftCenter = Tilemap.InstantiateWithParent(transform)
				.Name("LeftCenter");
			leftBottom = Tilemap.InstantiateWithParent(transform)
				.Name("LeftBottom");
			middleTop = Tilemap.InstantiateWithParent(transform)
				.Name("MiddleTop");
			middleCenter = Tilemap
				.Name("MiddleCenter");
			middleBottom = Tilemap.InstantiateWithParent(transform)
				.Name("MiddleBottom");
			rightTop = Tilemap.InstantiateWithParent(transform)
				.Name("RightTop");
			rightCenter = Tilemap.InstantiateWithParent(transform)
				.Name("RightCenter");
			rightBottom = Tilemap.InstantiateWithParent(transform)
				.Name("RightBottom");
		}

		void UpdateMapPosition()
		{
			leftTop.Position(new Vector3(((areaX - 1) * cellX), (areaY + 1) * cellY, 0));
			leftCenter.Position(new Vector3(((areaX - 1) * cellX), areaY * cellY, 0));
			leftBottom.Position(new Vector3(((areaX - 1) * cellX), (areaY - 1) * cellY, 0));
			middleTop.Position(new Vector3((areaX* cellX), (areaY + 1) * cellY, 0));
			middleCenter.Position(new Vector3((areaX* cellX), areaY * cellY, 0));
			middleBottom.Position(new Vector3((areaX * cellX), (areaY - 1) * cellY, 0));
			rightTop.Position(new Vector3(((areaX + 1) * cellX), (areaY + 1) * cellY, 0));
			rightCenter.Position(new Vector3(((areaX + 1) * cellX), areaY * cellY, 0));
			rightBottom.Position(new Vector3(((areaX + 1) * cellX), (areaY - 1) * cellY, 0));
		}
	}
}
