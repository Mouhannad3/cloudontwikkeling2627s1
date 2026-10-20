using System;
using Microsoft.AspNetCore.Mvc;
using Shipit.Shared;
using ShipIt.PriceQuote.Api.Contracts;
using ShipIt.PriceQuotes.Domain;
using static ShipIt.PriceQuote.Domain.ShipItExceptions;

namespace ShipIt.QuotePrice.Api.Controllers;

[ApiController]
[Route("pricequotes")]
public class PriceQuoteController(IPriceQuoteService quoteService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<PriceQuoteResponseContract>> CalculatePrice(
        [FromBody] PriceQuoteRequestContract requestContract)
    {
        try
        {
            var createdQuote = await quoteService.CreatePrice(requestContract);

            return CreatedAtAction(
                nameof(GetPrice),
                new { id = createdQuote.Id },
                createdQuote
            );
        }
        catch (FromCountryWeightException e)
        {
            return BadRequest(
                new ProblemDetails
                {
                    Title = "OLALALA",
                    Detail = e.Message
                }
            );
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PriceQuoteResponseContract>> GetPrice(
        [FromRoute] string id)
    {
        return Ok(await quoteService.GetById(id));
    }

    [HttpGet]
    public string Hello()
    {
        return "Hello World!";
    }
}