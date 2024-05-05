using System.Collections.Generic;
using UnityEngine;
using QFramework;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace Survivor
{
	public partial class Map : ViewController
	{
		enum NineGridType
		{
			LeftTop, // 左上
			LeftCenter, // 正左
			LeftBottom, // 左下
			MiddleTop, // 正上
			MiddleCenter, // 正中
			MiddleBottom, // 正下
			RightTop, // 右上
			RightCenter, // 正右
			RightBottom, // 右下
		}

		enum GridType // 格子地图类型
		{
			Water, // 水流
			Soil, // 土地
			Grass, // 草地
		}

		struct GridIndex
		{
			public int x;
			public int y;

			public GridIndex(int x, int y)
			{
				this.x = x;
				this.y = y;
			}
		}
		
		private Tilemap leftTop;
		private Tilemap leftCenter;
		private Tilemap leftBottom;
		private Tilemap middleTop;
		private Tilemap middleCenter;
		private Tilemap middleBottom;
		private Tilemap rightTop;
		private Tilemap rightCenter;
		private Tilemap rightBottom;
		
		// key: Gird的(x,y) value: Grid的所有Tile的Pos -> PerlinValue
		private Dictionary<GridIndex, Dictionary<Vector3Int, float>> nineGridDic =
			new Dictionary<GridIndex, Dictionary<Vector3Int, float>>();
		
		BindableProperty<GridIndex> gridIndex = new BindableProperty<GridIndex>(new GridIndex(0, 0)); // 当前九宫格格子所在坐标

		private BoundsInt.PositionEnumerator gridInitPositions; // 一个格子的所有位置偏移
		private Vector3Int playerTilePos; // 当前玩家所在的tile位置
		private int girdWidth;
		private int girdHeight;

		[SerializeField] private float _Seed; // 每局的随机种子
		[SerializeField] private float _Lacunarity; // 间隙
		[SerializeField] private float _WaterThred; // 水流的阈值
		[SerializeField] private float _SoilThred; // 土地的阈值
		Tilemap nowGridMap = null; // 当前更新的格子地图

		private ResLoader mLoader = ResLoader.Allocate();
		[Space] [SerializeField] private int _WaterBaseCount;
		[SerializeField] private int _SoilBaseCount;
		[SerializeField] private int _GrassBaseCount;

		// Test:
		public BindableProperty<float> Lacunarity = new BindableProperty<float>();
		public BindableProperty<float> WaterThred = new BindableProperty<float>();
		public BindableProperty<float> SoilThred = new BindableProperty<float>();

		void Start()
		{
			Tilemap.GetComponent<Tilemap>().CompressBounds();

			_Seed = Random.Range(100, 1001);
			Lacunarity.Value = _Lacunarity;
			WaterThred.Value = _WaterThred;
			SoilThred.Value = _SoilThred;

			girdWidth = Tilemap.size.x;
			girdHeight = Tilemap.size.y;
			gridInitPositions = Tilemap.cellBounds.allPositionsWithin;
			Tilemap.ClearAllTiles(); // 清空Tile
			CreateNineGridMaps();

			gridIndex.Register(_ => UpdateMapPosition()) // 玩家所在格子坐标变动时 才更新九宫格
				.UnRegisterWhenGameObjectDestroyed(gameObject);
			
			// Test:
			Lacunarity.Register(_ => UpdateNineGridMaps())
				.UnRegisterWhenGameObjectDestroyed(gameObject);
			WaterThred.Register(_ => UpdateNineGridMaps())
				.UnRegisterWhenGameObjectDestroyed(gameObject);
			SoilThred.Register(_ => UpdateNineGridMaps())
				.UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void Update()
		{
			if (Player.Default && Time.frameCount % 60 == 0)
			{
				playerTilePos = Tilemap.layoutGrid.WorldToCell(Player.Default.Position());
				gridIndex.Value = new GridIndex(playerTilePos.x / girdWidth, playerTilePos.y / girdHeight);

				Lacunarity.Value = _Lacunarity;
				WaterThred.Value = _WaterThred;
				SoilThred.Value = _SoilThred;
			}
		}

		void CreateNineGridMaps()
		{
			middleTop = Tilemap.InstantiateWithParent(transform) // 正上
				.Name("MiddleTop");
			middleCenter = Tilemap // 正中
				.Name("MiddleCenter");
			middleBottom = Tilemap.InstantiateWithParent(transform) // 正下
				.Name("MiddleBottom");
			leftTop = Tilemap.InstantiateWithParent(transform) // 左上
				.Name("LeftTop");
			leftCenter = Tilemap.InstantiateWithParent(transform) // 正左
				.Name("LeftCenter");
			leftBottom = Tilemap.InstantiateWithParent(transform) // 左下
				.Name("LeftBottom");
			rightTop = Tilemap.InstantiateWithParent(transform) // 右上
				.Name("RightTop");
			rightCenter = Tilemap.InstantiateWithParent(transform) // 正右
				.Name("RightCenter");
			rightBottom = Tilemap.InstantiateWithParent(transform) // 右下
				.Name("RightBottom");

			UpdateMapPosition(); // 先更新一下初始九宫格位置
		}
		
		void UpdateMapPosition()
		{
			leftTop.Position(new Vector3(((gridIndex.Value.x - 1) * girdWidth), (gridIndex.Value.y + 1) * girdHeight, 0));
			leftCenter.Position(new Vector3(((gridIndex.Value.x - 1) * girdWidth), gridIndex.Value.y * girdHeight, 0));
			leftBottom.Position(new Vector3(((gridIndex.Value.x - 1) * girdWidth), (gridIndex.Value.y - 1) * girdHeight, 0));
			middleTop.Position(new Vector3((gridIndex.Value.x * girdWidth), (gridIndex.Value.y + 1) * girdHeight, 0));
			middleCenter.Position(new Vector3((gridIndex.Value.x * girdWidth), gridIndex.Value.y * girdHeight, 0));
			middleBottom.Position(new Vector3((gridIndex.Value.x * girdWidth), (gridIndex.Value.y - 1) * girdHeight, 0));
			rightTop.Position(new Vector3(((gridIndex.Value.x + 1) * girdWidth), (gridIndex.Value.y + 1) * girdHeight, 0));
			rightCenter.Position(new Vector3(((gridIndex.Value.x + 1) * girdWidth), gridIndex.Value.y * girdHeight, 0));
			rightBottom.Position(new Vector3(((gridIndex.Value.x + 1) * girdWidth), (gridIndex.Value.y - 1) * girdHeight, 0));
			
			UpdateNineGridMaps(); // 更新九宫格地图
		}
		
		void UpdateNineGridMaps()
		{
			UpdateGridMap(NineGridType.MiddleTop);
			UpdateGridMap(NineGridType.MiddleCenter);
			UpdateGridMap(NineGridType.MiddleBottom);
			UpdateGridMap(NineGridType.LeftTop);
			UpdateGridMap(NineGridType.LeftCenter);
			UpdateGridMap(NineGridType.LeftBottom);
			UpdateGridMap(NineGridType.RightTop);
			UpdateGridMap(NineGridType.RightCenter);
			UpdateGridMap(NineGridType.RightBottom);
		}
		
		void UpdateGridMap(NineGridType gridType)
		{
			var tileOffset = Vector3Int.zero;
			switch (gridType)
			{
				case NineGridType.LeftTop:
					nowGridMap = leftTop;
					tileOffset.x = (gridIndex.Value.x - 1) * girdWidth;
					tileOffset.y = (gridIndex.Value.y + 1) * girdHeight;
					break;
				case NineGridType.LeftCenter:
					nowGridMap = leftCenter;
					tileOffset.x = (gridIndex.Value.x - 1) * girdWidth;
					tileOffset.y = gridIndex.Value.y * girdHeight;
					break;
				case NineGridType.LeftBottom:
					nowGridMap = leftBottom;
					tileOffset.x = (gridIndex.Value.x - 1) * girdWidth;
					tileOffset.y = (gridIndex.Value.y - 1) * girdHeight;
					break;
				case NineGridType.MiddleTop:
					nowGridMap = middleTop;
					tileOffset.x = gridIndex.Value.x * girdWidth;
					tileOffset.y = (gridIndex.Value.y + 1) * girdHeight;
					break;
				case NineGridType.MiddleCenter:
					nowGridMap = middleCenter;
					tileOffset.x = gridIndex.Value.x * girdWidth;
					tileOffset.y = gridIndex.Value.y * girdHeight;
					break;
				case NineGridType.MiddleBottom:
					nowGridMap = middleBottom;
					tileOffset.x = gridIndex.Value.x * girdWidth;
					tileOffset.y = (gridIndex.Value.y - 1) * girdHeight;
					break;
				case NineGridType.RightTop:
					nowGridMap = rightTop;
					tileOffset.x = (gridIndex.Value.x + 1) * girdWidth;
					tileOffset.y = (gridIndex.Value.y + 1) * girdHeight;
					break;
				case NineGridType.RightCenter:
					nowGridMap = rightCenter;
					tileOffset.x = (gridIndex.Value.x + 1) * girdWidth;
					tileOffset.y = gridIndex.Value.y * girdHeight;
					break;
				case NineGridType.RightBottom:
					nowGridMap = rightBottom;
					tileOffset.x = (gridIndex.Value.x + 1) * girdWidth;
					tileOffset.y = (gridIndex.Value.y - 1) * girdHeight;
					break;
			}

			var updateGridIndex = new GridIndex(tileOffset.x, tileOffset.y);
			var gridDic = new Dictionary<Vector3Int, float>();
			var perlinValue = 0f;
			var endPos = Vector3Int.zero;
			var nowTilePos = Vector3Int.zero;
			
			if (nineGridDic.ContainsKey(updateGridIndex)) // 已经在九宫格字典则直接取出来用即可
			{
				gridDic = nineGridDic[updateGridIndex];
				foreach (var keyValue in gridDic)
				{
					endPos = keyValue.Key;
					perlinValue = keyValue.Value;
					nowTilePos = endPos - tileOffset;
					GenerateTile(nowTilePos, perlinValue);
				}
			}
			else // 否则进行计算 并加入九宫格字典
			{
				foreach (var gridTilePos in gridInitPositions)
				{
					endPos = gridTilePos + tileOffset;
					perlinValue = Mathf.PerlinNoise(endPos.x * _Lacunarity + _Seed, endPos.y * _Lacunarity + _Seed);
					GenerateTile(gridTilePos, perlinValue);
					gridDic.Add(endPos, perlinValue);
				}
				nineGridDic.Add(updateGridIndex, gridDic);
			}
		}

		void GenerateTile(Vector3Int tilePos, float perlinValue)
		{
			if (perlinValue < _WaterThred)
			{
				nowGridMap.SetTile(tilePos,mLoader.LoadSync<Tile>($"{GridType.Water.ToString()}_Base_{Random.Range(0, _WaterBaseCount)}"));
			}
			else if (perlinValue < _SoilThred)
			{
				nowGridMap.SetTile(tilePos,mLoader.LoadSync<Tile>($"{GridType.Soil.ToString()}_Base_{Random.Range(0, _SoilBaseCount)}"));
			}
			else
			{
				nowGridMap.SetTile(tilePos, mLoader.LoadSync<Tile>($"{GridType.Grass.ToString()}_Base_{Random.Range(0, _GrassBaseCount)}"));
			}
		}
	}
}
