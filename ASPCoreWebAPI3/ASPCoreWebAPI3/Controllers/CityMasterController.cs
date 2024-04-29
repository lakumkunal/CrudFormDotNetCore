using ASPCoreWebAPI3.Data;
using ASPCoreWebAPI3.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlTypes;
using System;
using Microsoft.AspNetCore.Authorization;


namespace ASPCoreWebAPI3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CityMasterController : ControllerBase
    {
        private readonly CityMasterData cd;

        public CityMasterController(CityMasterData cd)
        {
            this.cd = cd;
        }
        [HttpGet("city")]
        [Authorize]
        public IActionResult Get()
        {
            try
            {
                var city = cd.GetCityMasterData();

                return Ok(city);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// add new city
        /// </summary>
        /// <param name="city"></param>
        /// <returns>adding cities</returns>
        [HttpPost("city")]
        [Authorize]
        public IActionResult Postcity([FromBody] CityMasterModel city)
        {
            try
            {
                
                // Call the method to insert the city
                cd.InsCity(city);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occured : {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, [FromBody] CityMasterModel city)
        {
            try
            {
                if (id != city.intCityID)
                {
                    return BadRequest("City ID mismatch");
                }

                var result = cd.updCity(city);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpDelete("{id}")]
        //public IActionResult delete(int id, [FromBody] CityMasterModel city)
        //{
        //    try
        //    {
        //        if (id != city.intCityID)
        //        {
        //            return BadRequest("id not present");
        //        }

        //        var result = cd.delCity(city);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, $"an error occured : {ex.Message}");
        //    }
        //}

        //[HttpPost]
        //public IActionResult insertyfromprocedure([FromBody] CityMasterModel model)
        //{
        //    try
        //    {
        //        cd.insertyfromprocedure(model.StrCityName, model.intStateID, model.intCountryID);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult deletefromprocedure(int id)
        {
            try
            {
                cd.deletefromprocedure(id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"an error occured : {e.Message}");
            }
        }

        //[HttpPut("{id}")]
        //public IActionResult updatefromprocedure(int id, [FromBody] CityMasterModel model)
        //{
        //    try
        //    {
        //        // Call the method to update the city using the stored procedure
        //        cd.updatefromprocedure(id, model.StrCityName, model.intStateID, model.intCountryID);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
        //    }
        //}

        //[HttpGet]
        //public IActionResult GetCitiesInIndia()
        //{
        //    try
        //    {
        //        var citiesInIndia = cd.GetCitiesInIndia();
        //        var citiesinindresponse = new
        //        {
        //            statuscode = StatusCodes.Status200OK,
        //            CityData = citiesInIndia
        //        };
        //        return Ok(citiesinindresponse);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
        //    }
        //}

        //[HttpGet]
        //public IActionResult countrynameind()
        //{
        //    try
        //    {
        //        var ctrind = cd.countrynameind();

        //        // If ctrind is not null, create a wrapper object containing the status code and data
        //        var response = new APIResponse<object>
        //        {
        //            Status = "success",
        //            Data = ctrind,
        //            Message = "success"
        //        };

        //        // Return the wrapper object with an Ok result
        //        return Ok(response);
        //    }
        //    catch (Exception e)
        //    {
        //        var errorResponse = new APIResponse<string>
        //        {
        //            Status = "error",
        //            Data = null,
        //            Message = $"Internal server error: {e.Message}"
        //        };
        //        return StatusCode(500, errorResponse);
        //    }
        //}

        [HttpGet("user")]
        [Authorize]
        public IActionResult GetUserData()
        {
            try
            {
                var userdata=cd.GetUserData();
                return Ok( userdata );
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("user")]
        public IActionResult PostUser(usersModel userobj)
        {
            try
            {
                //var token = cd.GenerateJwtToken(userobj); // Generate JWT token
                //return Ok(new { Token = token });
                cd.PostUser(userobj);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        //[HttpPost("user")]
        //public IActionResult PostUser(usersModel userobj)
        //{
        //    try
        //    {
        //        // Hash the password before storing it
        //        string result = cd.PostUser(userobj).Result;
        //        if (result == "SUCCESS")
        //        {
        //            return Ok();
        //        }
        //        else
        //        {
        //            return BadRequest(result);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}

        [HttpPost("userLogin")]
        public async Task<IActionResult> PostLoginUser(loginUser loginuserobj)
        {
            try
            {
                // Check if the user already exists
                bool userExists = await cd.CheckUserExists(loginuserobj.email, loginuserobj.password);

                if (userExists)
                {
                    var token = cd.GenerateJwtToken(loginuserobj);
                    return Ok(new { Token = token });
                }
                else
                {
                    return BadRequest("User not registered.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        //[HttpPost("userLogin")]

        //public IActionResult Login(loginUser user)
        //{
        //    try
        //    {
        //        //tempModel tm = cd.CheckUserExists(user.email, user.password);
        //        cd.CheckUserExists(user.email, user.password);
        //        if (tm != null)
        //        {
        //           // var tokenString = cd.GenerateTokens(user.email, tm);
        //            var tokenString = cd.GenerateTokens(user.email, tm);

        //            return Ok( tokenString );
        //        }
        //        return BadRequest();

        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}


        [HttpPost("refresh-token")]
        public IActionResult RefreshToken(string refreshToken)
        {
            try
            {
                // Validate the refresh token
                // For simplicity, assume it's validated

                // Generate new JWT token
                var newJwtToken = cd.RefreshAccessToken(refreshToken);

                return Ok(new { newToken = newJwtToken });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }





        [HttpGet("getcountrybystate{id}")]

        public IActionResult getcountry(int id)
        {
            try
            {
                var cities = cd.getcountrybystateid(id);
                if (cities == null)
                {
                    return NotFound();
                }
                return Ok(cities);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occured : {ex.Message}");
            }
        }

        [HttpGet("state")]
        public IActionResult getstate()
        {
            try
            {
                var state = cd.getstate();
                return Ok(state);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occured : {ex.Message}");
            }
        }

    }
}
