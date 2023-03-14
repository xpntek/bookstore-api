using Application.Dtos;
using Application.Errors;
using Application.Features.Favorites;
using Application.Features.Loans.Specification;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Features.Loans;

public class CreateLoan
{
    public class CreateLoanCommand : IRequest<Result<LoanDto>>
    {
        public DateTimeOffset LoanDate { get; set; }
        public DateTimeOffset ReturnDate { get; set; }
        public DateTimeOffset EffectiveReturnDate { get; set; }
        public string Status { get; set; }
        public float Forfeit { get; set; }
        public int BookId { get; set; }
    }
    
    public class CreateLoanCommandHandler : IRequestHandler<CreateLoanCommand,Result<LoanDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;

        public CreateLoanCommandHandler(IUnitOfWork unitOfWork, IMapper mapper,IUserAccessor userAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }
        public async Task<Result<LoanDto>> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
        {
            var loanSpec = new FoundLoanByUserIdSpecification(_userAccessor.GetCurrentUserID());
            var loan = await _unitOfWork.Repository<Loan>().GetEntityWithSpec(loanSpec);
            if (loan is null)
            {
                return Results.InternalError(" The user must make one loan ");
            }
            
            var clientSpec = new FoundClientByIdSpecification(_userAccessor.GetCurrentUserID());
            var client = await _unitOfWork.Repository<Client>().GetEntityWithSpec(clientSpec);
            if (client is  null)
            {
                return Results.NotFoundError(" User with Id:" + _userAccessor.GetCurrentUserID());
            }

            
            var book = await _unitOfWork.Repository<Book>().GetByIdAsync(request.BookId);
            if (book is null)
            {
                return Results.ConflictError(" Id:" + request.BookId);
            }
            
            var newLoan = new Loan()
            {
                BookId = request.BookId,
                UserId =_userAccessor.GetCurrentUserID(),
                Forfeit = request.Forfeit,
                Status = request.Status,
                LoanDate = request.LoanDate,
                ReturnDate = request.ReturnDate,
                EffectiveReturnDate = request.EffectiveReturnDate

            };
            
            _unitOfWork.Repository<Loan>().Add(newLoan);
            var result = await _unitOfWork.Complete();
            if (result < 0)
            {
                return Results.InternalError( " fail to save loan");
            }
            return _mapper.Map<LoanDto>(newLoan);
        }
    }
}