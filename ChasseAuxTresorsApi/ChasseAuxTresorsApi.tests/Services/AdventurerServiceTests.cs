using ChasseAuxTresorsApi.Classes;
using ChasseAuxTresorsApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChasseAuxTresorsApi.tests.Services
{
    public class AdventurerServiceTests
    {
        private readonly AdventurerService _adventurerService;
        public AdventurerServiceTests()
        {
            _adventurerService = new AdventurerService();
        }

        [Fact]
        public void CanMoveForward_True()
        {
            var map = new Map(10, 13);
            var newPositionMin = new Position(0, 0);
            var res = _adventurerService.CanMoveFoward(newPositionMin, map);
            Assert.True(res);

            var newPositionMax = new Position(9, 12);
            res = _adventurerService.CanMoveFoward(newPositionMax, map);
            Assert.True(res);
        }

        [Fact]
        public void CanMoveForward_False_OutOfRange()
        {
            var map = new Map(10, 13);
            var newPosition = new Position(10, 13);
            var res = _adventurerService.CanMoveFoward(newPosition, map);
            Assert.False(res);
        }

        [Fact]
        public void CanMoveForward_False_TryingToClimbAMountain()
        {
            var map = new Map(10, 13);
            map.Boxes[5, 6].Type = BoxType.Moutain;

            var newPosition = new Position(5, 6);
            var res = _adventurerService.CanMoveFoward(newPosition, map);
            Assert.False(res);
        }

        [Fact]
        public void TurnRight_Ok()
        {
            var adventurer = new Adventurer("Camélia", new Position(0, 0), Orientation.North, "AGADAGAD");
            _adventurerService.TurnRight(adventurer);
            Assert.Equal(Orientation.East, adventurer.Orientation);

            _adventurerService.TurnRight(adventurer);
            Assert.Equal(Orientation.South, adventurer.Orientation);

            _adventurerService.TurnRight(adventurer);
            Assert.Equal(Orientation.West, adventurer.Orientation);

            _adventurerService.TurnRight(adventurer);
            Assert.Equal(Orientation.North, adventurer.Orientation);
        }

        [Fact]
        public void TurnLeft_Ok()
        {
            var adventurer = new Adventurer("Camélia", new Position(0, 0), Orientation.North, "AGADAGAD");
            _adventurerService.TurnLeft(adventurer);
            Assert.Equal(Orientation.West, adventurer.Orientation);

            _adventurerService.TurnLeft(adventurer);
            Assert.Equal(Orientation.South, adventurer.Orientation);

            _adventurerService.TurnLeft(adventurer);
            Assert.Equal(Orientation.East, adventurer.Orientation);

            _adventurerService.TurnLeft(adventurer);
            Assert.Equal(Orientation.North, adventurer.Orientation);
        }

        [Fact]
        public void PlaceThemOnMap_ok()
        {
            var lines = new List<string>
            {
                "A - Jordan - 1 - 5 - S - AGDA",
                "A - Maria - 3 - 8 - E - DADDAG",
                "A - Michael - 8 - 8 - O - GGADGGA"
            };
            var map = new Map(10, 13);
            var res = _adventurerService.PlaceThemOnMap(lines, map);

            var expected = new List<Adventurer>
            {
                new Adventurer("Jordan", new Position(1,5),Orientation.South, "AGDA"),
                new Adventurer("Maria", new Position(3,8),Orientation.East, "DADDAG"),
                new Adventurer("Michael", new Position(8,8),Orientation.West, "GGADGGA")
            };

            Assert.NotNull(res);
            Assert.Equal(expected.Count, res.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].Name, res[i].Name);
                Assert.Equal(expected[i].Mouvements, res[i].Mouvements);
                Assert.Equal(expected[i].TreasureCount, res[i].TreasureCount);
                Assert.Equal(expected[i].LastMouvementxIndex, res[i].LastMouvementxIndex);
                Assert.Equal(expected[i].Position.x, res[i].Position.x);
                Assert.Equal(expected[i].Position.y, res[i].Position.y);
            }
        }

        [Fact]
        public void PlaceThemOnMap_ThrowWrongParameters()
        {
            var lines = new List<string>
            {
                "A - Jordan - 1 - 5 - AGDA",
            };
            var map = new Map(10, 13);
            Assert.Throws<Exception>(() => _adventurerService.PlaceThemOnMap(lines, map));
        }

        [Fact]
        public void MoveForward_ok()
        {
            var adv = new Adventurer("name1", new Position(4, 6), Orientation.North, "");
            var map = new Map(10, 13);

            _adventurerService.MoveForward(adv, map);
            Assert.Equal(4, adv.Position.x);
            Assert.Equal(5, adv.Position.y);
            Assert.Equal(Orientation.North, adv.Orientation);

            _adventurerService.MoveForward(adv, map);
            Assert.Equal(4, adv.Position.x);
            Assert.Equal(4, adv.Position.y);
            Assert.Equal(Orientation.North, adv.Orientation);
        }

        [Fact]
        public void DoNextAction_ok()
        {
            var adv = new Adventurer("Samia", new Position(4, 7), Orientation.South, "AGGADA");
            var map = new Map(10, 13);
            map.Boxes[4,7].Adventurer = adv;
            adv.LastMouvementxIndex = 4;

            _adventurerService.DoNextAction(adv, map);
            Assert.Equal(Orientation.West, adv.Orientation);
            Assert.Equal(5,adv.LastMouvementxIndex);
            
            _adventurerService.DoNextAction(adv, map);
            Assert.Equal(Orientation.West, adv.Orientation);
            Assert.Equal(6,adv.LastMouvementxIndex);
            Assert.Equal(3,adv.Position.x);
            Assert.Equal(7,adv.Position.y);
            Assert.Null(map.Boxes[4, 7].Adventurer);
            Assert.NotNull(map.Boxes[3, 7].Adventurer);


            _adventurerService.DoNextAction(adv, map);
            Assert.Equal(3,adv.Position.x);
            Assert.Equal(7,adv.Position.y);
            Assert.Equal(Orientation.West, adv.Orientation);

        }
    }
}
