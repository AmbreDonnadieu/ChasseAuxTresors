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
            var positionMin = new Position(0, 0);
            var res = _adventurerService.CanMoveFoward(positionMin, map);
            Assert.True(res);

            var positionMax = new Position(10, 13);
            res = _adventurerService.CanMoveFoward(positionMax, map);
            Assert.True(res);
        }


    }
}
