using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.StageData
{
    public class Stages
    {
        private GameObject StageLoader { get;  }
        private GameObject PlayerPrefab { get; }
        private GameObject TilePrefab { get; }
        private GameObject SpeakerPrefab { get; }
        
        public Stages(StageLoader stageLoader)
        {
            StageLoader = stageLoader.gameObject;
            PlayerPrefab = stageLoader.playerPrefab;
            TilePrefab = stageLoader.tilePrefab;
            SpeakerPrefab = stageLoader.speakerPrefab;
        }

        private StageElementData[] _currentStageData;

        public void InitFirstStage()
        {
            _currentStageData = FirstStageData;
            if (Camera.main != null)
            {
                Camera.main.transform.position = FirstStageCameraLookingPosition + CameraBehaviour.Position;
                Camera.main.transform.LookAt(FirstStageCameraLookingPosition);
            }
            CreateStageElements(FirstStageData, StageLoader);
        }
        
        private void CreateStageElements(StageElementData[] elementsData, GameObject parent)
        {
            foreach (var elementData in elementsData)
            {
                elementData.CreateStageElement(parent);
            }
        }
        
        public void InitZeroStage()
        {
            _currentStageData = ZeroStageData;
            if (Camera.main != null)
            {
                Camera.main.transform.position = ZeroStageCameraLookingPosition + CameraBehaviour.Position;
                Camera.main.transform.LookAt(ZeroStageCameraLookingPosition);
            }
            CreateStageElements(ZeroStageData, StageLoader);
        }

        private StageElementData[] ZeroStageData
        {
            get
            {
            var tileList = new List<StageElementData>();
                tileList.Add(NewData(new(3,-5), Direction.Up, StageElementType.Player));
                for(int i = 3; i <= 5; i++)
                {
                    for(int e = -3; e <= 5; e++)
                    {
                        tileList.Add(NewData(new(i, e), Direction.Up, StageElementType.FixTile));
                    }
                }

                for (int i = 3; i <= 5; i++)
                {
                    tileList.Add(NewData(new(i, -5), Direction.Up, StageElementType.Tile));
                }
            return tileList.ToArray();
            }
        }
        
        private static Vector3 ZeroStageCameraLookingPosition => new (4.5f, 0, 0);

        private StageElementData[] FirstStageData => new StageElementData[]
        {
            NewData(new (0, 0), Direction.Up,  StageElementType.Player),
            NewData(new (0, 0), Direction.Up, StageElementType.Tile),
            NewData(new (1, 0), Direction.Up, StageElementType.Tile),
            NewData(new (2, 0), Direction.Up, StageElementType.Tile),
            NewData(new (3, 0), Direction.Up, StageElementType.Tile),
            NewData(new (4, 0), Direction.Up, StageElementType.Tile),
            NewData(new (5, 0), Direction.Up, StageElementType.Tile),
            NewData(new (6, 0), Direction.Up, StageElementType.Tile),
            NewData(new (7, 0), Direction.Up, StageElementType.Tile),
            NewData(new (8, 0), Direction.Up, StageElementType.Tile),
            NewData(new (9, 0), Direction.Up, StageElementType.Tile),
            NewData(new (4, 1), Direction.Up, StageElementType.Tile),
            NewData(new (4, 2), Direction.Up, StageElementType.Tile),
            NewData(new (4, 3), Direction.Up, StageElementType.Tile),
            NewData(new (4, 4), Direction.Up, StageElementType.Tile),
            NewData(new (4, -1), Direction.Up, StageElementType.Tile),
            NewData(new (4, -2), Direction.Up, StageElementType.Tile),
            NewData(new (4, -3), Direction.Up, StageElementType.Tile),
            NewData(new (4, -4), Direction.Up, StageElementType.Tile),
            NewData(new (5, 1), Direction.Up, StageElementType.Tile),
            NewData(new (5, 2), Direction.Up, StageElementType.Tile),
            NewData(new (5, 3), Direction.Up, StageElementType.Tile),
            NewData(new (5, 4), Direction.Up, StageElementType.Tile),
            NewData(new (5, -1), Direction.Up, StageElementType.Tile),
            NewData(new (5, -2), Direction.Up, StageElementType.Tile),
            NewData(new (5, -3), Direction.Up, StageElementType.Tile),
            NewData(new (5, -4), Direction.Up, StageElementType.Tile),
            NewData(new (4, 4), Direction.Down, StageElementType.Speaker),
            NewData(new (5, -4), Direction.Up, StageElementType.Speaker),
        };
        
        private static Vector3 FirstStageCameraLookingPosition => new (4.5f, 0, 0);

        private StageElementData NewData(Coordinates coordinates, Direction direction, StageElementType type)
        {
            var prefab = type switch
            {
                StageElementType.Tile => TilePrefab,
                StageElementType.FixTile => TilePrefab,
                StageElementType.Player => PlayerPrefab,
                StageElementType.Speaker => SpeakerPrefab,
                _ => null
            };
            
            return new StageElementData(prefab, coordinates, direction, type, _currentStageData);
        }
    }
}