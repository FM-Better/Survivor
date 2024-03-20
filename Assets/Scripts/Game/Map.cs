using System;
using UnityEngine;
using QFramework;
using UnityEngine.Tilemaps;

namespace Survivor
{
	public partial class Map : ViewController
	{
		private Tilemap leftTop;
		private Tilemap left;
		private Tilemap leftBottom;
		private Tilemap middleTop;
		private Tilemap center;
		private Tilemap middleBottom;
		private Tilemap rightTop;
		private Tilemap right;
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
			leftTop = Tilemap.InstantiateWithParent(transform);
			left = Tilemap.InstantiateWithParent(transform);
			leftBottom = Tilemap.InstantiateWithParent(transform);
			middleTop = Tilemap.InstantiateWithParent(transform);
			center = Tilemap;
			middleBottom = Tilemap.InstantiateWithParent(transform);
			rightTop = Tilemap.InstantiateWithParent(transform);
			right = Tilemap.InstantiateWithParent(transform);
			rightBottom = Tilemap.InstantiateWithParent(transform);
		}

		void UpdateMapPosition()
		{
			leftTop.Position(new Vector3(((areaX - 1) * cellX), (areaY + 1) * cellY, 0));
			left.Position(new Vector3(((areaX - 1) * cellX), areaY * cellY, 0));
			leftBottom.Position(new Vector3(((areaX - 1) * cellX), (areaY - 1) * cellY, 0));
			middleTop.Position(new Vector3((areaX* cellX), (areaY + 1) * cellY, 0));
			center.Position(new Vector3((areaX* cellX), areaY * cellY, 0));
			middleBottom.Position(new Vector3((areaX * cellX), (areaY - 1) * cellY, 0));
			rightTop.Position(new Vector3(((areaX + 1) * cellX), (areaY + 1) * cellY, 0));
			right.Position(new Vector3(((areaX + 1) * cellX), areaY * cellY, 0));
			rightBottom.Position(new Vector3(((areaX + 1) * cellX), (areaY - 1) * cellY, 0));
		}
	}
}
