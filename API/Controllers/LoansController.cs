using Application.Dtos;
using Application.Features.Loans;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class LoansController : BaseApiController
{
    private readonly IMediator _mediator;

    public LoansController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<Result<LoanDto>> SaveLoan(CreateLoan.CreateLoanCommand command)
    {
        return await _mediator.Send(command);
    }
    
    [HttpGet]
    public async Task<Result<List<LoanDto>>> ListLoan()
    {
        return await _mediator.Send(new ListLoan.ListLoanQuery());
    }
    
    [HttpGet("{id}")]
    public async Task<Result<LoanDto>> GetLoanById(int id)
    {
        return await _mediator.Send(new GetLoanById.GetLoanByIdQuery{Id = id});
    }
    
    [HttpPut("{id}")]
    public async Task<Result<LoanDto>> UpdateLoan(int id, UpdateLoan.UpdateLoanCommand command)
    {
        command.Id=id;
        return await _mediator.Send(command);
    }
    
    [HttpDelete("{id}")]
    public async Task<Result<LoanDto>> DeleteLoan(int id)
    {
        return await _mediator.Send(new DeleteLoan.DeleteLoanCommand{Id = id});
    }

}