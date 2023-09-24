using Xunit;
using System;

namespace ChasseAuxTresorsApi.Tests;

public class UnitTest1
{

    [Fact]
    public void IsPrime_InputIs1_ReturnFalse()
    {
        var primeService = new PrimeService();
        bool result = primeService.IsPrime(1);

        Assert.False(result, "1 should not be prime");
    }
}