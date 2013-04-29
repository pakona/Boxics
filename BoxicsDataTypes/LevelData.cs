using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace BoxicsDataTypes
{
    public class LevelData
    {
        public float Precision;
        public CompletionSquareData CompletionSquareData;
        public List<BoxAreaData> BoxAreasData;

        [ContentSerializer(Optional = true)]
        public List<PlatformData> PlatformsData;

        [ContentSerializer(Optional = true)]
        public List<InstructionData> InstructionsData;

        [ContentSerializer(Optional = true)]
        public List<PropulsivePlatformData> PropulsivePlatformsData;

        [ContentSerializer(Optional = true)]
        public List<SpeedwalkData> SpeedwalksData;

        [ContentSerializer(Optional = true)]
        public List<SwingPlatformData> SwingPlatformsData;

        [ContentSerializer(Optional = true)]
        public List<FanData> FansData;

        public LevelData()
        {
            Precision = 0;
            CompletionSquareData = new CompletionSquareData() { Position = Vector2.Zero, Size = 0 };
            BoxAreasData = new List<BoxAreaData>();
            PlatformsData = new List<PlatformData>();
            InstructionsData = new List<InstructionData>();
            PropulsivePlatformsData = new List<PropulsivePlatformData>();
            SpeedwalksData = new List<SpeedwalkData>();
            SwingPlatformsData = new List<SwingPlatformData>();
            FansData = new List<FanData>();
        }
    }
}
