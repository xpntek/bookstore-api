


using Application.Dtos;
using Application.Errors;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Reserves;

    public class CreateReserve
    {

        public class CreateReserveCommand:IRequest<Result<ReserveDto>>
        {

        public DateTimeOffset BookingDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public int BookId { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; }

    }

    public class CreateReserveHandler : IRequestHandler<CreateReserveCommand, Result<ReserveDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateReserveHandler(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        public async Task<Result<ReserveDto>> Handle(CreateReserveCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user is null)
            {
                Results.NotFoundError("User doesnt exist");
            }

            var book = _unitOfWork.Repository<Book>().GetByIdAsync(request.BookId);
            if (book is null)
            {
                Results.NotFoundError("Book doesnt exist");
            }

            var reserve = new Reserve()
            {
                BookingDate = request.BookingDate,
                EndDate = request.EndDate,
                BookId = request.BookId,
                UserId = request.UserId,
                Status = request.Status

            };
            _unitOfWork.Repository<Reserve>().Add(reserve);
            var result = await _unitOfWork.Complete() < 0;
            if (result)
            {
                return Results.InternalError("Error While saving");
            }

            return _mapper.Map<ReserveDto>(reserve);


        }
    }
}

