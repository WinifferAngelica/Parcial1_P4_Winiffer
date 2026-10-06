using Microsoft.AspNetCore.Mvc;
using Parcial1_P4_Winiffer.Services;
using Parcial1_P4_Winiffer.Models;

namespace Parcial1_P4_Winiffer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NumberController(NumbersService numbersService) : ControllerBase
{

    [HttpGet("{number:int}")]
    public IActionResult Sumar(int number)
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

    [HttpPost]
    public async Task<IActionResult> Create(NumberRecord number)
    {
        double resultado = number.Numero + number.Numero;

        var record = new NumberRecord(0, DateTime.UtcNow, number.Numero, resultado);
        int id = await numbersService.SaveAsync(record);

        return Ok(new NumberRecord(id, record.Fecha, record.Numero, record.Resultado));
    }



    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, NumberRecord number)
    {
        var anterior = await numbersService.GetByIdAsync(id);

        if (anterior is null)
            return NotFound();

        double resultado = number.Numero + number.Numero;
        var actual = new NumberRecord(id, DateTime.UtcNow, number.Numero, resultado);

        bool guardado = await numbersService.UpdateAsync(actual);

        if (!guardado)
            return NotFound();

        return Ok(actual);
    }


}