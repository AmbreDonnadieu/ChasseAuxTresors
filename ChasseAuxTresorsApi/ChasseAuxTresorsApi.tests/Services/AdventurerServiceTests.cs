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
            var map = new Map(10,13);
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
    }
}
