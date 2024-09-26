using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApplication2.ViewModel;

namespace WebApplication2.Controllers
{
    public class StaffController : ApiController
    {
        private static readonly Dictionary<int, Staff> staffData = new Dictionary<int, Staff>
{
    { 1234567, new Staff { staff_id = 1234567, staff_name_en = "Jacky Loyd", department_en = "IT", email = "aa@ss.com" } },
    { 1234568, new Staff { staff_id = 1234568, staff_name_en = "Suraj Bhan", department_en = "DEV", email = "suraj@ss.com" } }
};
        [HttpGet]
        [Route("api/staff/{id}")]
        public async Task<IHttpActionResult> GetStaffById(int id)
        {
            try
            {
                //use default credentials aka Windows Credentials
                HttpClientHandler handler = new HttpClientHandler()
                {
                    UseDefaultCredentials = true
                };
                HttpClient client = new HttpClient(handler);

                //Note: Replace this with actual API endpoint
                string url = $"http://pscc-stg-test:303/api/Search/{id}";

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return Ok(data);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        //Note: This is dummy api
        //[Route("api/GetStaffByIdAsync/{id}")]
        //public IHttpActionResult GetStaffById(int id)
        //{
        //    if (staffData.TryGetValue(id, out var staff))
        //    {
        //        return Ok(staff);
        //    }
        //    return NotFound();
        //}
    }
}
