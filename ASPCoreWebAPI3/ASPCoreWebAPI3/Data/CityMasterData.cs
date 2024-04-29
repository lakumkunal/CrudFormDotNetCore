using ASPCoreWebAPI3.Models;
using Npgsql;
using System.Data;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using System.Data.Common;


namespace ASPCoreWebAPI3.Data
{
    public class CityMasterData
    {
        private readonly string constr;
        
        public CityMasterData(IConfiguration configuration)
        {
            constr = configuration.GetConnectionString("dbcs");
           
        }
        public IEnumerable<CityMasterModel> GetCityMasterData()
        {
            var cities = new List<CityMasterModel>();
            using (var con = new NpgsqlConnection(constr))
            {
                con.Open();

                using (var command = new NpgsqlCommand("SELECT * FROM \"tblCityMaster\"", con))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var city = new CityMasterModel
                        {
                            intCityID = reader.GetInt32(0),
                            StrCityName = reader.GetString(1),
                            intStateID = reader.GetInt32(2),
                            intCountryID = reader.GetInt32(3),
                        };

                        cities.Add(city);
                    }
                }
            }
            return cities;
        }

        //public async Task<ActionResult> InsEmp(CityMasterModel obj)
        //{
        //    using (var con = new NpgsqlConnection(constr))
        //    {
        //        con.Open();
        //        string insQuery = "insert into \"tblDepartmentMaster\" values('" + obj.cityId + "','" + obj.cityName + "','" + obj.stateId + "','" + obj.countryId + "')";
        //        NpgsqlCommand npgsqlInsCommand = new NpgsqlCommand(insQuery, con);
        //        npgsqlInsCommand.ExecuteNonQuery();
        //    }
        //    return "SUCCESS";
        //}

        public async Task<string> InsCity(CityMasterModel obj)
        {
            using (var con = new NpgsqlConnection(constr))
            {
                con.Open();
                string insQuery = "INSERT INTO \"tblCityMaster\" (\"StrCityName\", \"intStateID\", \"intCountryID\") VALUES (@StrCityName, @intStateID, @intCountryID)";
                NpgsqlCommand npgsqlInsCommand = new NpgsqlCommand(insQuery, con);
                npgsqlInsCommand.Parameters.AddWithValue("@StrCityName", obj.StrCityName);
                npgsqlInsCommand.Parameters.AddWithValue("@intStateID", obj.intStateID);
                npgsqlInsCommand.Parameters.AddWithValue("@intCountryID", obj.intCountryID);
                npgsqlInsCommand.ExecuteNonQuery();
            }

            return "SUCCESS";
        }


        public async Task<string> updCity(CityMasterModel obj)
        {
            using (var con = new NpgsqlConnection(constr))
            {
                con.Open();
                string UpdQuery = "UPDATE \"tblCityMaster\" SET \"StrCityName\" = @StrCityName, \"intStateID\" = @intStateID, \"intCountryID\" = @intCountryID WHERE \"intCityID\" = @intCityID";

                NpgsqlCommand npgsqlUpdCommand = new NpgsqlCommand(UpdQuery, con);
                npgsqlUpdCommand.Parameters.AddWithValue("@intCityID", obj.intCityID);
                npgsqlUpdCommand.Parameters.AddWithValue("@StrCityName", obj.StrCityName);
                npgsqlUpdCommand.Parameters.AddWithValue("@intStateID", obj.intStateID);
                npgsqlUpdCommand.Parameters.AddWithValue("@intCountryID", obj.intCountryID);
                npgsqlUpdCommand.ExecuteNonQuery();
            }

            return "success";
        }

        public async Task<string> delCity(CityMasterModel obj)
        {
            using (var con = new NpgsqlConnection(constr))
            {
                con.Open();
                string delQuery = "delete from \"tblCityMaster\" where \"intCountryID\" = @intCountryID";
                NpgsqlCommand npgsqlCommand = new NpgsqlCommand(delQuery, con);
                npgsqlCommand.ExecuteNonQuery();
            }
            return "success";
        }

        public void insertyfromprocedure(string CityName, int StateID, int CountryID)
        {
            using (var conn = new NpgsqlConnection(constr))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("insertIntoCity", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("CityName", CityName);
                    cmd.Parameters.AddWithValue("StateID", StateID);
                    cmd.Parameters.AddWithValue("CountryID", CountryID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void deletefromprocedure(int CityID)
        {
            using (var conn = new NpgsqlConnection(constr))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("spdeletecirt", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("CityID", CityID);
                    cmd.ExecuteNonQuery();

                }
            }
        }
        public void updatefromprocedure(int id, string cityName, int stateID, int countryID)
        {
            using (var conn = new NpgsqlConnection(constr))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("spUpdate", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Add parameters including the city ID
                    cmd.Parameters.AddWithValue("intCityID", id);
                    cmd.Parameters.AddWithValue("CityName", cityName);
                    cmd.Parameters.AddWithValue("StateID", stateID);
                    cmd.Parameters.AddWithValue("CountryID", countryID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<SourceModel> GetCitiesInIndia()
        {
            List<SourceModel> citiesInIndia = new List<SourceModel>();

            using (var connection = new NpgsqlConnection(constr))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("SELECT * FROM \"Show\"();", connection))
                {
                    //command.CommandType = CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var source = new SourceModel
                            //citiesInIndia.Add(new SourceModel
                            {
                                id1 = reader.GetInt32(0),
                                state1 = reader.GetString(1),
                            };
                            citiesInIndia.Add(source);
                        }
                    }
                }
            }

            return citiesInIndia;
        }
        public List<CityCountryModel> countrynameind()
        {
            List<CityCountryModel> cityMasterlist = new List<CityCountryModel>();
            using (var connection = new NpgsqlConnection(constr))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("select \"1CountryNameInd\"('ref1'); fetch all in \"ref1\";", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.NextResult())
                        {
                            while (reader.Read())
                            {

                                var citycountryobj = new CityCountryModel
                                {

                                    city = reader.GetString(0),


                                    country = reader.GetString(1)
                                };

                                cityMasterlist.Add(citycountryobj);
                            }
                        }
                    }
                }
            }
            return cityMasterlist;
        }
        public IEnumerable<usersModel> GetUserData()
        {
            var users = new List<usersModel>();
            using (var con = new NpgsqlConnection(constr))
            {
                con.Open();

                using (var command = new NpgsqlCommand("SELECT * FROM users", con))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = new usersModel
                        {
                            id = reader.GetInt32(0),
                            username = reader.GetString(1),
                            email = reader.GetString(2),
                            password = reader.GetString(3),
                        };

                        users.Add(user);
                    }
                }
            }
            return users;
        }

        public async Task<string> PostUser(usersModel users)
        {
            using (var con = new NpgsqlConnection(constr))
            {
                await con.OpenAsync();
                var command = new NpgsqlCommand("insert into users(username,email,password,role) values(@username,@email,@password,'user')", con);

                command.Parameters.Add(new NpgsqlParameter("@username", users.username));
                command.Parameters.Add(new NpgsqlParameter("@email", users.email));
                command.Parameters.Add(new NpgsqlParameter("@password", users.password));
                await command.ExecuteNonQueryAsync();
            }
            return "SUCCESS";
        }


        //public async Task<string> PostUser(usersModel users)
        //{
        //    try
        //    {
        //        string hashedPassword = HashPassword(users.password);

        //        using (var con = new NpgsqlConnection(constr))
        //        {
        //            await con.OpenAsync(); 

        //            var command = new NpgsqlCommand("insert into users(username,email,password) values(@username,@email,@password)", con);

        //            command.Parameters.AddWithValue("@username", users.username);
        //            command.Parameters.AddWithValue("@email", users.email);
        //            command.Parameters.AddWithValue("@password", hashedPassword);

        //            await command.ExecuteNonQueryAsync();
        //        }

        //        return "SUCCESS"; 
        //    }
        //    catch (Exception e)
        //    {
        //        return e.Message; 
        //    }
        //}


        //public string HashPassword(string password)
        //{
        //    using (SHA256 sha256Hash = SHA256.Create())
        //    {
        //        // ComputeHash - returns byte array
        //        byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

        //        // Convert byte array to a string
        //        StringBuilder builder = new StringBuilder();
        //        for (int i = 0; i < bytes.Length; i++)
        //        {
        //            builder.Append(bytes[i].ToString("x2")); // "x2" means convert byte to 2-digit hexadecimal representation
        //        }
        //        return builder.ToString();
        //    }
        //}

        public string GenerateJwtToken(loginUser newUser)
        {
            // Replace "veryverysecretkey" with your actual secret key
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("veryverysecretkeyabcdefgh123kkkk"));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("email", newUser.email),
                new Claim("password", newUser.password)
            };

            var token = new JwtSecurityToken(
                issuer: "https://localhost:44376/",
                audience: "https://localhost:44376/",
                claims: claims,
                expires: DateTime.Now.AddHours(1), // Token expiration time
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);


        }

        //private string GenerateRefreshToken()
        //{
        //    // Generate a random refresh token (you can implement your own logic here)
        //    var refreshToken = Guid.NewGuid().ToString();
        //    return refreshToken;
        //}

        private Dictionary<string, string> refreshtokens = new Dictionary<string, string>();


        //public  string  GenerateTokens(String email,tempModel tm)
        // public string GenerateTokens(String email)

        //{
        //     // Replace "veryverysecretkey" with your actual secret key
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("veryverysecretkeyabcdefgh123kkkk"));

        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var claims = new[]
        //    {
        //        new Claim("email", email),
        //        //new Claim("role", tm.role)
        //    };

        //    var jwtToken = new JwtSecurityToken(
        //    issuer: "https://localhost:44376/",
        //    audience: "https://localhost:44376/",
        //    claims: claims,
        //    expires: DateTime.Now.AddMinutes(1), // Token expiration time
        //    signingCredentials: credentials
        //    );


        //    return (new JwtSecurityTokenHandler().WriteToken(jwtToken));
        //}

        public string GenerateRefreshToken(string username)
        {
            // Generate a random refresh token (you can implement your own logic here)
            var refreshToken = Guid.NewGuid().ToString();
            refreshtokens[username] = refreshToken;
            return refreshToken;
        }


        public string RefreshAccessToken(string refreshtoken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
     var key = Encoding.ASCII.GetBytes("abcdefghijklmnopqrstuvwxyzsuperSecretKey@345");

     // Validate the refresh token
     if (refreshtokens.ContainsValue(refreshtoken))
     {
         var username = refreshtokens.FirstOrDefault(x => x.Value == refreshtoken).Key;

         // Generate a new access token
         var tokenDescriptor = new SecurityTokenDescriptor
         {
             Subject = new ClaimsIdentity(new Claim[]
             {
         new Claim(ClaimTypes.Email, username),
             }),
             Expires = DateTime.UtcNow.AddMinutes(1),
             Issuer = "https://localhost:44376/",
             Audience = "https://localhost:44376/",
             SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
         };

         var token = tokenHandler.CreateToken(tokenDescriptor);
         return tokenHandler.WriteToken(token);
     }
     else
     {
         throw new Exception("Invalid refresh token");
     }
        }






        //public async bool CheckUserExists(string email, string password)
        //{
        //    //tempModel tm;
        //    using (var con = new NpgsqlConnection(constr))
        //    {
        //        //string hashedPassword = HashPassword(password);
        //        await con.OpenAsync();
        //        //var command = new NpgsqlCommand("SELECT email,password,role FROM users WHERE email = @email and password=@password", con);
        //        var command = new NpgsqlCommand("SELECT email,password FROM users WHERE email = @email and password=@password", con);
        //        command.Parameters.Add(new NpgsqlParameter("@email", email));
        //        command.Parameters.Add(new NpgsqlParameter("@password", password));
        //        var result=await command.ExecuteNonQueryAsync();
        //        //using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(command))
        //        //{
        //        //    DataSet ds=new DataSet();
        //        //    da.Fill(ds);
        //        //    foreach(DataRow row in ds.Tables[0].Rows)
        //        //    {
        //        //        tm = new tempModel
        //        //        {
        //        //            email = row["email"].ToString(),
        //        //        password = row["password"].ToString(),
        //        //        role = row["role"].ToString()
        //        //    };
        //        //        return tm;
        //        //    }
        //        //   }
        //        //return null;
        //        return Convert.ToInt32(result) > 0;

        //    }

        //}
        public async Task<bool> CheckUserExists(string email, string password)
        {
            using (var con = new NpgsqlConnection(constr))
            {
                await con.OpenAsync();

                // Execute SQL command to check if user exists
                var command = new NpgsqlCommand("SELECT COUNT(*) FROM users WHERE email = @email and password = @password", con);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@password", password);

                // ExecuteScalarAsync returns the result of the query (number of rows matching)
                var result = await command.ExecuteScalarAsync();

                // If any rows are returned, the user exists
                return Convert.ToInt32(result) > 0;
            }
        }





        public List<statecountry> getcountrybystateid(int intStateID)
        {
            var states = new List<statecountry>();
            using (var con = new NpgsqlConnection(constr))
            {
                con.Open();
                using (var cmd = new NpgsqlCommand("select \"tblCountryMaster\".*,\"tblStateMaster\".\"intStateID\" from \"tblCountryMaster\" \r\njoin \"tblStateMaster\"  \r\non \"tblCountryMaster\".\"intCountryID\" = \"tblStateMaster\".\"intCountryID\"\r\nwhere \"tblStateMaster\".\"intStateID\" = @intStateID;", con))

                {
                    cmd.Parameters.AddWithValue("@intStateID", intStateID);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            statecountry state = new statecountry();
                            state.intStateID = reader.GetInt32(reader.GetOrdinal("intStateID"));
                            state.StrCountryName = reader.GetString(reader.GetOrdinal("StrCountryName"));
                            state.intCountryID = reader.GetInt32(reader.GetOrdinal("intCountryID"));
                            states.Add(state);
                        }

                    }
                }
            }
            return states;
        }

        public List<stateModel> getstate()
        {
            var state= new List<stateModel>();
            using(var con = new NpgsqlConnection(constr))
            {
                con.Open();
                using (var cmd = new NpgsqlCommand("select * from \"tblStateMaster\"", con))
                {
                   using(var reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            stateModel stateobj = new stateModel();
                            stateobj.intStateID = reader.GetInt32(reader.GetOrdinal("intStateID"));
                            stateobj.StrStateName = reader.GetString(reader.GetOrdinal("StrStateName"));
                            stateobj.intCountryID = reader.GetInt32(reader.GetOrdinal("intCountryID"));
                            state.Add(stateobj);
                        }
                    }
                }
            }
            return state;
        }

        
    }
}
