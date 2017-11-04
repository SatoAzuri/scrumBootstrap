using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Models
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore();
        public List<CityDto> Cities { get; set; }

        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id=1,
                    Name="Nastya",
                    Description="Awesome, Hot, Fun, Nice",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id=2,
                            Name = "Developer",
                            Description="Coding, front, back",
                            Password="Mama!999"
                        },
                         new PointOfInterestDto()
                        {
                            Id=3,
                            Name = "Mom",
                            Description="Kids, !Cook, Zumba"
                        }
                    }
                },
                new CityDto()
                {
                    Id=4,
                    Name="Pavan",
                    Description="Kind, Funny, Zen",
                     PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id=5,
                            Name = "Mentor",
                            Description="Help, Coding, Supervise"
                        },
                         new PointOfInterestDto()
                        {
                            Id=6,
                            Name = "Man",
                            Description="Girls, Drinks, Sport"
                        }
                    }
                },
                  new CityDto()
                {
                    Id=7,
                    Name="Gireesh",
                    Description="Smart, Fun, Leader",
                     PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id=8,
                            Name = "Leader",
                            Description="Motivated, Detail orientated, great commuticational skills"
                        }

                    }
                }

            };

        }
    }
}
