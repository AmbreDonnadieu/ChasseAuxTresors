using ChasseAuxTresorsApi.Classes;
using ChasseAuxTresorsApi.Services;
using System.ComponentModel;

namespace ChasseAuxTresorsApi.tests.Services
{
    public class MapServiceTests
    {
        private readonly MapService _mapService;

        public MapServiceTests()
        {
            _mapService = new MapService();
        }

        [Fact]
        public void BuildEmptyMap_Ok()
        {
            var lines = new List<string>()
            {
                "C - 3 - 4",
                "M - 1 - 1",
                "M - 2 - 2",
                "T - 0 - 3 - 2",
                "T - 1 - 3 - 1"
            };

            var map = _mapService.BuildEmptyMap(lines);
            Assert.NotNull(map);

            for (int i = 0; i < map.Width; i++)
            {
                for (int j = 0; j < map.Height; j++)
                {
                    Assert.Equal((BoxType)default, map.Boxes[i,j].Type);
                    Assert.Null(map.Boxes[i,j].Adventurer);
                    Assert.Equal(0, map.Boxes[i,j].TreasureCount);
                }
            }
        }

        [Fact]
        public void BuildEmptyMap_ThrowException_NoLines()
        {
            var lines = new List<string>();
            Assert.Throws<Exception>(() => _mapService.BuildEmptyMap(lines));
        }

        [Fact]
        public void BuildEmptyMap_ThrowException_TooManyMapLines()
        {
            var lines = new List<string>()
            {
                "C - 3 - 4",
                "C - 4 - 10",
            };
            Assert.Throws<Exception>(() => _mapService.BuildEmptyMap(lines));
        }

        [Fact]
        public void BuildEmptyMap_ThrowException_MapLineWithWrongFormat()
        {
            var lines = new List<string>()
            {
                "C - 3 - 4 - 10",
            };
            Assert.Throws<Exception>(() => _mapService.BuildEmptyMap(lines));
        }

        [Fact]
        public void PopulateMountain_ThrowException_MapLineWithWrongFormat()
        {
            var map = new Map(10, 15);
            var line = "M - 2 - 2 - 6";
            Assert.Throws<Exception>(() => _mapService.PopulateMountain(map,line));
            line = "M - 2";
            Assert.Throws<Exception>(() => _mapService.PopulateMountain(map, line));
        }

        [Fact]
        public void PopulateMountain_ThrowException_NumbersNotInt()
        {
            var map = new Map(10, 15);
            var line = "M - kphegf - 3";
            Assert.Throws<WarningException>(() => _mapService.PopulateMountain(map, line));

            line = "M - 1 - jzdfhl";
            Assert.Throws<WarningException>(() => _mapService.PopulateMountain(map, line));
        }

        [Fact]
        public void PopulateMountain_Ok()
        {
            var map = new Map(10, 15);
            var line = "M - 2 - 2";
            _mapService.PopulateMountain(map, line);

            Assert.Equal(BoxType.Moutain, map.Boxes[2, 2].Type);
        }

        [Fact]
        public void PopulateTreasure_ThrowException_MapLineWithWrongFormat()
        {
            var map = new Map(10, 15);
            var line = "T - 1 - 3 - 1 - 1";
            Assert.Throws<Exception>(() => _mapService.PopulateTreasure(map, line));

            line = "T - 1 - 3";
            Assert.Throws<Exception>(() => _mapService.PopulateTreasure(map, line));
        }

        [Fact]
        public void PopulateTreasure_ThrowException_NumbersNotInt()
        {
            var map = new Map(10, 15);
            var line = "T - kphegf - 3 - 1";
            Assert.Throws<WarningException>(() => _mapService.PopulateTreasure(map, line));

            line = "T - 1 - jzdfhl - 1";
            Assert.Throws<WarningException>(() => _mapService.PopulateTreasure(map, line));

            line = "T - 1 - 3 - zkjhifm";
            Assert.Throws<WarningException>(() => _mapService.PopulateTreasure(map, line));
        }

        [Fact]
        public void PopulateTreasure_Ok()
        {
            var map = new Map(10, 15);
            var line = "T - 1 - 3 - 1";
            _mapService.PopulateTreasure(map, line);

            Assert.Equal(1, map.Boxes[1, 3].TreasureCount);
            Assert.Equal(BoxType.Treasure, map.Boxes[1, 3].Type);
        }

        [Fact]
        public void CreateMapFromListOfLines_Ok()
        {
            var lines = new List<string>()
            {
                "C - 3 - 4",
                "M - 1 - 1",
                "M - 2 - 2",
                "T - 0 - 3 - 2",
                "T - 1 - 3 - 1"
            };

            var map = _mapService.CreateMapFromListOfLines(lines);

            Assert.Equal(3, map.Width);
            Assert.Equal(4, map.Height);
            Assert.Equal(BoxType.Moutain, map.Boxes[1, 1].Type);
            Assert.Equal(BoxType.Moutain, map.Boxes[2, 2].Type);
            Assert.Equal(BoxType.Treasure, map.Boxes[0, 3].Type);
            Assert.Equal(2, map.Boxes[0, 3].TreasureCount);
            Assert.Equal(1, map.Boxes[1, 3].TreasureCount);
        }


    }
}