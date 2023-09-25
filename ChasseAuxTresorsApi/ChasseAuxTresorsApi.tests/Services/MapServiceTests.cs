using ChasseAuxTresorsApi.Classes;
using ChasseAuxTresorsApi.Services;

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
        public void PopulateMountains_ThrowException_MapLineWithWrongFormat()
        {
            var map = new Map(10, 15);
            var lines = new List<string>()
            {
                "M - 2 - 2 - 6",
            };
            Assert.Throws<Exception>(() => _mapService.PopulateMountains(map,lines));
        }

        [Fact]
        public void PopulateMountains_ThrowException_BoxAlreadyDefined()
        {
            var map = new Map(10, 15);
            var lines = new List<string>()
            {
                "M - 2 - 2 - 6",
            };
            Assert.Throws<Exception>(() => _mapService.PopulateMountains(map, lines));
        }
    }
}