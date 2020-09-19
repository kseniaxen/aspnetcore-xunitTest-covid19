using SimpleApp.Models;
using Xunit;
namespace SimpleApp.Tests
{
    public class Covid19Test
    {
        [Fact]
        public void CanChangeCovid19Data()
        {
            // Arrange
            var p = new Covid19 { infected = 20000, dead = 1000, recovered = 10000, deadPt = 2.5, recPt = 10.4 };
            // Act
            p.infected = 10000;
            p.dead = 3004;
            p.recovered = 13333;
            p.deadPt = 23.5;
            p.recPt = 64.6;
            //Assert
            Assert.Equal(10000, p.infected);
            Assert.Equal(3004, p.dead);
            Assert.Equal(13333, p.recovered);
            Assert.Equal(23.5, p.deadPt);
            Assert.Equal(64.6, p.recPt);
        }
    }
}