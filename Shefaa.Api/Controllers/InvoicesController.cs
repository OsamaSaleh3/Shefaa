using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shefaa.Application.Invoices.Commands.CreateInvoice;
using Shefaa.Application.Invoices.Commands.AddInvoiceItem;
using Shefaa.Application.Invoices.Commands.RemoveInvoiceItem;
using Shefaa.Application.Invoices.Commands.RecordPayment;
using Shefaa.Application.Invoices.Commands.CancelInvoice;
using Shefaa.Application.Invoices.Queries.GetInvoiceById;
using Shefaa.Application.Invoices.Queries.GetPatientInvoices;
using Shefaa.Application.Invoices.Queries.GetUnpaidInvoices;
using Shefaa.Contracts.Invoices;

namespace Shefaa.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoicesController : ControllerBase
{
    private readonly ISender _sender;

    public InvoicesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceRequest request)
    {
        var command = new CreateInvoiceCommand(request.PatientId);
        var result = await _sender.Send(command);

        return result.Match(
            invoiceId => CreatedAtAction(nameof(GetInvoiceById), new { id = invoiceId }, new { id = invoiceId }),
            errors => Problem(statusCode: GetStatusCode(errors), detail: errors.First().Description)
        );
    }

    [HttpPut("{id}/items")]
    public async Task<IActionResult> AddInvoiceItem(Guid id, [FromBody] AddInvoiceItemRequest request)
    {
        var command = new AddInvoiceItemCommand(
            id,
            request.Description,
            request.Quantity,
            request.UnitPrice
        );

        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            _ => NoContent(),
            errors => Problem(statusCode: GetStatusCode(errors), detail: errors.First().Description)
        );
    }

    [HttpDelete("{id}/items/{itemId}")]
    public async Task<IActionResult> RemoveInvoiceItem(Guid id, Guid itemId)
    {
        var command = new RemoveInvoiceItemCommand(id, itemId);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            _ => NoContent(),
            errors => Problem(statusCode: GetStatusCode(errors), detail: errors.First().Description)
        );
    }

    [HttpPut("{id}/payment")]
    public async Task<IActionResult> RecordPayment(Guid id, [FromBody] RecordPaymentRequest request)
    {
        var command = new RecordPaymentCommand(
            id,
            request.Amount,
            request.PaymentMethod
        );

        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            _ => NoContent(),
            errors => Problem(statusCode: GetStatusCode(errors), detail: errors.First().Description)
        );
    }

    [HttpPut("{id}/cancel")]
    public async Task<IActionResult> CancelInvoice(Guid id)
    {
        var command = new CancelInvoiceCommand(id);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            _ => NoContent(),
            errors => Problem(statusCode: GetStatusCode(errors), detail: errors.First().Description)
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetInvoiceById(Guid id)
    {
        var query = new GetInvoiceByIdQuery(id);
        var result = await _sender.Send(query);

        return result.Match<IActionResult>(
            invoiceDto => invoiceDto is null
                ? NotFound()
                : Ok(new InvoiceResponse(
                    invoiceDto.Id,
                    invoiceDto.InvoiceNumber,
                    invoiceDto.PatientName,
                    invoiceDto.Date,
                    invoiceDto.TotalAmount,
                    invoiceDto.PaidAmount,
                    invoiceDto.RemainingAmount,
                    invoiceDto.Status,
                    invoiceDto.Notes,
                    invoiceDto.Items.Select(item => new InvoiceItemResponse(
                        item.Id,
                        item.Description,
                        item.Quantity,
                        item.UnitPrice,
                        item.TotalPrice
                    )).ToList()
                )),
            errors => Problem(statusCode: GetStatusCode(errors), detail: errors.First().Description)
        );
    }

    [HttpGet("patient/{patientId}")]
    public async Task<IActionResult> GetPatientInvoices(Guid patientId)
    {
        var query = new GetPatientInvoicesQuery(patientId);
        var result = await _sender.Send(query);

        return result.Match<IActionResult>(
            invoices => Ok(invoices.Select(inv => new InvoiceResponse(
                inv.Id,
                inv.InvoiceNumber,
                inv.PatientName,
                inv.Date,
                inv.TotalAmount,
                inv.PaidAmount,
                inv.RemainingAmount,
                inv.Status,
                inv.Notes,
                inv.Items.Select(item => new InvoiceItemResponse(
                    item.Id,
                    item.Description,
                    item.Quantity,
                    item.UnitPrice,
                    item.TotalPrice
                )).ToList()
            )).ToList()),
            errors => Problem(statusCode: GetStatusCode(errors), detail: errors.First().Description)
        );
    }

    [HttpGet("unpaid")]
    public async Task<IActionResult> GetUnpaidInvoices()
    {
        var query = new GetUnpaidInvoicesQuery();
        var result = await _sender.Send(query);

        return result.Match<IActionResult>(
            invoices => Ok(invoices.Select(inv => new InvoiceResponse(
                inv.Id,
                inv.InvoiceNumber,
                inv.PatientName,
                inv.Date,
                inv.TotalAmount,
                inv.PaidAmount,
                inv.RemainingAmount,
                inv.Status,
                inv.Notes,
                inv.Items.Select(item => new InvoiceItemResponse(
                    item.Id,
                    item.Description,
                    item.Quantity,
                    item.UnitPrice,
                    item.TotalPrice
                )).ToList()
            )).ToList()),
            errors => Problem(statusCode: GetStatusCode(errors), detail: errors.First().Description)
        );
    }

    private static int GetStatusCode(List<Error> errors)
    {
        return errors.First().Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}
