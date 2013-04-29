using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoxicsDataTypes;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

namespace BoxicsGame
{
    static class GameLevelsFactory
    {
        public static GameLevel CreateLevel(World world, int id)
        {
            LevelData data = BoxicsGame.LevelsData[id];
            List<BoxArea> boxAreas = BuildBoxAreas(world, data.BoxAreasData);
            List<Platform> platforms = BuildPlatforms(world, data.PlatformsData);
            List<Instruction> instructions = BuildInstructions(data.InstructionsData);
            List<Speedwalk> speedwalks = BuildSpeedwalks(world, data.SpeedwalksData);
            List<PropulsivePlatform> propulsivePlatforms = BuildPropulsivePlatforms(world, data.PropulsivePlatformsData);
            List<SwingPlatform> swingPlatforms = BuildSwingPlatforms(world, data.SwingPlatformsData);
            List<Fan> fans = BuildFans(world, data.FansData);

            return new GameLevel(id, boxAreas, platforms, instructions, speedwalks, propulsivePlatforms, swingPlatforms, fans, data.CompletionSquareData.Position, data.CompletionSquareData.Size);
        }

        private static List<BoxArea> BuildBoxAreas(World world, List<BoxAreaData> data)
        {
            List<BoxArea> boxAreas = new List<BoxArea>(data.Count);
            foreach (BoxAreaData boxAreaData in data)
            {
                boxAreas.Add(new BoxArea(world, boxAreaData));
            }
            return boxAreas;
        }

        private static List<Platform> BuildPlatforms(World world, List<PlatformData> data)
        {
            List<Platform> platforms = new List<Platform>(data.Count);
            foreach (PlatformData platformData in data)
            {
                platforms.Add(new Platform(world, platformData));
            }
            return platforms;
        }

        private static List<Instruction> BuildInstructions(List<InstructionData> data)
        {
            List<Instruction> instructions = new List<Instruction>(data.Count);
            foreach (InstructionData instructionData in data)
            {
                instructions.Add(new Instruction(instructionData));
            }
            return instructions;
        }

        private static List<Speedwalk> BuildSpeedwalks(World world, List<SpeedwalkData> data)
        {
            List<Speedwalk> speedwalks = new List<Speedwalk>(data.Count);
            foreach (SpeedwalkData speedwalkData in data)
            {
                speedwalks.Add(new Speedwalk(world, speedwalkData));
            }

            return new List<Speedwalk>();
        }

        private static List<SwingPlatform> BuildSwingPlatforms(World world, List<SwingPlatformData> data)
        {
            List<SwingPlatform> swingPlatforms = new List<SwingPlatform>(data.Count);
            foreach (SwingPlatformData swingPlatformData in data)
            {
                swingPlatforms.Add(new SwingPlatform(world, swingPlatformData));
            }
            return swingPlatforms;
        }

        private static List<PropulsivePlatform> BuildPropulsivePlatforms(World world, List<PropulsivePlatformData> data)
        {
            List<PropulsivePlatform> propulsivePlatforms = new List<PropulsivePlatform>(data.Count);
            foreach (PropulsivePlatformData propulsivePlatformData in data)
            {
                propulsivePlatforms.Add(new PropulsivePlatform(world, propulsivePlatformData));
            }
            return propulsivePlatforms;
        }

        private static List<Fan> BuildFans(World world, List<FanData> data)
        {
            List<Fan> fans = new List<Fan>(data.Count);
            foreach (FanData fanData in data)
            {
                fans.Add(new Fan(world, fanData));
            }

            return fans;
        }
    }
}
