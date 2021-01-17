using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    // atributul pentru controllerul API
    // ne scutește de verificarea manuală 
    //pentru a vedea dacă există erori de validare
    
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        
    }
}