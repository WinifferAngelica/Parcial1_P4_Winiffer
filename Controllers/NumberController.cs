using Microsoft.AspNetCore.Mvc;
using Parcial1_P4_Winiffer.Services;
using Parcial1_P4_Winiffer.Modelos;
using Microsoft.AspNetCore.Http;

namespace Parcial1_P4_Winiffer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NumberController(NumbersService numbersService) : ControllerBase
    {

        [HttpGet("{number:int}")]
        public IActionResult sumar(int number)
        {
            return Ok(number + number);
        }

        [HttpGet("historial")]
        public async Task<IActionResult> GetHistorial()
        {
            var historial = await numbersService.GetListAsync();
            return Ok(historial);
        }

        [HttpGet("historial/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var number = await numbersService.GetByIdAsync(id);
            if (number == null)
            {
                return NotFound();
            }
            return Ok(number);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] NumberRecord number)
        {
            var row = await numbersService.GetByIdAsync(id);

            if (row == null)
            {
                return NotFound();

            }
            else
                return Ok(new
                {
                    Numero_Anterior = row.Numero,
                    Resultado_Anterior = row.Resultado,
                    Numero_Actual = number.Numero,
                    Resultado_Actual = number.Resultado
                });
            }
        }




    
}