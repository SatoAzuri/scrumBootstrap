using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class PointsOfInterestsController : Controller
    {
        private ILogger<PointsOfInterestsController> _logger;

        public PointsOfInterestsController(ILogger<PointsOfInterestsController> logger)
        {
            _logger = logger;
        }
        [HttpGet("{cityId}/pointsofinterest")]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            try
            {
                //throw new Exception();
                var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

                if (city == null)
                {
                    _logger.LogInformation($"City with id {cityId} wastn't fount when accesing point of interest.");
                    return NotFound();
                }

                return Ok(city.PointsOfInterest);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting point of interest for city with id {cityId}.", ex);
                return StatusCode(500, "A problem happend while handling your request");
            }
        }
        [HttpGet("{cityId}/pointsofinterest/{id}")]
        public IActionResult GetPointsOfInterest(int cityId, int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterest = city.PointsOfInterest.FirstOrDefault(c => c.Id == id);

            if (pointOfInterest == null)
            {
                return NotFound();
            }

            return Ok(pointOfInterest);
        }


        //createing new point of interest
        /*
         *   {
         *      "name": "Paris",
         *      "description": "Beautiful place"
         *   }
         */
        [HttpPost("{cityId}/pointsofinterest", Name ="GetPointOfInterest")]
        public IActionResult CreatePointsOfInterest(int cityId,  [FromBody] PointOfInterestForCreationDto pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }

            if(pointOfInterest.Description == pointOfInterest.Name)
            {
                ModelState.AddModelError("Discription", "The provided description should be different from the nema.");
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            //demo purpose - to be improved
            var maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(
                c => c.PointsOfInterest).Max(p => p.Id);

            var finalPointOfInterest = new PointOfInterestDto()
            {
                Id = ++maxPointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description,
                Password = pointOfInterest.Password,
                HireDate = pointOfInterest.HireDate

        };
            city.PointsOfInterest.Add(finalPointOfInterest);
            return CreatedAtRoute("GetPointOfInterest", new
            { cityId = cityId, id = finalPointOfInterest.Id }, finalPointOfInterest);
        }

        //update all parameters of point of interest PUT is UPDATE
        /*
         *    {
         *      "name": "updated name",
         *      "description": "updated description"
         *    }              
        */
        [HttpPut("{cityId}/pointsofinterest/{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int id,
            [FromBody] PointOfInterestForUpdateDto pp)
        {
            
            if (pp == null)
            {
                return BadRequest();
            }

            if(pp.Description == pp.Name)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name");
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(c => c.Id == id);

            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            pointOfInterestFromStore.Name = pp.Name;
            pointOfInterestFromStore.Description = pp.Description;
            pointOfInterestFromStore.Password = pp.Password;
            pointOfInterestFromStore.HireDate = pp.HireDate;

            return NoContent();
        }

        //updating ona parametr of point of interest
        /*
         * [
         *      {
         *          "op": "replace:,
         *          "path": "/name",
         *          "value": "new_name"
         *       }
         * ]
         * */
        [HttpPatch("{cityId}/pointsofinterest/{id}")]
        public IActionResult PartiallyUpdatePointOfInterest(int cityId, int id,
            [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchDoc)
        {
            // if sending empty request
            if(patchDoc == null)
            {
                return BadRequest();
            }

            //checking if the city exist
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if(city == null)
            {
                return NotFound();
            }

            //checking if the pointOfInterest exist
            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(c => c.Id == id);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            //creating a variable to hold current values of the requested pointOfInterest 
            var pointOfInterestToPatch =
                new PointOfInterestForUpdateDto()
                {
                    Name = pointOfInterestFromStore.Name,
                    Description = pointOfInterestFromStore.Description,
                    Password = pointOfInterestFromStore.Password,
                    HireDate = pointOfInterestFromStore.HireDate
        };
            patchDoc.ApplyTo(pointOfInterestToPatch, ModelState);//patching requested fields and checking errors

            //error catching (if user trying to patch field that doesn't exist...) 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (pointOfInterestToPatch.Description == pointOfInterestToPatch.Name)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name");
            }

            TryValidateModel(pointOfInterestToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //updating the pointofInterest which was patched
            pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;
            pointOfInterestFromStore.Password = pointOfInterestToPatch.Password;
            pointOfInterestFromStore.HireDate = pointOfInterestToPatch.HireDate;
             
            return NoContent();
        }

        //deleting point of interest
        /*
         * 
         * 
         * 
         * */
        [HttpDelete("{cityId}/pointsofinterest/{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int id)
        {
            //checking if the city exist
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            //checking if the pointOfInterest exist
            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(c => c.Id == id);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            city.PointsOfInterest.Remove(pointOfInterestFromStore);

            return NoContent();
        }
    }

}
