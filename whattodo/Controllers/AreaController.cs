using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using whattodo.Content;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace whattodo.Controllers;

[Route("[controller]")]
public class AreaController : ControllerBase
{
    [HttpGet]
    public Task<PostnummerResult[]> GetArea(string query)
    {
        Address Address = new Address();
        var res = Address.SearchCity(query);
        return res;
    }

    [Route("search")]
    [HttpGet]
    public async Task<List<CustomSearchResult>> ICSearch(string query, int pageSize, int page)
    {
        Address Address = new Address();
        PostnummerResult[] res = await Address.SearchCity(query);
     
        return Address.ConvertToIC(res);
    }
}
